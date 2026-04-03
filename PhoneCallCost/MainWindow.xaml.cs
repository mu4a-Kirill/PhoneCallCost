using System;
using System.Windows;

namespace PhoneCallCost
{
    /// <summary>
    /// Главное окно приложения. Обрабатывает ввод данных и вызывает калькулятор.
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PhoneCallCostCalculator _calculator;

        /// <summary>
        /// Конструктор окна. Инициализирует компоненты и создаёт экземпляр калькулятора.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _calculator = new PhoneCallCostCalculator();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Вычислить". Проверяет корректность ввода,
        /// определяет выходной день, вызывает метод расчёта и выводит результат.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие (кнопка).</param>
        /// <param name="e">Параметры события.</param>
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(DurationTextBox.Text, out double duration) || duration <= 0)
            {
                MessageBox.Show("Введите положительное число минут.", "Ошибка ввода");
                return;
            }

            if (!double.TryParse(PricePerMinuteTextBox.Text, out double pricePerMinute) || pricePerMinute <= 0)
            {
                MessageBox.Show("Введите положительную цену за минуту.", "Ошибка ввода");
                return;
            }

            bool isWeekend = (SaturdayRadio.IsChecked == true) || (SundayRadio.IsChecked == true);

            double totalCost = _calculator.CalculateCost(duration, pricePerMinute, isWeekend);

            CostTextBox.Text = totalCost.ToString("F2");
        }
    }
}