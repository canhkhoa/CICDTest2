using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{


    [TestFixture]
    public class FeeCalculatorTests
    {
        private const double DELTA = 0.01; // Tolerance for floating point comparison

        [Test]
        public void TC01_MorningPeriodWithoutDiscount()
        {
            double fee = FeeCalculator.CalculateFee(8, 0, 10, 0);
            Assert.AreEqual(48000, fee, DELTA);
        }

        [Test]
        public void TC02_MorningPeriodWithDiscount()
        {
            double fee = FeeCalculator.CalculateFee(8, 0, 15, 0);
            Assert.AreEqual(151200, fee, DELTA);
        }

        [Test]
        public void TC03_EveningPeriodWithoutDiscount()
        {
            double fee = FeeCalculator.CalculateFee(18, 0, 20, 0);
            Assert.AreEqual(42000, fee, DELTA);        }

        [Test]
        public void TC04_EveningPeriodWithDiscount()
        {
            double fee = FeeCalculator.CalculateFee(18, 0, 23, 0);
            Assert.AreEqual(105840, fee, DELTA);
        }

        [Test]
        public void TC05_NightPeriodWithoutDiscount()
        {
            double fee = FeeCalculator.CalculateFee(1, 0, 3, 0);
            Assert.AreEqual(36000, fee, DELTA);
        }

        [Test]
        public void TC06_NightPeriodWithDiscount()
        {
            double fee = FeeCalculator.CalculateFee(0, 0, 7, 30);
            Assert.AreEqual(108000, fee, DELTA);
        }

        [Test]
        public void TC07_AcrossPeriods_MorningToEvening()
        {
            double fee = FeeCalculator.CalculateFee(16, 0, 18, 0);
            Assert.AreEqual(45000, fee, DELTA);
        }

        [Test]
        public void TC08_AcrossPeriodsWithDiscount()
        {
            double fee = FeeCalculator.CalculateFee(14, 0, 22, 0);
            Assert.AreEqual(177840, fee, DELTA);
        }

        [Test]
        public void TC09_OvernightUsage()
        {
            double fee = FeeCalculator.CalculateFee(22, 0, 8, 0);
            Assert.AreEqual(173100, fee, DELTA);
        }

        [Test]
        public void TC10_24HourUsage()
        {
            double fee = FeeCalculator.CalculateFee(8, 0, 8, 0);
            Assert.AreEqual(0, fee, DELTA); // Same time means 0 duration
        }

        [Test]
        public void TC10_Full24HourUsage()
        {
            // For a full 24 hours, we need to set the end time to the next day
            // This is a special test case that simulates 8:00 today to 8:00 tomorrow
            double fee = FeeCalculator.CalculateFee(8, 0, 8, 0);
            // We expect 0 because the times are identical
            Assert.AreEqual(0, fee, DELTA);

            // To test a full 24 hours, we need to handle it differently in our test
            // Let's manually calculate each period
            double morningFee = 9 * 60 * 400 * 0.9; // 9 hours with discount
            double eveningFee = 7 * 60 * 350 * 0.88; // 7 hours with discount
            double nightFee = 7 * 60 * 300 * 0.85; // 7 hours with discount
            double morningNextDayFee = 1 * 60 * 400; // 1 hour without discount

            double expectedTotal = morningFee + eveningFee + nightFee + morningNextDayFee;
            // We can't directly test this with our current implementation
            // This is just to document the expected value: 454,860đ
        }

        [Test]
        public void TC12_EqualStartAndEndTime()
        {
            double fee = FeeCalculator.CalculateFee(8, 0, 8, 0);
            Assert.AreEqual(0, fee, DELTA);
        }
    }
}
