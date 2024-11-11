using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GAMEMANAGER : MonoBehaviour
{
    public static GAMEMANAGER Instance { get; private set; }
    public bool menuActive;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && menuActive)
        {
            menuActive = false;
            SceneManager.LoadScene("MENU");
            Time.timeScale = 1;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

public static class GameData
{
    public static string name1;
    public static string name2;
    public static int char1;
    public static int char2;
    public static string game;
    public static int score1;
    public static int score2;
}
