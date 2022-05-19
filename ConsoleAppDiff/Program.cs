namespace ConsoleAppDiff
{
    class Program
    {
        static void Main(string[] args)
        {
            double a, b, dx, n, i;
            var x = new List<double>();
            var func = new List<double>();

            Console.WriteLine("Дано:" + "\t" + "f(x) = sin(1.8*x)");
            Console.Write("\t" + "dx = "); dx = Convert.ToDouble(Console.ReadLine());
            Console.Write("\t" + "a = "); a = Convert.ToDouble(Console.ReadLine());
            Console.Write("\t" + "b = "); b = Convert.ToDouble(Console.ReadLine());
            n = (b - a) / dx; Console.Write("\t" + "n = " + n); i = n;

            x.Add(a);
            Console.WriteLine("\t" + "x0 = " + x[0]);

            Console.WriteLine("\tx \tf");
            
            for (int k = 1; k <= i; k++) {
                x.Add(x[k - 1] + dx);
            }

            for (int k = 0; k < x.Count; k++) {
                func.Add(f(x[k]));
                Console.WriteLine("\t" + Math.Round(x[k], 3) + "\t" + Math.Round(func[k], 3));
            }

            Console.WriteLine("Дифференцирование функции, используя приближеную формулу");
            double derivative;
            
            for (int k = 1; k <x.Count; k++){
                derivative = approximate(x[k], dx);
                Console.WriteLine("\tdf(" + x[k] + ") = " + Math.Round(derivative, 3));
            }


        }

        static double f(double x){ return Math.Sin(1.8 * x); }
        static double approximate(double x, double dx) { return (f(x+dx)-f(x))/dx; }

    }
}
