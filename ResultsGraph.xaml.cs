using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Interface_NAA
{
    /// <summary>
    /// Interaction logic for ResultsGraph.xaml
    /// </summary>
    public partial class ResultsGraph : Window
    {

        List<UserControl> ListUc = new List<UserControl>();
        public ResultsGraph()
        {
                
            InitializeComponent();
            ListUc.Add(new UserControlGraph());
            ListUc.Add(new UserControlDatagridIsotope());
            ListUc.Add(new UserControlSolution());
            //set Initialize to GRAPH
            SwitchScreen(ListUc[0]);
      

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(10 + (150 * index), 0, 0, 0);
            switch (index)
            {
                case 0: //GRAPH
                    InitializeGraph();
                    break;
                case 1: //ISOTOPE DATA
                    InitializeIsotopeGrid();
                    break;
                case 2: //SOLUTION
                    InitializeSolution();
                    break;
                default:
                    MessageBox.Show("Error");
                    break;
            }

        }

        private void ButtonPower_Click(object sender,RoutedEventArgs e)
        {
            this.Close();

        }
        private void GridPanelRG_MouseDown(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }

        private void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);
            if (screen != null)
            {
                StackPanelRG.Children.Clear();
                StackPanelRG.Children.Add(screen);
            }
        }
        private void InitializeGraph()
        {
            SwitchScreen(ListUc[0]);//0
        }
        private void InitializeIsotopeGrid()
        {
            SwitchScreen(ListUc[1]);//1 

        }
        private void InitializeSolution()
        {
            SwitchScreen(ListUc[2]);//2
        }
 

   
    }



}
