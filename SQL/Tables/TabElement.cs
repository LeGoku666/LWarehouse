using System;
using System.Collections.Generic;
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
        public byte[] Image { get; set; }
        public string Info { get; set; }
    }
}
