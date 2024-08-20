using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_FT : MonoBehaviour
{
    public int movementSpeed;
    float timer;
    public int racketSpeed;
    public GameObject racket;
    public GameObject racketPivot;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float rotation = 1;
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


            if (Input.GetButtonDown("A"))
            {
                racket.transform.Rotate(-90, 0, 0);
            }
            if (Input.GetButton("A"))
            {
                
                timer += Time.deltaTime;
                rotation -= Time.deltaTime * racketSpeed;
                racketPivot.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(-90, 0, rotation), 0);
            }
            if (Input.GetButtonUp("A"))
            {
                racket.transform.Rotate(90, 0, 0);
                racketPivot.transform.rotation = Quaternion.Euler(0, 0, 0);
                rotation = 1;
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(new Vector3(movementSpeed * 1 * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(new Vector3(0, 0, movementSpeed * 1 * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(new Vector3(-movementSpeed * 1 * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(new Vector3(0, 0, -movementSpeed * 1 * Time.deltaTime));
            }
        }
    }
}
