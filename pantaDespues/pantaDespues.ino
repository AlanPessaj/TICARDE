#define LCD_CS A3 
#define LCD_CD A2 
#define LCD_WR A1 
#define LCD_RD A0 
#define LCD_RESET A4 

#include <SPI.h>          
#include "Adafruit_GFX.h"
#include <MCUFRIEND_kbv.h>
MCUFRIEND_kbv tft;



#define BLACK   0x0000
#define BLUE    0x001F
#define RED     0xF800
#define GREEN   0x07E0
#define CYAN    0x07FF
#define MAGENTA 0xF81F
#define YELLOW  0xFFE0
#define WHITE   0xFFFF

void setup() {
  Serial.begin(9600);
  
  uint16_t ID = tft.readID(); 
  tft.begin(ID);

  tft.setRotation(1);  
  tft.fillScreen(WHITE);
  tft.setTextColor(BLACK);
  tft.setTextSize(2);  
}

void loop() {
  if (Serial.available()) {
    String dataFromUnity = Serial.readStringUntil('\n'); // Lee el string enviado por Unity hasta un salto de línea

    // Procesar el string recibido como un array de strings separados por comas
    const int maxStrings = 10; // Máximo número de elementos en el array
    String stringArray[maxStrings]; // Array para almacenar las cadenas
    int index = 0;

    // Divide el string por comas y almacena en el array
    while (dataFromUnity.length() > 0 && index < maxStrings) {
      int commaIndex = dataFromUnity.indexOf(',');
      if (commaIndex == -1) {
        // Si no hay más comas, almacena el resto del string
        stringArray[index] = dataFromUnity;
        break;
      } else {
        // Almacena el substring antes de la coma
        stringArray[index] = dataFromUnity.substring(0, commaIndex);
        dataFromUnity = dataFromUnity.substring(commaIndex + 1); // Resto del string
      }
      index++;
    }

}



void drawThickLine(int x0, int y0, int x1, int y1, int thickness, uint16_t color) {
    int dx = abs(x1 - x0);    // Calcula la diferencia en la dirección x
    int dy = abs(y1 - y0);    // Calcula la diferencia en la dirección y
    int sx = (x0 < x1) ? 1 : -1; // Determina el sentido de incremento en x
    int sy = (y0 < y1) ? 1 : -1; // Determina el sentido de incremento en y
    int err = dx - dy;        // Calcula el error inicial para el algoritmo de Bresenham
    int e2;                  // Variable temporal para almacenar el valor del error

    // Bucle para dibujar líneas paralelas a la línea principal
    for (int i = -thickness / 2; i <= thickness / 2; i++) {
        int xi0 = x0;       // Coordenada x inicial de la línea
        int yi0 = y0;       // Coordenada y inicial de la línea
        int xi1 = x1;       // Coordenada x final de la línea
        int yi1 = y1;       // Coordenada y final de la línea
        
        // Ajusta las coordenadas dependiendo de la dirección principal de la línea
        if (abs(xi0 - xi1) > abs(yi0 - yi1)) {
            yi0 += i;       // Ajusta la coordenada y para crear grosor
            yi1 += i;       // Ajusta la coordenada y para crear grosor
        } else {
            xi0 += i;       // Ajusta la coordenada x para crear grosor
            xi1 += i;       // Ajusta la coordenada x para crear grosor
        }

        // Algoritmo de Bresenham para dibujar una línea
        while (true) {
            tft.drawPixel(xi0, yi0, color); // Dibuja un píxel en la posición actual
            if (xi0 == xi1 && yi0 == yi1) break; // Si hemos llegado al final de la línea, sal del bucle
            e2 = err * 2; // Doble del error actual para tomar decisiones
            if (e2 > -dy) { err -= dy; xi0 += sx; } // Ajusta x y el error si necesario
            if (e2 < dx) { err += dx; yi0 += sy; } // Ajusta y y el error si necesario
        }
    }
}
