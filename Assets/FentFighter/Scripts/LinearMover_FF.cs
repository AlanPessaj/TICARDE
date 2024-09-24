using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMover_FF : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 18 && transform.position.x > -18)
        {
            transform.Translate(0, speed * Time.deltaTime, 0, Space.Self);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
