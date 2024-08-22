using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camera;
    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = camera.transform.position;
    }

    Vector3 offset;
    int counter;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(initialPosition, camera.transform.position) > 2)
        {
            camera.transform.position = initialPosition;
            counter = 20;
        }
        if (counter >= 9)
        {
            offset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            counter = 0;
        }
        camera.transform.Translate(offset/10, Space.Self);
        counter++;
    }
}
