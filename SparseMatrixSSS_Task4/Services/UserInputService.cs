// Подключение базовых библиотек и интерфейсов
using System;
using SparseMatrixSSS_Task4.Interfaces;

namespace SparseMatrixSSS_Task4.Services
{
    // Сервис ввода данных от пользователя через консоль
    // Реализует интерфейс IUserInput
    public class UserInputService : IUserInput
    {
        // Ввод положительного целого числа
        // prompt - текст приглашения для пользователя
        // Возвращает число > 0 (продолжает запрос пока не введут корректно)
        public int ReadPositiveInt(string prompt)
        {
            int value;
            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                // Проверка: число ли введено и больше ли 0
                if (int.TryParse(input, out value) && value > 0)
                {
                    return value;
                }

                Console.WriteLine("[ERROR] Введите положительное целое число!");
            } while (true);
        }

        // Ввод вещественного числа
        // prompt - текст приглашения для пользователя
        // Возвращает любое числовое значение
        public double ReadDouble(string prompt)
        {
            double value;
            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                // Проверка: число ли введено
                if (double.TryParse(input, out value))
                {
                    return value;
                }

                Console.WriteLine("[ERROR] Введите корректное число!");
            } while (true);
        }

        // Ввод строки текста
        // prompt - текст приглашения для пользователя
        // Возвращает введённую строку или пустую строку если null
        public string ReadLine(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (input == null)
            {
                return string.Empty;
            }
            return input;
        }

        // Ввод элемента матрицы с координатами
        // row - строка, col - столбец
        // Форматирует приглашение как "S[row, col] = "
        public double ReadMatrixElement(int row, int col)
        {
            return ReadDouble("S[" + row + ", " + col + "] = ");
        }

        // Ожидание нажатия любой клавиши
        // Используется перед завершением программы
        public void WaitForKeyPress()
        {
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}