using AirSense.Application.Interfaces.Repositories;
using MQTTnet.Client.Options;
using MQTTnet.Client;
using MQTTnet;
using AirSense.Domain.AirQualityAggregate;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using AirSense.Application.CQRS.Commands.AirQuality;

public class MqttService : IHostedService
{
    private readonly IMqttClient _mqttClient;
    private readonly IMqttClientOptions _mqttOptions;
    private readonly IServiceScopeFactory _serviceScopeFactory; // Додано

    private readonly string _mqttBroker = "broker.hivemq.com";
    private readonly int _mqttPort = 1883;
    private readonly string _mqttTopic = "air/measurements";

    public string LastMessage { get; private set; }

    public MqttService(IServiceScopeFactory serviceScopeFactory) // Заміна IAirQualityRepository
    {
        _serviceScopeFactory = serviceScopeFactory;

        var factory = new MqttFactory();
        _mqttClient = factory.CreateMqttClient();

        _mqttOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(_mqttBroker, _mqttPort)
            .Build();

        ConfigureHandlers();
    }

    private void ConfigureHandlers()
    {
        _mqttClient.UseConnectedHandler(async e =>
        {
            Console.WriteLine("Підключено до брокера.");
            try
            {
                await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(_mqttTopic).Build());
                Console.WriteLine($"Підписано на тему: {_mqttTopic}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка підписки: {ex.Message}");
            }
        });

        _mqttClient.UseDisconnectedHandler(e =>
        {
            Console.WriteLine("Відключено від брокера.");
        });

        _mqttClient.UseApplicationMessageReceivedHandler(async e =>
        {
            LastMessage = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            Console.WriteLine($"Отримано повідомлення: {LastMessage}");

            try
            {

                var airQualityData = JsonConvert.DeserializeObject<AirQuality>(LastMessage);

                if (airQualityData != null)
                {
                    double aqi = AirQualityCalculator.CalculateAQI(airQualityData);
                    Console.WriteLine($"Розрахований AQI: {aqi:F2}");

                    // Збереження даних в базу
                    using var scope = _serviceScopeFactory.CreateScope();
                    var airQualityRepository = scope.ServiceProvider.GetRequiredService<IAirQualityRepository>();

                    await airQualityRepository.AddAirQualityAsync(airQualityData);
                    Console.WriteLine("Дані збережено в базу даних.");
                }

                else
                {
                    Console.WriteLine("Помилка десеріалізації даних.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при збереженні даних: {ex.Message}");
            }
        });
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _mqttClient.ConnectAsync(_mqttOptions, cancellationToken);
            Console.WriteLine("Очікування повідомлень...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка підключення: {ex.Message}");
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _mqttClient.DisconnectAsync();
        Console.WriteLine("Відключення від брокера...");
    }
}
