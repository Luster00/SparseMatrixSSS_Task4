// Подключение необходимых пространств имён
using SparseMatrixSSS_Task4.Core;
using SparseMatrixSSS_Task4.Exceptions;
using SparseMatrixSSS_Task4.Interfaces;
using SparseMatrixSSS_Task4.Models;
using SparseMatrixSSS_Task4.Services;
using SparseMatrixSSS_Task4.Traversal;
using System;
using System.Collections.Generic;

namespace SparseMatrixSSS_Task4
{
    // Главный класс программы - точка входа
    internal class Program
    {
        // Поля для сервисов ввода и вывода
        private static IUserInput _inputService;
        private static MatrixOutputService _outputService;

        // Точка входа в программу
        private static void Main(string[] args)
        {
            try
            {
                // Инициализация сервисов
                InitializeServices();

                // Запуск основной программы
                RunProgram();
            }
            catch (MatrixException ex)
            {
                // Обработка ошибок матрицы
                HandleMatrixException(ex);
            }
            catch (Exception ex)
            {
                // Обработка остальных ошибок
                HandleGeneralException(ex);
            }
        }

        // Инициализация сервисов ввода и вывода
        private static void InitializeServices()
        {
            // Установка кодировки для поддержки русских символов
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Создание сервиса ввода
            _inputService = new UserInputService();

            // Создание сервиса вывода
            _outputService = new MatrixOutputService();
        }

        // Основной цикл программы
        private static void RunProgram()
        {
            // Вывод заголовка
            PrintHeader();

            // Шаг 1: Ввод размеров матрицы
            int rows = InputRows();
            int cols = InputCols();

            // Шаг 2: Создание матрицы через фабрику
            IMatrix matrix = MatrixFactory.CreateSparseSSS(rows, cols);

            // Шаг 3: Ввод элементов матрицы
            InputMatrixElements(matrix, rows, cols);

            // Шаг 4: Вывод полной матрицы
            _outputService.PrintMatrix(matrix, "Полная матрица");

            // Шаг 5: Вывод статистики
            _outputService.PrintMatrixStatistics(matrix);

            // Шаг 6: Ввод порогового значения
            double threshold = InputThreshold();

            // Шаг 7: Обработка матрицы (обход + фильтрация)
            ProcessMatrix(matrix, threshold);

            // Вывод завершающего сообщения
            PrintFooter();

            // Ожидание нажатия клавиши
            _inputService.WaitForKeyPress();
        }

        // Ввод количества строк матрицы
        private static int InputRows()
        {
            Console.WriteLine();
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("  ШАГ 1: Ввод размеров матрицы");
            Console.WriteLine(new string('-', 60));

            return _inputService.ReadPositiveInt("Введите количество строк: ");
        }

        // Ввод количества столбцов матрицы
        private static int InputCols()
        {
            return _inputService.ReadPositiveInt("Введите количество столбцов: ");
        }

        // Ввод всех элементов матрицы
        private static void InputMatrixElements(IMatrix matrix, int rows, int cols)
        {
            Console.WriteLine();
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("  ШАГ 2: Ввод элементов матрицы");
            Console.WriteLine("  (введите 0 для пропуска элемента)");
            Console.WriteLine(new string('-', 60));

            // Построчный ввод всех элементов
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double value = _inputService.ReadMatrixElement(i, j);
                    matrix.SetElement(i, j, value);
                }
            }
        }

        // Ввод порогового значения для фильтрации
        private static double InputThreshold()
        {
            Console.WriteLine();
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("  ШАГ 3: Ввод порогового значения b");
            Console.WriteLine(new string('-', 60));

            return _inputService.ReadDouble("Введите значение b для фильтрации: ");
        }

        // Обработка матрицы: обход и фильтрация элементов
        private static void ProcessMatrix(IMatrix matrix, double threshold)
        {
            Console.WriteLine();
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("  ШАГ 4: Обработка матрицы");
            Console.WriteLine(new string('-', 60));

            // Создание стратегии обхода (вариант 4)
            IMatrixTraversal traversalStrategy = new TraversalVariant4();

            // Создание сервиса обработки
            MatrixProcessingService processingService = new MatrixProcessingService(
                matrix,
                traversalStrategy);

            // Вывод информации о стратегии
            Console.WriteLine();
            Console.WriteLine(processingService.GetTraversalInfo());
            Console.WriteLine();

            // Визуализация порядка обхода
            TraversalVariant4 variant4 = traversalStrategy as TraversalVariant4;
            if (variant4 != null)
            {
                variant4.PrintTraversalVisualization(matrix);
            }

            // Выполнение обхода с фильтрацией
            List<ElementInfo> filteredElements = processingService.ProcessWithFilter(threshold);

            // Вывод отфильтрованных элементов
            _outputService.PrintElements(
                filteredElements,
                "Элементы больше " + threshold);
        }

        // Вывод заголовка программы
        private static void PrintHeader()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("  Программа работы с разреженной матрицей SSS");
            Console.WriteLine("  Задача 4: Обход по варианту 4");
            Console.WriteLine(new string('=', 60));
        }

        // Вывод завершающего сообщения
        private static void PrintFooter()
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("  Программа завершена успешно");
            Console.WriteLine(new string('=', 60));
        }

        // Обработка исключений матрицы
        private static void HandleMatrixException(MatrixException ex)
        {
            Console.WriteLine();
            Console.WriteLine("[ERROR] Ошибка матрицы: " + ex.Message);

            // Безопасная проверка перед использованием
            if (_inputService != null)
            {
                _inputService.WaitForKeyPress();
            }
        }

        // Обработка общих исключений
        private static void HandleGeneralException(Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine("[ERROR] Критическая ошибка: " + ex.Message);
            Console.WriteLine("Детали: " + ex.StackTrace);

            // Безопасная проверка перед использованием
            if (_inputService != null)
            {
                _inputService.WaitForKeyPress();
            }
        }
    }
}