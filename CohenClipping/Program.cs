using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CohenClipping
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] points = window();
            int[] line = line1();
            int[] TBRL = Tbrl(line, points);
            foreach(int i in points)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            foreach (int i in TBRL)
            {
                Console.Write(i + " ");
            }
            Console.ReadKey();
        }
        static int[] window()
        {
            int[] points = new int[4];
            Console.WriteLine("Enter Xmin for the window :...");
            points[0] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Xmax for the window :...");
            points[1] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Ymin for the window :...");
            points[2] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Ymax for the window :...");
            points[3] = Convert.ToInt32(Console.ReadLine());
            return points;
        }
        static int[] line1()
        {
            int[] linePoint = new int[4];
            Console.WriteLine("Enter X value for the point 1:...");
            linePoint[0] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Y value for the point 1:...");
            linePoint[1] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter X value for the point 2:...");
            linePoint[2] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Y value for the point 2:...");
            linePoint[3] = Convert.ToInt32(Console.ReadLine());
            return linePoint;
        }
        static int[] Tbrl(int[] line, int[] points) 
        {
            int[] TBRL1 = new int[8] { 0, 0, 0, 0 ,0 ,0, 0, 0};
            
            if (line[0] < points[0])
                TBRL1[3] = 1;
            else if (line[0] > points[1])
                TBRL1[2] = 1;
            else if (line[1] < points[2])
                TBRL1[1] = 1;
            else if (line[1]  > points[3])
                TBRL1[0] = 1;


            if (line[2] < points[0])
                TBRL1[7] = 1;
            else if (line[2] > points[1])
                TBRL1[6] = 1;
            else if (line[3] < points[2])
                TBRL1[5] = 1;
            else if (line[3]  > points[3])
                TBRL1[4] = 1;
            return TBRL1;
        }
    }
}
