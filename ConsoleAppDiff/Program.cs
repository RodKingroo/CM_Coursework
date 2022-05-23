namespace ConsoleAppDiff
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Дано: \tf(x) = sin(1.8x) \n" +
                "\txпринадлежащей[0, PI]. \n" +
                "\tx |\t0 |\tPI/4 |\tPI/2 |\t3PI/4 |\tPI |\n" +
                "\t----------------------------------------------\n" +
                "\ty | \t  | \t     | \t     | \t      |    | \n");

            List<double> x = new List<double>();
            List<double> fy = new List<double>();
            List<double> dsimF = new List<double>();
            List<double> daprF = new List<double>();
            List<double> dLgrF = new List<double>();
            List<double> ERdLgrF = new List<double>();
            List<double> dNwtF = new List<double>();
            List<double> ERdNwtF = new List<double>();


            double dx = Math.PI / 4;

            x.Add(0);

            for (int i = 0; x[i] <= Math.PI; i++)
            {
                fy.Add(f(x[i]));
                Console.WriteLine($" f(x[{i}]) = f({Math.Round(x[i], 3)}) = {Math.Round(fy[i], 3)}");
                x.Add(x[i] + dx);
            }

            Console.WriteLine("\n");
            Console.WriteLine("\tДифференцирование функции, используя приближеную формулу");
            for (int i = 1; x[i] <= Math.PI; i++)
            {
                daprF.Add(approx(x[i], dx));
                Console.WriteLine($"df(x[{i - 1}]) = f({Math.Round(x[i - 1], 3)}) = {Math.Round(daprF[i - 1], 3)}");
            }

            Console.WriteLine("\n");
            Console.WriteLine("\tДифференцирование функций, используя симметричную формулу");
            for (int i = 1; x[i] <= Math.PI; i++)
            {
                dsimF.Add(simetrical(x[i], dx));
                Console.WriteLine($"df(x[{i - 1}]) = f({Math.Round(x[i - 1], 3)}) = {Math.Round(dsimF[i - 1], 3)}");
            }

            Console.WriteLine("\n");
            Console.WriteLine("\tДифференцирование функции через построение полинома Лангранжа");
            for (int j = 1; x[j] <= Math.PI; j++)
            {
                dLgrF.Add((Lagrange(x, fy, x[j] + dx) - Lagrange(x, fy, x[j])) / dx);
                Console.WriteLine($"df(x[{j}]) = f({Math.Round(x[j], 3)}) = {Math.Round(dLgrF[j - 1], 3)}");
            }

            Console.WriteLine("\tПогрешность полинома Лагранжа равна:");
            for (int i = 0; x[i] <= Math.PI; i++)
            {
                double L = Lagrange(x, fy, x[i]);
                ERdLgrF.Add(fy[i] - L);
                Console.WriteLine($"Rn({Math.Round(x[i], 3)}) = {Math.Round(fy[i], 3)}-{Math.Round(L, 3)}={Math.Round(ERdLgrF[i], 3)}");
            }
            Console.WriteLine("\n");
            Console.WriteLine("\tДифференцирование функции через построение интерполяции Ньютона");
            for (int j = 1; x[j] <= Math.PI; j++)
            {
                dNwtF.Add((Newton(x, fy, x[j] + dx, dx) - Newton(x, fy, x[j], dx)) / dx);
                Console.WriteLine($"df(x[{j}]) = f({Math.Round(x[j], 3)}) = {Math.Round(dNwtF[j - 1], 3)}");
            }

            Console.WriteLine("\tПогрешность интерполяции Ньютона равна:");
            for (int i = 0; x[i] <= Math.PI; i++)
            {
                double N = Newton(x, fy, x[i], dx);
                ERdNwtF.Add(fy[i] - N);
                Console.WriteLine($"Rn({Math.Round(x[i], 3)}) = {Math.Round(fy[i], 3)}-{Math.Round(N, 3)} = {Math.Round(ERdNwtF[i], 3)}");
            }


            while (Console.ReadKey().Key != ConsoleKey.Escape) ;
        }

        /*f(x) = sin(1.8*x)*/
        static double f(double x) { return Math.Sin(1.8 * x); }

        static double approx(double x, double h)
        {
            return (f(x + h) - f(x)) / h;
        }

        static double simetrical(double x, double h)
        {
            return (f(x + h) - f(x - h)) / (2 * h);
        }

        static double Lagrange(List<double> x, List<double> y, double p)
        {
            double yy = 0;
            double n = x.Count - 2;
            double t1;
            double t2;
            List<double> L = new List<double>();

            for (int i = 0; i < n; i++)
            {
                t1 = 1;
                t2 = 1;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        t1 *= (p - x[j]);
                        t2 *= (x[i] - x[j]);
                    }
                }
                L.Add(y[i] * (t1 / t2));
            }
            for (int i = 0; i < L.Count; i++)
            {
                yy += L[i];
            }
            return yy;

        }

        static double Newton(List<double> x, List<double> y, double p, double h)
        {

            int n = x.Count - 2;

            List<double> dy = new List<double>();

            for (int i = 0; i < n; i++) dy.Add(y[i]);

            for (int k = 0; k <= n; k++)
            {
                for (int i = 1; i < n - k; i++) dy[i] = dy[i] - dy[i - 1];
            }

            double yy = y[0];
            for (int k = 1; k < n; k++)
            {
                double c = 1;
                for (int i = 0; i < k; i++) c = c * (p - x[i]);
                yy = dy[k] / (Factorial(k) * Math.Pow(h, k)) * c + yy;
            }
            return yy;
        }


        static int Factorial(int n)
        {
            if (n == 1) return 1;

            return n * Factorial(n - 1);
        }
    }
}
