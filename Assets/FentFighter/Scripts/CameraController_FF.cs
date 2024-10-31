using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_FF : MonoBehaviour
{
    public GameObject[] players;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((players[0].transform.position.x + players[1].transform.position.x) / 2, 2.3f, -10.4f);
    }
}
