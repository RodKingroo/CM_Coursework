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
            List<double> daprF = new List<double>();
            List<double> dsimF = new List<double>();
            List<double> LgrF = new List<double>();
            List<double> dLgrF = new List<double>();
            List<double> ERdLgrF = new List<double>();
            List<double> NwtF = new List<double>();
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
            Console.WriteLine("\tДифференцирование функций, используя симметричную формулу");
            for (int i = 1; x[i] <= Math.PI; i++)
            {
                dsimF.Add(simetrical(x[i], dx));
                Console.WriteLine($"df(x[{i-1}]) = f({Math.Round(x[i-1], 3)}) = {Math.Round(dsimF[i - 1], 3)}");
            }

            Console.WriteLine("\n");
            Console.WriteLine("\tДифференцирование функции, используя приближеную формулу");
            for (int i = 1; x[i] <= Math.PI; i++)
            {
                daprF.Add(approx(x[i], dx));
                Console.WriteLine($"df(x[{i - 1}]) = f({Math.Round(x[i - 1], 3)}) = {Math.Round(daprF[i - 1], 3)}");
            }

            Console.WriteLine("\n");
            Console.WriteLine("\tДифференцирование функции через построение полинома Лангранжа");
            for (int i = 0; x[i] <= Math.PI; i++)  { LgrF.Add(Lagrange(x, fy, x[i])); }
            for (int j = 0; j < LgrF.Count - 1; j++)
            {
                dLgrF.Add((LgrF[j + 1] - LgrF[j] / dx));
                Console.WriteLine($"df(x[{j}]) = f({Math.Round(x[j], 3)}) = {Math.Round(dLgrF[j], 3)}");
            }

            Console.WriteLine("\tПогрешность полинома Лагранжа равна:");
            for(int i = 0;  x[i]<=Math.PI; i++)
            {
                ERdLgrF.Add(fy[i] - LgrF[i]);
                Console.WriteLine($"Rn({Math.Round(x[i], 3)}) = {Math.Round(fy[i], 3)}-{Math.Round(LgrF[i],3)}={Math.Round(ERdLgrF[i], 3)}");
            }
            Console.WriteLine("\n");
            Console.WriteLine("\tДифференцирование функции через построение интерполяции Ньютона");
            for (int i = 0; x[i] <= Math.PI; i++) { NwtF.Add(Newton(x, fy, x[i], dx)); }
            for (int j = 0; j < NwtF.Count - 1; j++) {
                dNwtF.Add((NwtF[j + 1] - NwtF[j] / dx));
                Console.WriteLine($"df(x[{j}]) = f({Math.Round(x[j], 3)}) = {Math.Round(dNwtF[j], 3)}");
            }

            Console.WriteLine("\tПогрешность интерполяции Ньютона равна:");
            for (int i = 0; x[i] <= Math.PI; i++)
            {
                ERdNwtF.Add(fy[i] - NwtF[i]);
                Console.WriteLine($"Rn({Math.Round(x[i],3)}) = {Math.Round(fy[i], 3)}-{Math.Round(NwtF[i], 3)} = {Math.Round(ERdNwtF[i], 3)}");
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
            double yy = 0;
            double c = 0;
            int n = x.Count - 2;

            List<double> dy = new List<double>();

            for (int i = 0; i < n; i++)
            {
                dy.Add(y[i]);
            }
            for (int j = 1; j < n; j++)
            {
                for (int jj = n; jj < j; jj--)
                {
                    dy[jj] = dy[jj] - dy[jj - 1];
                }
            }
            yy = y[0];
            c = (p - x[0])/h;
            for (int ii = 1; ii < n; ii++)
            {
                yy += dy[ii] * c;
                c = c * (p - x[0]) / (h * (ii + 1));
            }
            return yy;
        }
    }
}
