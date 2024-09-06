using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    new public Camera camera;
    Vector3 initialPosition;
    public Transform ball;
    Vector3 oldBall;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = camera.transform.position;
        /*averagePosition = (player1.transform.position - player2.transform.position) / 2;
        initAveragePosition = averagePosition;*/

    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(initialPosition, camera.transform.position) > 20)
        {
            camera.transform.position = initialPosition;
        }
        camera.transform.position += (ball.position - oldBall) / 10;
        oldBall = ball.position;
    }

    void melavoyaguradarporlasdudas()
    {
        /*averagePosition = (player1.transform.position + player2.transform.position) / 2;
        if (Vector3.Distance(initialPosition, camera.transform.position) > 2)
        {
            camera.transform.position = initialPosition;
            counter = 20;
        }
        if (counter >= 9)
        {
            counter = 0;
        }
        //camera.transform.Translate(offset/10, Space.Self);
        Vector3 finalPosition = averagePosition + initialPosition;
        camera.transform.position = new Vector3(finalPosition.x, finalPosition.y, Mathf.Clamp(finalPosition.z, -21, 21));
        counter++;
        camera.fieldOfView = Mathf.Lerp(50, 60, Time.timeScale);*/
    }
}
