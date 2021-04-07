using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Interface_NAA
{
    /// <summary>
    /// Interaction logic for UserControlDatagridIsotope.xaml
    /// </summary>
    /// 
   
    public partial class UserControlDatagridIsotope : UserControl
    {
        ObservableCollection<dataset> ds = new ObservableCollection<dataset>();
        public UserControlDatagridIsotope()
        {
            InitializeComponent();
        }

        private void DataGrid_Initialized(object sender, EventArgs e)
        {
            string Excelfile = @"C:\\code\\C#\\Project\\Interface NAA\\Interface NAA\\bin\\Debug\\net472\\Isotope data.csv";
            string[] lines = File.ReadAllLines(Excelfile);
            lines = lines.ToList().GetRange(1, lines.Length - 1).ToArray();
            foreach (var line in lines)
            {

                var Value = line.Split(',');
                ds.Add(new dataset
                {
                    Ele_A = Value[0],
                    A_num = Value[1],
                    Cs = Value[2],
                    Ele_B = Value[3],
                    B_num = Value[4],
                    Hf = Value[5],
                    Energy = Value[6],
                    Abun = Value[7],
                    Atmmass = Value[8]
                });
            }
            DataGridIsotope.ItemsSource = ds;
        }
    }

    class dataset
    {
        public string Ele_A { get; set; }
        public string A_num { get; set; }
        public string Cs { get; set; }
        public string Ele_B { get; set; }
        public string B_num { get; set; }
        public string Hf { get; set; }
        public string Energy { get; set; }
        public string Abun { get; set; }
        public string Atmmass { get; set; }
    }
}
