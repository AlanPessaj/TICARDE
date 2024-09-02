using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    bool serving;
    float stepSize;
    public float initialStepSize;
    public float maxBallHeight;
    public bool startServing;
    // Start is called before the first frame update
    void Start()
    {
        if (startServing)
        {
            StartServe(player1);
        }
        stepSize = initialStepSize;
    }
    float ballHeight;
    // Update is called once per frame
    void Update()
    {
        if (player1.GetComponent<PlayerController_FT>().slowMotion && player2.GetComponent<PlayerController_FT>().slowMotion)
        {

        }
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
        player1.transform.position = new Vector3(-50, 4, -30);
        player2.transform.position = new Vector3(50, 4, 30);
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
}
