using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class conexion : MonoBehaviour
{
    public InputField m; // Make sure to assign this in the Unity Inspector

    SerialPort arduinoPort = new SerialPort("COM8", 9600); // Corrected port name format

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

    public void SendMessagetoArduino(string msg)
    {
        if (arduinoPort.IsOpen)
        {
            try
            {
                arduinoPort.WriteLine(msg);
                Debug.Log("Message sent to Arduino: " + msg);
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

    public void Btn()
    {
        if (arduinoPort.IsOpen) // Ensure the port is open before sending the message
        {
            SendMessagetoArduino(m.text); // Ensure that m is properly initialized
        }
        else
        {
            Debug.LogWarning("Cannot send message, serial port is not open.");
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
