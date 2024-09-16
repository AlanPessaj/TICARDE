using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_FG : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public Generator_FG generator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!((player1.position.x + player2.position.x) / 2 < (generator.distance - generator.despawnRadius) + 7.5f || player1.position.x == 0))
        {
            transform.position = new Vector3((player1.position.x + player2.position.x) / 2 - 5.5f, transform.position.y, transform.position.z);
        }
    }
}
