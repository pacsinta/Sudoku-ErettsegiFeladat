using System;

namespace Sudoku
{
    class Sudoku
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. feladat");
            Console.Write("A beolvasandó file neve: ");
            String fileName = Console.ReadLine();

            Console.Write("Adj meg egy sor számot (1-9): ");
            int row = readBetween();

            Console.Write("Adj meg egy oszlop számot (1-9): ");
            int column = readBetween();

            row -= 1; column -= 1;  // Legyen 0-hoz igazítva

  
            Console.WriteLine("2. feladat");
            StreamReader sr;
            try
            {
                sr = new StreamReader(fileName);
            }
            catch (Exception)
            {
                Console.WriteLine("A megadott file nem található! (A nehez.txt lesz megnyitva)");
                sr = new StreamReader("nehez.txt");
            }

            int[][] sudoku = new int[9][];
            int kijeloltErtek = 0;
            int db_nulla = 0;

            for (int i = 0; i < 9; i++)
            {
                String[] numbers = sr.ReadLine().Split(' ');
                sudoku[i] = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    sudoku[i][j] = Convert.ToInt32(numbers[j]);
                    if (row == i && column == j)
                    {
                        kijeloltErtek = sudoku[i][j];
                    }
                    if(sudoku[i][j] == 0)
                    {
                        db_nulla++;
                    }
                }
            }

            List<int[]> lepesek = new List<int[]>();
            while (!sr.EndOfStream)
            {
                String[] numbers = sr.ReadLine().Split(' ');
                int[] lepes = new int[3];
                lepes[0] = Convert.ToInt32(numbers[0]);
                lepes[1] = Convert.ToInt32(numbers[1]) - 1;
                lepes[2] = Convert.ToInt32(numbers[2]) - 1;
                lepesek.Add(lepes);
            }

            sr.Close();


            Console.WriteLine("3. feladat");
            if(kijeloltErtek == 0)
            {
                Console.WriteLine("Az adott \r\nhelyet még nem töltötték ki.");
            }
            else
            {
                Console.WriteLine("Az adott helyen lévő szám: " + kijeloltErtek);
            }
            int resztablazat = getResztablazat(row, column);

            Console.WriteLine("Résztáblázat: "+resztablazat);


            Console.WriteLine("4. feladat");
            double szazalek = ((double)db_nulla / 81.0) * 100.0;
            Console.WriteLine("A feladvány kitöltöttsége: " + String.Format("{0:0.0}", szazalek) + "%");

            Console.WriteLine("5. feladat");

            for (int i = 0; i < lepesek.Count; i++)
            {
                Console.WriteLine("A kiválasztott sor: " + (lepesek[i][1]+1) + " oszlop: " + (lepesek[i][2]+1) + " a szám: " + lepesek[i][0]);
                testMove(sudoku, lepesek[i][1], lepesek[i][2], lepesek[i][0]);
            }

        }

        static void testMove(int[][] sudoku, int row, int column, int value)
        {
            if (sudoku[row][column] != 0)
            {
                Console.WriteLine("A helyet már kitöltötték");
                return;
            }
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[row][i] == value)
                {
                    Console.WriteLine("Az adott sorban már szerepel a szám");
                    return;
                }
                if (sudoku[i][column] == value)
                {
                    Console.WriteLine("Az adott oszlopban már szerepel a szám");
                    return;
                }
            }
            
            int resztablazat = getResztablazat(row, column);

            int columnNormalized = (resztablazat - 1) % 3;  // 0, 1, 2
            int startColumn = columnNormalized * 3;  // 0, 3, 6
            int startRow = (resztablazat-1) / 3;  // 0, 1, 2
            startRow *= 3;  // 0, 3, 6
            for (int i = startRow; i < startRow+3; i++)
            {
                for (int j = startColumn; j < startColumn+3; j++)
                {
                    if (sudoku[i][j] == value && (i != row || j != column))
                    {
                        Console.WriteLine("Az adott résztáblázatban már szerepel a szám");
                        return;
                    }
                }
            }

            Console.WriteLine("A lépés megtehető");
        }

        static int getResztablazat(int row, int column)
        {
            int resztablazat = (int)Math.Ceiling(((double)column + 1.0) / 3.0);  // Melyik rész-oszlopban van a kijelölt rész
            resztablazat = resztablazat + ((int)Math.Ceiling(((double)row + 1.0) / 3.0) - 1) * 3; // + (amelyik rész-sorban van a kijelölt rész) - 1 * 3

            return resztablazat;
        }

        static int readBetween(int a = 1, int b = 9)
        {
            int? x = null;

            do
            {
                x = int.Parse(Console.ReadLine());
                if (x < a || x > b)
                {
                    Console.WriteLine("Hibás adat! Adj meg egy számot {0} és {1} között!", a, b);
                    x = null;
                    Console.Write("Adj meg egy új számot: ");
                }
            } while (x == null);

            return x.Value;
        }
    }
}