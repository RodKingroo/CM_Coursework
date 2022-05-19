namespace ConsoleAppDiff
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double a, b, dx, n, i;
            var x = new List<double>();
            var f = new List<double>();


            Console.WriteLine("Дано:" + "\t" + "f(x) = sin(1.8*x)");
            Console.Write("\t"+"dx = ");  dx = Convert.ToDouble(Console.ReadLine());
            Console.Write("\t" + "a = "); a  = Convert.ToDouble(Console.ReadLine());
            Console.Write("\t" + "b = "); b  = Convert.ToDouble(Console.ReadLine());
            n = (b - a) / dx; Console.Write("\t" + "n = " + n); i = n;

            x.Add(a);
            Console.WriteLine("\t" + "x0 = " + x[0]);

            Console.WriteLine("\tx \tf");
            for (int k = 1; k <= i; k++){
                x.Add(x[k-1] + dx);
            }
            for (int k=0; k<x.Count; k++){
                f.Add(Math.Sin(1.8 * x[k]));
                Console.WriteLine("\t" + Math.Round(x[k], 3) + "\t" + Math.Round(f[k], 3));
            }

        }

        public void Derivative(double f){



        }
    }
}
