﻿using System.Collections;
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
    public ShotManager_FT shot;
    bool isPlayer1;
    public HitManager_FT hitManager;
    public ShotManager_FT simShot;
    public float timeSlow;
    public float minPower;
    public float maxPower;
    // Start is called before the first frame update
    void Start()
    {
        timeSlow += 1;
        isPlayer1 = gameObject.name == "Player1";
        shot = gameManager.gameObject.GetComponent<ShotManager_FT>();
    }
    float power;
    int direction;
    float driveRotation;
    float lobRotation;
    float smashRotation;
    bool chargingDrive;
    bool chargingLob;
    bool chargingSmash;
    public bool doingDrive;
    public bool doingLob;
    public bool doingSmash;
    // Update is called once per frame
    public void Update()
    {
        if (isPlayer1)
        {
            if (gameManager.serve == 1)
            {
                //PLAYER 1
                if (Input.GetKey(KeyCode.W) && transform.position.x < -45 && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3((movementSpeed * Time.deltaTime) / 4, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.S) && transform.position.x > -50 && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3((-movementSpeed * Time.deltaTime) / 4, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.A) && transform.position.z < 0 && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(0, 0, (movementSpeed * Time.deltaTime)/4), Space.World);
                }
                if (Input.GetKey(KeyCode.D) && transform.position.z > -30 && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(0, 0, (-movementSpeed * Time.deltaTime)/4), Space.World);
                }
                if (Input.GetButtonDown("A"))
                {
                    gameManager.ThrowBall();
                }
                CheckButtons(true);
            }
            else if(gameManager.serve == 2)
            {
                if (Input.GetKey(KeyCode.S) && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(-movementSpeed * 1 * Time.deltaTime, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.A) && transform.position.z < 0 && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(0, 0, (movementSpeed * Time.deltaTime)), Space.World);
                }
                if (Input.GetKey(KeyCode.W) && transform.position.x < -30 && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3((movementSpeed * Time.deltaTime), 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.D) && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(0, 0, -movementSpeed * 1 * Time.deltaTime), Space.World);
                }
            }
            else if (gameManager.serve == 0)
            {
                if (Input.GetKey(KeyCode.W) && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(movementSpeed * 1 * Time.deltaTime, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.A) && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(0, 0, movementSpeed * 1 * Time.deltaTime), Space.World);
                }
                if (Input.GetKey(KeyCode.S) && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(-movementSpeed * 1 * Time.deltaTime, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.D) && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(0, 0, -movementSpeed * 1 * Time.deltaTime), Space.World);
                }
                CheckButtons();
            }
        }
        else
        {
            if(gameManager.serve == 2)
            {
                if (Input.GetKey(KeyCode.UpArrow) && transform.position.x < 50 && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3((movementSpeed * Time.deltaTime)/4, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.LeftArrow) && transform.position.z < 30 && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(0, 0, (movementSpeed * Time.deltaTime)/4), Space.World);
                }
                if (Input.GetKey(KeyCode.DownArrow) && transform.position.x > 45 && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3((-movementSpeed * Time.deltaTime)/4, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.RightArrow) && transform.position.z > 0 && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(0, 0, (-movementSpeed * Time.deltaTime)/4), Space.World);
                }
                if (Input.GetButtonDown("A"))
                {
                    gameManager.ThrowBall();
                }
                CheckButtons(true);
            }
            else if (gameManager.serve == 1)
            {
                if (Input.GetKey(KeyCode.DownArrow) && transform.position.x > 30 && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(movementSpeed * -1 * Time.deltaTime, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.LeftArrow) && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(0, 0, (-movementSpeed * -1 * Time.deltaTime)), Space.World);
                }
                if (Input.GetKey(KeyCode.UpArrow) && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3((-movementSpeed * -1 * Time.deltaTime), 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.RightArrow) && transform.position.z > 0 && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(0, 0, movementSpeed * -1 * Time.deltaTime), Space.World);
                }
            }
            else if(gameManager.serve == 0)
            {
                if (Input.GetKey(KeyCode.UpArrow) && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(movementSpeed * 1 * Time.deltaTime, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.LeftArrow) && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(0, 0, movementSpeed * 1 * Time.deltaTime), Space.World);
                }
                if (Input.GetKey(KeyCode.DownArrow) && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(-movementSpeed * 1 * Time.deltaTime, 0, 0), Space.World);
                }
                if (Input.GetKey(KeyCode.RightArrow) && !doingDrive && !doingSmash && !doingLob && !chargingDrive && !chargingLob && !chargingSmash && driveRotation == 0 && lobRotation == 0 && smashRotation == 0)
                {
                    transform.Translate(new Vector3(0, 0, -movementSpeed * 1 * Time.deltaTime), Space.World);
                }
                CheckButtons();
            }
        }
        if (simShot != null)
        {
            CheckPrediction();
        }
    }

    void CheckPrediction()
    {
        if (doingDrive && hitManager.hColliders[1] != null)
        {
            simShot.ballHit = true;
        }
        if (doingLob && hitManager.hColliders[2] != null)
        {
            simShot.ballHit = true;
        }
        if (doingSmash && hitManager.hColliders[0] != null)
        {
            simShot.ballHit = true;
        }
    }
    
    void SlowMotion(float targetSpeed)
    {
        if (Time.timeScale != targetSpeed && simShot == null)
        {
            if (Time.timeScale > targetSpeed)
            {
                Time.timeScale -= 0.05f;
            }
            else
            {
                Time.timeScale += 0.05f;
            }
        }
        if (Time.timeScale > targetSpeed - 0.05f && Time.timeScale < targetSpeed + 0.05f)
        {
            Time.timeScale = targetSpeed;
        }
    }

    void CheckDirection()
    {
        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    direction = -1;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    direction = 1;
                }
                else
                {
                    direction = 0;
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                direction = -2;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction = 2;
            }
            else
            {
                //a discutir
                direction = 0;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    direction = -1;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    direction = 1;
                }
                else
                {
                    direction = 0;
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction = -2;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                direction = 2;
            }
            else
            {
                //a discutir
                direction = 0;
            }
        }
    }

    void CheckButtons(bool serve = false)
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
                if (!doingSmash && !doingLob && !chargingSmash && !chargingLob && smashRotation == 0 && lobRotation == 0)
                {
                    if (Input.GetButtonDown("A" + player))
                    {
                        if (simShot == null)
                        {
                            if (driveRotation == 0)
                            {
                                float[] results = shot.PredictShot(gameObject, ShotType.drive);
                                if (results != null)
                                {
                                    chargingDrive = true;
                                    racket.transform.Rotate(-90, 0, 0);
                                    //empezar a moverse
                                    Debug.Log("drive");
                                }
                                else
                                {
                                    //animacion de no pegarle
                                }
                            }
                            else
                            {
                                power = driveRotation;
                                doingDrive = true;
                                driveRotation = 0;
                                SlowMotion(1);
                            }
                        }
                    }
                    if (chargingDrive)
                    {
                        SlowMotion(1 / timeSlow);
                        timer += Time.deltaTime;
                        driveRotation += Time.deltaTime * racketSpeed * (1 / Time.timeScale) / 4;
                        racketPivot.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(0, 45, driveRotation), 0);
                    }
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
                if (!doingSmash && !doingDrive && !chargingSmash && !chargingDrive && smashRotation == 0 && driveRotation == 0)
                {
                    if (Input.GetButtonDown("B" + player))
                    {
                        if (simShot == null)
                        {
                            if (lobRotation == 0)
                            {
                                float[] results = shot.PredictShot(gameObject, ShotType.lob);
                                if (results != null)
                                {
                                    chargingLob = true;
                                    racket.transform.Rotate(0, 0, 180);
                                    racket.transform.localPosition = new Vector3(0, -0.25f, -3f);
                                    //empezar a moverse
                                    Debug.Log("lob");
                                }
                                else
                                {
                                    //animacion de no pegarle
                                }
                            }
                            else
                            {
                                power = lobRotation;
                                doingLob = true;
                                lobRotation = 0;
                                SlowMotion(1);
                            }
                        }
                    }
                    if (chargingLob)
                    {
                        SlowMotion(1 / timeSlow);
                        timer += Time.deltaTime;
                        lobRotation += Time.deltaTime * racketSpeed * (1 / Time.timeScale) / 4;
                        racketPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, -45, lobRotation));
                    }
                }
            }
        }
        if (Input.GetButton("C" + player) && !Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            //C
            if (!doingDrive && !chargingDrive && !chargingLob && !doingLob && driveRotation == 0 && lobRotation == 0)
            {
                if (Input.GetButtonDown("C" + player))
                {
                    if (simShot == null)
                    {
                        if (smashRotation == 0)
                        {
                            float[] results = shot.PredictShot(gameObject, ShotType.smash);
                            if (results != null)
                            {
                                chargingSmash = true;
                                racket.transform.localPosition = new Vector3(0, 1f, -3f);
                                //empezar a moverse
                                Debug.Log("smash");
                            }
                            else
                            {
                                //animacion de no pegarle
                            }
                        }
                        else
                        {
                            power = smashRotation;
                            doingSmash = true;
                            smashRotation = 0;
                            SlowMotion(1);
                        }
                    }
                }
                if (chargingSmash)
                {
                    SlowMotion(1 / timeSlow);
                    timer += Time.deltaTime;
                    smashRotation += Time.deltaTime * racketSpeed * (1 / Time.timeScale) / 4;
                    racketPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, 45, smashRotation));
                }
            }
        }
        if (!Input.GetButton("C" + player) && !Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            //NONE
            if (driveRotation != 0 || lobRotation != 0 || smashRotation != 0)
            {
                SlowMotion(1);
            }
            if (Input.GetButtonUp("A" + player))
            {
                chargingDrive = false;
                CheckDirection();
            }
            if (Input.GetButtonUp("B" + player))
            {
                chargingLob = false;
                CheckDirection();
            }
            if (Input.GetButtonUp("C" + player))
            {
                chargingSmash = false;
                CheckDirection();
            }
            if (doingDrive && simShot == null)
            {
                driveRotation += Time.deltaTime * racketSpeed;
                racketPivot.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(45, -90, driveRotation), 0);
                if (hitManager.hColliders[1] != null)
                {
                    gameObject.name = gameObject.name;
                    shot.FindShot(direction, Mathf.Lerp(minPower, maxPower, power), ShotType.drive, gameObject);
                    driveRotation = 2;
                }
                if (driveRotation >= 1)
                {
                    racketPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
                    racket.transform.Rotate(90, 0, 0);
                    driveRotation = 0;
                    doingDrive = false;
                }
            }
            if (doingLob && simShot == null)
            {
                lobRotation += Time.deltaTime * racketSpeed;
                racketPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(-45, 90, lobRotation));
                if (hitManager.hColliders[2] != null)
                {
                    shot.FindShot(direction, Mathf.Lerp(minPower, maxPower, power), ShotType.lob, gameObject);
                    lobRotation = 2;
                }
                if (lobRotation >= 1)
                {
                    racketPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
                    racket.transform.Rotate(0, 0, -180);
                    racket.transform.localPosition = new Vector3(0, 0, -3f);
                    lobRotation = 0;
                    doingLob = false;
                }
            }
            if (doingSmash && simShot == null)
            {
                smashRotation += Time.deltaTime * racketSpeed;
                racketPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(45, -90, smashRotation));
                if (hitManager.hColliders[0] != null)
                {
                    gameObject.name = gameObject.name;
                    if (serve)
                    {
                        gameManager.EndServe();
                        shot.FindShot(-2, Mathf.Lerp(minPower, maxPower, power), ShotType.smash, gameObject);
                    }
                    else
                    {
                        shot.FindShot(direction, Mathf.Lerp(minPower, maxPower, power), ShotType.smash, gameObject);
                    }
                    smashRotation = 2;
                }
                if (smashRotation >= 1)
                {
                    racketPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
                    racket.transform.localPosition = new Vector3(0, 0, -3f);
                    smashRotation = 0;
                    doingSmash = false;
                }
            }
        }
    }
}
