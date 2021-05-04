using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace eLibraryPortal.Data.Utils
{
    public class ImageHelper
    {

        public static string ConvertImage(Byte[] ImageBytes)
        {
            var base64String = string.Empty;
            try
            {
                MemoryStream ms = new MemoryStream(ImageBytes);
                Image returnImage = Image.FromStream(ms);
                using (MemoryStream m = new MemoryStream())
                {
                    returnImage.Save(m, ImageFormat.Png);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                    base64String = string.Format("data:image/png;base64,{0}", base64String);
                }
                return base64String;


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
