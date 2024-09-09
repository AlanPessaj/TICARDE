using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager_FT : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public ShotManager_FT shotmanager;
    public BallMover_FT ballmover;
    public int serve = 0;
    bool readyToServe;
    bool throwingBall = false;
    public bool serving;
    float stepSize;
    public float initialStepSize;
    public float maxBallHeight;
    public int startServing;
    public int points1 = 0;
    public int points2 = 0;
    public int games1 = 0;
    public int games2 = 0;
    public GameObject player1Canvas;
    public GameObject player2Canvas;
    string player1Name = "Player1";
    string player2Name = "Player2";
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        player1Canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player1Name;//Name
        player2Canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player2Name;//Name
        switch (startServing)
        {
            case 1:
                StartServe(player1);
            break;
            case 2:
                StartServe(player2);
            break;
        }
        stepSize = initialStepSize;
    }
    float ballHeight;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game(FT)");
        }
        if (serving)
        {
            if (throwingBall)
            {
                stepSize = (1 - ballHeight) * initialStepSize + 0.1f;
                ballHeight += Time.deltaTime * stepSize;
                ballmover.transform.localPosition = new Vector3(ballmover.transform.localPosition.x, Mathf.Lerp(0, maxBallHeight, ballHeight), ballmover.transform.localPosition.z);
                if (ballHeight >= 1)
                {
                    ballHeight = 0;
                    throwingBall = false;
                    stepSize = initialStepSize;
                }
            }
            else
            {
                stepSize = ballHeight * initialStepSize + 0.1f;
                ballHeight += Time.deltaTime * stepSize;
                ballmover.transform.localPosition = new Vector3(ballmover.transform.localPosition.x, Mathf.Lerp(maxBallHeight, 0, ballHeight), ballmover.transform.localPosition.z);
                if (ballHeight >= 1)
                {
                    ballHeight = 0;
                    serving = false;
                    stepSize = initialStepSize;
                }
            }
        }
    }

    public void StartServe(GameObject player)
    {
        if(player == player1)
        {
            serve = 1;
        }
        else
        {
            serve = 2;
        }
        player1.transform.position = new Vector3(-50, 6, -30);
        player2.transform.position = new Vector3(50, 6, 30);
        ballmover.active = false;
        ballmover.transform.parent = player.transform;
        ballmover.transform.localPosition = new Vector3(1.5f, 0, 0);
        readyToServe = true;
    }

    public void EndServe()
    {
        serve = 0;
        ballmover.active = true;
        ballmover.transform.parent = null;
        serving = false;
        readyToServe = false;
    }

    public void ThrowBall()
    {
        if (readyToServe)
        {
            serving = true;
            throwingBall = true;
        }
    }

    public void AddPoint(GameObject scorer)
    {
        if (scorer.name == "Player1")
        {
            points1++;
            if (points2 == 4 && points1 == 4)
            {
                points2--;
                points1--;
            }
            if (points2 < 4 && points1 >= 5)
            {
                games1++;
                points1 = 0;
                points2 = 0;
            }
            if (points2 < 3 && points2 > 0 && points1 >= 4)
            {
                games1++;
                points1 = 0;
                points2 = 0;
            }
            if (points2 == 0 && points1 >= 3)
            {
                games1++;
                points1 = 0;
                points2 = 0;
            }
            player1Canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = games1.ToString();
            switch (points1)
            {
                case 0:
                    player1Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "0";
                    break;
                case 1:
                    player1Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "15";
                    break;
                case 2:
                    player1Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "30";
                    break;
                case 3:
                    player1Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "40";
                    break;
                case 4:
                    player1Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "A";
                    break;
            }
            if (games1 >= 3)
            {
                canvas.transform.GetChild(4).gameObject.SetActive(true);
                canvas.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "GANADOR: " + player1Name;
                ResetPoints();
            }
        }
        else
        {
            points2++;
            if (points1 == 4 && points2 == 4)
            {
                points2--;
                points1--;
            }
            if (points1 < 4 && points2 >= 5)
            {
                games2++;
                points1 = 0;
                points2 = 0;
            }
            if (points1 < 3 && points1 > 0 && points2 >= 4)
            {
                games2++;
                points1 = 0;
                points2 = 0;
            }
            if (points1 == 0 && points2 >= 3)
            {
                games2++;
                points1 = 0;
                points2 = 0;
            }
            player2Canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = games2.ToString();
            switch (points2)
            {
                case 0:
                    player2Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "0";
                    break;
                case 1:
                    player2Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "15";
                    break;
                case 2:
                    player2Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "30";
                    break;
                case 3:
                    player2Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "40";
                    break;
                case 4:
                    player2Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "A";
                    break;
            }
            if (games2 >= 3)
            {
                canvas.transform.GetChild(4).gameObject.SetActive(true);
                canvas.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "GANADOR: " + player2Name;
                ResetPoints();
            }
        }
        if ((games1 + games2) % 2 == 0)
        {
            StartServe(player1);
        }
        else
        {
            StartServe(player2);
        }
    }
    void ResetPoints()
    {
        games1 = 0;
        games2 = 0;
        points1 = 0;
        points2 = 0;
        player1Canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = games1.ToString();
        player2Canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = games2.ToString();
        player2Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "0";
        player1Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "0";
    }
}
