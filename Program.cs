using System;
using System.Collections.Generic;

namespace Zahlenpyramide
{
    class Program
    {
        static void Main(string[] args)
        {
            // Wir bauen eine Pyramide aus 15 Zahlen in 5 Reihen            
            Pyramide pyramide = new Pyramide(15, 5);

            // Baue die Pyramide so, dass eine Lösung entsteht
            pyramide.LoesePyramide();

            // Gib Loesungen aus
            pyramide.GibLoesungenAus();

            // 
            Console.ReadLine();
        }
    }
}
