using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager_FT : MonoBehaviour
{
    public static GameManager_FT instance;
    public GameObject player1;
    public GameObject player2;
    public ShotManager_FT shotmanager;
    public BallMover_FT ballmover;
    public Transform environment;
    public new Transform light;
    public int serve = 0;
    public bool throwingBall = false;
    public bool serving;
    float stepSize;
    public float initialStepSize;
    public float maxBallHeight;
    public int startServing;
    [HideInInspector] public int points1 = 0;
    [HideInInspector] public int points2 = 0;
    [HideInInspector] public int games1 = 0;
    [HideInInspector] public int games2 = 0;
    [HideInInspector] public int score1 = 0;
    [HideInInspector] public int score2 = 0;
    public Material[] characters;
    public GameObject player1Canvas;
    public GameObject player2Canvas;
    public GameObject canvas;
    public GameObject transition;
    public int lastServePlayer1 = 1;
    bool goingToServe;
    bool readyToServe;
    float servingProgress;
    Vector3 player1PreServePos;
    Vector3 player2PreServePos;
    float ballHeight;
    public bool justServed = true;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player1.GetComponent<Renderer>().material = characters[GameData.char1];
        player2.GetComponent<Renderer>().material = characters[GameData.char2];
        player1Canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameData.name1;//Name
        player2Canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameData.name2;//Name
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
        if (goingToServe)
        {
            if (Mathf.Approximately(player1.transform.position.x, -50) && Mathf.Approximately(player1.transform.position.z, -30) && Mathf.Approximately(player2.transform.position.x, 50) && Mathf.Approximately(player2.transform.position.z, 30))
            {
                servingProgress = 1;
            }
            servingProgress += Time.deltaTime;
            player1.transform.position = Vector3.Lerp(player1PreServePos, new Vector3(-50, 6, -30), servingProgress);
            player2.transform.position = Vector3.Lerp(player2PreServePos, new Vector3(50, 6, 30), servingProgress);
            if (servingProgress >= 1)
            {
                servingProgress = 0;
                goingToServe = false;
                readyToServe = true;
            }
        }
    }
    public void StartServe(GameObject player)
    {
        ballmover.transform.position = Vector3.zero;
        if(player == player1)
        {
            serve = 1;
            environment.eulerAngles = Vector3.zero;
            light.eulerAngles = new Vector3(50, -30, 0);
            if (lastServePlayer1 != 1) GetComponent<CameraController_FT>().ResetPos();
            lastServePlayer1 = 1;
        }
        else
        {
            serve = 2;
            environment.eulerAngles = Vector3.up * 180;
            light.eulerAngles = new Vector3(50, 150, 0);
            if (lastServePlayer1 == 1) GetComponent<CameraController_FT>().ResetPos();
            lastServePlayer1 = -1;
        }
        goingToServe = true;
        player1PreServePos = player1.transform.position;
        player2PreServePos = player2.transform.position;
        player1.GetComponent<PlayerController_FT>().ResetRaquet();
        player2.GetComponent<PlayerController_FT>().ResetRaquet();
        ballmover.active = false;
        ballmover.transform.parent = player.transform;
        ballmover.transform.localPosition = new Vector3(1.5f, 0, 0);
    }

    public void EndServe()
    {
        ballHeight = 0;
        serve = 0;
        stepSize = initialStepSize;
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
        if (!readyToServe && !serving)
        {
            if (scorer.name == "Player1")
            {
                points1++;
                score1 += 10;
                if (points2 == 4 && points1 == 4)
                {
                    points2--;
                    points1--;
                }
                if (points2 < 4 && points1 >= 5)
                {
                    games1++;
                    score1 += 50;
                    points1 = 0;
                    points2 = 0;
                }
                if (points2 < 3 && points2 > 0 && points1 >= 4)
                {
                    games1++;
                    score1 += 50;
                    points1 = 0;
                    points2 = 0;
                }
                if (points2 == 0 && points1 >= 3)
                {
                    games1++;
                    score1 += 50;
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
                if (games1 >= 3)
                {
                    StartCoroutine(NextScene());
                }
            }
            else
            {
                points2++;
                score2 += 10;
                if (points1 == 4 && points2 == 4)
                {
                    points2--;
                    points1--;
                }
                if (points1 < 4 && points2 >= 5)
                {
                    games2++;
                    score2 += 50;
                    points1 = 0;
                    points2 = 0;
                }
                if (points1 < 3 && points1 > 0 && points2 >= 4)
                {
                    games2++;
                    score2 += 50;
                    points1 = 0;
                    points2 = 0;
                }
                if (points1 == 0 && points2 >= 3)
                {
                    games2++;
                    score2 += 50;
                    points1 = 0;
                    points2 = 0;
                }
                player2Canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = games2.ToString();
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
                    StartCoroutine(NextScene());
                }
            }
            GetComponents<AudioSource>()[1].Play();
            HandleServe();
        }
    }


    public void HandleServe()
    {
        if ((games1 + games2) % 2 == 0) StartServe(player1);
        else StartServe(player2);
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

    IEnumerator NextScene()
    {
        if (games1 >= 3) score1 = Mathf.RoundToInt(score1 * 1.5f); else score2 = Mathf.RoundToInt(score2 * 1.5f);
        SceneManager.LoadScene("END(FT)", LoadSceneMode.Additive);
        yield return null;
        SceneManager.GetSceneByName("END(FT)").GetRootGameObjects()[0].GetComponent<Points_FT>().UpdateScore(score1, score2);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
