// Подключение базовых библиотек
using System;
using System.Collections.Generic;
// Подключение интерфейсов и моделей проекта
using SparseMatrixSSS_Task4.Interfaces;
using SparseMatrixSSS_Task4.Models;
using SparseMatrixSSS_Task4.Exceptions;

namespace SparseMatrixSSS_Task4.Core
{
    // Класс разреженной матрицы в формате SSS
    // Реализует интерфейс IMatrix
    // Хранит только верхнюю треугольную часть + диагональ
    public class SparseMatrixSSS : IMatrix
    {
        // Поля класса - внутренние данные матрицы

        // Количество строк (только чтение)
        private readonly int _rows;

        // Количество столбцов (только чтение)
        private readonly int _cols;

        // Массив для хранения диагональных элементов
        private readonly double[] _diagonal;

        // Двумерный массив словарей для разреженных элементов
        // Хранит только верхнюю треугольную часть
        private readonly Dictionary<int, double>[,] _elements;

        // Счётчик ненулевых элементов матрицы
        private int _nonZeroCount;

        // Свойства для доступа к данным матрицы (только чтение)

        // Количество строк
        public int Rows { get { return _rows; } }

        // Количество столбцов
        public int Cols { get { return _cols; } }

        // Количество ненулевых элементов
        public int NonZeroCount { get { return _nonZeroCount; } }

        // Общее количество элементов (строки * столбцы)
        public int TotalElements { get { return _rows * _cols; } }

        // Коэффициент заполненности (ненулевые / все элементы)
        public double FillRatio { get { return (double)_nonZeroCount / TotalElements; } }

        // Конструктор класса - создание новой матрицы
        // rows - количество строк, cols - количество столбцов
        public SparseMatrixSSS(int rows, int cols)
        {
            // Проверка корректности размеров матрицы
            if (rows <= 0 || cols <= 0)
            {
                // Выбрасываем исключение если размеры некорректны
                throw new InvalidMatrixSizeException(rows, cols);
            }

            // Инициализация полей класса
            _rows = rows;
            _cols = cols;

            // Создание массива для диагонали (минимальный из размеров)
            _diagonal = new double[Math.Min(rows, cols)];

            // Создание двумерного массива словарей для элементов
            _elements = new Dictionary<int, double>[rows, cols];

            // Инициализация счётчика нулём
            _nonZeroCount = 0;
        }

        // Метод установки значения элемента матрицы
        // row - индекс строки, col - индекс столбца, value - значение
        public void SetElement(int row, int col, double value)
        {
            // Проверка корректности индексов
            ValidateIndices(row, col);

            // Проверка: был ли элемент нулевым до изменения
            bool wasZero = Math.Abs(GetElement(row, col)) < 0.0001;

            // Проверка: станет ли элемент нулевым после изменения
            bool isNowZero = Math.Abs(value) < 0.0001;

            // Установка значения в зависимости от позиции элемента
            if (row == col)
            {
                // Диагональный элемент - храним в отдельном массиве
                _diagonal[row] = value;
            }
            else if (col > row)
            {
                // Верхняя треугольная часть - храним напрямую
                if (_elements[row, col] == null)
                {
                    // Создание словаря если ещё не существует
                    _elements[row, col] = new Dictionary<int, double>();
                }
                _elements[row, col][0] = value;
            }
            else
            {
                // Нижняя треугольная часть - храним в симметричной позиции
                if (_elements[col, row] == null)
                {
                    _elements[col, row] = new Dictionary<int, double>();
                }
                _elements[col, row][0] = value;
            }

            // Обновление счётчика ненулевых элементов
            if (wasZero && !isNowZero)
            {
                // Элемент стал ненулевым
                _nonZeroCount++;
            }
            else if (!wasZero && isNowZero)
            {
                // Элемент стал нулевым
                _nonZeroCount--;
            }
        }

        // Метод получения значения элемента матрицы
        // row - индекс строки, col - индекс столбца
        // Возвращает значение элемента (double)
        public double GetElement(int row, int col)
        {
            // Проверка корректности индексов
            ValidateIndices(row, col);

            // Получение значения в зависимости от позиции
            if (row == col)
            {
                // Диагональный элемент
                return _diagonal[row];
            }
            else if (col > row)
            {
                // Верхняя треугольная часть
                if (_elements[row, col] != null &&
                    _elements[row, col].ContainsKey(0))
                {
                    return _elements[row, col][0];
                }
            }
            else
            {
                // Нижняя треугольная часть (симметрия)
                if (_elements[col, row] != null &&
                    _elements[col, row].ContainsKey(0))
                {
                    return _elements[col, row][0];
                }
            }

            // Элемент не найден - возвращаем 0
            return 0.0;
        }

        // Метод получения всех ненулевых элементов матрицы
        // Возвращает список ElementInfo с координатами и значениями
        public List<ElementInfo> GetAllNonZeroElements()
        {
            // Создание списка для результатов
            List<ElementInfo> elements = new List<ElementInfo>();

            // Проход по всем элементам матрицы
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    // Получение значения текущего элемента
                    double value = GetElement(i, j);

                    // Проверка: элемент ненулевой
                    if (Math.Abs(value) > 0.0001)
                    {
                        // Добавление элемента в список
                        elements.Add(new ElementInfo(i, j, value));
                    }
                }
            }

            // Возврат списка ненулевых элементов
            return elements;
        }

        // Метод проверки является ли матрица разреженной
        // threshold - порог разреженности (0.0-1.0)
        // Возвращает true если заполненность меньше порога
        public bool IsSparse(double threshold)
        {
            // Сравнение коэффициента заполненности с порогом
            return FillRatio < threshold;
        }

        // Приватный метод проверки корректности индексов
        // row - индекс строки, col - индекс столбца
        // Выбрасывает исключение если индексы вне диапазона
        private void ValidateIndices(int row, int col)
        {
            // Проверка границ индексов
            if (row < 0 || row >= _rows || col < 0 || col >= _cols)
            {
                // Выбрасывание исключения при выходе за границы
                throw new MatrixIndexOutOfRangeException(row, col, _rows, _cols);
            }
        }
    }
}