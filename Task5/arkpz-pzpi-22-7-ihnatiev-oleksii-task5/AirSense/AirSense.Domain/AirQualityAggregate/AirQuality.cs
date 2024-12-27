using AirSense.Domain.LocationAggregate;

namespace AirSense.Domain.AirQualityAggregate
{
    public class AirQuality
    {
        public Guid AirQualityId { get; set; }
        public Guid? LocationId { get; set; }  // Добавляем поле для связи с Location
        public float Co { get; set; }
        public float No { get; set; }
        public float No2 { get; set; }
        public float O3 { get; set; }
        public float So2 { get; set; }
        public float Pm2_5 { get; set; }
        public float Pm10 { get; set; }
        public float Nh3 { get; set; }
        public DateTime MeasuredAt { get; set; }
        public string? DeviceId { get; set; }
        public bool? IsSynced { get; set; }

        public Location Location { get; set; } // Навигационное свойство
    }
}
