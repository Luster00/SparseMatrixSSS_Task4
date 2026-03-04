// Подключение базовых библиотек и компонентов проекта
using System;
using System.Collections.Generic;
using SparseMatrixSSS_Task4.Interfaces;
using SparseMatrixSSS_Task4.Models;
using SparseMatrixSSS_Task4.Core;

namespace SparseMatrixSSS_Task4.Services
{
    // Сервис форматированного вывода матрицы на экран
    // Отвечает только за отображение данных (принцип Single Responsibility)
    public class MatrixOutputService
    {
        // Вывод полной матрицы на экран
        // matrix - матрица для вывода, title - заголовок
        public void PrintMatrix(IMatrix matrix, string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("  " + title + " (" + matrix.Rows + "x" + matrix.Cols + ")");
            Console.WriteLine(new string('=', 60));

            // Вывод заголовка столбцов
            Console.Write("     ");
            for (int j = 0; j < matrix.Cols; j++)
            {
                Console.Write("[" + j.ToString().PadLeft(2) + "]  ");
            }
            Console.WriteLine();

            // Вывод элементов по строкам
            for (int i = 0; i < matrix.Rows; i++)
            {
                Console.Write("[" + i.ToString().PadLeft(2) + "]  ");
                for (int j = 0; j < matrix.Cols; j++)
                {
                    Console.Write(matrix.GetElement(i, j).ToString("F2").PadLeft(8) + "  ");
                }
                Console.WriteLine();
            }

            Console.WriteLine(new string('=', 60));
        }

        // Вывод списка элементов матрицы
        // elements - список элементов, title - заголовок
        public void PrintElements(List<ElementInfo> elements, string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("  " + title + " (всего: " + elements.Count + ") ");
            Console.WriteLine(new string('=', 60));

            if (elements.Count > 0)
            {
                foreach (ElementInfo element in elements)
                {
                    Console.WriteLine("  " + element);
                }
            }
            else
            {
                Console.WriteLine("  Нет элементов для отображения");
            }

            Console.WriteLine(new string('=', 60));
        }

        // Вывод статистики матрицы
        // matrix - матрица для анализа
        public void PrintMatrixStatistics(IMatrix matrix)
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("  Статистика матрицы");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("  Размер: " + matrix.Rows + "x" + matrix.Cols);
            Console.WriteLine("  Всего элементов: " + (matrix.Rows * matrix.Cols));

            // Попытка приведения к SSS для получения дополнительной статистики
            SparseMatrixSSS sss = matrix as SparseMatrixSSS;
            if (sss != null)
            {
                Console.WriteLine("  Ненулевых элементов: " + sss.NonZeroCount);
                Console.WriteLine("  Заполненность: " + sss.FillRatio.ToString("P2"));
            }

            Console.WriteLine(new string('=', 60));
        }
    }
}