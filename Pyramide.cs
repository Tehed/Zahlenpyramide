using System;
using System.Collections.Generic;

namespace Zahlenpyramide
{
    public class Pyramide
    {

        // Unsere Pyramide
        int[] pyramide;

        // Anzahl der Reihen der Pyramide
        int reihen;

        // Die höchste Zahl, die benutzt wird. 
        // Alle Zahlen in der Pyramide liegen zwischen 1 und maxZahl;
        int maxZahl;

        List<int[]> untersteReihe;

        /**
         * Konstruktor
         */
        public Pyramide(int zahlen, int reihen)
        {
            // Initialisiere die Attribute der Klasse
            this.pyramide = new int[zahlen];
            this.maxZahl = pyramide.Length;
            this.reihen = reihen;
            this.untersteReihe = new List<int[]>();

            // Initial werden alle Werte auf 0 gesetzt
            for (int i = 0; i < pyramide.Length; i++)
            {
                pyramide[i] = 0;
            }

            // Wir generieren alle Möglichkeiten, die in der untersten Reihe stehen können. 
            // Das sind insg. 15*14*13*12*11 = 360.360 Möglichkeiten. 
            GeneriereMoeglichkeitenFuerUntersteReihe();
        }

        private void GeneriereMoeglichkeitenFuerUntersteReihe()
        {
            for (int a = 1; a <= 15; a++)
            {
                for (int b = 1; b <= 14; b++)
                {
                    for (int c = 1; c <= 13; c++)
                    {
                        for (int d = 1; d <= 12; d++)
                        {
                            for (int e = 1; e <= 11; e++)
                            {
                                int[] permutation = new int[] { a, b, c, d, e };
                                untersteReihe.Add(permutation);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Es gibt {0} Möglichkeiten für die unterste Reihe", untersteReihe.Count);
        }


        public void LoesePyramide()
        {
            // Wir probieren alle Möglichkeiten der unteren Reihe durch
            //foreach (int[] reihe in untersteReihe)
            for (int i = 0; i < 2; i++)
            {
                int[] reihe = untersteReihe[i];

                // wir fangen hinten in der Pyramide an und setzen erst
                // einmal alle Zahlen dieser Möglichkeit in die unterste
                // Reihe ein
                int pyramidenPosition = pyramide.Length - 1;
                for (int j = 0; j < reihe.Length; j++)
                {
                    int zahl = reihe[j];
                    SetzeZahlAnPosition(pyramidenPosition, zahl);
                    pyramidenPosition--;
                }

                BerechneLoestungMitUntersterReihe();

                GibPyramideAus();
            }
        }

        private void BerechneLoestungMitUntersterReihe()
        {
            // TODO
        }

        List<int> benutzteZahlen = new List<int>();
        private void SetzeZahlAnPosition(int pyramidenPosition, int zahl)
        {
            if (pyramidenPosition >= 0 && pyramidenPosition < pyramide.Length)
            {
                pyramide[pyramidenPosition] = zahl;
                BenutzeZahl(zahl);
            }
        }

        private bool BenutzeZahl(int zahl)
        {
            if (!benutzteZahlen.Contains(zahl))
            {
                benutzteZahlen.Add(zahl);
                return true;
            }

            return false;
        }

        private bool GibZahlFrei(int zahl)
        {
            if (benutzteZahlen.Contains(zahl))
            {
                benutzteZahlen.Remove(zahl);
                return true;
            }

            return false;
        }

        /**
         * Gibt die Pyramide auf der Console aus.
         */
        public void GibPyramideAus()
        {
            int index = 0;
            for (int i = 1; i <= reihen; i++)
            {
                // Gib Leerzeichen links von der Pyramide aus
                for (int j = 0; j < (reihen - i); j++)
                {
                    Console.Write("  ");
                }

                // Gib Zahlen pro Reihe aus.
                for (int j = 1; j <= i; j++)
                {
                    if (pyramide[index] > 10)
                    {
                        Console.Write(" " + pyramide[index] + "  ");
                    }
                    else
                    {
                        Console.Write("  " + pyramide[index] + " ");
                    }

                    index++;
                }
                Console.WriteLine("\n");
            }
        }

    }
}