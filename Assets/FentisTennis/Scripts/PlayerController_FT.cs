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
    public GameManager gamemanager;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float rotation;
    bool chargingShot = true;
    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Player1")
        {
            if (gamemanager.serve == 1)
            {
                //PLAYER 1
                if (Input.GetKey(KeyCode.W) && transform.position.x < -45)
                {
                    transform.Translate(new Vector3((movementSpeed * Time.deltaTime) / 4, 0, 0));
                }
                if (Input.GetKey(KeyCode.S) && transform.position.x > -50)
                {
                    transform.Translate(new Vector3((-movementSpeed * Time.deltaTime) / 4, 0, 0));
                }
                if (Input.GetKey(KeyCode.A) && transform.position.z < 0)
                {
                    transform.Translate(new Vector3(0, 0, (movementSpeed * Time.deltaTime)/4));
                }
                if (Input.GetKey(KeyCode.D) && transform.position.z > -30)
                {
                    transform.Translate(new Vector3(0, 0, (-movementSpeed * Time.deltaTime)/4));
                }

            }
            else if(gamemanager.serve == 2)
            {
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(new Vector3(-movementSpeed * 1 * Time.deltaTime, 0, 0));
                }
                if (Input.GetKey(KeyCode.A) && transform.position.z < 0)
                {
                    transform.Translate(new Vector3(0, 0, (movementSpeed * Time.deltaTime)));
                }
                if (Input.GetKey(KeyCode.W) && transform.position.x < -30)
                {
                    transform.Translate(new Vector3((movementSpeed * Time.deltaTime), 0, 0));
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Translate(new Vector3(0, 0, -movementSpeed * 1 * Time.deltaTime));
                }
            }
            else if (gamemanager.serve == 0)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(new Vector3(movementSpeed * 1 * Time.deltaTime, 0, 0));
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
                    if (chargingShot)
                    {
                        rotation += Time.deltaTime * racketSpeed / 2;
                        racketPivot.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(0, 45, rotation), 0);
                        chargingShot = rotation <= 1;
                        if (!chargingShot)
                            rotation = 0;
                    }
                    else
                    {
                        rotation += Time.deltaTime * racketSpeed;
                        racketPivot.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(45, -90, rotation), 0);
                    }
                }
                if (Input.GetButtonUp("A"))
                {
                    racket.transform.Rotate(90, 0, 0);
                    racketPivot.transform.rotation = Quaternion.Euler(0, 0, 0);
                    rotation = 0;
                    chargingShot = true;
                }
            }
        }
        else
        {
            if(gamemanager.serve == 2)
            {
                if (Input.GetKey(KeyCode.UpArrow) && transform.position.x < 50)
                {
                    transform.Translate(new Vector3((movementSpeed * Time.deltaTime)/4, 0, 0));
                }
                if (Input.GetKey(KeyCode.LeftArrow) && transform.position.z < 30)
                {
                    transform.Translate(new Vector3(0, 0, (movementSpeed * Time.deltaTime)/4));
                }
                if (Input.GetKey(KeyCode.DownArrow) && transform.position.x > 45)
                {
                    transform.Translate(new Vector3((-movementSpeed * Time.deltaTime)/4, 0, 0));
                }
                if (Input.GetKey(KeyCode.RightArrow) && transform.position.z > 0)
                {
                    transform.Translate(new Vector3(0, 0, (-movementSpeed * Time.deltaTime)/4));
                }
                if (Input.GetButtonDown("A2"))
                {
                    racket.transform.Rotate(-90, 0, 0);
                }
            }
            else if (gamemanager.serve == 1)
            {
                if (Input.GetKey(KeyCode.DownArrow) && transform.position.x > 30)
                {
                    transform.Translate(new Vector3(movementSpeed * -1 * Time.deltaTime, 0, 0));
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(new Vector3(0, 0, (-movementSpeed * -1 * Time.deltaTime)));
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(new Vector3((-movementSpeed * -1 * Time.deltaTime), 0, 0));
                }
                if (Input.GetKey(KeyCode.RightArrow) && transform.position.z > 0)
                {
                    transform.Translate(new Vector3(0, 0, movementSpeed * -1 * Time.deltaTime));
                }
            }
            else if(gamemanager.serve == 0)
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
                if (Input.GetButtonDown("A2"))
                {
                    racket.transform.Rotate(-90, 0, 0);
                }
                if (Input.GetButton("A2"))
                {

                    timer += Time.deltaTime;
                    if (chargingShot)
                    {
                        rotation += Time.deltaTime * racketSpeed / 2;
                        racketPivot.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(180, 225, rotation), 0);
                        chargingShot = rotation <= 1;
                        if (!chargingShot)
                            rotation = 0;
                    }
                    else
                    {
                        rotation += Time.deltaTime * racketSpeed;
                        racketPivot.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(225, 90, rotation), 0);
                    }
                }
                if (Input.GetButtonUp("A2"))
                {
                    racket.transform.Rotate(90, 0, 0);
                    racketPivot.transform.rotation = Quaternion.Euler(0, 180, 0);
                    rotation = 0;
                    chargingShot = true;
                }
            }
        }
    }
}
