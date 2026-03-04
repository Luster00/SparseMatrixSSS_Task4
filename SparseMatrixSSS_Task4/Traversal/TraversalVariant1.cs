// Подключение базовых библиотек и интерфейсов
using SparseMatrixSSS_Task4.Interfaces;
using SparseMatrixSSS_Task4.Models;
using System.Collections.Generic;
using System;

namespace SparseMatrixSSS_Task4.Traversal
{
    // Стратегия обхода матрицы по варианту 1 (альтернативный)
    // Зигзагообразный обход: нечётные строки слева-направо, чётные справа-налево
    public class TraversalVariant1 : IMatrixTraversal
    {
        // Название варианта обхода
        public string VariantName => "Вариант 1";

        // Описание алгоритма обхода
        public string Description =>
            "Зигзагообразный обход (альтернативный)";

        // Полный обход матрицы
        // matrix - матрица для обхода
        public List<ElementInfo> Traverse(IMatrix matrix)
        {
            var result = new List<ElementInfo>();

            // Проход по всем строкам матрицы
            for (int i = 0; i < matrix.Rows; i++)
            {
                if (i % 2 != 0)
                {
                    // Нечётная строка - обход слева направо
                    for (int j = 0; j < matrix.Cols; j++)
                    {
                        result.Add(new ElementInfo(i, j, matrix.GetElement(i, j)));
                    }
                }
                else
                {
                    // Чётная строка - обход справа налево
                    for (int j = matrix.Cols - 1; j >= 0; j--)
                    {
                        result.Add(new ElementInfo(i, j, matrix.GetElement(i, j)));
                    }
                }
            }

            return result;
        }

        // Обход матрицы с фильтрацией по значению
        // matrix - матрица, threshold - пороговое значение
        public List<ElementInfo> TraverseWithFilter(IMatrix matrix, double threshold)
        {
            var result = new List<ElementInfo>();

            // Проход по всем строкам матрицы
            for (int i = 0; i < matrix.Rows; i++)
            {
                if (i % 2 != 0)
                {
                    // Нечётная строка - обход слева направо
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
                    // Чётная строка - обход справа налево
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
            var result = new List<MatrixCoordinates>();

            // Проход по всем строкам матрицы
            for (int i = 0; i < matrix.Rows; i++)
            {
                if (i % 2 != 0)
                {
                    // Нечётная строка - слева направо
                    for (int j = 0; j < matrix.Cols; j++)
                    {
                        result.Add(new MatrixCoordinates(i, j));
                    }
                }
                else
                {
                    // Чётная строка - справа налево
                    for (int j = matrix.Cols - 1; j >= 0; j--)
                    {
                        result.Add(new MatrixCoordinates(i, j));
                    }
                }
            }

            return result;
        }
    }
}