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
    public GameManager_FT gameManager;
    ShotManager_FT shot;
    bool isPlayer1;
    public HitManager_FT hitManager;
    public ShotManager_FT simShot;
    // Start is called before the first frame update
    void Start()
    {
        isPlayer1 = gameObject.name == "Player1";
        shot = gameManager.gameObject.GetComponent<ShotManager_FT>();
    }
    float rotation;
    bool doingDrive = false;
    bool doingLob = false;
    bool doingSmash = false;
    // Update is called once per frame
    public void Update()
    {
        if (isPlayer1)
        {
            if (gameManager.serve == 1)
            {
                //PLAYER 1
                if (Input.GetKey(KeyCode.W) && transform.position.x < -45)
                {
                    transform.Translate(new Vector3((movementSpeed * Time.deltaTime) / 4, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.S) && transform.position.x > -50)
                {
                    transform.Translate(new Vector3((-movementSpeed * Time.deltaTime) / 4, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.A) && transform.position.z < 0)
                {
                    transform.Translate(new Vector3(0, 0, (movementSpeed * Time.deltaTime)/4), Space.World);
                }
                if (Input.GetKey(KeyCode.D) && transform.position.z > -30)
                {
                    transform.Translate(new Vector3(0, 0, (-movementSpeed * Time.deltaTime)/4), Space.World);
                }
                if (Input.GetButtonDown("A"))
                {
                    gameManager.ThrowBall();
                }
                checkButtons(true);
            }
            else if(gameManager.serve == 2)
            {
                if (Input.GetKey(KeyCode.S) && !doingDrive && !doingSmash && !doingLob)
                {
                    transform.Translate(new Vector3(-movementSpeed * 1 * Time.deltaTime, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.A) && transform.position.z < 0 && !doingDrive && !doingSmash && !doingLob)
                {
                    transform.Translate(new Vector3(0, 0, (movementSpeed * Time.deltaTime)), Space.World);
                }
                if (Input.GetKey(KeyCode.W) && transform.position.x < -30 && !doingDrive && !doingSmash && !doingLob)
                {
                    transform.Translate(new Vector3((movementSpeed * Time.deltaTime), 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.D) && !doingDrive && !doingSmash && !doingLob)
                {
                    transform.Translate(new Vector3(0, 0, -movementSpeed * 1 * Time.deltaTime), Space.World);
                }
            }
            else if (gameManager.serve == 0)
            {
                if (Input.GetKey(KeyCode.W) && !doingDrive && !doingSmash && !doingLob)
                {
                    transform.Translate(new Vector3(movementSpeed * 1 * Time.deltaTime, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.A) && !doingDrive && !doingSmash && !doingLob)
                {
                    transform.Translate(new Vector3(0, 0, movementSpeed * 1 * Time.deltaTime), Space.World);
                }
                if (Input.GetKey(KeyCode.S) && !doingDrive && !doingSmash && !doingLob)
                {
                    transform.Translate(new Vector3(-movementSpeed * 1 * Time.deltaTime, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.D) && !doingDrive && !doingSmash && !doingLob)
                {
                    transform.Translate(new Vector3(0, 0, -movementSpeed * 1 * Time.deltaTime), Space.World);
                }
                checkButtons();
            }
        }
        else
        {
            if(gameManager.serve == 2)
            {
                if (Input.GetKey(KeyCode.UpArrow) && transform.position.x < 50)
                {
                    transform.Translate(new Vector3((movementSpeed * Time.deltaTime)/4, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.LeftArrow) && transform.position.z < 30)
                {
                    transform.Translate(new Vector3(0, 0, (movementSpeed * Time.deltaTime)/4), Space.World);
                }
                if (Input.GetKey(KeyCode.DownArrow) && transform.position.x > 45)
                {
                    transform.Translate(new Vector3((-movementSpeed * Time.deltaTime)/4, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.RightArrow) && transform.position.z > 0)
                {
                    transform.Translate(new Vector3(0, 0, (-movementSpeed * Time.deltaTime)/4), Space.World);
                }
                if (Input.GetButtonDown("A"))
                {
                    gameManager.ThrowBall();
                }
                checkButtons(true);
            }
            else if (gameManager.serve == 1)
            {
                if (Input.GetKey(KeyCode.DownArrow) && transform.position.x > 30 && !doingDrive && !doingSmash && !doingLob)
                {
                    transform.Translate(new Vector3(movementSpeed * -1 * Time.deltaTime, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.LeftArrow) && !doingDrive && !doingSmash && !doingLob)
                {
                    transform.Translate(new Vector3(0, 0, (-movementSpeed * -1 * Time.deltaTime)), Space.World);
                }
                if (Input.GetKey(KeyCode.UpArrow) && !doingDrive && !doingSmash && !doingLob)
                {
                    transform.Translate(new Vector3((-movementSpeed * -1 * Time.deltaTime), 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.RightArrow) && transform.position.z > 0 && !doingDrive && !doingSmash && !doingLob)
                {
                    transform.Translate(new Vector3(0, 0, movementSpeed * -1 * Time.deltaTime), Space.World);
                }
            }
            else if(gameManager.serve == 0)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(new Vector3(movementSpeed * 1 * Time.deltaTime, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(new Vector3(0, 0, movementSpeed * 1 * Time.deltaTime), Space.World);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(new Vector3(-movementSpeed * 1 * Time.deltaTime, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Translate(new Vector3(0, 0, -movementSpeed * 1 * Time.deltaTime), Space.World);
                }
                checkButtons();
            }
        }
    }
    void checkButtons(bool serve = false)
    {
        string player = "";
        if (!isPlayer1)
        {
            player = "2";
        }
        if (Input.GetButton("A" + player) && !serve)
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
                if (!doingSmash && !doingLob)
                {
                    if (Input.GetButtonDown("A" + player))
                    {
                        doingDrive = true;
                        racket.transform.Rotate(-90, 0, 0);
                    }
                    timer += Time.deltaTime;
                    rotation += Time.deltaTime * racketSpeed / 2;
                    racketPivot.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(0, 45, rotation), 0);
                }
            }

        }
        if (Input.GetButton("B" + player) && !Input.GetButton("A" + player) && !serve)
        {
            if (Input.GetButton("C" + player))
            {
                //B + C
            }
            else
            {
                //B
                if (!doingSmash && !doingDrive)
                {
                    if (Input.GetButtonDown("B" + player))
                    {
                        doingLob = true;
                        racket.transform.Rotate(0, 0, 180);
                        racket.transform.localPosition = new Vector3(0, -0.25f, -3f);

                    }
                    timer += Time.deltaTime;
                    rotation += Time.deltaTime * racketSpeed / 2;
                    racketPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, -45, rotation));
                }
            }
        }
        if (Input.GetButton("C" + player) && !Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            //C
            if (!doingDrive && !doingLob)
            {
                if (Input.GetButtonDown("C" + player))
                {
                    doingSmash = true;
                    racket.transform.localPosition = new Vector3(0, 1f, -3f);

                }
                timer += Time.deltaTime;
                rotation += Time.deltaTime * racketSpeed / 2;
                racketPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, 45, rotation));
            }
        }
        if (!Input.GetButton("C" + player) && !Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            //NONE
            if (Input.GetButtonUp("A" + player))
            {
                rotation = 0;
                if (simShot != null)
                {
                    float[] results = shot.PredictShot(gameObject);
                    if (results != null )
                    {
                        if (Mathf.Abs(results[1]) > transform.GetChild(0).GetChild(1).GetComponent<BoxCollider>().size.z / 2)
                        {
                            //empezar a moverse
                            Debug.Log("drive");
                        }
                    }
                }
            }
            if (Input.GetButtonUp("B" + player))
            {
                rotation = 0;
                if (simShot != null)
                {
                    float[] results = shot.PredictShot(gameObject);
                    if (results != null)
                    {
                        if (Mathf.Abs(results[1]) > transform.GetChild(0).GetChild(2).GetComponent<BoxCollider>().size.z / 2)
                        {
                            //empezar a moverse
                            Debug.Log("lob");
                        }
                    }
                }
            }
            if (Input.GetButtonUp("C" + player))
            {
                rotation = 0;
                if (simShot != null)
                {
                    float[] results = shot.PredictShot(gameObject);
                    if (results != null)
                    {
                        if (Mathf.Abs(results[1]) > transform.GetChild(0).GetChild(0).GetComponent<BoxCollider>().size.z / 2)
                        {
                            //empezar a moverse
                            Debug.Log("smash");
                        }
                    }
                }
            }
            if (doingDrive)
            {
                rotation += Time.deltaTime * racketSpeed;
                racketPivot.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(45, -90, rotation), 0);
                if (hitManager.hColliders[1] != null)
                {
                    if (simShot == null)
                    {
                        shot.FindShot(-2, 60, ShotType.drive, gameObject);
                        rotation = 2;
                    }
                    else
                    {
                        simShot.ballHit = 2;
                    }
                }
                if (rotation >= 1)
                {
                    racketPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
                    racket.transform.Rotate(90, 0, 0);
                    rotation = 0;
                    doingDrive = false;
                }
            }
            if (doingLob)
            {
                rotation += Time.deltaTime * racketSpeed;
                racketPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(-45, 90, rotation));
                if (hitManager.hColliders[2] != null)
                {
                    if (simShot == null)
                    {
                        shot.FindShot(-2, 60, ShotType.lob, gameObject);
                        rotation = 2;
                    }
                    else
                    {
                        simShot.ballHit = 2;
                    }
                }
                if (rotation >= 1)
                {
                    racketPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
                    racket.transform.Rotate(0, 0, -180);
                    racket.transform.localPosition = new Vector3(0, 0, -3f);
                    rotation = 0;
                    doingLob = false;
                }
            }
            if (doingSmash)
            {
                rotation += Time.deltaTime * racketSpeed;
                racketPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(45, -90, rotation));
                if (hitManager.hColliders[0] != null)
                {
                    if (simShot == null)
                    {
                        if (serve)
                        {
                        gameManager.EndServe();
                        }
                        shot.FindShot(-2, 60, ShotType.smash, gameObject);
                        rotation = 2;
                    }
                    else
                    {
                        simShot.ballHit = 2;
                    }
                }
                if (rotation >= 1)
                {
                    racketPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
                    racket.transform.localPosition = new Vector3(0, 0, -3f);
                    rotation = 0;
                    doingSmash = false;
                }
            }
        }
    }
}
