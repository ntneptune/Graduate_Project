using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.IO;

namespace Interface_NAA
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class InstructorWin : Window
    {
        public static double neutronflux;
        public InstructorWin()
        {
            InitializeComponent();
        }


        private void addsam_Click(object sender, RoutedEventArgs e)
        {
            ListBox1.Items.Add(samiso.Text);
            samiso.Clear();
            if (ListBox1.Items.Count == 5)
            {
                if (stdisotope.Text.Trim() == "")
                {
                    samiso.Visibility = Visibility.Hidden;
                    MessageBox.Show("Please fill in all required data", "Error");

                }
                else
                {
                    string stdiSotope = stdisotope.Text.Trim();
                    var stdiso = stdiSotope.Split(',');
                    samiso.Visibility = Visibility.Hidden;        
                    this.Visibility = Visibility.Hidden;
                }
            }
        }

        public void addno_Click(object sender, RoutedEventArgs e)
        {
            var stdiSotope = stdisotope.Text.Trim();
            //string[] lines = sample.Text.Trim();
            if (stdiSotope == "")
            {
                MessageBox.Show("Please fill in all required data", "Error");
            }
            else if (stdiSotope != "")
            {
                var Ele_Iso = stdiSotope.Split('-');
                TextWriter txt = new StreamWriter("C:\\code\\C#\\Project\\Interface NAA\\Interface NAA\\txtFile" +
                    "\\InstructorInputFile.txt");
                txt.Write(Neutronflux.Text.Trim() + "\n");
                string path = @"Isotope data.csv";
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using (var input = new StreamReader(fs))
                {
                    input.ReadLine();
                    string Ele_A, IsoNum, CrossSec, HF, Energy;
                    List<string> Element = new List<string>();
                    List<string> Isotopenum = new List<string>();
                    List<double> CS = new List<double>();
                    List<double> hf = new List<double>();
                    List<double> E = new List<double>();
                    List<double> Mass = new List<double>();
                    while (!input.EndOfStream)
                    {
                        var line = input.ReadLine();
                        var values = line.Split(',');
                        int i = ListBox1.Items.Count;
                        if (values[0] == Ele_Iso[0])
                        {
                            var ele_iso = Ele_Iso[1].Split(',');
                            if (ele_iso[0] == values[1])
                            {
                                Ele_A = values[0];
                                IsoNum = values[1];
                                CrossSec = values[2];
                                HF = values[5];
                                Energy = values[6];
                                txt.Write("Standard Isotope: " + Ele_Iso[0] + "-" + ele_iso[0] + "\n");
                                txt.Write(ele_iso[1] + "," + CrossSec + "," + HF + "," + Energy + "\n");
                            }
                        }
                        for (int j = 0; j < i; j++)
                        {
                            string[] EleB_Mass = Convert.ToString(ListBox1.Items[j]).Split(',');
                            string[] ELE_B = EleB_Mass[0].Split('-');
                            if (values[0] == ELE_B[0] && values[1] == ELE_B[1] && values[5] != "-")
                            {
                                Element.Add(values[0]);
                                Isotopenum.Add(values[1]);
                                CS.Add(Convert.ToDouble(values[2]));
                                hf.Add(Convert.ToDouble(values[5]));
                                E.Add(Convert.ToDouble(values[6]));
                                Mass.Add(Convert.ToDouble(EleB_Mass[1]));
                            }
                        }
                    }
                    for (int o = 0; o < Element.Count(); o++)
                    {
                        txt.Write("Sample Isotope: " + Element[o] + "-" + Isotopenum[o] + "\n");
                        txt.Write(Mass[o] + "," + CS[o] + "," + hf[o] + "," + E[o] + "\n");
                    }
                    //var a = Neutronflux.Text.Split('^');
                    //neutronflux = Math.Pow(Convert.ToDouble(a[0]), Convert.ToDouble(a[1]));   //assume input data is correct
                }
                txt.Close();
                this.Visibility = Visibility.Hidden;
                this.Close();
            }
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ListBox1.Items.Clear();
        }

        private void Reverse_Click(object sender, RoutedEventArgs e)
        {
            int j = ListBox1.Items.Count;
            ListBox1.Items.RemoveAt(j - 1);
        }

    }
}
