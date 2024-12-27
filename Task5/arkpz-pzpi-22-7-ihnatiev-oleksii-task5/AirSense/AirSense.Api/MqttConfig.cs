using AirSense.Domain;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

public class MqttConfig
{
    private const string BrokerUrl = "broker.hivemq.com";  // Или другой адрес брокера
    private const int BrokerPort = 1883;
    private const string ClientId = "netcore_mqtt_client";
    private const string Topic = "air/measurements";  // Тема, на которую подписываемся

    private IMqttClient _mqttClient;

    public async Task ConnectAsync()
    {
        var mqttFactory = new MqttFactory();
        _mqttClient = mqttFactory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(BrokerUrl, BrokerPort)
            .WithClientId(ClientId)
            .Build();

        _mqttClient.UseConnectedHandler(async e =>
        {
            Console.WriteLine("Connected to MQTT Broker.");
            await SubscribeAsync();
        });

        _mqttClient.UseApplicationMessageReceivedHandler(e =>
        {
            var payload = e.ApplicationMessage.Payload;
            var message = Encoding.UTF8.GetString(payload);
            HandleMqttMessage(message);
        });

        await _mqttClient.ConnectAsync(options);
    }

    private async Task SubscribeAsync()
    {
        var topicFilter = new MqttTopicFilterBuilder().WithTopic(Topic).Build();
        await _mqttClient.SubscribeAsync(topicFilter);
    }

    private void HandleMqttMessage(string message)
    {
        // Десериализация и обработка полученного сообщения
        try
        {
            var condition = JsonConvert.DeserializeObject<StorageCondition>(message);
            Console.WriteLine($"Received message: {condition}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error handling message: {ex.Message}");
        }
    }
}
