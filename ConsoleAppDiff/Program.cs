namespace ConsoleAppDiff
{
    class Program{
        static void Main(string[] args){
            
            Console.WriteLine("Дано: \tf(x) = sin(1.8x) \n" +
                "\txпринадлежащей[0, PI]. \n" +
                "\tx |\t0 |\tPI/4 |\tPI/2 |\t3PI/4 |\tPI |\n" +
                "\t----------------------------------------------\n" +
                "\ty | \t  | \t     | \t     | \t      |    | \n");
            
            List<double> x = new List<double>();
            List<double> fy = new List<double>();
            List<double> daprF = new List<double>();
            List<double> dsimF = new List<double>();
            List<double> dLgrF = new List<double>();


            double dx = Math.PI / 4;
            
            x.Add(0);
            
            for (int i = 0; x[i] <= Math.PI; i++) {
                fy.Add(Math.Sin(1.8 * x[i]));
                Console.WriteLine($" f(x[{i}]) = f({Math.Round(x[i], 3)}) = {Math.Round(fy[i],3)}");
                x.Add(x[i] + dx);
            }

            Console.WriteLine("\tДифференцирование функций, используя симметричную формулу");
            for (int i = 1; x[i] <= Math.PI; i++){
                dsimF.Add(simetrical(x[i], dx));
                Console.WriteLine($"df(x[{i}]) = f({Math.Round(x[i], 3)}) = {Math.Round(dsimF[i-1],3)}");
            }

            Console.WriteLine("\tДифференцирование функции, используя приближеную формулу");
            for (int i = 1; x[i] <= Math.PI; i++)
            {
                daprF.Add(approx(x[i], dx));
                Console.WriteLine($"df(x[{i}]) = f({Math.Round(x[i], 3)}) = {Math.Round(daprF[i - 1], 3)}");
            }

            Console.WriteLine("\tДифференцирование функции через построение полинома Лангранжа");
            for (int i = 1; x[i] <= Math.PI; i++)
            {
                dLgrF.Add(Lagrange(x, fy, x[i]));
                Console.WriteLine($"df(x[{i}]) = f({Math.Round(x[i], 3)}) = {Math.Round(dLgrF[i - 1], 3)}");
            }

            while (Console.ReadKey().Key != ConsoleKey.Escape) ;
        }

        /*f(x) = sin(1.8*x)*/
        static double f(double x){ return Math.Sin(1.8 * x); }

        static double approx(double x, double h)
        {
            return (f(x + h) - f(x)) / h;
        }

        static double simetrical(double x, double h)
        {
            return (f(x + h) - f(x - h)) / (2 * h);
        }

        static double Lagrange(List <double> x, List <double> y, double p)
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
                    if (i != j) {
                        t1 *= (p - x[j]);
                        t2 *= (x[i] - x[j]);
                        
                    }
                }

                L.Add(y[i] * (t1 / t2));
            }

            for(int i = 0; i < L.Count; i++)
            {
                yy += L[i];
            }

            return yy;

        }
    }
}
