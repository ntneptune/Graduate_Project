using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace Interface_NAA
{
    /// <summary>
    /// Interaction logic for UserControlUser.xaml
    /// </summary>
    public partial class UserControlUser : UserControl
    {
        public static double irr_T, rest_T, count_T;
        public static string _FILENAME = "";
        private string getFilename = "";

        public UserControlUser()
        {
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string strError = "";
            int checkError = 0;
            try
            {
                _FILENAME = getFilename;
                irr_T = Convert.ToDouble(irr_t.Text);
                rest_T = Convert.ToDouble(rest_t.Text);
                count_T = Convert.ToDouble(count_t.Text);
                ;
            }

            catch (Exception)
            {
                checkError = -1;
                strError = strError + "please input Time Value." + Environment.NewLine;

            }

            if (checkError == 0)
            {
                _FILENAME = getFilename;
                irr_T = Convert.ToDouble(irr_t.Text);
                rest_T = Convert.ToDouble(rest_t.Text);
                count_T = Convert.ToDouble(count_t.Text);

                ResultsGraph objResultsGraph = new ResultsGraph();
               
                //this.Visibility = Visibility.Hidden;
                objResultsGraph.Show();

            }
            else
            {
                MessageBox.Show(strError, "Error!!!", MessageBoxButton.OK);
            }


        }

        ObservableCollection<Class.InstructorFiletxt> txtFileDetail = new ObservableCollection<Class.InstructorFiletxt>();
        private void dgtxtFile_Initialized(object sender, EventArgs e)
        {
            string path = "C:\\code\\C#\\Project\\Interface NAA\\Interface NAA\\txtFile";
            DirectoryInfo d = new DirectoryInfo(@path);
            FileInfo[] Files = d.GetFiles("*.txt");
            int No = 1;
            foreach (FileInfo file in Files)
            {
                string flux = File.ReadAllLines(path + "\\" + file.Name).Take(1).First();
                string stdEleline = File.ReadLines(path + "\\" + file.Name).Skip(1).Take(1).First();
                string stdMassline = File.ReadLines(path + "\\" + file.Name).Skip(2).Take(1).First();
                string stdEle = stdEleline.Split(':')[1];
                string Mass = stdMassline.Split(',')[0];
                flux = flux.Replace("x10^", "E");
                txtFileDetail.Add(new Class.InstructorFiletxt
                {
                    ID = No,
                    Filename = file.Name,
                    Detail = "STD_ELE : " + stdEle + "  Mass : " + Mass + "  Flux : " + flux

                });
                No += 1;
            }
            dgtxtFile.ItemsSource = txtFileDetail;
        }

        private void dgtxtFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            var row_Selected = (Class.InstructorFiletxt)dg.SelectedItem;
            if (row_Selected != null)
            {
                getFilename = row_Selected.Filename;

            }
        }
        private void irr_t_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                e.Handled = true;
                rest_t.Focus();
            }
            if ((e.Key < Key.D0 || e.Key > Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }



        private void rest_t_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                e.Handled = true;
                count_t.Focus();
            }
            if ((e.Key < Key.D0 || e.Key > Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }



        private void count_t_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                e.Handled = true;
                confirm.Focus();
            }
            if ((e.Key < Key.D0 || e.Key > Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }

        private void irr_t_GotFocus(object sender, RoutedEventArgs e)
        {
            irr_t.SelectAll();
        }

   

        private void rest_t_GotFocus(object sender, RoutedEventArgs e)
        {
            rest_t.SelectAll();
        }

        private void count_t_GotFocus(object sender, RoutedEventArgs e)
        {
            count_t.SelectAll();
        }


    }
}
