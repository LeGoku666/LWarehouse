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
        public string SetImage { set => _image = GetImageFromPatch(value); }
        public byte[] GetImage { get => _image; }
        private byte[] _image { get; set; }
        public string Info { get; set; }

        private byte[] GetImageFromPatch(string imagePatch)
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
    }
}
