using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class FeeCalculator
    {
        // Constants for rates and discounts
        private const double MORNING_RATE = 400; // 7:00-17:00
        private const double EVENING_RATE = 350; // 17:00-24:00
        private const double NIGHT_RATE = 300;   // 0:00-7:00

        private const double MORNING_DISCOUNT = 0.10; // 10%
        private const double EVENING_DISCOUNT = 0.12; // 12%
        private const double NIGHT_DISCOUNT = 0.15;   // 15%

        private const int MORNING_DISCOUNT_THRESHOLD = 6 * 60; // 6 hours in minutes
        private const int EVENING_DISCOUNT_THRESHOLD = 4 * 60; // 4 hours in minutes
        private const int NIGHT_DISCOUNT_THRESHOLD = 7 * 60;   // 7 hours in minutes

        public static double CalculateFee(int startHour, int startMinute, int endHour, int endMinute)
        {
            // Convert start and end times to minutes since midnight
            int startTimeInMinutes = startHour * 60 + startMinute;
            int endTimeInMinutes = endHour * 60 + endMinute;

            // Handle case when end time is on the next day
            if (endTimeInMinutes < startTimeInMinutes)
            {
                endTimeInMinutes += 24 * 60; // Add 24 hours in minutes
            }

            double totalFee = 0;
            int currentTime = startTimeInMinutes;

            // Process each time period
            while (currentTime < endTimeInMinutes)
            {
                int periodStart = currentTime;
                int periodEnd;
                double rate;
                double discount;
                int discountThreshold;

                // Determine the rate period
                int normalizedTime = currentTime % (24 * 60);

                if (normalizedTime >= 7 * 60 && normalizedTime < 17 * 60)
                {
                    // Morning period (7:00-17:00)
                    periodEnd = Math.Min(endTimeInMinutes, ((normalizedTime / (24 * 60)) * 24 * 60) + 17 * 60);
                    rate = MORNING_RATE;
                    discount = MORNING_DISCOUNT;
                    discountThreshold = MORNING_DISCOUNT_THRESHOLD;
                }
                else if (normalizedTime >= 17 * 60 && normalizedTime < 24 * 60)
                {
                    // Evening period (17:00-24:00)
                    periodEnd = Math.Min(endTimeInMinutes, ((normalizedTime / (24 * 60)) * 24 * 60) + 24 * 60);
                    rate = EVENING_RATE;
                    discount = EVENING_DISCOUNT;
                    discountThreshold = EVENING_DISCOUNT_THRESHOLD;
                }
                else
                {
                    // Night period (0:00-7:00)
                    periodEnd = Math.Min(endTimeInMinutes, ((normalizedTime / (24 * 60)) * 24 * 60) + 7 * 60);
                    rate = NIGHT_RATE;
                    discount = NIGHT_DISCOUNT;
                    discountThreshold = NIGHT_DISCOUNT_THRESHOLD;
                }

                int periodDuration = periodEnd - periodStart;
                double periodFee = periodDuration * rate;

                // Apply discount if applicable
                if (periodDuration > discountThreshold)
                {
                    periodFee *= (1 - discount);
                }

                totalFee += periodFee;
                currentTime = periodEnd;
            }

            return totalFee;
        }
    }
}
