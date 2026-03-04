using System;

namespace SparseMatrixSSS_Task4.Models
{
    // Класс для хранения информации об элементе матрицы
    public class ElementInfo
    {
        // Индекс строки (только чтение)
        public int Row { get; private set; }

        // Индекс столбца (только чтение)
        public int Col { get; private set; }

        // Значение элемента (только чтение)
        public double Value { get; private set; }

        // Конструктор - создание элемента
        // row - строка, col - столбец, value - значение
        public ElementInfo(int row, int col, double value)
        {
            Row = row;
            Col = col;
            Value = value;
        }

        // Преобразование элемента в строку (формат: "S[row,col] = value")
        public override string ToString()
        {
            return "S[" + Row + ", " + Col + "] = " + Value.ToString("F2");
        }

        // Проверка равенства элементов (по координатам и значению)
        public override bool Equals(object obj)
        {
            ElementInfo other = obj as ElementInfo;
            if (other == null)
            {
                return false;
            }
            return Row == other.Row &&
                   Col == other.Col &&
                   Math.Abs(Value - other.Value) < 0.0001;
        }

        // Получение хэш-кода элемента
        public override int GetHashCode()
        {
            return Row.GetHashCode() ^ Col.GetHashCode() ^ Value.GetHashCode();
        }
    }
}