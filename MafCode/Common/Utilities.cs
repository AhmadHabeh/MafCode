using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace MafCode.Common
{
    public class Utilities
    {
        public static void WriteLogs(string log)
        {
            try
            {
                var path = "LogFileLocation".CM();
                File.AppendAllText(path, $"{DateTime.Now} - {log}");
            }
            catch (Exception e)
            {

            }
        }
        public static string GenerateQrCode(string url)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    var qrGenerator = new QRCodeGenerator();
                    var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                    var qrCode = new QRCode(qrCodeData);
                    using (var bitMap = qrCode.GetGraphic(20))
                    {
                        bitMap.Save(ms, ImageFormat.Png);
                        return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
