// Подключение коллекций и моделей
using System.Collections.Generic;
using SparseMatrixSSS_Task4.Models;

namespace SparseMatrixSSS_Task4.Interfaces
{
    // Интерфейс операций с матрицей
    public interface IMatrix
    {
        // Количество строк (только чтение)
        int Rows { get; }

        // Количество столбцов (только чтение)
        int Cols { get; }

        // Установить элемент матрицы
        // row - строка, col - столбец, value - значение
        void SetElement(int row, int col, double value);

        // Получить элемент матрицы
        // row - строка, col - столбец
        double GetElement(int row, int col);

        // Получить все ненулевые элементы
        List<ElementInfo> GetAllNonZeroElements();

        // Проверка на разреженность
        // threshold - порог заполненности (0.0-1.0)
        bool IsSparse(double threshold);
    }
}