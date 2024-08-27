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
    bool isPlayer1;
    // Start is called before the first frame update
    void Start()
    {
        isPlayer1 = gameObject.name == "Player1";
    }
    float rotation;
    bool doingDrive = false;
    bool doingLob = false;
    // Update is called once per frame
    void Update()
    {
        if (isPlayer1)
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
                checkButtons();
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
                checkButtons();
            }
        }
    }
    void checkButtons()
    {
        string player = "";
        if (!isPlayer1)
        {
            player = "2";
        }
        if (Input.GetButton("A" + player))
        {
            if (Input.GetButton("B" + player))
            {
                if (Input.GetButton("C" + player))
                {
                    //A + B + C
                }
                else
                {
                    //A + B
                }
            }
            else if (Input.GetButton("C" + player))
            {
                //A + C
            }
            else
            {
                //A
                if (Input.GetButtonDown("A" + player))
                {
                    doingDrive = true;
                    racket.transform.Rotate(-90, 0, 0);
                }
                timer += Time.deltaTime;
                rotation += Time.deltaTime * racketSpeed / 2;
                if (isPlayer1)
                {
                    racketPivot.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(0, 45, rotation), 0);
                }
                else
                {
                    racketPivot.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(180, 225, rotation), 0);
                }
            }

        }
        if (Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            if (Input.GetButton("C" + player))
            {
                //B + C
            }
            else
            {
                //B
                if (Input.GetButtonDown("B" + player))
                {
                    doingLob = true;
                    racket.transform.Translate(0, -0.25f, 0, Space.World);
                    racket.transform.transform.position = new Vector3(racket.transform.position.x, racket.transform.position.y - 0.25f, racket.transform.position.z);
                }
                timer += Time.deltaTime;
                rotation += Time.deltaTime * racketSpeed / 2;
                racketPivot.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, -45, rotation));
            }
        }
        if (Input.GetButton("C" + player) && !Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            //C
        }
        if (!Input.GetButton("C" + player) && !Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            //NONE
            if (Input.GetButtonUp("A" + player))
            {
                rotation = 0;
            }
            if (Input.GetButtonUp("B" + player))
            {
                rotation = 0;
            }
            if (Input.GetButtonUp("C" + player))
            {
                rotation = 0;
            }
            if (doingDrive)
            {
                rotation += Time.deltaTime * racketSpeed;
                if (isPlayer1)
                {
                    racketPivot.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(45, -90, rotation), 0);
                }
                else
                {
                    racketPivot.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(225, 90, rotation), 0);
                }
                if (rotation >= 1)
                {
                    if (isPlayer1)
                    {
                        racketPivot.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        racketPivot.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    racket.transform.Rotate(90, 0, 0);
                    rotation = 0;
                    doingDrive = false;
                }
            }
            if (doingLob)
            {
                rotation += Time.deltaTime * racketSpeed;
                racketPivot.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(-45, 90, rotation));
                if (rotation >= 1)
                {
                    if (isPlayer1)
                    {
                        racketPivot.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        racketPivot.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    racket.transform.Rotate(0, 0, -180);
                    racket.transform.transform.position = new Vector3(racket.transform.position.x, racket.transform.position.y + 0.25f, racket.transform.position.z);
                    rotation = 0;
                    doingLob = false;
                }
            }
        }
    }
}
