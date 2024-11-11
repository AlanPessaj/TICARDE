using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedsController : MonoBehaviour
{
    conexion conexion;

    void Start()
    {
        conexion.SendMessagestoArduino("16", new string[] { ColorTranslator("RED") });
    }

    private void Awake()
    {
        conexion = GetComponent<conexion>();
    }

    public void FillAll(string color)
    {
        conexion.SendMessagestoArduino("11", new string[] { ColorTranslator(color) });
        conexion.SendMessagestoArduino("12", new string[] { ColorTranslator(color) });
    }

    public void FillSide(bool player1, string color, bool multiplayer = true)
    {
        if (!multiplayer)
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
        conexion.SendMessagestoArduino("15", new string[] { ColorTranslator(color), "100" });
    }

    public IEnumerator SideBlink(bool player1, string color, bool multiplayer = true, bool isTherePlayer1 = false)
    {
        if (!multiplayer)
        {
            Blink(color);
            yield break;
        }
        string player = "11";
        if (!player1) player = "12";
        conexion.SendMessagestoArduino(player, new string[] { ColorTranslator(color) });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino(player, new string[] { "000000" });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino(player, new string[] { ColorTranslator(color) });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino(player, new string[] { "000000" });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino(player, new string[] { ColorTranslator(color) });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino(player, new string[] { "000000" });
        if (color == "RED")
        {
            if (player1 && !multiplayer && isTherePlayer1) FillSide(true, "RED");
            else if (!player1 && !multiplayer && !isTherePlayer1) FillSide(false, "RED");
        }
    }

    public IEnumerator SingleBlink(bool player1, string color, bool multiplayer = true)
    {
        string player = "11";
        if (!player1) player = "12";
        
        if (multiplayer) conexion.SendMessagestoArduino(player, new string[] { ColorTranslator(color) });
        else
        {
            conexion.SendMessagestoArduino("11", new string[] { ColorTranslator(color) });
            conexion.SendMessagestoArduino("12", new string[] { ColorTranslator(color) });
        }
        yield return new WaitForSeconds(0.3f);
        if (multiplayer) conexion.SendMessagestoArduino(player, new string[] { "000000" });
        else
        {
            conexion.SendMessagestoArduino("11", new string[] { "000000" });
            conexion.SendMessagestoArduino("12", new string[] { "000000" });
        }
    }

    public void HalfRound(bool player1, string color)
    {
        string player = player1 ? "9" : "10";
        conexion.SendMessagestoArduino(player, new string[] { ColorTranslator(color), "200" });
    }

    public IEnumerator Blink(string color)
    {
        conexion.SendMessagestoArduino("12", new string[] { ColorTranslator(color) });
        conexion.SendMessagestoArduino("11", new string[] { ColorTranslator(color) });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino("12", new string[] { "000000" });
        conexion.SendMessagestoArduino("11", new string[] { "000000" });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino("12", new string[] { ColorTranslator(color) });
        conexion.SendMessagestoArduino("11", new string[] { ColorTranslator(color) });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino("12", new string[] { "000000" });
        conexion.SendMessagestoArduino("11", new string[] { "000000" });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino("12", new string[] { ColorTranslator(color) });
        conexion.SendMessagestoArduino("11", new string[] { ColorTranslator(color) });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino("12", new string[] { "000000" });
        conexion.SendMessagestoArduino("11", new string[] { "000000" });
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
