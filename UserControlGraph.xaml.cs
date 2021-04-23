using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Configurations;
using Numpy;



namespace Interface_NAA
{
    /// <summary>
    /// Interaction logic for UserControlGraph.xaml
    /// </summary>
    public partial class UserControlGraph : UserControl
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
        private double irrTime = UserControlUser.irr_T;
        private double restTime = UserControlUser.rest_T;
        private double countTime = UserControlUser.count_T;
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


            //data from txtfile
            double N_A0 = Class.NAAmath.Atomic(mass[i], A_num[i], Avg_num);
            double dc = Class.NAAmath.decay_constant(hf[i]);
            double Cs = Class.NAAmath.cross_section(cs[i]);
            //Constant value
            double _NA = Class.NAAmath.N_A(N_A0, irrTime, flux, Cs);
            double NB1 = Class.NAAmath.N_B1(N_A0, irrTime, flux, Cs, dc);
            double N1 = Class.NAAmath.N_1(N_A0, flux, Cs, dc, irrTime);
            double NBmax = Class.NAAmath.N_infinity(N_A0, flux, Cs, dc, irrTime);
            double P_t1 = N1 / NBmax;
            double P1 = NB1 / NBmax;

            //random M time
            int M = 10000000; //rand ten million time
            double  M23;
            var Px_random = np.random.rand(M);
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
            double ratio_NA_Subtraction = (NA0[i] - NA[i]) / (NA0[0] - NA[0]);
            //Calculate dNB23 and collect the data
            double NB23 = M23 * ratio_NA_Subtraction;
            counts.Add(NB23);

        }
        public UserControlGraph()
        {
            readInputtxt("Resources\\txtFile\\" + UserControlUser._FILENAME);
            InitializeComponent();

            //montecarlo
            for (int index = 0; index < Element.Count; index++)
            {
                montecarlo(index);

            }

            plotGraph();

        }
        private void plotGraph()
        {
            Base = 10;
            var mapper = Mappers.Xy<Class.DataLinePoint>()
              .X(value => value.EnergyPoint) //a 10 base log scale in the X axis
              .Y(value => Math.Log(value.CountsPoint, Base));

            //MinAxisValue = double.NegativeInfinity;
            MinAxisValue = 0;
            double maxcounts = counts.Max<double>();
            MaxAxisValue = Math.Log(maxcounts, Base) + Math.Log(maxcounts, Base) / (double)3;


            SeriesCollection1 = new SeriesCollection(mapper);
            SeriesCollection2 = new SeriesCollection(mapper);
            int n = counts.Count;
            for (int index = 0; index < n; index++)
            {
                if (counts[index] <= 1)
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
                var Linepoint2 = new ChartValues<Class.DataLinePoint>();
                var temporal_Lp = new List<Class.DataLinePoint>();
                var temporal_Lp2 = new List<Class.DataLinePoint>();
                /*
                for (int i = 0; i < y.size; i++)
                {
                    temporal_Cp.Add(new Class.DataLinePoint() { CountsPoint = (double)y[i] });
                    temporal_Lp.Add(new Class.DataLinePoint() { EnergyPoint = (double)x[i], CountsPoint = (double)y[i], ShowEnergy = Energy[index], ShowArea = Area });
                }

                Linepoint.AddRange(temporal_Lp);
                Columnpoint.AddRange(temporal_Cp);
                */
                Linepoint.Add(new Class.DataLinePoint() { EnergyPoint = (double)Energy[index], CountsPoint = counts[index],ShowEnergy = (double)Energy[index],ShowCounts = counts[index] });
                Linepoint.Add(new Class.DataLinePoint() { EnergyPoint = (double)Energy[index], CountsPoint = 1 ,ShowEnergy = (double)Energy[index],ShowCounts = counts[index]});
                string name = index == 0 ? "Std Ele" : "Ele" + index.ToString();

                if (index == 0)
                {
                    SeriesCollection1.Add(new LineSeries
                    {
                        Title = name,
                        Values = Linepoint,
                        PointGeometry = null,
                        Stroke = Brushes.SkyBlue

                    });

                }
                else
                {
                    SeriesCollection2.Add(new LineSeries
                    {
                        Title = name,
                        Values = Linepoint,
                        PointGeometry = null,
                        Stroke = Brushes.IndianRed

                    });
                }


                //Stroke = Brushes.IndianRed
                //PointGeometry = null,
                //Fill = Brushes.Transparent

            }


            FormatterX = value => value.ToString("N");
            FormatterY = value => Math.Pow(Base, value).ToString("0.##e+00");
            DataContext = this;

        }
        public SeriesCollection SeriesCollection1 { get; set; }
        public SeriesCollection SeriesCollection2 { get; set; }
        public Func<double, string> FormatterX { get; set; }
        public Func<double, string> FormatterY { get; set; }
        public double Base { get; set; }

        public double MaxAxisValue { get; set; }
        public double MinAxisValue { get; set; }
        public ChartValues<double> Datapoint { get; set; }

    }
}
