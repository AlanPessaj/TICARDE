using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMover_FG : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < 18)
        {
            transform.Translate(0,0,speed);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
