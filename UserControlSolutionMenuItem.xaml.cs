
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Interface_NAA.Class;

namespace Interface_NAA
{
    /// <summary>
    /// Interaction logic for UserControlSolutionMenuItem.xaml
    /// </summary>
    public partial class UserControlSolutionMenuItem : UserControl
    {
        UserControlSolution _context;
        public UserControlSolutionMenuItem(ItemMenu itemMenu,UserControlSolution context)
        {
            InitializeComponent();

            _context = context;

            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;

            this.DataContext = itemMenu;
        }

        
        private void ListItemMenu_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _context.SwitchScreen(((ItemMenu)((ListBoxItem)sender).DataContext).Screen);
        }


        private void TextBlock_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _context.SwitchScreen(((TextBlock)sender).Tag);
        }
    }
}
