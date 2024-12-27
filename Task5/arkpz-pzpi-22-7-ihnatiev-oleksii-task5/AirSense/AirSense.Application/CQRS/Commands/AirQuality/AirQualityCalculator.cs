using AirSense.Domain.AirQualityAggregate;

namespace AirSense.Application.CQRS.Commands.AirQuality
{
    public class AirQualityCalculator
    {
        public static double CalculateAQI(AirSense.Domain.AirQualityAggregate.AirQuality data)
        {
            double coWeight = 0.2;
            double noWeight = 0.1;
            double no2Weight = 0.15;
            double o3Weight = 0.2;
            double so2Weight = 0.15;
            double pm2_5Weight = 0.1;
            double pm10Weight = 0.05;
            double nh3Weight = 0.05;

            double coNormalized = NormalizeValue(data.Co, 0, 10);
            double noNormalized = NormalizeValue(data.No, 0, 10);
            double no2Normalized = NormalizeValue(data.No2, 0, 10);
            double o3Normalized = NormalizeValue(data.O3, 0, 10);
            double so2Normalized = NormalizeValue(data.So2, 0, 10);
            double pm2_5Normalized = NormalizeValue(data.Pm2_5, 0, 25);
            double pm10Normalized = NormalizeValue(data.Pm10, 0, 50);
            double nh3Normalized = NormalizeValue(data.Nh3, 0, 10);

            double aqi = (coWeight * coNormalized) +
                         (noWeight * noNormalized) +
                         (no2Weight * no2Normalized) +
                         (o3Weight * o3Normalized) +
                         (so2Weight * so2Normalized) +
                         (pm2_5Weight * pm2_5Normalized) +
                         (pm10Weight * pm10Normalized) +
                         (nh3Weight * nh3Normalized);

            return aqi;
        }


        private static double NormalizeValue(double value, double min, double max)
        {
            return (value - min) / (max - min) * 100;
        }
    }
}
