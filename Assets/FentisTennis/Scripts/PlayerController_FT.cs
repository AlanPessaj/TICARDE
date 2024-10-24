﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController_FT : MonoBehaviour
{
    public float movementSpeed;
    float timer = 0.25f;
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
        speedConst = movementSpeed;
    }
    int direction;
    float driveRotation;
    float lobRotation;
    float smashRotation;
    public bool doingDrive;
    public bool doingLob;
    public bool doingSmash;
    float speedConst;
    bool canHit = true;
    // Update is called once per frame
    public void Update()
    {
        Vector3 movement = new Vector3();
        if (driveRotation != 0 || lobRotation != 0 || smashRotation != 0 || doingDrive || doingLob || doingSmash || (gameManager.serving && gameManager.serve == int.Parse(gameObject.name.Substring(gameObject.name.Length - 1))))
        {
            movementSpeed = 0;
        }
        else
        {
            movementSpeed = speedConst;
        }
        if (isPlayer1)
        {
            if(gameManager.serve == 2)
            {
                if (Input.GetKey(KeyCode.W) && transform.position.x < -30)
                {
                    movement += new Vector3(1, 0, 0);
                }
                if (Input.GetKey(KeyCode.A) && transform.position.z < 0)
                {
                    movement += new Vector3(0, 0, 1);
                }
                if (Input.GetKey(KeyCode.S) && transform.position.x > -50)
                {
                    movement += new Vector3(-1, 0, 0);
                }
                if (Input.GetKey(KeyCode.D) && transform.position.z > -30)
                {
                    movement += new Vector3(0, 0, -1);
                }
                CheckButtons();
            }
            if (gameManager.serve == 0)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    movement += new Vector3(1, 0, 0);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    movement += new Vector3(0, 0, 1);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    movement += new Vector3(-1, 0, 0);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    movement += new Vector3(0, 0, -1);
                }
                CheckButtons();
            }
            if (gameManager.serve == 1)
            {
                //PLAYER 1
                if (Input.GetKey(KeyCode.W) && transform.position.x < -45)
                {
                    movement += new Vector3(1, 0, 0);
                }
                if (Input.GetKey(KeyCode.A) && transform.position.z < 0)
                {
                    movement += new Vector3(0, 0, 1);
                }
                if (Input.GetKey(KeyCode.S) && transform.position.x > -50)
                {
                    movement += new Vector3(-1, 0, 0);
                }
                if (Input.GetKey(KeyCode.D) && transform.position.z > -30)
                {
                    movement += new Vector3(0, 0, -1);
                }
                if (Input.GetButtonDown("A"))
                {
                    gameManager.ThrowBall();
                }
                CheckButtons(true);
                transform.Translate(movement.normalized / 4 * Time.deltaTime * movementSpeed);
            }
            else
            {
                transform.Translate(movement.normalized * Time.deltaTime * movementSpeed);
            }
        }
        else
        {
            if (gameManager.serve == 1)
            {
                if (Input.GetKey(KeyCode.UpArrow) && transform.position.x < 50)
                {
                    movement += new Vector3(1, 0, 0);
                }
                if (Input.GetKey(KeyCode.LeftArrow) && transform.position.z < 30)
                {
                    movement += new Vector3(0, 0, 1);
                }
                if (Input.GetKey(KeyCode.DownArrow) && transform.position.x > 30)
                {
                    movement += new Vector3(-1, 0, 0);
                }
                if (Input.GetKey(KeyCode.RightArrow) && transform.position.z > 0)
                {
                    movement += new Vector3(0, 0, -1);
                }
                CheckButtons();
            }
            if(gameManager.serve == 0)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    movement += new Vector3(1, 0, 0);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    movement += new Vector3(0, 0, 1);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    movement += new Vector3(-1, 0, 0);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    movement += new Vector3(0, 0, -1);
                }
                CheckButtons();
            }
            if (gameManager.serve == 2)
            {
                if (Input.GetKey(KeyCode.UpArrow) && transform.position.x < 50)
                {
                    movement += new Vector3(1, 0, 0);
                }
                if (Input.GetKey(KeyCode.LeftArrow) && transform.position.z < 30)
                {
                    movement += new Vector3(0, 0, 1);
                }
                if (Input.GetKey(KeyCode.DownArrow) && transform.position.x > 45)
                {
                    movement += new Vector3(-1, 0, 0);
                }
                if (Input.GetKey(KeyCode.RightArrow) && transform.position.z > 0)
                {
                    movement += new Vector3(0, 0, -1);
                }
                if (Input.GetButtonDown("A2"))
                {
                    gameManager.ThrowBall();
                }
                CheckButtons(true);
                transform.Translate(movement.normalized / 4 * Time.deltaTime * -movementSpeed);
            }
            else
            {
                transform.Translate(movement.normalized * Time.deltaTime * -movementSpeed);
            }
        }
        if (!canHit)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                timer = 0.25f;
                canHit = true;
            }
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
                if (!doingSmash && !doingLob && !doingDrive && smashRotation == 0 && lobRotation == 0 && canHit)
                {
                    if (Input.GetButtonDown("A" + player))
                    {
                        racket.transform.Rotate(-90, 0, 0);
                        //empezar a moverse
                        Debug.Log("drive");
                    }
                    timer += Time.deltaTime;
                    driveRotation += Time.deltaTime * racketSpeed * (1 / Time.timeScale) / 4;
                    racketPivot.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(0, 45, driveRotation), 0);
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
                if (!doingSmash && !doingDrive && !doingLob && smashRotation == 0 && driveRotation == 0 && canHit)
                {
                    if (Input.GetButtonDown("B" + player))
                    {
                        racket.transform.Rotate(0, 0, 180);
                        racket.transform.localPosition = new Vector3(0, -0.25f, -3f);
                        //empezar a moverse
                        Debug.Log("lob");
                    }
                    timer += Time.deltaTime;
                    lobRotation += Time.deltaTime * racketSpeed * (1 / Time.timeScale) / 4;
                    racketPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, -45, lobRotation));
                }
            }
        }
        if (Input.GetButton("C" + player) && !Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            //C
            if (!doingDrive && !doingLob && !doingSmash && driveRotation == 0 && lobRotation == 0 && canHit)
            {
                if (Input.GetButtonDown("C" + player))
                {
                    racket.transform.localPosition = new Vector3(0, 1f, -3f);
                    //empezar a moverse
                    Debug.Log("smash");
                }
                timer += Time.deltaTime;
                smashRotation += Time.deltaTime * racketSpeed * (1 / Time.timeScale) / 4;
                racketPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, 45, smashRotation));
            }
        }
        if (!Input.GetButton("C" + player) && !Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            //NONE
            if (Input.GetButtonUp("A" + player) && !doingSmash && !doingLob && !doingDrive && smashRotation == 0 && lobRotation == 0 && canHit && !serve)
            {
                CheckDirection();
                doingDrive = true;
                driveRotation = Mathf.InverseLerp(45, -90, racketPivot.transform.eulerAngles.y);
            }
            if (Input.GetButtonUp("B" + player) && !doingSmash && !doingLob && !doingDrive && smashRotation == 0 && driveRotation == 0 && canHit && !serve)
            {
                CheckDirection();
                doingLob = true;
                lobRotation = Mathf.InverseLerp(315, 90, racketPivot.transform.localEulerAngles.z);
            }
            if (Input.GetButtonUp("C" + player) && !doingSmash && !doingDrive && !doingLob && driveRotation == 0 && lobRotation == 0 && canHit)
            {
                CheckDirection();
                doingSmash = true;
                smashRotation = Mathf.InverseLerp(45, -90, racketPivot.transform.localEulerAngles.z);
            }
            if (doingDrive && simShot == null)
            {
                driveRotation += Time.deltaTime * racketSpeed;
                racketPivot.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(45, -90, driveRotation), 0);
                if (hitManager.hColliders[1] != null)
                {
                    shot.ball.bounced = false;
                    shot.ball.wasPlayer1 = isPlayer1;
                    gameObject.name = gameObject.name;
                    shot.FindShot(direction, ShotType.drive, isPlayer1);
                    racketPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
                    racket.transform.Rotate(90, 0, 0);
                    driveRotation = 0;
                    doingDrive = false;
                }
                if (driveRotation >= 1)
                {
                    racketPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
                    racket.transform.Rotate(90, 0, 0);
                    driveRotation = 0;
                    doingDrive = false;
                    canHit = true;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -15);
                }
            }
            if (doingLob && simShot == null)
            {
                lobRotation += Time.deltaTime * racketSpeed;
                racketPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(-45, 90, lobRotation));
                if (hitManager.hColliders[2] != null)
                {
                    shot.ball.bounced = false;
                    shot.ball.wasPlayer1 = isPlayer1;
                    shot.FindShot(direction, ShotType.lob, isPlayer1);
                    racketPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
                    racket.transform.Rotate(0, 0, -180);
                    racket.transform.localPosition = new Vector3(0, 0, -3f);
                    lobRotation = 0;
                    doingLob = false;
                }
                if (lobRotation >= 1)
                {
                    racketPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
                    racket.transform.Rotate(0, 0, -180);
                    racket.transform.localPosition = new Vector3(0, 0, -3f);
                    lobRotation = 0;
                    doingLob = false;
                    canHit = true;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -15);
                }
            }
            if (doingSmash && simShot == null)
            {
                smashRotation += Time.deltaTime * racketSpeed;
                racketPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(45, -90, smashRotation));
                if (hitManager.hColliders[0] != null)
                {
                    if (serve)
                    {
                        gameManager.EndServe();
                        shot.FindShot(-2, ShotType.smash, isPlayer1, true);
                    }
                    else
                    {
                        shot.FindShot(direction, ShotType.smash, isPlayer1);
                    }
                    shot.ball.bounced = false;
                    shot.ball.wasPlayer1 = isPlayer1;
                    racketPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
                    racket.transform.localPosition = new Vector3(0, 0, -3f);
                    smashRotation = 0;
                    doingSmash = false;
                }
                if (smashRotation >= 1)
                {
                    racketPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
                    racket.transform.localPosition = new Vector3(0, 0, -3f);
                    smashRotation = 0;
                    doingSmash = false;
                    if (!serve)
                    {
                        canHit = true;
                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -15);
                    }
                }
            }
        }
    }
}
