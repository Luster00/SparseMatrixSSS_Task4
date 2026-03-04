// Подключение коллекций и моделей
using System.Collections.Generic;
using SparseMatrixSSS_Task4.Models;

namespace SparseMatrixSSS_Task4.Interfaces
{
    // Интерфейс стратегии обхода матрицы
    public interface IMatrixTraversal
    {
        // Название варианта обхода (например, "Вариант 4")
        string VariantName { get; }

        // Описание алгоритма обхода
        string Description { get; }

        // Полный обход матрицы
        // matrix - матрица для обхода
        List<ElementInfo> Traverse(IMatrix matrix);

        // Обход с фильтрацией (элементы > threshold)
        // matrix - матрица, threshold - пороговое значение
        List<ElementInfo> TraverseWithFilter(IMatrix matrix, double threshold);

        // Порядок обхода (координаты без значений)
        // matrix - матрица для получения порядка
        List<MatrixCoordinates> GetTraversalOrder(IMatrix matrix);
    }
}