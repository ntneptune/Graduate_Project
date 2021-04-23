using System.Windows.Controls;
using System.Windows.Input;

namespace Interface_NAA
{
    /// <summary>
    /// Interaction logic for UserControlHome.xaml
    /// </summary>
    public partial class UserControlHome : UserControl
    {
        public UserControlHome()
        {
            InitializeComponent();
            ZoomViewbox.Width = 1024;
            ZoomViewbox.Height = 2000;
        }

 
        private void UpdateViewBox(int newValue)
        {
            if ((ZoomViewbox.Width >= 0) && ZoomViewbox.Height >= 0)
            {
                ZoomViewbox.Width += newValue;
                ZoomViewbox.Height += newValue;
            }
        }

        private void UserControl_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control)
                return;
            UpdateViewBox((e.Delta > 0) ? 50 : -50);
        }
    }
}
