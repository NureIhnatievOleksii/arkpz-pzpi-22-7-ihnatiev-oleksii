using AirSense.Domain.AirQualityAggregate;
using AirSense.Domain.UserAggregate;

namespace AirSense.Domain.AirQualityHistoryAggregate;

public class AirQualityHistory
{
    public Guid AirQualityHistoryId { get; set; }
    public Guid AirQualityId { get; set; } // Идентификатор записи качества воздуха (ссылка на таблицу AirQuality)
    public Guid UserId { get; set; } // Идентификатор пользователя (ссылка на таблицу Users)
    public DateTime Timestamp { get; set; }
    public string AqiHistory { get; set; } // Исторические данные по качеству воздуха (JSON структура)
    public AirQuality AirQuality { get; set; } // Навигационное свойство
    public User User { get; set; } // Навигационное свойство
}
