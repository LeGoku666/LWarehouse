using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LWarehouse.SQL
{
    class TabElement
    {
        public string Symbol { get; set; }
        public string Warehouse { get; set; }
        public string Komponent { get; set; }
        public string SetImageFromPatch { set => Image = SetImageAsPatch(value); }
        public byte[] SetImageFromBytes { set => Image = SetImageAsBytes(value); }
        public byte[] GetImage { get => Image; }
        private byte[] Image { get; set; }
        public string Info { get; set; }

        private static byte[] SetImageAsPatch(string imagePatch)
        {
            if (imagePatch == "")
                return null;

            try
            {
                //  FileStream inputStream = new(@"Images\x.PNG", FileMode.Open);
                FileStream inputStream = new(imagePatch, FileMode.Open);

                byte[] image = new byte[inputStream.Length];

                inputStream.Read(image, 0, image.Length);
                inputStream.Close();

                return image;
            }
            catch
            {
                return null;
            }
        }

        private static byte[] SetImageAsBytes(byte[] image)
        {
            if (image == null || image.Length == 0)
                return null;

            return image;
        }
    }
}
