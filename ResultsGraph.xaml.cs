using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.IO;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Configurations;
using Numpy;



namespace Interface_NAA
{
    /// <summary>
    /// Interaction logic for ResultsGraph.xaml
    /// </summary>
    public partial class ResultsGraph : Window
    {
        //data collection from inputData.txt
        private List<string> Element = new List<string>();
        private List<double> mass = new List<double>();
        private List<double> A_num = new List<double>();
        private List<double> cs = new List<double>();
        private List<double> hf = new List<double>();
        private List<double> Energy = new List<double>();
        //constant value
        private double Avg_num = 6.02214179e23;
        
        private double flux;
        private double irrTime = UserWin.irr_T;
        private double restTime = UserWin.rest_T;
        private double countTime = UserWin.count_T;
        //Results
        private List<double> NA0 = new List<double>();
        private List<double> NA = new List<double>();
        private List<double> counts = new List<double>();

     


        //read input data form inputData.txt and collect to List 
        public void readInputtxt(string inputFile)
        {
            string[] lines = File.ReadAllLines(inputFile);
            int column = 1;
            foreach (var line in lines)
            {
                if (column == 1)
                {
                    var b = line.Split('x');
                    var a = b[1].Split('^');
                    flux = Convert.ToDouble(b[0]) * Math.Pow(Convert.ToDouble(a[0]), Convert.ToDouble(a[1]));   //assume input data is correct
                }
                else if (column % 2 == 0)
                {
                    var a = line.Split(':');
                    var data = a[1].Split('-');
                    Element.Add(data[0].Trim());
                    A_num.Add(Convert.ToDouble(data[1]));

                }
                else
                {
                    var data = line.Split(',');

                    mass.Add(Convert.ToDouble(data[0]));
                    cs.Add(Convert.ToDouble(data[1]));
                    hf.Add(Convert.ToDouble(data[2]));
                    Energy.Add(Convert.ToDouble(data[3]));
                }

                column += 1;
            }
        }

        //monte carlo simulation



        public void montecarlo(int i)
        {


            //data from Ele
            double N_A0 = Class.NAAmath.Atomic(mass[i], A_num[i], Avg_num);
            double dc = Class.NAAmath.decay_constant(hf[i]);
            double Cs = Class.NAAmath.cross_section(cs[i]);
            double _NA = Class.NAAmath.N_A(N_A0, irrTime, flux, Cs);
            //Constant value
            double NB1 = Class.NAAmath.N_B1(N_A0, irrTime, flux, Cs, dc);
            double N1 = Class.NAAmath.N_1(N_A0, flux, Cs, dc, irrTime);
            double NBmax = Class.NAAmath.N_infinity(N_A0, flux, Cs, dc, irrTime);
            double P_t1 = N1 / NBmax;
            double P1 = NB1 / NBmax;

            //random M time
            int n_Repeat = 10000000; //rand ten million time
            double M, M23;
            M = (double)n_Repeat;
            var Px_random = np.random.rand(n_Repeat);
            var Px_morethan_P_t1 = Px_random[Px_random >= P_t1];
            var time = irrTime - (np.log(1 - (Px_morethan_P_t1 - P_t1) * dc / P1)) / dc;
            double lower = irrTime + restTime;
            double upper = irrTime + restTime + countTime;
            //var timeincase = time[(time >= lower) && (time <= upper)];
            var lowercase = time[time >= lower];
            var upperandlowercase = lowercase[lowercase <= upper];
            M23 = upperandlowercase.size;

            NA0.Add(N_A0);
            NA.Add(_NA);
            double ratio_NA_Subtraction = (NA0[i]-NA[i])/(NA0[0]-NA[0]);
            //Calculate dNB23 and collect the data
            double NB23 = M23 * ratio_NA_Subtraction;
            counts.Add(NB23);
          



        }

        public ResultsGraph()
        {
         
            
            readInputtxt(@"C:\\code\\C#\\Project\\Interface NAA\\Interface NAA\\txtFile\\" + UserWin._FILENAME);
            InitializeComponent();


            //montecarlo
            for (int index = 0; index < Element.Count; index++)
            {
                montecarlo(index);
               
            }

            Base = 10;


            var mapper = Mappers.Xy<Class.DataLinePoint>()
              .X(value => value.EnergyPoint) //a 10 base log scale in the X axis
              .Y(value => Math.Log(value.CountsPoint, Base));

            //MinAxisValue = double.NegativeInfinity;
            MinAxisValue = 0;
            double maxcounts = counts.Max<double>();
            MaxAxisValue = Math.Log(maxcounts, Base) + Math.Log(maxcounts, Base)/(double)3;
         




            SeriesCollection = new SeriesCollection(mapper);
            int n = counts.Count;
            for (int index = 0; index < n; index++)
            {
                if(counts[index] <= 1)
                {
                    counts[index] = 1;
                }
            }
        
            for (int index = 0; index < Element.Count; index++)
            {
                /*
                var x0 = Energy[index];
                var lowerE0 = x0 - 10;
                var upperE0 = x0 + 10;
                var x = np.arange(lowerE0, upperE0, 1);
               // var x_p = np.power((x - Energy[index]), np.array(2));
                double sigma = 11;
                var A = counts[index] / (20.0 - 2000.0/3.0/Math.Pow(sigma, 2));
                var Nd = np.power((x - x0) / sigma,np.array(2)); // natural distribution
                //var y = A * (1 - Nd);
                var y_ = A * (1 - Nd);
                //var Ar = 0.5 * (y[":-1"] + y["1:"]) * (x["1:"] - x[":-1"]);
                var y = y_[y_ >= 1];
                double Area = 1;
                //double Area = (double)np.sum(Ar);
                */

                var Linepoint = new ChartValues<Class.DataLinePoint>();
                var Columnpoint = new ChartValues<Class.DataLinePoint>();
                var temporal_Lp = new List<Class.DataLinePoint>();
                var temporal_Cp = new List<Class.DataLinePoint>();
                /*
                for (int i = 0; i < y.size; i++)
                {
                    temporal_Cp.Add(new Class.DataLinePoint() { CountsPoint = (double)y[i] });
                    temporal_Lp.Add(new Class.DataLinePoint() { EnergyPoint = (double)x[i], CountsPoint = (double)y[i], ShowEnergy = Energy[index], ShowArea = Area });
                }

                Linepoint.AddRange(temporal_Lp);
                Columnpoint.AddRange(temporal_Cp);
                */
                Linepoint.Add(new Class.DataLinePoint() { EnergyPoint = (double)Energy[index], CountsPoint = counts[index] });
                
                string name = index == 0? "Std Ele" :"Ele" + index.ToString();


                SeriesCollection.Add(new LineSeries
                {
                    Title = name,
                    Values = Linepoint,
                   
                  
                    
                });

              



                //Stroke = Brushes.IndianRed
                //PointGeometry = null,
                //Fill = Brushes.Transparent
                //Labels = new[] { "Maria", "Susan", "Charles", "Frida" };
            }
            /*
            var x0_ = Energy[0];
            var lowerE0_ = x0_ - 10;
            var upperE0_ = x0_ + 10;
            var x_ = np.arange(lowerE0_, upperE0_, 1);
            // var x_p = np.power((x - Energy[index]), np.array(2));
            double sigma_ = 11;
            var A_ = counts[0] / (20.0 - 2000.0 / 3.0 / Math.Pow(sigma_, 2));
            var Nd_ = np.power((x_ - x0_) / sigma_, np.array(2)); // natural distribution
            var y_ = A_ * (1 - Nd_);

            var Columnpoint_ = new ChartValues<Class.DataLinePoint>();
          
            var temporal_Cp_ = new List<Class.DataLinePoint>();

            for (int i = 0; i < y_.size; i++)
            {
                temporal_Cp_.Add(new Class.DataLinePoint() { CountsPoint = (double)y_[i] });
             
            }


            Columnpoint_.AddRange(temporal_Cp_);

            var v = new double[20];
            for (var i = 0; i < v.Length; i++)
            {
                v[i] = (double)y_[i];
                //Labels.Add(String.Format("{0} - {1}", texts[i % texts.Count], i));
                
            }

            Datapoint = new ChartValues<double>(v);
            SeriesCollection.Add(new ColumnSeries
            {

                //Values = Columnpoint,
                Values = Datapoint,

                Width = 1,
                Fill = Brushes.IndianRed



            });

            */
            //FormatterX = value => Math.Pow(Base, value).ToString("N");

            FormatterX = value => value.ToString("N");
            FormatterY = value => Math.Pow(Base, value).ToString("0.##e+00");
            DataContext = this;

           
           // SolidColorBrush gray = Brushes.Gray;
         
            //foreach (LineSeries series in SeriesCollection){
            //    series.Fill = Brushes.IndianRed;
            //};
            
 



        }
        public SeriesCollection SeriesCollection { get; set; } 
        public Func<double, string> FormatterX { get; set; }
        public Func<double, string> FormatterY { get; set; }
        public string[] Labels { get; set; }
        public double Base { get; set; }

        public double MaxAxisValue { get; set; }
        public double MinAxisValue { get; set; }
        public ChartValues<double> Datapoint { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            datagraid objDatagrid = new datagraid();
            objDatagrid.Show();
        }

  
    }



}
