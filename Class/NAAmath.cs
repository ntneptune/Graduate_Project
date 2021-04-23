using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_NAA.Class
{
    class NAAmath
    {
		//mathematic function
		public static double N_A(double N_A0, double irr_T, double flux, double cr)
		{
			return (N_A0 * Math.Exp(-cr * flux * irr_T)); //not use
		}

		public static double N_B1(double N_A0, double irr_T, double flux, double cr, double dc)
		{
			return ((N_A0 * cr * flux) * (Math.Exp(-cr * flux * irr_T) -
				Math.Exp(-dc * irr_T)) / ((-cr * flux) + dc));
		}

		public static double N_B2(double N_A0, double irr_T, double flux, double cr, double dc, double rest_T)
		{
			return ((N_A0 * cr * flux) * (Math.Exp(-cr * flux * irr_T) -  //not use
				Math.Exp(-dc * irr_T)) * Math.Exp(-dc * rest_T)) / ((-cr * flux) + dc);
		}

		public static double N_B3(double N_A0, double irr_T, double flux, double cr, double dc, double rest_T,
			double count_T)
		{
			return ((N_A0 * cr * flux) * (Math.Exp(-cr * flux * irr_T) -  //not use
				Math.Exp(-dc * irr_T)) * Math.Exp(-dc * rest_T) * Math.Exp(-dc * count_T)) / ((-cr * flux) + dc);
		}

		public static double N_1(double N_A0, double flux, double cr, double dc, double irr_T)
		{
			return (((N_A0 * cr * flux) / ((-cr * flux) + dc))
				* ((Math.Exp(-dc * irr_T) / dc) - (Math.Exp(-cr * flux * irr_T) / (cr * flux)) - ((1.0 / dc) - (1.0 / (cr * flux)))));  //Integral NB  0 to T1   irr_time
		}

		public static double N_2(double N_A0, double flux, double cr, double dc, double irr_T, double rest_T)
		{
			return (((N_A0 * cr * flux) / ((-cr * flux) + dc))
				* (Math.Exp(-cr * flux * irr_T) - (Math.Exp(-dc * irr_T))) //Integral NB  T1 to T1+T2   rest_time
				* ((1.0 - Math.Exp(-dc * rest_T)) / dc));
		}

		public static double N_3(double N_A0, double flux, double cr, double dc, double irr_T, double rest_T, double count_T)
		{
			return (((N_A0 * cr * flux) / ((-cr * flux) + dc))
				* (Math.Exp(-cr * flux * irr_T) - (Math.Exp(-dc * irr_T))) * ((Math.Exp(-dc * rest_T) - Math.Exp(-dc * (rest_T + count_T))) / dc)); //Integral NB  T1+T2 to T1+T2+T3   count_time
		}

		public static double N_infinity(double N_A0, double flux, double cr, double dc, double irr_T)
		{
			return ((N_A0 / dc) * (1.0 - Math.Exp(-cr * flux * irr_T)));  //Integral NB  0 to infinity
		}

		public static double decay_constant(double hf)
		{
			return ((Math.Log(2) / Math.Log(Math.Exp(1))) / hf); //change hf to decay_const
		}

		public static double cross_section(double cross_section)
		{
			return (cross_section * Math.Pow(10, -24)); //unit barn to cm2

		}

		public static double Atomic(double mass, double ATM, double Avg_num)
		{
			return ((mass / ATM) * Avg_num);
		}


		public static double N_A0_monte(double count, double irr_T, double flux, double cr, double dc, double rest_T,  // สมการคิดย้อนกลับหา NA0
			double count_T)
		{
			return Math.Pow(((cr * flux) / ((-cr * flux) + dc))
				* (Math.Exp(-cr * flux * irr_T) - (Math.Exp(-dc * irr_T))) * (Math.Exp(-dc * rest_T)) * (1 - Math.Exp(-dc * count_T)), -1) * count;


		}

       
    }
}
