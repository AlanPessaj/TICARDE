using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_FT : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    new public Camera camera;
    Vector3 initialPosition;
    public Transform ball;
    public bool resetPos;
    Vector3 oldBall;
    public void Start()
    {
        initialPosition = camera.transform.localPosition;
    }

    void Update()
    {
        if (ball.position.y > -2)
        {
            if (Vector3.Distance(initialPosition, camera.transform.localPosition) > 20) camera.transform.localPosition = initialPosition;
            if (player1.GetComponent<PlayerController_FT>().gameManager.lastServePlayer1 == 1) camera.transform.localPosition += (ball.position - oldBall) / 10;
            else camera.transform.localPosition += new Vector3((oldBall.x - ball.position.x) / 10, (oldBall.y - ball.position.y) / -10, (oldBall.z - ball.position.z) / 10);

            oldBall = ball.position;
            if (resetPos)
            {
                camera.transform.localPosition = initialPosition; 
                resetPos = false;
            }
        }
    }
}
