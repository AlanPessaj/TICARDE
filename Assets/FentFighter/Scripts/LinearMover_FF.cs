using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMover_FF : MonoBehaviour
{
    public float speed;
    public Vector3 angularSpeed;
    public bool goingLeft;
    // Start is called before the first frame update
    void Start()
    {
        if (goingLeft) speed *= -1;
        if (goingLeft) angularSpeed *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 18 && transform.position.x > -18)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
            transform.GetChild(0).Rotate(angularSpeed);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
