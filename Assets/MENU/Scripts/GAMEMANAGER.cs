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
    public AudioClip accessDenied;
    bool showingCredits = false;
    [HideInInspector] public bool insufficientCreditsActive = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && menuActive)
        {
            menuActive = false;
            SceneManager.LoadScene("MENU");
            GAMEMANAGER.Instance.txtCredits.rectTransform.position = new Vector2(862, 59);
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameData.credits++;
            GetComponent<AudioSource>().Play();
            txtCredits.text = "CREDITS: " + GameData.credits;
            txtCredits.color = new Color(0.4433962f, 0.4433962f, 0.4433962f, 1);
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
        showingCredits = true;
        txtCredits.enabled = true;
        yield return new WaitForSeconds(3f);
        showingCredits = false;
        txtCredits.enabled = false;
    }

    public IEnumerator InsufficientCredits(bool endScene = false)
    {
        if (endScene) txtCredits.rectTransform.position = new Vector2(512, 159);
        else txtCredits.rectTransform.position = new Vector2(862, 59);
        insufficientCreditsActive = true;
        GetComponent<AudioSource>().PlayOneShot(accessDenied);
        txtCredits.color = new Color(1, 0, 0, 1);

        for (int i = 0; i < 3; i++)
        {
            txtCredits.enabled = true;
            yield return new WaitForSeconds(0.1f);
            txtCredits.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
        insufficientCreditsActive = false;
    }

    public void UpdateCreditsValue()
    {
        txtCredits.rectTransform.position = new Vector2(862, 59);
        txtCredits.text = "CREDITS: " + GameData.credits;
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
