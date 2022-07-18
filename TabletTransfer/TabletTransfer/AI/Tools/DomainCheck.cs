using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletTransfer.AI.Tools
{
    class DomainCheck
    {
        public static bool IsInDomain()
        {
            Interop.NetJoinStatus status = Interop.NetJoinStatus.NetSetupUnknownStatus;
            IntPtr pDomain = IntPtr.Zero;
            int result = Interop.NetGetJoinInformation(null, out pDomain, out status);
            if (pDomain != IntPtr.Zero)
            {
                Interop.NetApiBufferFree(pDomain);
            }
            if (result == Interop.ErrorSuccess)
            {
                return status == Interop.NetJoinStatus.NetSetupDomainName;
            }
            else
            {
                return false;
            }
        }
    }
}
