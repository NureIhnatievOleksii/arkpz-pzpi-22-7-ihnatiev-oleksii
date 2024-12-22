using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using AirSense.Domain.AirQualityAggregate;
using AirSense.Application.Interfaces.Repositories;

namespace AirSense.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MqttController : ControllerBase
    {
        private readonly MqttService _mqttService;

        public MqttController(MqttService mqttService)
        {
            _mqttService = mqttService;
        }

        [HttpGet("last-message")]
        public IActionResult GetLastMessage()
        {
            if (string.IsNullOrEmpty(_mqttService.LastMessage))
            {
                return NotFound("Немає отриманих повідомлень.");
            }

            return Ok(_mqttService.LastMessage);
        }
    }
}
