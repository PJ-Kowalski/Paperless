using CommonDatabase.Libs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CommonDatabase.Tools
{
    public class PhotoDownloader
    {
        static TimeSpan TTL = TimeSpan.FromHours(1);
        class CachedImage
        {
            public Image Img { get; set; } = null;
            public DateTime DownloadTime { get; set; }
        }
  

        static Dictionary<string, CachedImage> photo_cache = new Dictionary<string, CachedImage>();
        static string[] paths = new string[] {
            @"\\fscommon\common\photos\ngk\",
            @"\\fscommon\common\photos\ngk\acj\"
        };
        public static Image GetPhoto(string name)
        {
            CachedImage image = new CachedImage();
            var tmpLocal = Path.Combine("c:\\Photos\\", name + ".jpg");
            //if (!photo_cache.ContainsKey(name))
            //{
                if (File.Exists(tmpLocal))
                {
                    image.Img = Image.FromFile(tmpLocal);
                    image.DownloadTime = DateTime.Now;
                    //photo_cache.Add(name, new CachedImage() {Img=image.Img, DownloadTime = DateTime.Now });
                }
                else
                {
                    try
                    {
                        using (new Impersonator(@"svc_smt_config", "ngk.local", "q&O*wKMy*$TSxv0!1+ri:7HAlD%l6wwR"))
                        {
                            foreach (var path in paths)
                            {
                                var tmp = Path.Combine(path, name + ".jpg");
                                if (File.Exists(tmp))
                                {
                                    image.Img = Image.FromFile(tmp);
                                    image.DownloadTime = DateTime.Now;
                                    //photo_cache.Add(name, new CachedImage() { DownloadTime = DateTime.Now });
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }

                if (image.Img == null)
                {
                    image.Img = Image.FromFile("Resources\\defuser.png");
                    image.DownloadTime = DateTime.Now;
                }
                return image.Img; 
            //}
            //else
            //{
            //    return photo_cache[name].Img;
            //}
        }


        public static Image GetPhotoCashe(string name)
        {
            CachedImage image = new CachedImage();
            if (!photo_cache.ContainsKey(name) || DateTime.Now.Subtract(photo_cache[name].DownloadTime) > TTL)
            {
        
                using (new Impersonator(@"svc_smt_config", "ngk.local", "q&O*wKMy*$TSxv0!1+ri:7HAlD%l6wwR"))
                {
                        if (!photo_cache.ContainsKey(name))
                        {
                            photo_cache.Add(name, new CachedImage() { DownloadTime = DateTime.Now });
                        }
                        foreach (var path in paths)
                    {
                        var tmp = Path.Combine(path, name + ".jpg");
                        if (File.Exists(tmp))
                        {
                            photo_cache[name].Img = Image.FromFile(tmp);
                            photo_cache[name].DownloadTime = DateTime.Now;
                            break;
                        }
                    }
                }
            }

            if (photo_cache.Count > 100)
            {
                photo_cache.OrderByDescending(x => x.Value.DownloadTime).Take(50).ToList().ForEach(y => photo_cache.Remove(y.Key));
            }
            return photo_cache[name].Img;

        }

        public static void GetPhotosForLocation(List<string> names)
        {
            List<string> photosToDownload=new List<string>();

            if (!Directory.Exists("c:\\Photos"))
            {
                Directory.CreateDirectory("c:\\Photos");
            }

            foreach (var name in names)
            {
                if (!File.Exists($"c:\\Photos\\{name}.jpg"))
                {
                    photosToDownload.Add(name);
                }
            }
            try
            {
                using (new Impersonator(@"svc_smt_config", "ngk.local", "q&O*wKMy*$TSxv0!1+ri:7HAlD%l6wwR"))
                {
                    var options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount * 10 };
                    Parallel.ForEach(photosToDownload, options, (name) =>
                    {
                        foreach (var path in paths)
                        {
                            var tmp = Path.Combine(path, name + ".jpg");
                            if (File.Exists(tmp))
                            {
                                Image myImage = Image.FromFile(tmp);
                                EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 20L);
                                EncoderParameters encoderParams = new EncoderParameters(1);
                                encoderParams.Param[0] = qualityParam;
                                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                                myImage.Save($"c:\\Photos\\{name}.jpg", jpgEncoder, encoderParams);
                                //File.Copy(tmp, $"c:\\Photos\\{name}.jpg", true);
                                break;
                            }

                        }
                    });
                }
            }
            catch (Exception e)
            {
            } 
            
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
