using System;
using System.Collections.Generic;

namespace Zahlenpyramide
{
    public class Pyramide
    {

        // Alle Möglichen Lösungen werden hier gespeichert
        List<int[]> loesungen;

        // So viele Zahlen hat eine Pyramide. 
        // Die Zahlen gehen immer von 1 bis "zahlen"
        int maxZahl;

        // Anzahl der Reihen der Pyramide
        int reihen;

        List<int[]> untersteReihe;

        /**
         * Konstruktor
         */
        public Pyramide(int zahlen, int reihen)
        {
            // Initialisiere die Attribute der Klasse
            this.loesungen = new List<int[]>();
            this.maxZahl = zahlen;
            this.reihen = reihen;
            this.untersteReihe = new List<int[]>();

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
                                int[] moeglicheReihe = new int[] { a, b, c, d, e };
                                if (IstUntersteReiheMoeglich(moeglicheReihe))
                                {
                                    untersteReihe.Add(moeglicheReihe);
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Es gibt {0} Möglichkeiten für die unterste Reihe", untersteReihe.Count);
        }

        /**
         * Eine Reihe ist dann ok, wenn keine der Zahlen doppelt ist.
         * In der untersten Reihe muss außerdem auf jeden Fall die 
         * höchste Zahl vorkommen.
         */
        private bool IstUntersteReiheMoeglich(int[] reihe)
        {
            List<int> test = new List<int>();
            for (int i = 0; i < reihe.Length; i++)
            {
                if (test.Contains(reihe[i]))
                {
                    // Wert ist schon vorhanden => doppelter Wert
                    // Wir brechen hier ab - die Zeile ist nicht möglich!
                    return false;
                }
                else
                {
                    test.Add(reihe[i]);
                }
            }

            // wenn die Liste so lang ist wie das array, dann ist die Reihe gültig
            return (test.Count == reihe.Length);
        }

        public void LoesePyramide()
        {
            // Wir probieren alle Möglichkeiten der unteren Reihe durch
            //foreach (int[] reihe in untersteReihe)
            for (int i = 0; i < 2; i++)
            {
                int[] reihe = untersteReihe[i];

                BerechneLoestungMitUntersterReihe(reihe);
            }
        }

        private void BerechneLoestungMitUntersterReihe(int[] untersteReihe)
        {
            // Das hier könnte eine Lösung werden
            int[] pyramide = new int[maxZahl];

            // wir fangen hinten in der Pyramide an und setzen erst
            // einmal alle Zahlen dieser Möglichkeit in die unterste
            // Reihe ein
            int aktuellePosition = pyramide.Length - 1;
            for (int i = 0; i < untersteReihe.Length; i++)
            {
                int zahl = untersteReihe[i];
                SetzeZahlAnPosition(pyramide, aktuellePosition, zahl);
                aktuellePosition--;
            }

            // alle anderen Werte werden berechnet
            for (int i = aktuellePosition; i >= 0; i--)
            {

            }

            GibPyramideAus(pyramide);
        }

        List<int> benutzteZahlen = new List<int>();
        private void SetzeZahlAnPosition(int[] pyramide, int position, int zahl)
        {
            if (position >= 0 && position < pyramide.Length)
            {
                pyramide[position] = zahl;
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

        /**
         * Gibt die Pyramide auf der Console aus.
         */
        public void GibPyramideAus(int[] pyramide)
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