 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TabletTransfer.AI.Tools;
using TabletTransfer.UI;
using HidBarcodeHandler;
using static HidBarcodeHandler.BarcodeHandler;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using CommonDatabase.Data;
using CommonUI.Tools;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using System.Net.NetworkInformation;

namespace TabletTransfer.AI
{
    public sealed partial class StateSingleton
    {
        #region Singleton
        private static StateSingleton instance;
        private static object instance_lock = new object();
        static StateSingleton()
        {
        }
        private StateSingleton()
        { }
        public static StateSingleton Instance
        {
            get
            {
                lock (instance_lock)
                {
                    if (instance == null)
                    {
                        instance = new StateSingleton();
                    }
                }
                return instance;
            }
        }
        #endregion Singleton
        internal string[] GetAllLocations() => DbAccess.GetAllLocations().Select(x => x.Name).ToArray();

        //Status indicator properities
        internal string StatusInfo;
        internal string ProgresBarValue;

        BarcodeHandler barcode;

        //creation of singleton cannot depend on ceration of windows, that depend on singleton
        Docked d = null;
        PassDeviceScreen _pd = null;
        AddTabletScreen _at = null;
        AnnotateScreen _an = null;
        StartupScreen _ss = null;
        LockedScreen _ls = null;

        PassDeviceScreen pd { get { if (_pd == null) _pd = new PassDeviceScreen(); return _pd; } }
        AddTabletScreen at { get { if (_at == null) _at = new AddTabletScreen(); return _at; } }

        AnnotateScreen an { get { if (_an == null) _an = new AnnotateScreen(); return _an; } }
        StartupScreen ss { get { if (_ss == null) _ss = new StartupScreen(); return _ss; } }
        LockedScreen ls { get { if (_ls == null) _ls = new LockedScreen(); return _ls; } }

        Operator _currentUser;
        internal Operator CurrentUser { get { return _currentUser; } set { _currentUser = value; CurrentUserChanged?.Invoke(CurrentUser); } }
        internal Operator NextUser;
        internal Operator SL;

        internal Tablet Myself;
        internal void AuthenticateAsCurrent(string capacno)
        {
            CurrentUser = DbAccess.GetOperator(capacno, Myself?.Location);
        }
        internal bool AuthenticateAsSL(string capacno)
        {
            var op = DbAccess.GetOperator(capacno, Myself?.Location);
            if (op.IsAllowed(Operator.PositionEnum.SL))
            {
                SL = op;
                return true;
            }
            return false;
        }
        internal void AuthenticateAsNext(string capacno)
        {
            NextUser = DbAccess.GetOperator(capacno, Myself?.Location);
        }
        internal Operator Authenticate(string capacno)
        { 
            return DbAccess.GetOperator(capacno, Myself?.Location);
        }
        internal Action<Operator> CurrentUserChanged;// = new Action<Operator>();

        internal enum StateEnum
        { 
            None,
            Adding,
            VerifyLastShutdown,
            InUse,
            InStorage,
            Locked
        }

        internal void CancelPass()
        {

            if (CurrentState == StateEnum.InStorage)
            {
                StateSingleton.Instance.CurrentUser = null;
                StateSingleton.Instance.NextUser = null;
                StateSingleton.Instance.SL = null;
                DoLockdown();
            }
            else
            {
                StateSingleton.Instance.NextUser = null;
                StateSingleton.Instance.SL = null;
                d.Show();
            }
        }

        

        internal StateEnum CurrentState = StateEnum.None;
        private CancellationTokenSource deadManSwitch = new CancellationTokenSource();

        internal void DoStartup(Docked dock_Window, bool sim_barcode)
        {
            DbAccess.Setup(Properties.Settings.Default.ProgAuthConnectionString);

            //Task.Run(() => {
            //    try
            //    {
            //        while(true)
            //        {
            //            if (Myself?.Id != null && NetworkInterface.GetIsNetworkAvailable())
            //            {
            //                DbAccess.DeadManSwitch(Myself.Id);
            //                //DbAccess.AddAlert(int.Parse(Myself.Id.ToString()), "ALERT_2", "TEST ALERT");
            //                //OracleConnection.ClearAllPools();
            //            }

            //            Thread.Sleep(60000);
            //        }
            //    }
            //    catch (Exception ex)
            //    { 
                
            //    }
            //}, deadManSwitch.Token);
            if (CurrentState == StateEnum.None)
            {
                if (sim_barcode)
                {
                    barcode = new SimulatedBarcodeHandler();
                }
                else
                {
                    barcode = new HidBarcodeHandler.HidBarcodeHandler();
                }
                barcode.Init(dock_Window.Handle);
                barcode.LetterTimeout = 100;
                barcode.AllowedDevices.Add(@"\\?\HID#VID_0C2E");
                barcode.MessageReceived += Barcode_MessageReceived;

                if (!DomainCheck.IsInDomain())
                {
                    DoLockdown(LockType.SYSTEM_LOCK, System.Security.Principal.WindowsIdentity.GetCurrent().Name, "Tablet nie dodany do domeny.", DateTime.Now);
                    return;
                }
                Myself = DbAccess.GetTablet(Properties.Settings.Default.TabletId, Dns.GetHostName());
                
                d = dock_Window;

                if (Myself == null)
                {
                    while (StartAdd() != DialogResult.OK)
                    {
                        MessageBox.Show("Aby korzystać z tabletu musi on być dodany do systemu. Może to zrobić tylko SL.");
                    }
                    StateSingleton.Instance.FinishVerifyLastShutdown("NEW_TABLET");
                }
                else
                {
                    /*if (Myself.Locked)
                    {
                        CurrentState = StateEnum.Locked;
                        DisplayLockdownScreen(Myself.LockType, Myself.LockedBy.ACPno, Myself.LockedReason);
                    }
                    else*/
                    {
                        StartVerifyLastShutdown();
                    }
                }
            }
        }

        private void Barcode_MessageReceived(object source, MessageReceivedEventArgs e)
        {
            barcodeHandlerMessageReceived.OnNext(e);
        }

        internal bool FinishAdd(string location)
        {
            if (CurrentState == StateEnum.Adding)
            {
                if (location != "" && SL != null)
                {
                    var tab = DbAccess.CreateTablet(Dns.GetHostName(), SL.ACPno, location);
                    if (tab != null)
                    {
                        Myself = tab;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        internal DialogResult StartAdd()
        {
            var ret = DialogResult.Cancel;

            CurrentState = StateEnum.Adding;
            DesktopAccess.LockExplorer();
            ret = at.ShowDialog();
            DesktopAccess.UnlockExplorer();

            return ret;
        }
        internal bool DoScan(string acpNo)
        {
            if (CurrentState == StateEnum.Adding || CurrentState == StateEnum.VerifyLastShutdown)
            {
                var tmp = DbAccess.GetOperator(acpNo, Myself?.Location);
                if (tmp != null && tmp.IsAllowed(Operator.PositionEnum.SL))
                {
                    StateSingleton.Instance.SL = tmp;
                    StateSingleton.Instance.StatusInfo = "Kod OK";
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (CurrentState == StateEnum.InStorage)
            {
                var tmp = DbAccess.GetOperator(acpNo, Myself?.Location);
                if (tmp != null && tmp.IsAllowed(Operator.PositionEnum.SL))
                {
                    StateSingleton.Instance.SL = tmp;
                    return true;
                }
                else
                {
                    MessageBox.Show("Tylko SL może odblokować tablet.");
                    return false;
                }
            }
            if (CurrentState == StateEnum.Locked)
            {
                var tmp = DbAccess.GetOperator(acpNo, Myself?.Location);
                if (tmp != null && tmp.IsAllowed(Operator.PositionEnum.Service))
                {
                    StateSingleton.Instance.SL = tmp;
                    return true;
                }
                else
                {
                    MessageBox.Show("Tylko SL może odblokować tablet.");
                    return false;
                }
            }
            return false;
        }
        internal void StartVerifyLastShutdown()
        {
            CurrentState = StateEnum.VerifyLastShutdown;

            DesktopAccess.LockExplorer();
            ss.ShowDialog();
            DesktopAccess.UnlockExplorer();
        }
        internal bool FinishVerifyLastShutdown(string reason)
        {
            if (DbAccess.PutInStorage(Myself, SL, reason))
            {
                CurrentState = StateEnum.InStorage;
                Myself = DbAccess.GetTablet(Myself.Id, Myself.WindowsName);

                DisplayLockdownScreen(Myself.LockType, Myself.LockedBy.ACPno, Myself.LockedReason);

                return true;
            }
            else
            {
                return false;
            }
        }
        internal void PutInStorage()//!
        {
            if (CurrentUser.IsAllowed(Operator.PositionEnum.SL))
            {
                StateSingleton.Instance.DoLockdown(CurrentUser?.ACPno);
                DbAccess.PutInStorage(Myself, SL, "Przekazanie na magazyn");
                StateSingleton.Instance.CurrentState = StateSingleton.StateEnum.InStorage;
                StateSingleton.Instance.CurrentUser = null;
                StateSingleton.Instance.NextUser = null;
                StateSingleton.Instance.SL = null;
                d.Hide();
                
            }
        }

        private void DisplayLockdownScreen(LockType lockType, string who, string why)
        {
            DesktopAccess.LockExplorer();
            ls.Setup(lockType, who, why);
            ls.Show();
            DesktopAccess.UnlockExplorer();
        }
        internal void DoLockdown(string who = null)
        {
            if (Myself.Locked)
            {
                if (who == null)
                {
                    DisplayLockdownScreen(Myself.LockType, Myself.LockedBy.ACPno, Myself.LockedReason);
                }
                else
                {
                    DisplayLockdownScreen(Myself.LockType, CurrentUser.ACPno, Myself.LockedReason);
                }
            }
        }
        internal void DoLockdown(LockType lockType, string who, string why, DateTime when)
        {
            if (!DomainCheck.IsInDomain() || CommonDatabase.CommonDbAccess.Lock(Myself, lockType, who, why, when))
            {
                CurrentState = StateEnum.Locked;
                if (DomainCheck.IsInDomain())
                {
                    Myself = DbAccess.GetTablet(Myself.Id, Myself.WindowsName);
                }
                else
                {
                    Myself = null;
                }
                DisplayLockdownScreen(lockType, who, why);
            }
        }

        internal void DoPass(bool slPassingFromStorage = false)
        {
            pd.UseSl();
            DesktopAccess.LockExplorer();
            pd.ShowDialog();
            DesktopAccess.UnlockExplorer();
        }
        internal void FinishPass(bool tabletOk)
        {
            if (tabletOk)
            {
                if (CommonDatabase.CommonDbAccess.PassTheDevice(Myself, NextUser, false, null, "IN USE"))
                {
                    CurrentUser = NextUser;
                    NextUser = null;

                    d.Show();
                }
            }
            else
            {
                an.UseNext();
                an.Show();
            }
        }
        internal void FinishAnnotate(bool shouldLock, string operatorReason, string slComment)
        {
            DbAccess.RegisterEvent(Myself, NextUser.ACPno, LockType.DAMAGE_LOCK.ToString(), $"OP: {operatorReason}");
            DbAccess.RegisterEvent(Myself, SL.ACPno, LockType.DAMAGE_LOCK.ToString(), $"SL: {slComment}");
            if (!shouldLock)
            {
                if (DbAccess.PassTheDevice(Myself, NextUser, false, null, "IN USE"))
                {
                    CurrentUser = NextUser;
                    NextUser = null;
                    SL = null;
                    d.Show();
                }
            }
            else
            {
                DbAccess.Lock(Myself, LockType.DAMAGE_LOCK, SL.ACPno, slComment, DateTime.Now);
                DoLockdown(LockType.DAMAGE_LOCK, SL.ACPno, slComment, DateTime.Now);
            }
        }
        internal void DoShutdown()
        {
            deadManSwitch.Cancel();
            barcode?.Close();
        }

        internal void AddAlert(string alertType, string alertText)
        {
            DbAccess.AddAlert(int.Parse(Myself.Id.ToString()), alertType, alertText);
        }
        Subject<MessageReceivedEventArgs> barcodeHandlerMessageReceived = new Subject<MessageReceivedEventArgs>();
        public IObservable<MessageReceivedEventArgs> BarcodeHandlerMessageReceived => barcodeHandlerMessageReceived.AsObservable();

        public bool BarcodeIsSim { get { return barcode.IsSim; } }
        /*public MessageReceivedEvent barcodeHandlerMessageReceived;
public MessageReceivedEvent BarcodeHandlerMessageReceived
{
get => barcodeHandlerMessageReceived;
set
{
barcodeHandlerMessageReceived = value;
if (listenToBarcodes)
{
barcode.MessageReceived -= BarcodeHandlerMessageReceived;
barcode.MessageReceived += BarcodeHandlerMessageReceived;
}
else
{
barcode.MessageReceived -= BarcodeHandlerMessageReceived;
}
}
}*/
        /*private bool listenToBarcodes = false;
        internal void ListenToBarcodes(bool yes)
        {
            if (barcode is SimulatedBarcodeHandler)
            {
                yes = true;
            }
            listenToBarcodes = yes;
            if (barcode != null && BarcodeHandlerMessageReceived != null)
            {
                if (yes)
                {
                    barcode.MessageReceived += BarcodeHandlerMessageReceived;
                }
                else
                {
                    barcode.MessageReceived -= BarcodeHandlerMessageReceived;
                }
            }
            else
            { 
            
            }
        }*/
    }
}
