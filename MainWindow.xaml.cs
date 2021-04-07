using Dragablz;
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
        List<UserControl> ListUc = new List<UserControl>();
        public MainWindow()
        {
            InitializeComponent();            
            ListUc.Add(new UserControlInstructor());
            ListUc.Add(new UserControlUser());
            ListUc.Add(new UserControlHelp());
           
        }


        private void ButtonPower_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GridPanel_MouseDown(object sender, RoutedEventArgs e)
        {
            DragMove();
        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBlock text = new TextBlock();
            
        }

        private void tcMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int tabItem = ((sender as TabablzControl)).SelectedIndex;
            if (e.Source is TabablzControl) 
            {
                switch (tabItem)
                {
                    case 0:    // HOME
                        //MessageBox.Show("HOME");
                        break;
                    case 1:    //INSTRUCTOR
                        //MessageBox.Show("INSTRUCTOR");
                        InitializeInstructor();
                        break;
                    case 2:   // USER
                        //MessageBox.Show("USER");
                        InitializeUser();
                        //UserWin objuser = new UserWin();
                        //objuser.Show();
                        
                        break;
                    case 3: //HELP or FAQ
                        InitializeFAQ();
                        //MessageBox.Show("HELP");
                        break;
                    case 4: //Github
                        //MessageBox.Show("Github");
                        break;
                    default:
                        MessageBox.Show("ERROR");
                        break;
                }
            }

        }

        private void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);
            if(screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }

        private void InitializeInstructor()
        {
            
            SwitchScreen(ListUc[0]);
        }
        private void InitializeUser()
        {
           
            SwitchScreen(ListUc[1]);
        }
        private void InitializeFAQ()
        {
            SwitchScreen(ListUc[2]);
        }
    }
}
