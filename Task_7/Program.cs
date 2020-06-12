using System;

namespace Task_7
{
    class Program
    {
        static int num = 1;            // Номер очередного размещения

        static void Main(string[] args)
        {
            int N, K;

            N = IntChecked("N");
            K = IntChecked("K", N);

            char[] arr = new char[N];  // Массив введенных символов

            // Заполнение массива символами
            for (int i = 0; i < N; i++)
            {
                arr[i] = CharCheck(arr, i);
            }

            Console.WriteLine("\nВведенный вами набор: ");

            // Вывод массива
            foreach (char sym in arr)
            {
                Console.Write(sym + " ");
            }

            Sort(arr);
            Print(arr, K);

            // Вывод каждого найденного размещения
            while (FindSet(ref arr, N, K))
            {
                Print(arr, K);
            }
        }

        // Сортировка массива в лексикографическом порядке
        static public void Sort(char[] arr)
        {
            Console.WriteLine();

            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (Convert.ToInt32(arr[i]) > Convert.ToInt32(arr[j]))
                    {
                        Swap(ref arr, i, j);
                    }
                }
            }
        }

        // Поиск очередного размещения
        static public bool FindSet(ref char[] arr, int N, int K)
        {
            int i, j, x, y;

            // Повторяем пока не будет найдено следующее размещение
            do
            {
                i = N - 1;
                j = N - 2;

                while (j != -1 && Convert.ToInt32(arr[j]) >= Convert.ToInt32(arr[j + 1]))
                {
                    j--;
                }

                // Если размещения отсутствуют, завершаем функцию
                if (j == -1)
                {
                    return false;
                }

                while (Convert.ToInt32(arr[j]) >= Convert.ToInt32(arr[i]))
                {
                    i--;
                }

                Swap(ref arr, i, j);

                x = j + 1;
                y = N - 1;

                // Сортировка оставшейся части последовательности
                while (x < y)
                {
                    Swap(ref arr, x++, y--);
                }
            } while (j > K - 1);

            return true;
        }

        // Обмен значений
        static public void Swap(ref char[] arr, int i, int j)
        {
            char s = arr[i];
            arr[i] = arr[j];
            arr[j] = s;
        }

        // Вывод очередного размещения
        static public void Print(char[] arr, int N)
        {
            Console.Write("\n" + num + ": ");

            num++;

            for (int i = 0; i < N; i++)
            {
                Console.Write(arr[i]);
            }
        }

        // Проверка на ввод целочисленного числа
        static public int IntChecked(string input, int N = 0)
        {
            int foo = 0;
            bool ok;

            Console.Write($"Введите {input}: ");

            do
            {
                if (N == 0 && int.TryParse(Console.ReadLine(), out foo) && foo > 0)
                    ok = true;
                else if (N != 0 && int.TryParse(Console.ReadLine(), out foo) && foo > 0 && foo <= N)
                {
                    ok = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Введите целочисленное число: ");
                    Console.ResetColor();
                    ok = false;
                }
            } while (!ok);

            return foo;
        }

        // Проверка на ввод символа
        static public char CharCheck(char[] arr, int i)
        {
            char foo;
            bool ok, flag;

            Console.Write($"Введите {i + 1} элемент: ");

            do
            {
                flag = false;
                ok = char.TryParse(Console.ReadLine(), out foo);

                if (ok)
                {
                    foreach (char sym in arr)
                    {
                        if (sym == foo)
                        {
                            flag = true;
                        }
                    }
                }

                if (ok && flag)
                {
                    SetColor("Этот символ уже введен! Попробуйте еще раз: ");
                    ok = false;
                }
                else if (ok)
                    ok = true;
                else
                {
                    SetColor("Введите символ: ");
                    ok = false;
                }
            } while (!ok);

            return foo;
        }

        // Печатает заданный текст красным цветом
        static public void SetColor(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}
