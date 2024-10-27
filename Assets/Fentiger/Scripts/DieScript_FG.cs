using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieScript_FG : MonoBehaviour
{
    public bool playerGhost;
    public Generator_FG generator;

    private void Awake()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
    }

    void Update()
    {
        transform.position += Vector3.up * 2.0f * Time.deltaTime;
        float newZ = transform.position.z + Mathf.Sin(Time.time) * 0.01f;
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

        if (transform.position.y >= 15)
        {
            bool thereIsAnother = false;
            foreach (DieScript_FG script in FindObjectsOfType<DieScript_FG>())
            {
                if (script.playerGhost && script != this)
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
        generator.GetComponent<SoundManager_FG>().Ended = true;
        SceneManager.sceneLoaded += OnEndSceneLoaded;
        SceneManager.LoadScene("End(FG)", LoadSceneMode.Additive);
    }

    private void OnEndSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "End(FG)")
        {
            EndManager_FG endManager = scene.GetRootGameObjects()[0].GetComponent<EndManager_FG>();
            endManager.player1Name = generator.player1Name;
            endManager.player2Name = generator.player2Name;
            endManager.player1Score = generator.player1Score;
            endManager.player2Score = generator.player2Score;
            endManager.UpdateValues();
            SceneManager.UnloadSceneAsync(generator.gameObject.scene);
            SceneManager.sceneLoaded -= OnEndSceneLoaded;
        }
    }
}
