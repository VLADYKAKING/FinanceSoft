using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
/// <summary>
/// Выполнил: Кислов Влад    vadalize.vi@gmail.com
/// 
/// 
/// Все задания выполенены в одном консольном приложении. Реализована небольшая навигация по задачам.
/// Не было понятно являются ли стандартные библиотеки
///                                                  using System.Collections.Generic;
///                                                  using System.Linq;
///                                                  using System.Text;
///                                                  using System.Threading.Tasks;
/// "сторонними библиотеками". Поэтому реализовал решения несколькими способами, с ними и без них:
/// 1 задание реализовано 1 способом
/// 2 задание реализовано 2 способами
/// 3 задание реализовано 3 способами
/// 
/// Краткие пояснения к решениям:
/// 1-1  читаем строки по циклу, с конца
/// 
/// 2-1  очищаем строки от ненужных символов -> разворачиваем -> сравниваем -> выводим ответ
/// 
/// 2-2  так же как предыдущее, но очистка с помощью регулярного выражения
/// 
/// 3-1  создаем int[] размером 1200 -> ascii код символа = индекс; значение по индексу = колличество -> 
/// считаем и заносим значения в массив -> выводим
/// 
/// 3-2  очищаем строку от повтряющихся символов с помощью метода Distinct() -> 
/// ищем элементы этой строки в введенной строке -> выводим
/// 
/// 3-3  записываем в Dictionary<char, int> символы и их колличество -> выводим
/// </summary>
namespace FinanceSoft_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int iter = 0;
            while (true)//навигация
            {
                iter++;
                //Чтобы посмотреть результат других реализаций, нужно поменять эти флаги.
                //f2 и f3 для задач 2 и 3 соответственно
                int f2 = 1;//f2 = {1,2}
                int f3 = 1;//f3 = {1,2,3}
                Console.WriteLine(new string('-', 50));
                Console.WriteLine(iter + ")Выберите задачу для проверки, нажав клавишу от 0 до 3");
                Console.WriteLine("\t0: выход\n\t1,2,3: задачи");
                var regime = Console.ReadKey();

                if (regime.KeyChar == '0')//выход
                {
                    break;
                }

                else if (regime.KeyChar == '1')//задача 1 (без доп.библиотек)
                {
                    Console.WriteLine("\nВведите строку для разворота");
                    string input = Console.ReadLine();
                    Console.WriteLine(Reverse(input).ToLower());
                }

                else if (regime.KeyChar == '2')
                {
                    if (f2 == 1)//задача 2 (без доп.библиотек)
                    {
                        Console.WriteLine("\nПроверка на палиндром (введите строку)");
                        string input = Console.ReadLine().ToLower();
                        char[] forbiddenSymbols = { ' ', '.', ',', '-', '_', '"', '!', '?', 'ь', 'ъ' };
                        string clearedString = CleanString(input, forbiddenSymbols);//очистка
                        if (clearedString == Reverse(clearedString))//сравнение
                            Console.WriteLine("Да,это - палиндром.");
                        else
                            Console.WriteLine("Нет,это - не палиндром.");
                    }
                    else if (f2 == 2)//задача 2 (использовал System.Text.RegularExpressions)
                    {
                        Console.WriteLine("\nПроверка на палиндром (введите строку)");
                        string input = Console.ReadLine().ToLower();
                        Regex regex1 = new Regex(@"\W"); /*или*/ Regex regex2 = new Regex(@"[-!#$%&'()* ,./:;<=>?@[\]_`{|}~ъь]");
                        string clearedString = regex2.Replace(input, "");//очистка
                        if (clearedString == Reverse(clearedString))//сравнение
                            Console.WriteLine("Да,это - палиндром.");
                        else
                            Console.WriteLine("Нет,это - не палиндром.");
                    }
                    else Console.WriteLine("Выставлен не верный флаг. Флаг f2 может быть 1,2");
                }

                else if (regime.KeyChar == '3')
                {
                    if (f3 == 1)//задача 3 (без доп.библиотек)
                    {
                        Console.WriteLine("\nВведите строку для подсчета символов");
                        string input = Console.ReadLine().ToLower();
                        int[] result = new int[1200];
                        string shortened = "";//строка без повторяющихся символов
                        for (int i = 0; i < input.Length; i++)//считаем колличество символов
                        {
                            if (result[(int)input[i]] == 0)
                                shortened += input[i];
                            result[(int)input[i]]++;
                        }
                        for (int i = 0; i < shortened.Length; i++)//выводим
                            Console.WriteLine($"{shortened[i]}:{result[(int)shortened[i]]}");
                    }
                    else if (f3 == 2)//задача 3 (использовал System.Linq)
                    {
                        Console.WriteLine("\nВведите строку для подсчета символов");
                        string input = Console.ReadLine().ToLower();
                        string shortened = new string(input.Distinct().ToArray());
                        int k = 0;
                        for (int i = 0; i < shortened.Length; i++)//подсчет и вывод
                        {
                            for (int j = 0; j < input.Length; j++)
                            {
                                if (shortened[i] == input[j])
                                    k++;
                            }
                            Console.WriteLine($"{shortened[i]}:{k}");
                            k = 0;
                        }
                    }
                    else if (f3 == 3)//задача 3 (использовал System.Collections.Generic)
                    {
                        Console.WriteLine("\nВведите строку для подсчета символов");
                        string input = Console.ReadLine().ToLower();
                        var result = new Dictionary<char, int>();
                        for (int i = 0; i < input.Length; i++)//подсчет
                        {
                            if (result.ContainsKey(input[i]))
                                result[input[i]]++;
                            else
                                result.Add(input[i], 1);
                        }
                        foreach (var el in result)//вывод
                        {
                            Console.WriteLine($"{el.Key}:{el.Value}");
                        }
                    }
                    else Console.WriteLine("\nВыставлен неверный флаг. Флаг f3 может быть 1,2,3");
                }
                else Console.WriteLine("\nВыбрана неверная функция.");
            }
        }

        //методы
        public static string Reverse(string input)//разворот строки
        {
            string result = "";
            for (int i = input.Length - 1; i >= 0; i--)
                result += input[i];
            return result;
        }
        public static string CleanString(string input, char[] forbiddenSymbols)//очистка строки от ненужных символов
        {
            string clearedString = "";
            bool flag = false;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < forbiddenSymbols.Length; j++)
                {
                    if (input[i] == forbiddenSymbols[j])
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    clearedString += input[i];
                else
                    flag = false;
            }
            return clearedString;
        }
        public static void ASCIIcheck()//для проверки кодов (не часть задания)
        {
            string str = @"№[-!#$%&'()* ,./:;<=>?@[\]_`{|}~ъь]абвгдийклмнопрстуфхцшщъыьэюяabcdefghijklmnopqrstuvwxyz";
            char[] c = str.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                Console.WriteLine($"{str[i]} {(int)c[i]}");
            }
        }
    }
}
