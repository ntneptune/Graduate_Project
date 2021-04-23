using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace Interface_NAA
{
    /// <summary>
    /// Interaction logic for UserControlInstructor.xaml
    /// </summary>
    public partial class UserControlInstructor : UserControl
    {
        public static double neutronflux;
        public UserControlInstructor()
        {
            InitializeComponent();
            //InitializeUploadInStructortxt();

        }
        
        private void InitializeUploadInStructortxt()
        {

            string path = "Resources\\txtFile\\InstructorInputFile.txt";
            string[] lines = System.IO.File.ReadAllLines(@path);
            StreamWriter sw = new StreamWriter(@path);
            foreach (string line in lines)
            {
                sw.WriteLine(line);
            }   
            sw.Close();

        }

        private void Addsam_Click(object sender, RoutedEventArgs e)
        {
            string TBsampletext = TB_sample.Text.Trim();
            int checkError = 0;
            try
            {
                string [] sampletext = TBsampletext.Split(',');
                double mass = Convert.ToDouble(sampletext[1]);
            }
            catch
            {
                checkError = -1;
            }

            if(checkError == 0)
            {
                ListBox1.Items.Add(TBsampletext);
                TB_sample.Clear();
            }
            else
            {
                MessageBox.Show("Please fill the correct sample data.", "Error!!!", MessageBoxButton.OK);
            }
        
        }

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            string TBstdtext = TB_stdisotope.Text.Trim();
            string TBfluxtext = TB_Neutronflux.Text.Trim();
            string Error = "";
            int checkError = 0;
            try
            {
                string[] stdtext = TBstdtext.Split(',');
                double mass = Convert.ToDouble(stdtext[1]);
            }
            catch
            {
                checkError = -1;
                Error += "Please fill the correct std data." + Environment.NewLine;
            }

            try
            {
                string[] fluxtext = TBfluxtext.Split('x');
                double number = Convert.ToDouble(fluxtext[0]);
                string[] Epower = fluxtext[1].Split('^');
                int power = Convert.ToInt32(Epower[1]);
                int E = Convert.ToInt32(Epower[0]);
                if (E != 10)
                {
                    throw new Exception();
                }
               

            }
            catch(Exception)
            {
                checkError = -1;
                Error += "Please fill the correct neutorn flux data." + Environment.NewLine;
            }
            catch
            {
                checkError = -1;
                Error += "Please fill the correct neutorn flux data." + Environment.NewLine;
            }

            try
            {
                if (ListBox1.Items.Count == 0)
                {
                    throw new ArgumentException();
                }
            }
            catch(ArgumentException)
            {
                checkError = -1;
                Error += "Please fill in Listbox with sample data." + Environment.NewLine;
            }

            if (checkError == 0)
            {
 
                    var Ele_Iso = TBstdtext.Split('-');
                    TextWriter txt = new StreamWriter("Resources\\txtFile\\InstructorInputFile.txt");
                    txt.Write(TBfluxtext + "\n");
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

                    }
                    txt.Close();
                    MainWindow.ListUc[2] = new UserControlUser();

            }
            else
            {
                MessageBox.Show(Error, "Error!!!", MessageBoxButton.OK);
            }

           
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ListBox1.Items.Clear();
        }

        private void Reverse_Click(object sender, RoutedEventArgs e)
        {
            int j = ListBox1.Items.Count;
            int index = j - 1;
            if (index < 0)
            {
                index = 0;   
            }
            else
            {
                ListBox1.Items.RemoveAt(j - 1);
            }         

        }

     
    }
}
