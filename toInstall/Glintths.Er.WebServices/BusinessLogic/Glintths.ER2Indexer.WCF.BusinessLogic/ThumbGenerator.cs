using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Cpchs.ER2Indexer.WCF.BusinessLogic
{
    public class ThumbGenerator
    {
        public static byte[] GetThumb(byte[] imgBytes)
        {
            MemoryStream imgStream = new MemoryStream(imgBytes);

            System.Drawing.Image image = System.Drawing.Image.FromStream(imgStream);
            System.Drawing.Image thumbnailImage = image.GetThumbnailImage(200, 150, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);

            MemoryStream thumbnailStream = new MemoryStream();

            thumbnailImage.Save(thumbnailStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            thumbnailStream.Position = 0;

            byte[] imageBytes = new byte[thumbnailStream.Length];
            thumbnailStream.Read(imageBytes, 0, (int)thumbnailStream.Length);

            imgStream.Dispose();
            image.Dispose();
            thumbnailImage.Dispose();
            thumbnailStream.Dispose();

            return imageBytes;
        }

        //necessário...
        public static bool ThumbnailCallback() { return true; }
    }
}
