namespace SparseMatrixSSS_Task4.Interfaces
{
    // Интерфейс ввода данных от пользователя
    public interface IUserInput
    {
        // Ввод положительного целого числа
        // prompt - текст приглашения
        int ReadPositiveInt(string prompt);

        // Ввод вещественного числа
        // prompt - текст приглашения
        double ReadDouble(string prompt);

        // Ввод строки
        // prompt - текст приглашения
        string ReadLine(string prompt);

        // Ввод элемента матрицы с координатами
        // row - строка, col - столбец
        double ReadMatrixElement(int row, int col);

        // Ожидание нажатия клавиши
        void WaitForKeyPress();
    }
}