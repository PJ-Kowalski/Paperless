using CommonDatabase.Data;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonDatabase.Tools
{
    public class BarcodeOperations
    {
        public static DecodedOperatorBarcode BadgeDecoder(string barcode)
        {
            //example: ["FACP173001\r"]
            //example: [@B04@01ACP173001 @02ACP173001           ]
            foreach (var pattern in new string[] { "@B04.*@01(?<capacno>[^@]*)", "F(?<capacno>.*)\r", "F(?<capacno>.*)" })
            {

                var match = Regex.Match(barcode, pattern);
                if (match.Success)
                {
                    return new DecodedOperatorBarcode() { ACPNo = match.Groups["capacno"].Value.Trim(), IsTemporary = false };
                }
            }
            //example: [@B69 @01 ACP173001 @0220210128 1345]
            foreach (var pattern in new string[] { "@B69.*@01(?<capacno>[^@]*).*@02(?<timestamp>[^\r]*)" })
            {
                var match = Regex.Match(barcode, pattern);
                if (match.Success)
                {
                    var acpno = match.Groups["capacno"].Value.Trim();
                    var timestamp = match.Groups["timestamp"].Value.Trim();
                    var creationDate = DateTime.ParseExact(timestamp, "yyyyMMdd HHmm", CultureInfo.InvariantCulture);
                    var expirationDate = CommonDatabase.CommonDbAccess.CheckTemporaryBadge(acpno, creationDate);
                    if (expirationDate.HasValue && expirationDate > DateTime.Now)
                    {
                        return new DecodedOperatorBarcode() { ACPNo = acpno, IsTemporary = true, CreationDate = creationDate, ExpirationDate = expirationDate.Value };
                    }
                }
            }
            return new DecodedOperatorBarcode() { ACPNo = "ACP000000", IsTemporary = false };//throw new Exception($"Wrong badge. Received: {barcode}");
        }
        public static string GenerateOperatorBarcodeBase64(Operator op, DateTime timestamp)
        {
            string text = $"@B69@01{op.ACPno}@02{timestamp.ToString("yyyyMMdd HHmm")}";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(2);
            using (MemoryStream ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, ImageFormat.Png);
                ms.Flush();

                var tmp = Convert.ToBase64String(ms.ToArray());

                return tmp;
            }
        }
        public static (bool needToSave, string barcode, string image) GenerateTabletBarcodeBase64(Tablet tab)
        {
            bool needToSave = false;
            string barcode;
            string image;
            if (tab.Barcode == null || tab.Barcode == "")
            {
                needToSave = true;
                barcode = RandomString(6);
            }
            else
            {
                barcode = tab.Barcode;
            }
            string text = $"@B69@01{barcode}";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(2);
            using (MemoryStream ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, ImageFormat.Png);
                ms.Flush();

                image = Convert.ToBase64String(ms.ToArray());

                return (needToSave, barcode, image);
            }
        }
        private static string RandomString(int length = 6)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
