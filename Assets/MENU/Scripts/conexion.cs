using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class conexion : MonoBehaviour
{
    SerialPort arduinoPort = new SerialPort("COM9", 9600); // Corrected port name format

    public string id;
    public string[] prueba;

    private void Awake()
    {
        // Set the serial port properties
        arduinoPort.Parity = Parity.None;
        arduinoPort.StopBits = StopBits.One;
        arduinoPort.DataBits = 8;
        arduinoPort.Handshake = Handshake.None;
    }

    void Start()
    {
        // Open the serial port in Start()
        try
        {
            if (!arduinoPort.IsOpen) // Check if the port is already open
            {
                arduinoPort.Open();
                Debug.Log("Serial Port Opened");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to open serial port: " + e.Message);
        }


    }

    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SendMessagestoArduino(id, prueba);
        }

    }

    public void SendMessagestoArduino(string identifier, string[] messages){
    if (arduinoPort.IsOpen)
    {
        try
        {
                string msg2send = identifier + "," + string.Join(",", messages);
                arduinoPort.WriteLine(msg2send);
                Debug.Log(msg2send);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to send message to Arduino: " + e.Message);
        }
    }
    else
    {
        Debug.LogWarning("Serial port is not open.");
    }
    }



    public void ClosePort()
    {
        if (arduinoPort.IsOpen)
        {
            arduinoPort.Close();
            Debug.Log("Serial Port Closed");
        }
        else
        {
            Debug.LogWarning("Serial port is already closed.");
        }
    }

    private void OnApplicationQuit()
    {
        ClosePort(); // Close the port when the application quits
    }
}

/*
Funcion SendMessageToArduino tiene 2 parametros: el primero es un identificador numerico
(que tiene que ser un string) y un array de strings en el que este eñ mensaje a mostrar o
el patron de iluminacion de los leds.

Si el id es 0=
Se muestra por pantalla .:TICARDE:.
No necesita que se le pase nada en el array (mandar vacio)

Si el id es 1=
Se muestra el selector de personaje para 1 jugador
Posicion 0 del array: nombre del personaje seleccionado

Si el id es 2=
Se muestra el selector de personaje para 2 jugadores
Posicion 0 del array: nombre del personaje seleccionado por jugador 1
Posicion 1 del array: nombre del personaje seleccionado por jugador 2

Si el id es 3=
El marcador de puntos de tennis
Posicion 0 del array: games de jugador 1
Posicion 1 del array: puntos de jugador 1
Posicion 2 del array: games de jugador 2
Posicion 3 del array: puntos de jugador 2

Si el id es 4=
El score del frogger (tiles avanzadas) de un solo player
Posicion 0 del array: Score de player

Si el id es 5=
El score del frogger (tiles avanzadas) de 2 players
Posicion 0 del array: Score de player 1
Posicion 1 del array: Score de player 2

Si el id es 6= 
Daño hecho en el street fighter
Posicion 0 del array: Score de player 1
Posicion 1 del array: Score de player 2


Los colores para el led se tienen que mandar en formato hexagesimal
Ejemplo:
FF0000 para el rojo
000000 para negro (pasar negro es apagar el led)
00FF00 para el verde


Si el id es 7=
Un color va de una punta a la otra de cierto color del player 1
Posicion 0 del array: color
Posicion 1 del array: (string) cuantos milisegundos esperar entre cada led

Si el id es 8=
Un color va de una punta a la otra de cierto color del player 2
Posicion 0 del array: color
Posicion 1 del array: (string) cuantos milisegundos esperar entre cada led

Si el id es 9=
Un color va de una punta a la otra (como en el anterior) y tambien vuelve del player 1
Posicion 0 del array: color
Posicion 1 del array: (string) cuantos milisegundos esperar entre cada led

Si el id es 10=
Un color va de una punta a la otra (como en el anterior) y tambien vuelve del player 2
Posicion 0 del array: color
Posicion 1 del array: (string) cuantos milisegundos esperar entre cada led

Si el id es 11=
Todos los leds de player 1
Posicion 0 del array: color

Si el id es 12=
Todos los leds de player 2
Posicion 0 del array: color

Si el id es 13=
Un led especifico (del 0 al 12) del array de player 1
Posicion 0 del array: color
Posicion 1 del array: (string) posicion en el array del led a encender(0-12)

Si el id es 14=
Un led especifico (del 0 al 12) del array de player 2
Posicion 0 del array: color
Posicion 1 del array: (string) posicion en el array del led a encender(0-12)


Si es 15=
Se va a hacer un recorrido de ida desde una punta de la tira (la mitad de la izquierda) hasta la otra (la mitad de la derecha)
Posicion 0 del array: color
Posicion 1 del array: (string) cuantos milisegundos esperar entre cada led

Si es 16=
Se va a hacer un recorrido de idadesde una punta de la tira (la mitad de la izquierda) hasta la otra (la mitad de la derecha), y despues de vuelta en la direccion contraria (derecha a izquierda)
Posicion 0 del array: color
Posicion 1 del array: (string) cuantos milisegundos esperar entre cada led
*/
