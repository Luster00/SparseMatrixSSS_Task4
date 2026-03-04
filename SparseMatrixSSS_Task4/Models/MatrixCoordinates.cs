using System;

namespace SparseMatrixSSS_Task4.Models
{
    // Класс для хранения координат элемента матрицы
    public class MatrixCoordinates
    {
        // Индекс строки (только чтение)
        public int Row { get; }

        // Индекс столбца (только чтение)
        public int Col { get; }

        // Конструктор - создание координат
        // row - строка, col - столбец
        public MatrixCoordinates(int row, int col)
        {
            Row = row;
            Col = col;
        }

        // Проверка корректности координат для матрицы заданного размера
        // rows - количество строк, cols - количество столбцов
        public bool IsValid(int rows, int cols)
        {
            return Row >= 0 && Row < rows && Col >= 0 && Col < cols;
        }

        // Преобразование координат в строку
        public override string ToString()
        {
            return "(" + Row + ", " + Col + ")";
        }

        // Проверка равенства координат
        public override bool Equals(object obj)
        {
            MatrixCoordinates other = obj as MatrixCoordinates;
            if (other == null)
            {
                return false;
            }
            return Row == other.Row && Col == other.Col;
        }

        // Получение хэш-кода координат
        public override int GetHashCode()
        {
            return Row.GetHashCode() ^ Col.GetHashCode();
        }
    }
}