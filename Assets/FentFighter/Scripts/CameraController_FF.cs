using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_FF : MonoBehaviour
{
    public Transform[] players;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((players[0].position.x + players[1].position.x) / 2, 2.3f, -10.4f);
    }
}
