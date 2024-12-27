#include <WiFi.h>
#include <PubSubClient.h>

// WiFi налаштування
const char* ssid = "Wokwi-GUEST";
const char* password = "";

// MQTT налаштування
const char* mqtt_server = ""; 
const int mqtt_port = ;
const char* mqtt_user = "";  
const char* mqtt_password = "";

WiFiClient espClient;
PubSubClient client(espClient);

bool dataSent = false;  // Початково - дані не відправлені

// Функція для підключення до WiFi
void setup_wifi() {
  delay(10);
  Serial.println();
  Serial.print("Підключення до ");
  Serial.println(ssid);

  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  Serial.println("");
  Serial.println("WiFi підключено!");
  Serial.print("IP address: ");
  Serial.println(WiFi.localIP());
}

// Функція для підключення до MQTT брокера
void reconnect() {
  while (!client.connected()) {
    Serial.print("Підключення до MQTT...");
    if (client.connect("ESP32Client", mqtt_user, mqtt_password)) {
      Serial.println("підключено!");
    } else {
      Serial.print("помилка, rc=");
      Serial.print(client.state());
      Serial.println(" спроба підключення через 5 секунд");
      delay(5000);
    }
  }
}

float generateRandomMeasurement(float min, float max) {
  return random(min * 100, max * 100) / 100.0;  // Генерація випадкових значень з точністю до 2 знаків
}

// Відправка даних
void sendData() {
  if (dataSent) {
    Serial.println("Дані вже були відправлені. Припиняємо відправку.");
    return;  // Якщо дані вже відправлені, припиняємо відправку
  }

  // Генерація випадкових значень для газів та часток
  float co = generateRandomMeasurement(0.0, 10.0);  // CO (0-10 ppm)
  float no = generateRandomMeasurement(0.0, 10.0);  // NO (0-10 ppm)
  float no2 = generateRandomMeasurement(0.0, 10.0);  // NO₂ (0-10 ppm)
  float o3 = generateRandomMeasurement(0.0, 10.0);  // O₃ (0-10 ppm)
  float so2 = generateRandomMeasurement(0.0, 10.0);  // SO₂ (0-10 ppm)
  float pm2_5 = generateRandomMeasurement(0.0, 50.0);  // PM2.5 (0-50 µg/m³)
  float pm10 = generateRandomMeasurement(0.0, 50.0);  // PM10 (0-50 µg/m³)
  float nh3 = generateRandomMeasurement(0.0, 10.0);  // NH₃ (0-10 ppm)

  // Створення JSON-формату
  String jsonBody = "{"
                     "\"co\": " + String(co) + ","
                     "\"no\": " + String(no) + ","
                     "\"no2\": " + String(no2) + ","
                     "\"o3\": " + String(o3) + ","
                     "\"so2\": " + String(so2) + ","
                     "\"pm2_5\": " + String(pm2_5) + ","
                     "\"pm10\": " + String(pm10) + ","
                     "\"nh3\": " + String(nh3) + ","
                     "\"measuredAt\": \"" + getCurrentTime() + "\","
                     "\"deviceId\": \"ESP32Device\","
                     "\"isSynced\": true"
                     "}";

  Serial.println("Відправка даних...");
  Serial.println(jsonBody);

  // Публікація даних на тему "air/measurements"
  if (client.publish("air/measurements", jsonBody.c_str())) {
    Serial.println("Дані успішно відправлені!");
    dataSent = true;  // Позначаємо, що дані були відправлені
    stopProgram();  // Зупинка програми після успішної відправки
  } else {
    Serial.println("Помилка відправки даних!");
  }
}

// Отримання поточного часу у форматі ISO 8601
String getCurrentTime() {
  unsigned long currentMillis = millis();
  char timeBuffer[30];
  snprintf(timeBuffer, sizeof(timeBuffer), "%04d-%02d-%02dT%02d:%02d:%02d.%03dZ", 
           2024, 12, 22, 15, 8, 19, currentMillis);
  return String(timeBuffer);
}

// Функція для зупинки програми
void stopProgram() {
  Serial.println("Програма зупинена.");
  while (true) {
    // Завершуємо виконання програми, зупиняючи всі цикли
    delay(1000);  // Безкінечно зупиняємо програму
  }
}

void setup() {
  Serial.begin(115200);
  setup_wifi();
  client.setServer(mqtt_server, mqtt_port);
}

void loop() {
  if (!client.connected()) {
    reconnect();
  }
  client.loop();
  // Відправка даних лише один раз
  if (!dataSent) {  // Якщо дані ще не були відправлені
    static unsigned long lastMsg = 0;
    unsigned long now = millis();
    if (now - lastMsg > 10000) {  // 10 секунд
      lastMsg = now;
      sendData();
    }
  }
}
