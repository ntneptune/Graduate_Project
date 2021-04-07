using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace Interface_NAA.Class
{
    public class ItemMenu
    {
        public ItemMenu(string header,List<SubItem> subItems)
        {
            Header = header;
            SubItems = subItems;

        }
        public string Header { get; set; }
        public List<SubItem> SubItems { get; set; }
        public UserControl Screen { get; set; }
    }
}
