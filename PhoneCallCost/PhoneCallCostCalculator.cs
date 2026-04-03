using System;

namespace PhoneCallCost
{
    /// <summary>
    /// Класс для расчёта стоимости телефонного разговора с учётом скидок.
    /// </summary>
    public class PhoneCallCostCalculator
    {
        /// <summary>
        /// Вычисляет итоговую стоимость разговора.
        /// </summary>
        /// <param name="duration">Длительность разговора в минутах (положительное число).</param>
        /// <param name="pricePerMinute">Цена одной минуты (положительное число).</param>
        /// <param name="isWeekend">Флаг выходного дня (true – суббота или воскресенье).</param>
        /// <returns>Итоговая стоимость с учётом применённых скидок.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если duration или pricePerMinute ≤ 0.</exception>
        public double CalculateCost(double duration, double pricePerMinute, bool isWeekend)
        {
            if (duration <= 0)
                throw new ArgumentException("Длительность должна быть положительной.", nameof(duration));
            if (pricePerMinute <= 0)
                throw new ArgumentException("Цена за минуту должна быть положительной.", nameof(pricePerMinute));

            double totalCost;

            if (duration <= 30)
            {
                totalCost = duration * pricePerMinute;
            }
            else
            {
                totalCost = 30 * pricePerMinute + (duration - 30) * pricePerMinute * 0.7;
            }

            if (isWeekend)
            {
                totalCost *= 0.85;
            }

            return totalCost;
        }
    }
}