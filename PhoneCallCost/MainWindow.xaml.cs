using System;
using System.Windows;

namespace PhoneCallCost
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

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

            double totalCost = 0;

            if (duration <= 30)
            {
                totalCost = duration * pricePerMinute;
            }
            else
            {
                totalCost = 30 * pricePerMinute + (duration - 30) * pricePerMinute * 0.7;
            }

            bool isWeekend = (SaturdayRadio.IsChecked == true) || (SundayRadio.IsChecked == true);
            if (isWeekend)
            {
                totalCost *= 0.85;
            }

            CostTextBox.Text = totalCost.ToString("F2");
        }
    }
}