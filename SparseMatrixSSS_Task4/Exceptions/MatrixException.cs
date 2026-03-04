// Подключение базового пространства имён для работы с исключениями
using System;

namespace SparseMatrixSSS_Task4.Exceptions
{
    // Базовый класс для всех исключений работы с матрицей
    // Наследуется от стандартного Exception
    // Используется для группировки специфичных ошибок матрицы
    public class MatrixException : Exception
    {
        // Конструктор без параметров
        // Вызывает базовый конструктор Exception
        public MatrixException() : base() { }

        // Конструктор с сообщением об ошибке
        // message - текст сообщения об ошибке
        public MatrixException(string message) : base(message) { }

        // Конструктор с сообщением и внутренним исключением
        // message - текст сообщения об ошибке
        // innerException - оригинальное исключение (для отладки)
        public MatrixException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}