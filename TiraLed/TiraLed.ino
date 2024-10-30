#include <FastLED.h>

#define NUM_LEDS 5
#define LED_PIN 3

CRGB leds [NUM_LEDS];

void setup() {
  FastLED.addLeds<WS2812B,LED_PIN,GRB>(leds,NUM_LEDS);
  FastLED.setBrightness(50);

}

void loop() {
  for (int i=0;i<NUM_LEDS;i++)
  {
    leds[i] = CRGB::Yellow;
    FastLED.show();
    delay(500);
    leds[i] = CRGB::Black;
    FastLED.show();
    delay(500);
  }
}
