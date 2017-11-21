using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shilling_GroupProject
{
    public class PictureConverting
    {
        public byte[] converImageToBlob(Image img)
        {
            //convert image to varbinary
            ImageConverter con = new ImageConverter();
            return (byte[])con.ConvertTo(img, typeof(byte[]));
        }

        public Image convertBlobToImage(byte[] byteImg)
        {
            //converts blob to image using memorystream
            MemoryStream memstream = new MemoryStream(byteImg);
            return Image.FromStream(memstream);
        }
    }
}
