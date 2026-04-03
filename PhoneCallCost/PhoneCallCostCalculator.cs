using System;

namespace PhoneCallCost
{
    public class PhoneCallCostCalculator
    {
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