using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GAMEMANAGER : MonoBehaviour
{
    public static GAMEMANAGER Instance { get; private set; }
    public bool menuActive;
    public TextMeshProUGUI txtCredits;
    bool showingCredits = false;
    float time = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && menuActive)
        {
            menuActive = false;
            SceneManager.LoadScene("MENU");
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameData.credits++;
            GetComponent<AudioSource>().Play();
            txtCredits.text = "CREDITS: " + GameData.credits;
            time += 3f;
            if (!showingCredits) StartCoroutine(ShowCredits());
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

    IEnumerator ShowCredits()
    {
        float localTime = time;
        time = 0f;
        showingCredits = true;
        txtCredits.enabled = true;
        yield return new WaitForSeconds(localTime * Time.deltaTime);
        while (time > 0f)
        {
            localTime = time;
            yield return new WaitForSeconds(localTime * Time.deltaTime);
        }
        showingCredits = false;
        txtCredits.enabled = false;
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
    public static bool p1Winner;
    public static int credits;
}
