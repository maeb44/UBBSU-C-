using System;
using System.Data;

class Program {
  static void Main()
  {
      double x = 1.5;

      double up = 4 + Math.Pow(x, 4) * Math.Pow(Math.Sin(x), 2) + Math.Exp(Math.Pow(x,2));
      double down = Math.Pow(x, 3) + Math.Log(Math.Pow(x,2) + 4);
      Console.WriteLine(up / down +Math.Abs(Math.Pow(x,3)));
    }
  }
  