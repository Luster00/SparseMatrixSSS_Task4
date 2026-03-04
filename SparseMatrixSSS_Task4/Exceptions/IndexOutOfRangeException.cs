// Подключение базового пространства имён для работы с исключениями
using System;

namespace SparseMatrixSSS_Task4.Exceptions
{
    // Исключение выхода за границы индексов матрицы
    // Наследуется от MatrixException (базовое исключение матрицы)
    // Выбрасывается когда индексы row/col вне допустимого диапазона
    public class MatrixIndexOutOfRangeException : MatrixException
    {
        // Конструктор с пользовательским сообщением
        // message - текст сообщения об ошибке
        public MatrixIndexOutOfRangeException(string message) : base(message) { }

        // Конструктор с координатами и границами матрицы
        // row - индекс строки (некорректный)
        // col - индекс столбца (некорректный)
        // maxRow - максимальное количество строк
        // maxCol - максимальное количество столбцов
        // Автоматически формирует сообщение с допустимыми границами
        public MatrixIndexOutOfRangeException(int row, int col, int maxRow, int maxCol)
            : base("Индексы вне диапазона: [" + row + "," + col + "], " +
                   "допустимые: [0-" + (maxRow - 1) + ", 0-" + (maxCol - 1) + "]")
        { }
    }
}