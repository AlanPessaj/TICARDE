using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieScript_FG : MonoBehaviour
{
    public bool playerGhost;
    public Generator_FG generator;
    bool firstSend = true;

    private void Awake()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
    }

    void Update()
    {
        transform.position += Vector3.up * 2.0f * Time.deltaTime;
        float newZ = transform.position.z + Mathf.Sin(Time.time) * 0.01f;
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

        if (playerGhost && !generator.isTherePlayer1 && !generator.isTherePlayer2)
        {
            if (Input.GetButtonDown("A") || Input.GetButtonDown("A2"))
            {
                GameOver();
                foreach (DieScript_FG ghost in GameObject.FindObjectsOfType<DieScript_FG>())
                {
                    Destroy(ghost.gameObject);
                }
            }
            if (firstSend)
            {
                generator.GetComponent<SoundManager_FG>().EndSound();
                firstSend = false;
            }
        }

        if (transform.position.y >= 15)
        {
            bool thereIsAnother = false;
            foreach (DieScript_FG script in FindObjectsOfType<DieScript_FG>())
            {
                if (script.playerGhost && script != this && Vector3.Distance(transform.position, script.transform.position) > 0.5)
                {
                    thereIsAnother = true;
                    break;
                }
            }

            if (!generator.isTherePlayer1 && !generator.isTherePlayer2 && playerGhost && !thereIsAnother)
            {
                GameOver();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void GameOver()
    {
        GameData.score1 = generator.player1Score;
        GameData.score2 = generator.player2Score;
        SceneManager.LoadScene("End(FG)");
    }
}
