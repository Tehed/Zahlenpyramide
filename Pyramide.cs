using System;
using System.Collections.Generic;
using System.Linq;

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

        // Struktur der Tabelle. In Zeile 1 gibt es nur 1 Position, nämlich "0". 
        // In Zeile 2 gibt es zwei Positionen, nämlich: "1" und "2" usw. 
        List<IEnumerable<int>> positionenProReihe;

        // Eine Liste aller Möglichkeiten für die unterste Reihe
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

            // Erstelle eine Struktur zum einfacheren Herausfinden, 
            // in welcher Zeile, welche Position steht. 
            positionenProReihe = new List<IEnumerable<int>>();
            int startPosition = 0;
            for (int i = 1; i <= reihen; i++)
            {
                positionenProReihe.Add(Enumerable.Range(startPosition, i));
                startPosition += i;
            }

            // Wir generieren alle Möglichkeiten, die in der untersten Reihe stehen können. 
            // Das sind insg. 15*14*13*12*11 = 360.360 Möglichkeiten. 
            GeneriereMoeglichkeitenFuerUntersteReihe();
        }

        /**
         * In der untersten Reihe gibt es maximal 15*14*13*12*11 Möglichkeiten.
         * Das kann eingeschränkt werden dadurch, dass...
         *     a) ... die höchste Zahl auf jeden Fall in der untersten Reihe sein muss
         *     b) ... keine der Zahlen doppelt sein darf.
         *
         * TODO: Diese Funktion generiert nur alle möglichen Kombinationen für
         *       eine unterste Reihe mit 5 Elementen. 
         */
        private void GeneriereMoeglichkeitenFuerUntersteReihe()
        {
            for (int a = 1; a <= 15; a++)
            {
                for (int b = 1; b <= 15; b++)
                {
                    for (int c = 1; c <= 15; c++)
                    {
                        for (int d = 1; d <= 15; d++)
                        {
                            for (int e = 1; e <= 15; e++)
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

            // wenn die Liste so lang ist wie das array und die höchste Zahl 
            // vorkommt, dann ist die Reihe gültig
            return (test.Count == reihe.Length && test.Contains(maxZahl));
        }

        /**
         * Gehe durch alle Möglichkeiten für die unterste Reihe,
         * berechne aus jeder dieser Möglichkeiten eine Pyramide
         * und überprüfe dabei, ob das eine richtige Lösung ist.
         */
        public void LoesePyramide()
        {
            // Wir probieren alle Möglichkeiten der unteren Reihe durch
            foreach (int[] reihe in untersteReihe)
            // for (int i = 0; i < 1; i++)
            {
                // int[] reihe = untersteReihe[i];
                // int[] reihe = new int[] { 6, 14, 15, 3, 13 };
                // Hier soll die Lösung rein
                int[] pyramide = new int[maxZahl];

                bool loesungOk = BerechneLoestungMitUntersterReihe(pyramide, reihe);
                if (loesungOk)
                {
                    loesungen.Add(pyramide);
                }
            }

            Console.WriteLine("Gefundene Lösungen: " + loesungen.Count);
        }

        private bool BerechneLoestungMitUntersterReihe(int[] pyramide, int[] untersteReihe)
        {
            // Wir müssen uns irgendwo merken, welche Zahlen schon benutzt wurden
            List<int> benutzteZahlen = new List<int>();

            // wir fangen hinten in der Pyramide an und setzen erst
            // einmal alle Zahlen dieser Möglichkeit in die unterste
            // Reihe ein
            int aktuellePosition = pyramide.Length - 1;
            for (int i = 0; i < untersteReihe.Length; i++)
            {
                int zahl = untersteReihe[i];
                pyramide[aktuellePosition] = zahl;
                benutzteZahlen.Add(zahl);
                aktuellePosition--;
            }

            // alle anderen Werte werden berechnet
            for (int i = aktuellePosition; i >= 0; i--)
            {
                int positionLinks = SuchePosition(i, false);
                int positionRechts = SuchePosition(i, true);
                int zahl = Math.Abs(pyramide[positionLinks] - pyramide[positionRechts]);

                // Console.WriteLine("Berechne Wert an Position " + i);
                // Console.WriteLine("\t=> untenLinks = " + positionLinks + " | untenRechts = " + positionRechts);
                // Console.WriteLine("\t=> zahl = " + pyramide[positionLinks] + " - " + pyramide[positionRechts]);

                if (benutzteZahlen.Contains(zahl) || zahl > maxZahl)
                {
                    // Die Zahl wurde schon benutzt => keine gültige Lösung
                    // Wir können hier abbrechen.
                    return false;
                }
                else
                {
                    // Alles ok - wir fügen die Zahl ein
                    pyramide[i] = zahl;
                    benutzteZahlen.Add(zahl);
                }
            }

            // Wenn wir bis hier nicht abgebrochen haben, war alles richtig
            return true;
        }

        private int SuchePosition(int position, bool rechts)
        {
            int reihe = SucheReiheFuerPosition(position);
            if (!rechts)
            {
                // Gesucht ist die Zahl links unter unserer Position. 
                // Die Position dieser gesuchten Zahl ist unsere aktuelle Position
                // plus unsere Reihe
                return position + reihe;
            }
            else
            {
                // Gesucht ist die Zahl rechts unter unserer Position. 
                // Die Position dieser gesuchten Zahl ist unsere aktuelle Position
                // plus unsere Reihe + 1
                return position + reihe + 1;
            }
        }

        private int SucheReiheFuerPosition(int position)
        {
            int aktuelleReihe = 1;
            foreach (IEnumerable<int> positionenInReihe in positionenProReihe)
            {
                if (positionenInReihe.Contains(position))
                {
                    // wir haben unsere Position gefunden und geben die Reihe zurück,
                    // in der wir die Position gefunden haben. 
                    return aktuelleReihe;
                }
                aktuelleReihe++;
            }

            // Wir haben gar keine Reihe gefunden => irgendwas war falsch
            return -1;
        }


        /** 
         * Gibt alle gefundenen Lösungen auf der Console aus.
         */
        public void GibLoesungenAus()
        {
            for (int i = 0; i < loesungen.Count; i++)
            {
                int[] loesung = loesungen[i];
                Console.WriteLine("Loesung {0}:", (i + 1));
                Console.WriteLine();
                GibPyramideAus(loesung);
                Console.WriteLine();
                Console.WriteLine("===========================");
                Console.WriteLine();
            }
        }

        /**
         * Gibt die Pyramide auf der Console aus.
         */
        private void GibPyramideAus(int[] pyramide)
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
                    Console.Write(" " + pyramide[index] + "  ");
                    index++;
                }
                Console.WriteLine("\n");
            }
        }

    }
}
