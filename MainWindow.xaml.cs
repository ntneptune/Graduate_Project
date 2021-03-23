using System;
using System.Collections.Generic;
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

namespace Interface_NAA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {
            UserWin objUserWin = new UserWin();
            //this.Visibility = Visibility.Hidden;
            this.Close();
            objUserWin.Show();
           

        }
        private void Instructor_Click(object sender, RoutedEventArgs e)
        {
            InstructorWin objInstructorWin = new InstructorWin();
            //this.Visibility = Visibility.Hidden;
            objInstructorWin.Show();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBlock text = new TextBlock();
            
        }
    }
}