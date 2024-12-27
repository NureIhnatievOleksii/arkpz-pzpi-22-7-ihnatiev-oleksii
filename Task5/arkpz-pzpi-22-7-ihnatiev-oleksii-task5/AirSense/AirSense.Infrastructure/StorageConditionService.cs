using System;
using AirSense.Domain;
using Newtonsoft.Json;

public class StorageConditionService
{
    // Это ваш сервис для работы с данными, например, для сохранения в базе данных
    public void CreateCondition(StorageCondition condition)
    {
        // Здесь можно добавить код для сохранения в базу данных или другие операции
        Console.WriteLine("Data saved: " + JsonConvert.SerializeObject(condition));
    }
}
