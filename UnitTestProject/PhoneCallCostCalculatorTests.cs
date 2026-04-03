using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneCallCost;

namespace PhoneCallCost.Tests
{
    [TestClass]
    public class PhoneCallCostCalculatorTests
    {
        private PhoneCallCostCalculator _calculator;

        [TestInitialize]
        public void Setup()
        {
            _calculator = new PhoneCallCostCalculator();
        }

        [TestMethod]
        public void CalculateCost_Duration30_Weekday_NoDiscount()
        {
            double result = _calculator.CalculateCost(30, 2, false);
            Assert.AreEqual(60.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateCost_Duration15_Weekday_NoDiscount()
        {
            double result = _calculator.CalculateCost(15, 3.5, false);
            Assert.AreEqual(52.5, result, 0.001);
        }

        [TestMethod]
        public void CalculateCost_Duration1_Weekday_NoDiscount()
        {
            double result = _calculator.CalculateCost(1, 10, false);
            Assert.AreEqual(10.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateCost_Duration31_Weekday_DiscountAfter30()
        {
            double result = _calculator.CalculateCost(31, 2, false);
            // 30*2 + 1*2*0.7 = 60 + 1.4 = 61.4
            Assert.AreEqual(61.4, result, 0.001);
        }

        [TestMethod]
        public void CalculateCost_Duration45_Weekday_DiscountAfter30()
        {
            double result = _calculator.CalculateCost(45, 2, false);
            // 30*2 + 15*1.4 = 60 + 21 = 81
            Assert.AreEqual(81.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateCost_Duration100_Weekday_DiscountAfter30()
        {
            double result = _calculator.CalculateCost(100, 1, false);
            // 30*1 + 70*0.7 = 30 + 49 = 79
            Assert.AreEqual(79.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateCost_Duration30_Weekend_Discount15Percent()
        {
            double result = _calculator.CalculateCost(30, 2, true);
            // 60 * 0.85 = 51
            Assert.AreEqual(51.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateCost_Duration15_Weekend_Discount15Percent()
        {
            double result = _calculator.CalculateCost(15, 4, true);
            // 60 * 0.85 = 51
            Assert.AreEqual(51.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateCost_Duration31_Weekend_CombinedDiscount()
        {
            double result = _calculator.CalculateCost(31, 2, true);
            // (30*2 + 1*1.4) * 0.85 = (60+1.4)*0.85 = 61.4*0.85 = 52.19
            Assert.AreEqual(52.19, result, 0.001);
        }

        [TestMethod]
        public void CalculateCost_Duration45_Weekend_CombinedDiscount()
        {
            double result = _calculator.CalculateCost(45, 2, true);
            // (60+21)*0.85 = 81*0.85 = 68.85
            Assert.AreEqual(68.85, result, 0.001);
        }

        [TestMethod]
        public void CalculateCost_Duration50_Weekend_CombinedDiscount()
        {
            double result = _calculator.CalculateCost(50, 4, true);
            // (30*4 + 20*2.8) * 0.85 = (120+56)*0.85 = 176*0.85 = 149.6
            Assert.AreEqual(149.6, result, 0.001);
        }

        [TestMethod]
        public void CalculateCost_PriceWithDecimal_Weekday()
        {
            double result = _calculator.CalculateCost(25, 1.99, false);
            Assert.AreEqual(49.75, result, 0.001);
        }

        [TestMethod]
        public void CalculateCost_PriceWithDecimal_Weekend()
        {
            double result = _calculator.CalculateCost(25, 1.99, true);
            Assert.AreEqual(42.2875, result, 0.001);
        }

        [TestMethod]
        public void CalculateCost_VeryLargeDuration_Weekday()
        {
            double result = _calculator.CalculateCost(1000, 0.5, false);
            // 30*0.5 + 970*0.35 = 15 + 339.5 = 354.5
            Assert.AreEqual(354.5, result, 0.001);
        }

        [TestMethod]
        public void CalculateCost_VeryLargeDuration_Weekend()
        {
            double result = _calculator.CalculateCost(1000, 0.5, true);
            // 354.5 * 0.85 = 301.325
            Assert.AreEqual(301.325, result, 0.001);
        }

        [TestMethod]
        public void CalculateCost_Exactly30Minutes_Weekend()
        {
            double result = _calculator.CalculateCost(30, 3, true);
            Assert.AreEqual(76.5, result, 0.001); // 90 * 0.85 = 76.5
        }

        [TestMethod]
        public void CalculateCost_Exactly30Minutes_Weekday()
        {
            double result = _calculator.CalculateCost(30, 3, false);
            Assert.AreEqual(90.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateCost_Exactly31Minutes_Weekday()
        {
            double result = _calculator.CalculateCost(31, 3, false);
            // 30*3 + 1*2.1 = 90 + 2.1 = 92.1
            Assert.AreEqual(92.1, result, 0.001);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Длительность должна быть положительной.")]
        public void CalculateCost_ZeroDuration_ThrowsException()
        {
            _calculator.CalculateCost(0, 5, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Длительность должна быть положительной.")]
        public void CalculateCost_NegativeDuration_ThrowsException()
        {
            _calculator.CalculateCost(-10, 5, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Цена за минуту должна быть положительной.")]
        public void CalculateCost_ZeroPrice_ThrowsException()
        {
            _calculator.CalculateCost(10, 0, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Цена за минуту должна быть положительной.")]
        public void CalculateCost_NegativePrice_ThrowsException()
        {
            _calculator.CalculateCost(10, -2.5, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateCost_NegativeDurationAndPrice_ThrowsException()
        {
            _calculator.CalculateCost(-5, -3, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateCost_ZeroDurationAndZeroPrice_ThrowsException()
        {
            _calculator.CalculateCost(0, 0, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateCost_ZeroDurationWeekend_ThrowsException()
        {
            _calculator.CalculateCost(0, 10, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateCost_NegativeDurationWeekend_ThrowsException()
        {
            _calculator.CalculateCost(-1, 10, true);
        }
    }
}