// Подключение базового пространства имён для работы с исключениями
using System;

namespace SparseMatrixSSS_Task4.Exceptions
{
    // Исключение некорректного размера матрицы
    // Наследуется от MatrixException (базовое исключение матрицы)
    // Выбрасывается когда размеры матрицы <= 0
    public class InvalidMatrixSizeException : MatrixException
    {
        // Конструктор с пользовательским сообщением
        // message - текст сообщения об ошибке
        public InvalidMatrixSizeException(string message) : base(message) { }

        // Конструктор с размерами матрицы
        // rows - количество строк (некорректное)
        // cols - количество столбцов (некорректное)
        // Автоматически формирует сообщение об ошибке
        public InvalidMatrixSizeException(int rows, int cols)
            : base("Некорректный размер матрицы: " + rows + "x" + cols + ". " +
                   "Размеры должны быть положительными.")
        { }
    }
}