using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    new public GameObject camera;
    Vector3 initialPosition;
    Vector3 averagePosition;
    Vector3 initAveragePosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = camera.transform.position;
        averagePosition = (player1.transform.position - player2.transform.position) / 2;
        initAveragePosition = averagePosition;
    }

    Vector3 offset;
    int counter;
    // Update is called once per frame
    void Update()
    {
        averagePosition = (player1.transform.position + player2.transform.position) / 2;
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
    }
}
