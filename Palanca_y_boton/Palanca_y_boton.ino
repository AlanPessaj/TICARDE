#include <Keyboard.h>

#define ABAJO1 2
#define ARRIBA1 3
#define IZQUIERDA1 4
#define DERECHA1 5

#define ABAJO2 8
#define ARRIBA2 9
#define IZQUIERDA2 10
#define DERECHA2 11

#define BTN_R A0
#define BTN_T A1
#define BTN_Y A2

#define BTN_I A3
#define BTN_O A4
#define BTN_P A5

#define BTN_MENU 7

#define D/C 12
#define CS 13
#define SDI 6
#define SCK 7

/* 
*/

void setup()
{
  pinMode(ABAJO1,INPUT_PULLUP);
  pinMode(ARRIBA1,INPUT_PULLUP);
  pinMode(DERECHA1,INPUT_PULLUP);
  pinMode(IZQUIERDA1,INPUT_PULLUP);  
  
  pinMode(ABAJO2,INPUT_PULLUP);
  pinMode(ARRIBA2,INPUT_PULLUP);
  pinMode(DERECHA2,INPUT_PULLUP);
  pinMode(IZQUIERDA2,INPUT_PULLUP);  
  
  pinMode(BTN_R,INPUT_PULLUP);
  pinMode(BTN_T,INPUT_PULLUP);
  pinMode(BTN_Y,INPUT_PULLUP);
  
  pinMode(BTN_I,INPUT_PULLUP);
  pinMode(BTN_O,INPUT_PULLUP);
  pinMode(BTN_P,INPUT_PULLUP);

  pinMode(BTN_MENU, INPUT_PULLUP);

  Keyboard.begin();

}

void loop()
{

  BTN_on_click(ARRIBA1,'w');
  BTN_on_click(ABAJO1,'s');
  BTN_on_click(DERECHA1,'d');
  BTN_on_click(IZQUIERDA1,'a');

  BTN_on_click(ARRIBA2,KEY_UP_ARROW);
  BTN_on_click(ABAJO2,KEY_DOWN_ARROW);
  BTN_on_click(DERECHA2,KEY_RIGHT_ARROW);
  BTN_on_click(IZQUIERDA2,KEY_LEFT_ARROW);

  BTN_on_click(BTN_R,'r');
  BTN_on_click(BTN_T,'t');
  BTN_on_click(BTN_Y,'y');

  BTN_on_click(BTN_I,'i');
  BTN_on_click(BTN_O,'o');
  BTN_on_click(BTN_P,'p');

  BTN_on_click(BTN_MENU, 'm');

}

void BTN_on_click(int direcc, char tecla)
{

  if(digitalRead(direcc)==false)
  {
    Keyboard.press(tecla);
  }
  else
  {
    Keyboard.release(tecla);
  }
}
