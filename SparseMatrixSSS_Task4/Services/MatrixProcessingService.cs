// Подключение базовых библиотек и компонентов проекта
using System;
using System.Collections.Generic;
using SparseMatrixSSS_Task4.Interfaces;
using SparseMatrixSSS_Task4.Models;
using SparseMatrixSSS_Task4.Traversal;

namespace SparseMatrixSSS_Task4.Services
{
    // Сервис обработки матрицы
    // Координирует обход матрицы с выбранной стратегией
    public class MatrixProcessingService
    {
        // Поля класса
        private readonly IMatrix _matrix;
        private readonly TraversalContext _traversalContext;

        // Конструктор сервиса
        // matrix - матрица для обработки, traversalStrategy - стратегия обхода
        public MatrixProcessingService(IMatrix matrix, IMatrixTraversal traversalStrategy)
        {
            // Проверка на null
            if (matrix == null)
                throw new ArgumentNullException("matrix");
            _matrix = matrix;
            _traversalContext = new TraversalContext(traversalStrategy);
        }

        // Обработка матрицы с фильтрацией по значению
        // threshold - пороговое значение (элементы > threshold)
        // Возвращает список отфильтрованных элементов
        public List<ElementInfo> ProcessWithFilter(double threshold)
        {
            return _traversalContext.ExecuteTraversalWithFilter(_matrix, threshold);
        }

        // Получение информации о текущей стратегии обхода
        // Возвращает название и описание стратегии
        public string GetTraversalInfo()
        {
            return _traversalContext.GetStrategyInfo();
        }
    }
}