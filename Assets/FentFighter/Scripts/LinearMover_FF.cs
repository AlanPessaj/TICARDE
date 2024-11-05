using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMover_FF : MonoBehaviour
{
    public float speed;
    public float angularSpeed;
    public bool goingLeft;
    // Start is called before the first frame update
    void Start()
    {
        if (goingLeft) speed *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 18 && transform.position.x > -18)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            transform.Rotate(angularSpeed * Vector3.forward);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
