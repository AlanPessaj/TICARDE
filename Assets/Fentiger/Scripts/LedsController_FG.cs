using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedsController_FG : MonoBehaviour
{
    conexion conexion;
    Generator_FG generator;

    void Start()
    {
        FillAll("WHITE");
    }

    private void Awake()
    {
        conexion = GameObject.Find("TICARDEMANAGER").GetComponent<conexion>();
        generator = GetComponent<Generator_FG>();
    }

    public void FillAll(string color)
    {
        conexion.SendMessagestoArduino("11", new string[] { ColorTranslator(color) });
        conexion.SendMessagestoArduino("12", new string[] { ColorTranslator(color) });
    }

    public void FillSide(bool player1, string color)
    {
        if (!generator.initialMultiplayer)
        {
            FillAll(color);
            return;
        }
        string player = "11";
        if (!player1) player = "12";
        conexion.SendMessagestoArduino(player, new string[] { ColorTranslator(color) });
    }

    public void FullRound(string color)
    {
        conexion.SendMessagestoArduino("15", new string[] { ColorTranslator(color) });
    }

    public IEnumerator SideBlink(bool player1, string color)
    {
        if (!generator.initialMultiplayer)
        {
            Blink(color);
            yield break;
        }
        string player = "11";
        if (!player1) player = "12";
        conexion.SendMessagestoArduino(player, new string[] { ColorTranslator(color) });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino(player, new string[] { "FFFFFF" });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino(player, new string[] { ColorTranslator(color) });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino(player, new string[] { "FFFFFF" });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino(player, new string[] { ColorTranslator(color) });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino(player, new string[] { "FFFFFF" });
        if (color == "RED")
        {
            if (player1 && !generator.multiplayer && !generator.isTherePlayer1) FillSide(true, "RED");
            else if (!player1 && !generator.multiplayer && !generator.isTherePlayer2) FillSide(false, "RED");
        }
    }

    public IEnumerator PickUpStar(bool player1)
    {
        string player = "11";
        if (!player1) player = "12";
        
        if (generator.initialMultiplayer) conexion.SendMessagestoArduino(player, new string[] { "000099" });
        else
        {
            conexion.SendMessagestoArduino("11", new string[] { "000099" });
            conexion.SendMessagestoArduino("12", new string[] { "000099" });
        }
        yield return new WaitForSeconds(0.3f);
        if (generator.initialMultiplayer) conexion.SendMessagestoArduino(player, new string[] { "FFFFFF" });
        else
        {
            conexion.SendMessagestoArduino("11", new string[] { "FFFFFF" });
            conexion.SendMessagestoArduino("12", new string[] { "FFFFFF" });
        }
    }

    public IEnumerator Blink(string color)
    {
        conexion.SendMessagestoArduino("12", new string[] { ColorTranslator(color) });
        conexion.SendMessagestoArduino("11", new string[] { ColorTranslator(color) });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino("12", new string[] { "FFFFFF" });
        conexion.SendMessagestoArduino("11", new string[] { "FFFFFF" });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino("12", new string[] { ColorTranslator(color) });
        conexion.SendMessagestoArduino("11", new string[] { ColorTranslator(color) });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino("12", new string[] { "FFFFFF" });
        conexion.SendMessagestoArduino("11", new string[] { "FFFFFF" });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino("12", new string[] { ColorTranslator(color) });
        conexion.SendMessagestoArduino("11", new string[] { ColorTranslator(color) });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino("12", new string[] { "FFFFFF" });
        conexion.SendMessagestoArduino("11", new string[] { "FFFFFF" });
    }

    public string ColorTranslator(string color)
    {
        switch (color)
        {
            case "BLUE":
                return "000099";
            case "RED":
                return "FF0000";
            case "GREEN":
                return "00FF00";
            case "CYAN":
                return "00FFFF";
            case "MAGENTA":
                return "660099";
            case "YELLOW":
                return "FFFF00";
            case "WHITE":
                return "FFFFFF";
            default:
                return "";
        }
    }
}
