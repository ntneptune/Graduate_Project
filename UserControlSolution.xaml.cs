using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
            menuCaseI.Add(new SubItem("Case I",new UserControlSolutionCase1()));
            menuCaseI.Add(new SubItem("Case II",new UserControlSolutionCase2()));
         
            var Item0 = new ItemMenu("Case", menuCaseI);


            var Item2 = new ItemMenu("Home", new UserControlSolutionHome());

          
            Menu.Children.Add(new UserControlSolutionMenuItem(Item2, this));
            Menu.Children.Add(new UserControlSolutionMenuItem(Item0,this));
     

            // add to display created the SolutionPage as starting display.
            StackPanelSolution.Children.Add(new UserControlSolutionHome());





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
