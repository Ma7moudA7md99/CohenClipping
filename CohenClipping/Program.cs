﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CohenClipping
{
    internal class Program
    {
        public const int INSIDE = 0; // 0000
        public const int LEFT = 1; // 0001
        public const int RIGHT = 2; // 0010
        public const int BOTTOM = 4; // 0100
        public const int TOP = 8; // 1000

        // Defining x_max, y_max and x_min, y_min for
        // clipping rectangle. Since diagonal points are
        // enough to define a rectangle
        public const int x_max = 10;
        public const int y_max = 8;
        public const int x_min = 4;
        public const int y_min = 4;

        // Function to compute region code for a point(x, y)
        public static int computeCode(double x, double y)
        {
            // initialized as being inside
            int code = INSIDE;

            if (x < x_min) // to the left of rectangle
                code |= LEFT;
            else if (x > x_max) // to the right of rectangle
                code |= RIGHT;
            if (y < y_min) // below the rectangle
                code |= BOTTOM;
            else if (y > y_max) // above the rectangle
                code |= TOP;

            return code;
        }

        // Implementing Cohen-Sutherland algorithm
        // Clipping a line from P1 = (x2, y2) to P2 = (x2, y2)
        public static void cohenSutherlandClip(double x1, double y1,
                                 double x2, double y2)
        {
            // Compute region codes for P1, P2
            int code1 = computeCode(x1, y1);
            int code2 = computeCode(x2, y2);

            // Initialize line as outside the rectangular window
            bool accept = false;

            while (true)
            {
                if ((code1 == 0) && (code2 == 0))
                {
                    // If both endpoints lie within rectangle
                    accept = true;
                    break;
                }
                else if ((code1 & code2) != 0)
                {
                    // If both endpoints are outside rectangle,
                    // in same region
                    break;
                }
                else
                {
                    // Some segment of line lies within the
                    // rectangle
                    int code_out;
                    double x = 0.0;
                    double y = 0.0;

                    // At least one endpoint is outside the
                    // rectangle, pick it.
                    if (code1 != 0)
                        code_out = code1;
                    else
                        code_out = code2;

                    // Find intersection point;
                    // using formulas y = y1 + slope * (x - x1),
                    // x = x1 + (1 / slope) * (y - y1)
                    if ((code_out & TOP) != 0)
                    {
                        // point is above the clip rectangle
                        x = x1 + (x2 - x1) * (y_max - y1) / (y2 - y1);
                        y = y_max;
                    }
                    else if ((code_out & BOTTOM) != 0)
                    {
                        // point is below the rectangle
                        x = x1 + (x2 - x1) * (y_min - y1) / (y2 - y1);
                        y = y_min;
                    }
                    else if ((code_out & RIGHT) != 0)
                    {
                        // point is to the right of rectangle
                        y = y1 + (y2 - y1) * (x_max - x1) / (x2 - x1);
                        x = x_max;
                    }
                    else if ((code_out & LEFT) != 0)
                    {
                        // point is to the left of rectangle
                        y = y1 + (y2 - y1) * (x_min - x1) / (x2 - x1);
                        x = x_min;
                    }

                    // Now intersection point x, y is found
                    // We replace point outside rectangle
                    // by intersection point
                    if (code_out == code1)
                    {
                        x1 = x;
                        y1 = y;
                        code1 = computeCode(x1, y1);
                    }
                    else
                    {
                        x2 = x;
                        y2 = y;
                        code2 = computeCode(x2, y2);
                    }
                }
            }
            if (accept)
            {
                Console.WriteLine("Line accepted from " + x1 + ", "
                     + y1 + " to " + x2 + ", " + y2);
                // Here the user can add code to display the rectangle
                // along with the accepted (portion of) lines
            }
            else
                Console.WriteLine("Line rejected");
        }

        // Driver code

        static public void Main()
        {
            // First Line segment
            // P11 = (5, 5), P12 = (7, 7)
            cohenSutherlandClip(5, 5, 7, 7);

            // Second Line segment
            // P21 = (7, 9), P22 = (11, 4)
            cohenSutherlandClip(7, 9, 11, 4);

            // Third Line segment
            // P31 = (1, 5), P32 = (4, 1)
            cohenSutherlandClip(1, 5, 4, 1);
            Console.ReadKey();

        }
    }
}
