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
using Interface_NAA.Class;

namespace Interface_NAA
{
    /// <summary>
    /// Interaction logic for UserControlSolution.xaml
    /// </summary>
    public partial class UserControlSolution : UserControl
    {
        public UserControlSolution()
        {
            InitializeComponent();

            var menuCaseI = new List<SubItem>();
            menuCaseI.Add(new SubItem("Step 1",new UserControlSolutionCase1Step1()));
            menuCaseI.Add(new SubItem("Step 2",new UserControlSolutionCase1Step2()));
            menuCaseI.Add(new SubItem("Step 3",new UserControlSolutionCase1Step3()));

            var Item0 = new ItemMenu("Case I", menuCaseI);

            var menuCaseII = new List<SubItem>();
            menuCaseII.Add(new SubItem("Step 1"));
            menuCaseII.Add(new SubItem("Step 2"));
            menuCaseII.Add(new SubItem("Step 3"));

            var Item1 = new ItemMenu("Case II", menuCaseII);

            Menu.Children.Add(new UserControlSolutionMenuItem(Item0,this));
            Menu.Children.Add(new UserControlSolutionMenuItem(Item1,this));


        }
        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);
            if (screen != null)
            {
                StackPanelSolution.Children.Clear();
                StackPanelSolution.Children.Add(screen);
            }
        }
    }
}
