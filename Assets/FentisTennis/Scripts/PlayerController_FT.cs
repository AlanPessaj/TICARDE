using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_FT : MonoBehaviour
{
    public int movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name == "Player1")
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3 (movementSpeed*1*Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(0, 0, movementSpeed * 1 * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(-movementSpeed * 1 * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(0, 0, -movementSpeed * 1 * Time.deltaTime));
            }
        }
        else
        {

        }
    }
}
