using AirSense.Domain.LocationAggregate;

namespace AirSense.Domain.AirQualityAggregate;

public class AirQuality
{
    public Guid AirQualityId { get; set; }
    public Guid LocationId { get; set; } // Идентификатор геолокации (ссылка на таблицу Locations)
    public DateTime Timestamp { get; set; }
    public float Aqi { get; set; } // Индекс качества воздуха
    public string Components { get; set; } // JSON структура для компонентов
    public Location Location { get; set; } // Навигационное свойство
}
