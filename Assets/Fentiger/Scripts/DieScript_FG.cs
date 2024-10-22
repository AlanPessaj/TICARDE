using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieScript_FG : MonoBehaviour
{
    public Generator_FG generator;
    void Update()
    {
        transform.position += Vector3.up * 2.0f * Time.deltaTime;
        float newZ = transform.position.z + Mathf.Sin(Time.time) * 0.01f;
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        if (transform.position.y >= 15)
        {
            if (!generator.isTherePlayer1 && !generator.isTherePlayer2)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("End(FG)", LoadSceneMode.Additive);
        StartCoroutine(WaitAndExecute());
    }

    private IEnumerator WaitAndExecute()
    {
        yield return null;
        Scene end = SceneManager.GetSceneByName("End(FG)");
        end.GetRootGameObjects()[0].GetComponent<EndManager_FG>().player1Name = generator.player1Name;
        end.GetRootGameObjects()[0].GetComponent<EndManager_FG>().player2Name = generator.player2Name;
        end.GetRootGameObjects()[0].GetComponent<EndManager_FG>().player1Score = generator.player1Score;
        end.GetRootGameObjects()[0].GetComponent<EndManager_FG>().player2Score = generator.player2Score;
        end.GetRootGameObjects()[0].GetComponent<EndManager_FG>().UpdateValues();
        SceneManager.UnloadSceneAsync(gameObject.scene);

    }

    private void Awake()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
    }
}
