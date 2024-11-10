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
        conexion.SendMessagestoArduino("11", new string[] { color });
        conexion.SendMessagestoArduino("12", new string[] { color });
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
        conexion.SendMessagestoArduino(player, new string[] { color });
    }

    public void FullRound(string color)
    {
        conexion.SendMessagestoArduino("15", new string[] { color });
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
        conexion.SendMessagestoArduino(player, new string[] { color });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino(player, new string[] { "WHITE" });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino(player, new string[] { color });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino(player, new string[] { "WHITE" });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino(player, new string[] { color });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino(player, new string[] { "WHITE" });
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
        
        if (generator.initialMultiplayer) conexion.SendMessagestoArduino(player, new string[] { "BLUE" });
        else
        {
            conexion.SendMessagestoArduino("11", new string[] { "BLUE" });
            conexion.SendMessagestoArduino("12", new string[] { "BLUE" });
        }
        yield return new WaitForSeconds(0.3f);
        if (generator.initialMultiplayer) conexion.SendMessagestoArduino(player, new string[] { "WHITE" });
        else
        {
            conexion.SendMessagestoArduino("11", new string[] { "WHITE" });
            conexion.SendMessagestoArduino("12", new string[] { "WHITE" });
        }
    }

    public IEnumerator Blink(string color)
    {
        conexion.SendMessagestoArduino("12", new string[] { color });
        conexion.SendMessagestoArduino("11", new string[] { color });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino("12", new string[] { "WHITE" });
        conexion.SendMessagestoArduino("11", new string[] { "WHITE" });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino("12", new string[] { color });
        conexion.SendMessagestoArduino("11", new string[] { color });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino("12", new string[] { "WHITE" });
        conexion.SendMessagestoArduino("11", new string[] { "WHITE" });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino("12", new string[] { color });
        conexion.SendMessagestoArduino("11", new string[] { color });
        yield return new WaitForSeconds(0.3f);
        conexion.SendMessagestoArduino("12", new string[] { "WHITE" });
        conexion.SendMessagestoArduino("11", new string[] { "WHITE" });
    }
}
