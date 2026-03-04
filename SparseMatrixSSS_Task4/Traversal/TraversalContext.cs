// Подключение базовых библиотек и интерфейсов
using System;
using System.Collections.Generic;
using SparseMatrixSSS_Task4.Interfaces;
using SparseMatrixSSS_Task4.Models;

namespace SparseMatrixSSS_Task4.Traversal
{
    // Контекст для управления стратегиями обхода матрицы
    // Паттерн: Context (из Strategy Pattern)
    public class TraversalContext
    {
        // Текущая стратегия обхода
        private IMatrixTraversal _traversalStrategy;

        // Свойство для доступа к текущей стратегии
        public IMatrixTraversal CurrentStrategy
        {
            get { return _traversalStrategy; }
            set
            {
                // Проверка на null
                if (value == null)
                    throw new ArgumentNullException("value");
                _traversalStrategy = value;
            }
        }

        // Конструктор - создание контекста с начальной стратегией
        // initialStrategy - стратегия обхода для использования
        public TraversalContext(IMatrixTraversal initialStrategy)
        {
            // Проверка на null
            if (initialStrategy == null)
                throw new ArgumentNullException("initialStrategy");
            _traversalStrategy = initialStrategy;
        }

        // Выполнить полный обход матрицы
        // matrix - матрица для обхода
        public List<ElementInfo> ExecuteTraversal(IMatrix matrix)
        {
            return _traversalStrategy.Traverse(matrix);
        }

        // Выполнить обход матрицы с фильтрацией
        // matrix - матрица, threshold - пороговое значение
        public List<ElementInfo> ExecuteTraversalWithFilter(IMatrix matrix, double threshold)
        {
            return _traversalStrategy.TraverseWithFilter(matrix, threshold);
        }

        // Изменить стратегию обхода
        // newStrategy - новая стратегия для использования
        public void ChangeStrategy(IMatrixTraversal newStrategy)
        {
            if (newStrategy == null)
                throw new ArgumentNullException("newStrategy");
            _traversalStrategy = newStrategy;
        }

        // Получить информацию о текущей стратегии
        // Возвращает название и описание стратегии
        public string GetStrategyInfo()
        {
            return "Стратегия: " + _traversalStrategy.VariantName + "\n" +
                   "Описание: " + _traversalStrategy.Description;
        }
    }
}