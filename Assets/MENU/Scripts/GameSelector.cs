using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelector : MonoBehaviour
{
    public GameObject[] logos;
    int index;
    public bool animationFinished = false;
    public AudioClip joinTitle;
    bool loadtitle;
    bool loadleaderboard;
    bool firstTime = true;
    public GameObject insertCoin;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(StartScreen());
    }

    IEnumerator StartScreen()
    {
        yield return new WaitForSeconds(1f);
        GAMEMANAGER.Instance.GetComponent<conexion>().SendMessagestoArduino("0", new string[] { "" });
    }
    void Update()
    {
        if (animationFinished)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if ((GetComponent<AudioSource>().time > 0.15f && GetComponent<AudioSource>().isPlaying) || !GetComponent<AudioSource>().isPlaying)
                {
                    GetComponent<AudioSource>().Play();
                    StartCoroutine(GAMEMANAGER.Instance.GetComponent<LedsController>().SingleBlink(true, "GREEN"));
                }
                if (index > 0)
                {
                    index--;
                }
                else
                {
                    index = logos.Length - 1;
                }
                foreach (var item in logos)
                {
                    item.SetActive(false);
                }
                logos[index].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if ((GetComponent<AudioSource>().time > 0.15f && GetComponent<AudioSource>().isPlaying) || !GetComponent<AudioSource>().isPlaying)
                {
                    GetComponent<AudioSource>().Play();
                    StartCoroutine(GAMEMANAGER.Instance.GetComponent<LedsController>().SingleBlink(false, "GREEN"));
                }
                if (index < logos.Length - 1)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
                foreach (var item in logos)
                {
                    item.SetActive(false);
                }
                logos[index].SetActive(true);
            }
            if (Input.GetButtonDown("A") && firstTime)
            {
                if (GameData.credits < 1)
                {
                    StartCoroutine(InsertCoinBlink());
                    return;
                }
                switch (index)
                {
                    case 0:
                        GameData.game = "FF";
                        break;
                    case 1:
                        GameData.game = "FT";
                        break;
                    case 2:
                        GameData.game = "FG";
                        break;
                }
                GetComponent<AudioSource>().clip = joinTitle;
                GetComponent<AudioSource>().Play();
                StartCoroutine(GAMEMANAGER.Instance.GetComponent<LedsController>().Blink("GREEN"));
                firstTime = false;
                if (!loadleaderboard) loadtitle = true;
            }
            if (Input.GetButtonDown("C") && firstTime)
            {
                switch (index)
                {
                    case 0:
                        GameData.game = "FF";
                        break;
                    case 1:
                        GameData.game = "FT";
                        break;
                    case 2:
                        GameData.game = "FG";
                        break;
                }
                GetComponent<AudioSource>().clip = joinTitle;
                GetComponent<AudioSource>().Play();
                StartCoroutine(GAMEMANAGER.Instance.GetComponent<LedsController>().Blink("GREEN"));
                firstTime = false;
                if (!loadtitle) loadleaderboard = true;
            }
        }

        if (loadtitle && !GetComponent<AudioSource>().isPlaying) SceneManager.LoadScene("NameInput");
        if (loadleaderboard && !GetComponent<AudioSource>().isPlaying) SceneManager.LoadScene("Leaderboard");

    }

    public IEnumerator InsertCoinBlink()
    {
        for (int i = 0; i < 3; i++)
        {
            insertCoin.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            insertCoin.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
