// Подключение базовых библиотек и интерфейсов
using System;
using System.Collections.Generic;
using SparseMatrixSSS_Task4.Interfaces;
using SparseMatrixSSS_Task4.Models;

namespace SparseMatrixSSS_Task4.Traversal
{
    // Стратегия обхода матрицы по варианту 4
    // Зигзагообразный обход: чётные строки слева-направо, нечётные справа-налево
    public class TraversalVariant4 : IMatrixTraversal
    {
        // Название варианта обхода
        public string VariantName { get { return "Вариант 4"; } }

        // Описание алгоритма обхода
        public string Description
        {
            get { return "Зигзагообразный обход: чётные строки слева направо, нечётные справа налево"; }
        }

        // Полный обход матрицы
        // matrix - матрица для обхода
        public List<ElementInfo> Traverse(IMatrix matrix)
        {
            List<ElementInfo> result = new List<ElementInfo>();

            // Проход по всем строкам матрицы
            for (int i = 0; i < matrix.Rows; i++)
            {
                if (i % 2 == 0)
                {
                    // Чётная строка - обход слева направо
                    for (int j = 0; j < matrix.Cols; j++)
                    {
                        double value = matrix.GetElement(i, j);
                        result.Add(new ElementInfo(i, j, value));
                    }
                }
                else
                {
                    // Нечётная строка - обход справа налево
                    for (int j = matrix.Cols - 1; j >= 0; j--)
                    {
                        double value = matrix.GetElement(i, j);
                        result.Add(new ElementInfo(i, j, value));
                    }
                }
            }

            return result;
        }

        // Обход матрицы с фильтрацией по значению
        // matrix - матрица, threshold - пороговое значение (элементы > threshold)
        public List<ElementInfo> TraverseWithFilter(IMatrix matrix, double threshold)
        {
            List<ElementInfo> result = new List<ElementInfo>();

            // Проход по всем строкам матрицы
            for (int i = 0; i < matrix.Rows; i++)
            {
                if (i % 2 == 0)
                {
                    // Чётная строка - обход слева направо
                    for (int j = 0; j < matrix.Cols; j++)
                    {
                        double value = matrix.GetElement(i, j);
                        // Фильтрация: только элементы больше порога
                        if (value > threshold)
                        {
                            result.Add(new ElementInfo(i, j, value));
                        }
                    }
                }
                else
                {
                    // Нечётная строка - обход справа налево
                    for (int j = matrix.Cols - 1; j >= 0; j--)
                    {
                        double value = matrix.GetElement(i, j);
                        // Фильтрация: только элементы больше порога
                        if (value > threshold)
                        {
                            result.Add(new ElementInfo(i, j, value));
                        }
                    }
                }
            }

            return result;
        }

        // Получить порядок обхода (координаты без значений)
        // matrix - матрица для получения порядка
        public List<MatrixCoordinates> GetTraversalOrder(IMatrix matrix)
        {
            List<MatrixCoordinates> result = new List<MatrixCoordinates>();

            // Проход по всем строкам матрицы
            for (int i = 0; i < matrix.Rows; i++)
            {
                if (i % 2 == 0)
                {
                    // Чётная строка - слева направо
                    for (int j = 0; j < matrix.Cols; j++)
                    {
                        result.Add(new MatrixCoordinates(i, j));
                    }
                }
                else
                {
                    // Нечётная строка - справа налево
                    for (int j = matrix.Cols - 1; j >= 0; j--)
                    {
                        result.Add(new MatrixCoordinates(i, j));
                    }
                }
            }

            return result;
        }

        // Вывод визуализации порядка обхода
        // matrix - матрица для визуализации
        public void PrintTraversalVisualization(IMatrix matrix)
        {
            Console.WriteLine();
            Console.WriteLine("=== Визуализация обхода (" + VariantName + ") ===");
            Console.WriteLine();

            // Получение порядка обхода
            List<MatrixCoordinates> order = GetTraversalOrder(matrix);

            // Вывод номеров шагов обхода для каждой позиции
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Cols; j++)
                {
                    for (int k = 0; k < order.Count; k++)
                    {
                        if (order[k].Row == i && order[k].Col == j)
                        {
                            Console.Write((k + 1).ToString().PadLeft(4) + "  ");
                            break;
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}