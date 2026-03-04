// Подключение интерфейса IMatrix для работы с матрицами
using SparseMatrixSSS_Task4.Interfaces;

namespace SparseMatrixSSS_Task4.Core
{
    // Фабричный класс для создания объектов матриц
    // Статический - не требует создания экземпляра
    public static class MatrixFactory
    {
        // Метод создания разреженной матрицы в формате SSS
        // rows - количество строк, cols - количество столбцов
        // Возвращает интерфейс IMatrix (абстракция)
        public static IMatrix CreateSparseSSS(int rows, int cols)
        {
            // Создание и возврат нового экземпляра матрицы
            // Проверка размеров выполняется в конструкторе SparseMatrixSSS
            return new SparseMatrixSSS(rows, cols);
        }
    }
}