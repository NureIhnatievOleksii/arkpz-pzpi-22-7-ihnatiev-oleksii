using System.Threading.Tasks;
using AirSense.Domain;
using Newtonsoft.Json;

public class MqttListener
{
    private readonly StorageConditionService _storageConditionService;

    public MqttListener(StorageConditionService storageConditionService)
    {
        _storageConditionService = storageConditionService;
    }

    public async Task StartListeningAsync()
    {
        var mqttConfig = new MqttConfig();
        await mqttConfig.ConnectAsync();
    }

    private void HandleMqttMessage(string message)
    {
        try
        {
            var condition = JsonConvert.DeserializeObject<StorageCondition>(message);
            _storageConditionService.CreateCondition(condition);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error processing MQTT message: {e.Message}");
        }
    }
}
