using System.Collections;
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
    GameObject transition;
    bool isPlayer1;
    public HitManager_FT hitManager;
    public ShotManager_FT simShot;
    public float timeSlow;
    public float minPower;
    public float maxPower;
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
    bool didDrive;
    bool didLob;
    bool didSmash;
    float speedConst;
    bool canHit = true;
    public static List<Frame> replay;
    public static bool inReplay;
    public static int frameIndex;
    public static Frame currentFrame;
    // Start is called before the first frame update
    void Start()
    {
        timeSlow += 1;
        isPlayer1 = gameObject.name == "Player1";
        shot = gameManager.gameObject.GetComponent<ShotManager_FT>();
        speedConst = movementSpeed;
        transition = gameManager.transition;
    }

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
        if (isPlayer1 && !transition.activeSelf)
        {
            if(gameManager.serve == 2)
            {
                if (((Input.GetKey(KeyCode.W) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.W))) && ((transform.position.x < -30 && gameManager.lastServePlayer1 == 1) || (transform.position.x > -50 && gameManager.lastServePlayer1 == -1)))
                {
                    movement += new Vector3(1, 0, 0);
                }
                if (((Input.GetKey(KeyCode.A) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.A))) && ((transform.position.z < 0 && gameManager.lastServePlayer1 == 1) || (transform.position.z > -30 && gameManager.lastServePlayer1 == -1)))
                {
                    movement += new Vector3(0, 0, 1);
                }
                if (((Input.GetKey(KeyCode.S) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.S))) && ((transform.position.x < -30 && gameManager.lastServePlayer1 == -1) || (transform.position.x > -50 && gameManager.lastServePlayer1 == 1)))
                {
                    movement += new Vector3(-1, 0, 0);
                }
                if (((Input.GetKey(KeyCode.D) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.D))) && ((transform.position.z < 0 && gameManager.lastServePlayer1 == -1) || (transform.position.z > -30 && gameManager.lastServePlayer1 == 1)))
                {
                    movement += new Vector3(0, 0, -1);
                }
                CheckButtons();
            }
            if (gameManager.serve == 0)
            {
                if ((Input.GetKey(KeyCode.W) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.W)))
                {
                    movement += new Vector3(1, 0, 0);
                }
                if (((Input.GetKey(KeyCode.A) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.A))))
                {
                    movement += new Vector3(0, 0, 1);
                }
                if (((Input.GetKey(KeyCode.S) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.S))))
                {
                    movement += new Vector3(-1, 0, 0);
                }
                if (((Input.GetKey(KeyCode.D) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.D))))
                {
                    movement += new Vector3(0, 0, -1);
                }
                CheckButtons();
            }
            if (gameManager.serve == 1)
            {
                //PLAYER 1  
                if (((Input.GetKey(KeyCode.W) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.W))) && ((transform.position.x < -45 && gameManager.lastServePlayer1 == 1) || (transform.position.x > -50 && gameManager.lastServePlayer1 == -1)))
                {
                    movement += new Vector3(1, 0, 0);
                }
                if (((Input.GetKey(KeyCode.A) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.A))) && ((transform.position.z < 0 && gameManager.lastServePlayer1 == 1) || (transform.position.z > -30 && gameManager.lastServePlayer1 == -1)))
                {
                    movement += new Vector3(0, 0, 1);
                }
                if (((Input.GetKey(KeyCode.S) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.S))) && ((transform.position.x < -45 && gameManager.lastServePlayer1 == -1) || (transform.position.x > -50 && gameManager.lastServePlayer1 == 1)))
                {
                    movement += new Vector3(-1, 0, 0);
                }
                if (((Input.GetKey(KeyCode.D) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.D))) && ((transform.position.z < 0 && gameManager.lastServePlayer1 == -1) || (transform.position.z > -30 && gameManager.lastServePlayer1 == 1)))
                {
                    movement += new Vector3(0, 0, -1);
                }
                if (((Input.GetButtonDown("A") && !inReplay) || (inReplay && currentFrame.buttonDowns.Contains("A"))) && !gameManager.serving)
                {
                    gameManager.ThrowBall();
                }
                CheckButtons(true);
                transform.Translate(movement.normalized / 4 * Time.deltaTime * movementSpeed * gameManager.lastServePlayer1);
            }
            else
            {
                transform.Translate(movement.normalized * Time.deltaTime * movementSpeed * gameManager.lastServePlayer1);
            }
        }
        else if (!transition.activeSelf)
        {
            if (gameManager.serve == 1)
            {
                if (((Input.GetKey(KeyCode.UpArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.UpArrow))) && ((transform.position.x > 30 && gameManager.lastServePlayer1 == -1) || (transform.position.x < 50 && gameManager.lastServePlayer1 == 1)))
                {
                    movement += new Vector3(1, 0, 0);
                }
                if (((Input.GetKey(KeyCode.LeftArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.LeftArrow))) && ((transform.position.z > 0 && gameManager.lastServePlayer1 == -1) || (transform.position.z < 30 && gameManager.lastServePlayer1 == 1)))
                {
                    movement += new Vector3(0, 0, 1);
                }
                if (((Input.GetKey(KeyCode.DownArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.DownArrow))) && ((transform.position.x > 30 && gameManager.lastServePlayer1 == 1) || (transform.position.x < 50 && gameManager.lastServePlayer1 == -1)))
                {
                    movement += new Vector3(-1, 0, 0);
                }
                if (((Input.GetKey(KeyCode.RightArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.RightArrow))) && ((transform.position.z > 0 && gameManager.lastServePlayer1 == 1) || (transform.position.z < 30 && gameManager.lastServePlayer1 == -1)))
                {
                    movement += new Vector3(0, 0, -1);
                }
                CheckButtons();
            }
            if(gameManager.serve == 0)
            {
                if (((Input.GetKey(KeyCode.UpArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.UpArrow))))
                {
                    movement += new Vector3(1, 0, 0);
                }
                if (((Input.GetKey(KeyCode.LeftArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.LeftArrow))))
                {
                    movement += new Vector3(0, 0, 1);
                }
                if (((Input.GetKey(KeyCode.DownArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.DownArrow))))
                {
                    movement += new Vector3(-1, 0, 0);
                }
                if (((Input.GetKey(KeyCode.RightArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.RightArrow))))
                {
                    movement += new Vector3(0, 0, -1);
                }
                CheckButtons();
            }
            if (gameManager.serve == 2)
            {
                if (((Input.GetKey(KeyCode.UpArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.UpArrow))) && ((transform.position.x > 45 && gameManager.lastServePlayer1 == -1) || (transform.position.x < 50 && gameManager.lastServePlayer1 == 1)))
                {
                    movement += new Vector3(1, 0, 0);
                }
                if (((Input.GetKey(KeyCode.LeftArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.LeftArrow))) && ((transform.position.z > 0 && gameManager.lastServePlayer1 == -1) || (transform.position.z < 30 && gameManager.lastServePlayer1 == 1)))
                {
                    movement += new Vector3(0, 0, 1);
                }
                if (((Input.GetKey(KeyCode.DownArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.DownArrow))) && ((transform.position.x > 45 && gameManager.lastServePlayer1 == 1) || (transform.position.x < 50 && gameManager.lastServePlayer1 == -1)))
                {
                    movement += new Vector3(-1, 0, 0);
                }
                if (((Input.GetKey(KeyCode.RightArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.RightArrow))) && ((transform.position.z > 0 && gameManager.lastServePlayer1 == 1) || (transform.position.z < 30 && gameManager.lastServePlayer1 == -1)))
                {
                    movement += new Vector3(0, 0, -1);
                }
                if (((Input.GetButtonDown("A2") && !inReplay) || (inReplay && currentFrame.buttonDowns.Contains("A2"))) && !gameManager.serving)
                {
                    gameManager.ThrowBall();
                }
                CheckButtons(true);
                transform.Translate(movement.normalized / 4 * Time.deltaTime * -movementSpeed * gameManager.lastServePlayer1);
            }
            else
            {
                transform.Translate(movement.normalized * Time.deltaTime * -movementSpeed * gameManager.lastServePlayer1);
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
        hitManager.hColliders = new Collider[3];
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
            if ((Input.GetKey(KeyCode.W) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.W)))
            {
                if (((Input.GetKey(KeyCode.A) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.A))))
                {
                    direction = -1;
                }
                else if (((Input.GetKey(KeyCode.D) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.D))))
                {
                    direction = 1;
                }
                else
                {
                    direction = 0;
                }
            }
            else if (((Input.GetKey(KeyCode.A) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.A))))
            {
                direction = -2;
            }
            else if (((Input.GetKey(KeyCode.D) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.D))))
            {
                direction = 2;
            }
            else
            {
                direction = 0;
            }
        }
        else
        {
            if (((Input.GetKey(KeyCode.UpArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.UpArrow))))
            {
                if (((Input.GetKey(KeyCode.LeftArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.LeftArrow))))
                {
                    direction = -1;
                }
                else if (((Input.GetKey(KeyCode.RightArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.RightArrow))))
                {
                    direction = 1;
                }
                else
                {
                    direction = 0;
                }
            }
            else if (((Input.GetKey(KeyCode.LeftArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.LeftArrow))))
            {
                direction = -2;
            }
            else if (((Input.GetKey(KeyCode.RightArrow) && !inReplay) || (inReplay && currentFrame.keys.Contains(KeyCode.RightArrow))))
            {
                direction = 2;
            }
            else
            {
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
        if (((Input.GetButton("A" + player) && !inReplay) || (inReplay && currentFrame.buttons.Contains("A" + player))) && !serve)
        {
            if (((Input.GetButton("B" + player) && !inReplay) || (inReplay && currentFrame.buttons.Contains("B" + player))))
            {
                if (((Input.GetButton("C" + player) && !inReplay) || (inReplay && currentFrame.buttons.Contains("C" + player))))
                {
                    //A + B + C
                }
                else
                {
                    //A + B
                }
            }
            else if (((Input.GetButton("C" + player) && !inReplay) || (inReplay && currentFrame.buttons.Contains("C" + player))))
            {
                //A + C
            }
            else
            {
                //A
                if (((Input.GetButtonDown("A" + player) && !inReplay) || (inReplay && currentFrame.buttonDowns.Contains("A" + player))) && !doingSmash && !doingLob && !doingDrive && smashRotation == 0 && lobRotation == 0 && driveRotation == 0 && canHit)
                {
                    ResetRaquet();
                    racket.transform.Rotate(-90, 0, 0);
                    chargingDrive = true;
                    if (gameManager.serve == 0) gameManager.justServed = false;
                    //empezar a moverse
                }
                if (chargingDrive)
                {
                    timer += Time.deltaTime;
                    driveRotation += Time.deltaTime * racketSpeed * (1 / Time.timeScale) / 4;
                    racketPivot.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(0, 45, driveRotation), 0);
                }
            }

        }
        if (((Input.GetButton("B" + player) && !inReplay) || (inReplay && currentFrame.buttons.Contains("B" + player))) && !((Input.GetButton("A" + player) && !inReplay) || (inReplay && currentFrame.buttons.Contains("A" + player))) && !serve)
        {
            if (((Input.GetButton("C" + player) && !inReplay) || (inReplay && currentFrame.buttons.Contains("C" + player))))
            {
                //B + C
            }
            else
            {
                //B
                if (((Input.GetButtonDown("B" + player) && !inReplay) || (inReplay && currentFrame.buttonDowns.Contains("B" + player))) && !doingSmash && !doingDrive && !doingLob && smashRotation == 0 && lobRotation == 0 && driveRotation == 0 && canHit)
                {
                    ResetRaquet();
                    racket.transform.Rotate(0, 0, 180);
                    racket.transform.localPosition = new Vector3(0, -0.25f, -3f);
                    chargingLob = true;
                    if (gameManager.serve == 0) gameManager.justServed = false;
                    //empezar a moverse
                }
                if (chargingLob)
                {
                    timer += Time.deltaTime;
                    lobRotation += Time.deltaTime * racketSpeed * (1 / Time.timeScale) / 4;
                    racketPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, -45, lobRotation));
                }
            }
        }
        if (((Input.GetButton("C" + player) && !inReplay) || (inReplay && currentFrame.buttons.Contains("C" + player))) && !((Input.GetButton("B" + player) && !inReplay) || (inReplay && currentFrame.buttons.Contains("B" + player))) && !((Input.GetButton("A" + player) && !inReplay) || (inReplay && currentFrame.buttons.Contains("A" + player))))
        {
            //C
            if (((Input.GetButtonDown("C" + player) && !inReplay) || (inReplay && currentFrame.buttonDowns.Contains("C" + player))) && !doingDrive && !doingLob && !doingSmash && smashRotation == 0 && driveRotation == 0 && lobRotation == 0 && canHit)
            {
                ResetRaquet();
                racket.transform.localPosition = new Vector3(0, 1f, -3f);
                chargingSmash = true;
                if (gameManager.serve == 0) gameManager.justServed = false;
                //empezar a moverse
            }
            if (chargingSmash)
            {
                timer += Time.deltaTime;
                smashRotation += Time.deltaTime * racketSpeed * (1 / Time.timeScale) / 4;
                racketPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, 45, smashRotation));
            }
        }
        if (!((Input.GetButton("C" + player) && !inReplay) || (inReplay && currentFrame.buttons.Contains("C" + player))) && !((Input.GetButton("B" + player) && !inReplay) || (inReplay && currentFrame.buttons.Contains("B" + player))) && !((Input.GetButton("A" + player) && !inReplay) || (inReplay && currentFrame.buttons.Contains("A" + player))))
        {
            //NONE
        }
        MoveRaquet(serve, player);
    }

    public void MoveRaquet(bool serve, string player)
    {
        if (((Input.GetButtonUp("A" + player) && !inReplay) || (inReplay && currentFrame.buttonUps.Contains("A" + player))) && chargingDrive && !doingSmash && !doingLob && !doingDrive && smashRotation == 0 && lobRotation == 0 && canHit && !serve)
        {
            CheckDirection();
            doingDrive = true;
            chargingDrive = false;
            driveRotation = Mathf.InverseLerp(45, -90, racketPivot.transform.eulerAngles.y);
        }
        if (((Input.GetButtonUp("B" + player) && !inReplay) || (inReplay && currentFrame.buttonUps.Contains("B" + player))) && chargingLob && !doingSmash && !doingLob && !doingDrive && smashRotation == 0 && driveRotation == 0 && canHit && !serve)
        {
            CheckDirection();
            doingLob = true;
            chargingLob = false;
            lobRotation = Mathf.InverseLerp(315, 90, racketPivot.transform.localEulerAngles.z);
        }
        if (((Input.GetButtonUp("C" + player) && !inReplay) || (inReplay && currentFrame.buttonUps.Contains("C" + player))) && chargingSmash && !doingSmash && !doingDrive && !doingLob && driveRotation == 0 && lobRotation == 0 && canHit)
        {
            CheckDirection();
            doingSmash = true;
            chargingSmash = false;
            smashRotation = Mathf.InverseLerp(45, -90, racketPivot.transform.localEulerAngles.z);
        }
        if (doingDrive && simShot == null)
        {
            driveRotation += Time.deltaTime * racketSpeed;
            racketPivot.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(45, -90, driveRotation), 0);
            if (hitManager.hColliders[1] != null && !didDrive)
            {
                //acerto drive
                HitBall(direction, ShotType.drive);
                didDrive = true;
                if (isPlayer1)
                {
                    gameManager.score1++;
                }
                else
                {
                    gameManager.score2++;
                }
            }
            if (driveRotation >= 1)
            {
                racketPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
                racket.transform.Rotate(90, 0, 0);
                driveRotation = 0;
                doingDrive = false;
                didDrive = false;
                canHit = true;
            }
        }
        if (doingLob && simShot == null)
        {
            lobRotation += Time.deltaTime * racketSpeed;
            racketPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(-45, 90, lobRotation));
            if (hitManager.hColliders[2] != null && !didLob)
            {
                //acerto lob
                HitBall(direction, ShotType.lob);
                didLob = true;
                if (isPlayer1)
                {
                    gameManager.score1++;
                }
                else
                {
                    gameManager.score2++;
                }
            }
            if (lobRotation >= 1)
            {
                racketPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
                racket.transform.Rotate(0, 0, -180);
                racket.transform.localPosition = new Vector3(0, 0, -3f);
                lobRotation = 0;
                doingLob = false;
                didLob = false;
                canHit = true;
            }
        }
        if (doingSmash && simShot == null)
        {
            smashRotation += Time.deltaTime * racketSpeed;
            racketPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(45, -90, smashRotation));
            if (hitManager.hColliders[0] != null && !didSmash)
            {
                //acerto smash
                if (serve)
                {
                    if (!gameManager.throwingBall)
                    {
                        gameManager.EndServe();
                        HitBall(-2, ShotType.smash, true);
                    }
                }
                else
                {
                    HitBall(direction, ShotType.smash);
                }
                didSmash = true;
                if (isPlayer1)
                {
                    gameManager.score1++;
                }
                else
                {
                    gameManager.score2++;
                }
            }
            if (smashRotation >= 1)
            {
                racketPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
                racket.transform.localPosition = new Vector3(0, 0, -3f);
                smashRotation = 0;
                doingSmash = false;
                didSmash = false;
                if (!serve)
                {
                    canHit = true;
                }
            }
        }
    }

    void HitBall(int direction, ShotType shotType, bool serve = false)
    {
        PointReplay_FT.instance.replay = new List<Frame>();
        PointReplay_FT.instance.iDirection = direction;
        PointReplay_FT.instance.shot = shotType;
        PointReplay_FT.instance.wasPlayer1 = isPlayer1;
        PointReplay_FT.instance.wasServe = serve;
        PointReplay_FT.instance.iBallPos = shot.ball.transform.position;
        PointReplay_FT.instance.iP1Pos = isPlayer1 ? transform.position : gameManager.player1.transform.position;
        PointReplay_FT.instance.iP2Pos = !isPlayer1 ? transform.position : gameManager.player2.transform.position;
        PointReplay_FT.instance.shot = shotType;
        GetComponent<AudioSource>().Play();
        StartCoroutine(GAMEMANAGER.Instance.GetComponent<LedsController>().SingleBlink(true, "YELLOW", false));
        shot.ball.bounced = false;
        shot.ball.wasPlayer1 = isPlayer1;
        shot.FindShot(direction, shotType, isPlayer1, serve);
    }

    public void ResetRaquet()
    {
        doingDrive = false;
        doingLob = false;
        doingSmash = false;
        driveRotation = 0;
        lobRotation = 0;
        smashRotation = 0;
        didDrive = false;
        didLob = false;
        didSmash = false;
        canHit = true;
        chargingDrive = false;
        chargingLob = false;
        chargingSmash = false;
        racketPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
        racket.transform.localPosition = new Vector3(0, 0, -3f);
        racket.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
