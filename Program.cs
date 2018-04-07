using System;
using System.Collections.Generic;

namespace Zahlenpyramide
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Wir bauen eine Pyramide aus 15 Zahlen in 5 Reihen            
            Pyramide pyramide = new Pyramide(15, 5);

            // Wir geben die Pyramide am Anfang aus
            pyramide.GibPyramideAus();

            // Baue die Pyramide so, dass eine Lösung entsteht
            pyramide.LoesePyramide();

            // 
            Console.ReadLine();
        }
    }
}
