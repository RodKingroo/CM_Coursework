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
            
            double dx = Math.PI / 4;
            
            x.Add(0);
            
            for (int i = 0; x[i] <= Math.PI; i++) {
                fy.Add(Math.Sin(1.8 * x[i]));
                Console.WriteLine($" f(x[{i}]) = f({Math.Round(x[i], 3)}) = {Math.Round(fy[i],3)}");
                x.Add(x[i] + dx);
            }

            Console.WriteLine("\tДифференцирование функций, используя симметричную формулу");

            List<double> dsimF = new List<double>();

            for (int i = 1; x[i] <= Math.PI; i++){
                dsimF.Add(simetrical(x[i], dx));
                Console.WriteLine($"df(x[{i}]) = f({Math.Round(x[i], 3)}) = {Math.Round(dsimF[i-1],3)}");
            }

            while (Console.ReadKey().Key != ConsoleKey.Escape) ;
        }

        /*f(x) = sin(1.8*x)*/
        static double f(double x){ return Math.Sin(1.8 * x); }

        static double simetrical(double x, double h)
        {
            return (f(x + h) - f(x - h)) / (2 * h);
        }
    }
}
