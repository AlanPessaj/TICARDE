﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_FG : MonoBehaviour
{
    public Generator_FG generator;
    bool isPlayer1;
    public bool immortal;
    public GameObject otherPlayer;
    public GameObject[] skins;
    bool onLog = false;
    bool onTruck = false;
    bool facingTreeRight;
    bool facingTreeLeft;
    bool facingTreeUp;
    bool facingTreeDown;
    bool onFrog;
    bool onHippo;
    bool facingPortal;
    bool hasMoved;
    FrogController_FG rana;
    public Vector3 raycastPos;
    public GameObject ghost;
    public Material ghostMaterial;
    [HideInInspector] public bool AFK;
    Material material;
    GameObject portal;
    GameObject hippo;
    GameObject log;
    List<string[]> combos = new List<string[]>();
    public float comboTime;
    public bool notField;
    // Start is called before the first frame update
    void Start()
    {
        isPlayer1 = name == "Player1";
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer1)
        {
            if (Input.GetButtonDown("A"))
            {
                AFK = false;
                DetectCombo("A", "B", "", HeartAbility, HippoAbility);
            }
            if (Input.GetButtonDown("B"))
            {
                AFK = false;
                DetectCombo("B", "A", "", HeartAbility, PortalAbility);
            }
            if (Input.GetButtonDown("C") && GetComponent<UIManager_FG>().RemoveXP(25)) StartCoroutine(Invulnerability());
            if (Input.GetKeyDown(KeyCode.W))
            {
                AFK = false;
                if (generator.multiplayer && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x <= otherPlayer.transform.position.x) || !generator.multiplayer)
                {
                    if (!facingTreeDown)
                    {
                        MoveForward();
                    }
                }

                if (facingPortal)
                {
                    transform.position = portal.GetComponent<Glad0s_FG>().redPortal.transform.position + Vector3.right * 0.5f;
                    if (generator.multiplayer)
                    {
                        otherPlayer.transform.position = portal.GetComponent<Glad0s_FG>().redPortal.transform.position + Vector3.right * 0.5f;
                    }
                    generator.GenerateZones();
                    GAMEMANAGER.Instance.GetComponent<LedsController>().FullRound("BLUE");
                    portal.GetComponent<AudioSource>().Play();
                }
            }
            if (Input.GetKeyDown(KeyCode.S) && generator.distance - generator.despawnRadius < transform.position.x && transform.position.x > 0 && !facingTreeUp)
            {
                AFK = false;
                if (generator.multiplayer && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x >= otherPlayer.transform.position.x) || !generator.multiplayer)
                {
                    MoveBackward();
                }
            }
            if (Input.GetKeyDown(KeyCode.A) && transform.position.z < 12f && !facingTreeRight)
            {
                AFK = false;
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.D) && transform.position.z > -12f && !facingTreeLeft)
            {
                AFK = false;
                MoveRight();
            }
        }
        else
        {
            if (Input.GetButtonDown("A2"))
            {
                AFK = false;
                DetectCombo("A", "B", "2", HeartAbility, HippoAbility);
            }
            if (Input.GetButtonDown("B2"))
            {
                AFK = false;
                DetectCombo("B", "A", "2", HeartAbility, PortalAbility);
            }
            if (Input.GetButtonDown("C2") && GetComponent<UIManager_FG>().RemoveXP(25)) StartCoroutine(Invulnerability());
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                AFK = false;
                if (generator.multiplayer && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x <= otherPlayer.transform.position.x) || !generator.multiplayer)
                {
                    if (!facingTreeDown)
                    {
                        MoveForward();
                    }
                }

                if (facingPortal)
                {
                    transform.position = portal.GetComponent<Glad0s_FG>().redPortal.transform.position + Vector3.right * 0.5f;
                    if (generator.multiplayer)
                    {
                        otherPlayer.transform.position = portal.GetComponent<Glad0s_FG>().redPortal.transform.position + Vector3.right * 0.5f;
                    }
                    while (generator.distance < generator.despawnRadius)
                    {
                        generator.GenerateZones();
                    }
                    generator.GenerateZones();
                    GAMEMANAGER.Instance.GetComponent<LedsController>().FullRound("BLUE");
                    portal.GetComponent<AudioSource>().Play();
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && generator.distance - generator.despawnRadius < transform.position.x && transform.position.x > 0 && !facingTreeUp)
            {
                AFK = false;
                if (generator.multiplayer && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x >= otherPlayer.transform.position.x) || !generator.multiplayer)
                {
                    MoveBackward();
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.z < 12f && !facingTreeRight)
            {
                AFK = false;
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.z > -12f && !facingTreeLeft)
            {
                AFK = false;
                MoveRight();
            }
        }

        if (transform.position.z > 15f || transform.position.z < -15f)
        {
            //Perder vida
            if ((onHippo && !onTruck) || onLog || transform.parent.name.Contains("LillyPad")) generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().waterFalling);
            if (onTruck) generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().carRunOver);
            Die();
        }
        CheckTile();
        UpdateCombo();

        if (isPlayer1 && transform.position == otherPlayer.transform.position) transform.position += Vector3.forward * 0.01f;
    }

    void MoveForward()
    {
        if (!onFrog && !onTruck && !onHippo)
        {
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) + 1, transform.position.y, transform.position.z);
            hasMoved = true;
        }
        else if (onTruck && !hippo.GetComponent<LinearMover_FG>().hippoRotating && !hippo.GetComponent<LinearMover_FG>().hippoResuming)
        {
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x + 1.5f), transform.position.y, transform.position.z);
            onHippo = false;
            onTruck = false;
            hasMoved = true;
        }
        else if (onHippo && !hippo.GetComponent<LinearMover_FG>().hippoRotating && !hippo.GetComponent<LinearMover_FG>().hippoResuming)
        {
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) + 1, transform.position.y, transform.position.z);
            transform.parent = null;
            CheckTile();
            onHippo = false;
            hasMoved = true;
        }
        else
        {
            if (rana != null && !rana.isJumping)
            {
                transform.rotation = Quaternion.identity;
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) + 1, transform.position.y, transform.position.z);
                onFrog = false;
                hasMoved = true;
            }
        }
        if (generator.distance <= transform.position.x + (generator.despawnRadius / 2))
        {
            generator.GenerateZones();
        }
    }

    void MoveBackward()
    {
        if (!onFrog && !onTruck && !onHippo)
        {
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) - 1, transform.position.y, transform.position.z);
            hasMoved = true;
        }
        else if (onTruck && !hippo.GetComponent<LinearMover_FG>().hippoRotating && !hippo.GetComponent<LinearMover_FG>().hippoResuming)
        {
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x - 1.5f), transform.position.y, transform.position.z);
            onHippo = false;
            onTruck = false;
            hasMoved = true;
        }
        else if (onHippo && !hippo.GetComponent<LinearMover_FG>().hippoRotating && !hippo.GetComponent<LinearMover_FG>().hippoResuming)
        {
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) - 1, transform.position.y, transform.position.z);
            transform.parent = null;
            CheckTile();
            onHippo = false;
            hasMoved = true;
        }
        else
        {
            if (rana != null && !rana.isJumping)
            {
                transform.rotation = Quaternion.identity;
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) - 1, transform.position.y, transform.position.z);
                onFrog = false;
                hasMoved = true;
            }
        }
    }

    void MoveLeft()
    {
        if (!onLog && !onFrog && !onHippo)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) + 1);
            hasMoved = true;
        }
        else if (onLog)
        {
            hasMoved = true;
            if (transform.localPosition.z < 0 && transform.localPosition.z >= -1)
            {
                if (transform.localPosition.z > 0.3f)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 1);
                }
                else
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
                }
            }
            else if (transform.localPosition.z >= 0 && transform.localPosition.z < 1)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 1);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) + 1);
            }
            log.transform.GetComponent<AudioSource>().PlayOneShot(log.transform.GetComponent<AudioSource>().clip);
        }
        else if (onHippo && !onTruck)
        {
            if (!hippo.GetComponent<LinearMover_FG>().hippoResuming && !hippo.GetComponent<LinearMover_FG>().hippoRotating)
            {
                if (hippo.GetComponent<LinearMover_FG>().movingForward)
                {
                    if (transform.localPosition.z <= 0.5f) transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.5f);
                    else transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.25f);
                }
                else
                {
                    if (transform.localPosition.z <= 0f) transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.25f);
                    else transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 1f);
                }
                hasMoved = true;
            }
        }
        else if (onTruck && !hippo.GetComponent<LinearMover_FG>().hippoRotating && !hippo.GetComponent<LinearMover_FG>().hippoResuming)
        {
            if (!hippo.GetComponent<LinearMover_FG>().hippoResuming && !hippo.GetComponent<LinearMover_FG>().hippoRotating)
            {
                if (!hippo.GetComponent<LinearMover_FG>().movingForward)
                {
                    transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, Mathf.RoundToInt(transform.position.z) + 1f);
                    transform.parent = null;
                    hasMoved = true;
                }
            }
        }
        else
        {
            if (rana != null && !rana.isJumping)
            {
                transform.rotation = Quaternion.identity;
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) + 1);
                onFrog = false;
                hasMoved = true;
            }
        }
    }
    void DetectCombo(string button1, string button2, string player, System.Action func, System.Action noCombo = null, bool isBtn1 = true, bool isBtn2 = true)
    {
        if (isBtn1)
            button1 += player;
        if (isBtn2)
            button2 += player;
        if (noCombo == null)
            noCombo = Nothing;
        if (!combos.Contains(new string[] { button1, button2, comboTime.ToString(), func.Method.Name, noCombo.Method.Name, isBtn1.ToString(), isBtn2.ToString() }))
            combos.Add(new string[] { button1, button2, comboTime.ToString(), func.Method.Name, noCombo.Method.Name, isBtn1.ToString(), isBtn2.ToString() });
    }

    void UpdateCombo()
    {
        Queue<string[]> removeQueue = new Queue<string[]>();
        foreach (var item in combos)
        {
            if (float.Parse(item[2]) <= 0)
            {
                Invoke(item[4], 0);
                removeQueue.Enqueue(item);
                continue;
            }
            if (bool.Parse(item[5]))
            {
                if (bool.Parse(item[6]))
                {
                    if (Input.GetButton(item[0]) && Input.GetButtonDown(item[1]))
                    {
                        Invoke(item[3], 0);
                        removeQueue.Enqueue(item);
                        continue;
                    }
                }
                else
                {
                    if (Input.GetButton(item[0]) && Input.GetKeyDown(item[1]))
                    {
                        Invoke(item[3], 0);
                        removeQueue.Enqueue(item);
                        continue;
                    }
                }
            }
            else
            {
                if (bool.Parse(item[6]))
                {
                    if (Input.GetKey(item[0]) && Input.GetButtonDown(item[1]))
                    {
                        Invoke(item[3], 0);
                        removeQueue.Enqueue(item);
                        continue;
                    }
                }
                else
                {
                    if (Input.GetKey(item[0]) && Input.GetKeyDown(item[1]))
                    {
                        Invoke(item[3], 0);
                        removeQueue.Enqueue(item);
                        continue;
                    }
                }
            }
            item[2] = (float.Parse(item[2]) - Time.deltaTime).ToString();
        }
        while (removeQueue.Count > 0)
        {
            combos.Remove(removeQueue.Dequeue());
        }
    }

    void HeartAbility()
    {
        //3
        if (!transform.GetChild(0).gameObject.activeSelf)
        {
            if (GetComponent<UIManager_FG>().RemoveXP(75)) transform.GetChild(0).gameObject.SetActive(true);
            generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().abilitySound);
            StartCoroutine(GAMEMANAGER.Instance.GetComponent<LedsController>().SideBlink(isPlayer1, "MAGENTA"));
        }
    }

    void HippoAbility()
    {
        //1
        if (Physics.Raycast(transform.position + Vector3.right + Vector3.up, Vector3.down, out RaycastHit hippoCheck, 5f, LayerMask.GetMask("Out")) && hippoCheck.transform.name == "agua")
        {
            if (GetComponent<UIManager_FG>().RemoveXP(25))
            {
                if (hippoCheck.transform.parent.GetComponent<LinearSpawner_FG>().changedSide)
                {
                    Instantiate(hippoCheck.transform.parent.GetComponent<LinearSpawner_FG>().hippo, new Vector3(transform.position.x + 1, -2.7f, transform.position.z + 2), Quaternion.Euler(0, 0, 90), hippoCheck.transform.parent).GetComponent<LinearMover_FG>().spawner = hippoCheck.transform.parent.GetComponent<LinearSpawner_FG>();
                }
                else
                {
                    Instantiate(hippoCheck.transform.parent.GetComponent<LinearSpawner_FG>().hippo, new Vector3(transform.position.x + 1, -2.7f, transform.position.z - 2), Quaternion.Euler(0, 180, 90), hippoCheck.transform.parent).GetComponent<LinearMover_FG>().spawner = hippoCheck.transform.parent.GetComponent<LinearSpawner_FG>();
                }
                generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().abilitySound);
                StartCoroutine(GAMEMANAGER.Instance.GetComponent<LedsController>().SideBlink(isPlayer1, "MAGENTA"));
            }
        }
        else if (Physics.Raycast(transform.position + Vector3.right * 2 + Vector3.up, Vector3.down, out RaycastHit check, 5f, LayerMask.GetMask("Default")) && Physics.Raycast(transform.position + Vector3.right + Vector3.up, Vector3.down, out RaycastHit check2, 5f, LayerMask.GetMask("Default")) && check.transform.name == "calle" && check2.transform.name == "calle")
        {
            //Debug.DrawRay(transform.position + Vector3.right * 2 + Vector3.up, Vector3.down * 5f, Color.red, 10f);
            //Debug.DrawRay(transform.position + Vector3.right + Vector3.up, Vector3.down * 5f, Color.red, 10f);
            if (GetComponent<UIManager_FG>().RemoveXP(25))
            {
                if (check.transform.parent.GetComponent<LinearSpawner_FG>().changedSide)
                {
                    Instantiate(check.transform.parent.GetComponent<LinearSpawner_FG>().hippo, new Vector3(transform.position.x + 1.5f, -2.7f, transform.position.z + 2), Quaternion.Euler(0, 180, 0), check.transform.parent).GetComponent<LinearMover_FG>().spawner = check.transform.parent.GetComponent<LinearSpawner_FG>();
                }
                else
                {
                    Instantiate(check.transform.parent.GetComponent<LinearSpawner_FG>().hippo, new Vector3(transform.position.x + 1.5f, -2.7f, transform.position.z - 2), Quaternion.Euler(0, 0, 0), check.transform.parent).GetComponent<LinearMover_FG>().spawner = check.transform.parent.GetComponent<LinearSpawner_FG>();
                }
                generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().abilitySound);
                StartCoroutine(GAMEMANAGER.Instance.GetComponent<LedsController>().SideBlink(isPlayer1, "MAGENTA"));
            }
        }
    }

    void PortalAbility()
    {
        //2
        if (GetComponent<UIManager_FG>().RemoveXP(50))
        {
            Instantiate(generator.specials[2], new Vector3(transform.position.x + 0.5f, -1.5f, transform.position.z), Quaternion.Euler(0, 0, 90));
            generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().abilitySound);
            StartCoroutine(GAMEMANAGER.Instance.GetComponent<LedsController>().SideBlink(isPlayer1, "MAGENTA"));
        }
    }

    void MoveRight()
    {
        if (!onLog && !onFrog && !onHippo)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) - 1);
            hasMoved = true;
        }
        else if (onLog)
        {
            if (transform.localPosition.z <= 1 && transform.localPosition.z > 0)
            {
                if (transform.localPosition.z <= 0.3f)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -1);
                }
                else
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
                }
            }
            else if (transform.localPosition.z <= 0 && transform.localPosition.z > -1)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -1);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) - 1);
            }
            hasMoved = true;
            log.transform.GetComponent<AudioSource>().PlayOneShot(log.transform.GetComponent<AudioSource>().clip);
        }
        else if (onHippo && !onTruck)
        {
            if (!hippo.GetComponent<LinearMover_FG>().hippoResuming && !hippo.GetComponent<LinearMover_FG>().hippoRotating)
            {
                if (hippo.GetComponent<LinearMover_FG>().movingForward)
                {
                    if (transform.localPosition.z >= 0) transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 1f);
                    else transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.25f);
                }
                else
                {
                    if (transform.localPosition.z >= 0.5f) transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.25f);
                    else transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.5f);
                }
                hasMoved = true;
            }
        }
        else if (onTruck && !hippo.GetComponent<LinearMover_FG>().hippoRotating && !hippo.GetComponent<LinearMover_FG>().hippoResuming)
        {
            if (!hippo.GetComponent<LinearMover_FG>().hippoResuming && !hippo.GetComponent<LinearMover_FG>().hippoRotating)
            {
                if (hippo.GetComponent<LinearMover_FG>().movingForward)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) - 1f);
                    transform.parent = null;
                    hasMoved = true;
                }
            }
        }
        else
        {
            if (rana != null && !rana.isJumping)
            {
                transform.rotation = Quaternion.identity;
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) - 1);
                onFrog = false;
                hasMoved = true;
            }
        }
    }
    
    public void Die()
    {
        StartCoroutine(GAMEMANAGER.Instance.GetComponent<LedsController>().SideBlink(isPlayer1, "RED", generator.multiplayer, generator.isTherePlayer1));
        if (transform.GetChild(0).gameObject.activeSelf || immortal)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            int tries = 1;
            bool foundGrass = false;
            //TODO: Revisar este while, puede causar problemas si no se encuentra pasto
            while (tries < 100 && !foundGrass)
            {
                if (Physics.Raycast(transform.position + new Vector3(tries, 0, 0), Vector3.down, out RaycastHit hit, 10f))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Grass"))
                    {
                        foundGrass = true;
                    }
                }
                tries++;
            }
            
            if (foundGrass)
            {
                transform.position = new Vector3(transform.position.x + tries - 1, -2, 0);
                StartCoroutine(Invulnerability());
            }
        }
        else
        {
            if (isPlayer1) generator.player1Score = (int)transform.position.x;
            else generator.player2Score = (int)transform.position.x;
            Instantiate(ghost, transform.position, Quaternion.identity).GetComponent<DieScript_FG>().playerGhost = true;
            gameObject.SetActive(false);
        }
    }


    void CheckTile()
    {
        //Debug.DrawRay(transform.position + Vector3.up * 2f, Vector3.down * 10f, Color.red, 1f, false);
        Physics.Raycast(transform.position + Vector3.up * 2f, Vector3.down, out RaycastHit hit, 10f, Physics.AllLayers - LayerMask.GetMask("Tree", "Player", "Lion"));
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Transport"))
            {
                if (hit.collider.gameObject.name == "LillyPad(Clone)" && !onHippo)
                {
                    transform.parent = hit.transform;
                    transform.localPosition = new Vector3(0, 1, 0);
                }
                else if (hit.collider.gameObject.name == "Frog(Clone)" && !onHippo)
                {
                    if (!hit.transform.gameObject.GetComponent<FrogController_FG>().isJumping && hit.transform.childCount == 1)
                    {
                        rana = hit.transform.gameObject.GetComponent<FrogController_FG>();
                        transform.parent = hit.transform;
                        transform.localPosition = new Vector3(0, 1.5f, -0.5f);
                        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, -90, transform.localRotation.eulerAngles.z);
                        onFrog = true;
                    }
                }
                else if (hit.collider.gameObject.name == "Hippo(Clone)" && !onHippo)
                {
                    if (hit.transform.childCount == 0)
                    {
                        transform.parent = hit.transform;
                        transform.localPosition = new Vector3(1.5f, 0, 0.25f);
                        transform.localRotation = Quaternion.Euler(90, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
                        onHippo = true;
                        hippo = hit.collider.gameObject;
                    }
                }
                else if (hit.collider.gameObject.name == "MonsterTruck(Clone)" && !onHippo)
                {
                    if (hit.transform.childCount == 3)
                    {
                        transform.parent = hit.transform;
                        transform.localPosition = new Vector3(0, 3.6f, -1.4f);
                        transform.localRotation = Quaternion.Euler(0, -90, 0);
                        onHippo = true;
                        onTruck = true;
                        hippo = hit.collider.gameObject;
                    }
                    else
                    {
                        Die();
                        generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().carRunOver);
                    }
                }
                else if (hit.collider.gameObject.name.Contains("Log(Clone)") && !onHippo)
                {
                    transform.parent = hit.transform;
                    onLog = true;
                    log = hit.transform.gameObject;
                    transform.position = new Vector3(transform.position.x, -1.5f, transform.position.z);
                }
            }
            else
            {
                transform.parent = null;
                transform.rotation = Quaternion.identity;
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), -2, Mathf.RoundToInt(transform.position.z));
                onHippo = false;
                onTruck = false;
                onLog = false;
            }
            if ((hit.collider.gameObject.layer == LayerMask.NameToLayer("Field") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Grass")) && hasMoved)
            {
                generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().step);
                hasMoved = false;
                notField = hit.collider.gameObject.layer == LayerMask.NameToLayer("Grass");
            }
            try
            {
                if ((hit.collider.gameObject.name == "Cars(Clone)" || hit.transform.parent.name == "Cars(Clone)") && hasMoved)
                {
                    generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().roadStep);
                    hasMoved = false;
                }
            }
            catch { }

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Out") && !immortal)
            {
                //perder vida
                Die();

                if (hit.collider.gameObject.name == "Car(Clone)" || hit.collider.gameObject.name == "MonsterOut")
                {
                    generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().carRunOver);
                }
                else if (hit.collider.gameObject.name == "Out")
                {
                    hit.collider.GetComponent<AudioSource>().Play();
                }
                else if (hit.transform.parent.name.Contains("Logs") || hit.transform.parent.name == "LilyPads(Clone)")
                {
                    generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().waterFalling);
                }
                return;
            }
        }
        raycastPos = transform.TransformPoint(Vector3.zero);
        //Debug.DrawRay(raycastPos, Vector3.forward * 1.3f, Color.red, 1, false);   // Derecha
        //Debug.DrawRay(raycastPos, -Vector3.forward * 1.3f, Color.blue, 1, false); // Izquierda
        //Debug.DrawRay(raycastPos, Vector3.right * 1.3f, Color.green, 1, false);   // Abajo
        //Debug.DrawRay(raycastPos, -Vector3.right * 1.3f, Color.yellow, 1, false);  // Arriba
        //Debug.DrawRay(transform.position, Vector3.right * 1f, Color.red);
        facingTreeRight = Physics.Raycast(raycastPos, Vector3.forward, 1.3f, LayerMask.GetMask("Tree"));
        facingTreeLeft = Physics.Raycast(raycastPos, -Vector3.forward, 1.3f, LayerMask.GetMask("Tree"));
        facingTreeDown = Physics.Raycast(raycastPos, Vector3.right, 1.3f, LayerMask.GetMask("Tree"));
        facingTreeUp = Physics.Raycast(raycastPos, -Vector3.right, 1.3f, LayerMask.GetMask("Tree"));
        facingPortal = Physics.Raycast(transform.position, Vector3.right, out RaycastHit dPortal, 1f, LayerMask.GetMask("Portal"));
        if (facingPortal)
        {
            this.portal = dPortal.transform.gameObject;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((other.collider.gameObject.layer == LayerMask.NameToLayer("Lion") || other.collider.gameObject.layer == LayerMask.NameToLayer("Out")) && !immortal && !onFrog && other.collider.gameObject.name != "agua")
        {
            //perder vida
            //Debug.Log(other.collider.gameObject.name);
            Die();
            if (other.collider.gameObject.name == "Car(Clone)" || other.collider.gameObject.name == "MonsterOut") 
            {
                generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().carRunOver);
            }
            if (other.collider.gameObject.layer == LayerMask.NameToLayer("Lion"))
            {
                generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().lionBite);
            }
        }
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Seagull") && !immortal)
        {
            //perder vida
            Die();
            if (other.transform.name == "Laser")
            {
                generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().laserImpact);
            }
            if (other.transform.name.Contains("Shit"))
            {
                Destroy(other.gameObject);
            }
        }
    }

    public IEnumerator Invulnerability()
    {
        AFK = false;
        immortal = true;
        int index = 4;
        for (int i = 0; i < index; i++)
        {
            yield return new WaitForSeconds(0.3f);
            GetComponent<Renderer>().enabled = true;
            transform.GetChild(1).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            GetComponent<Renderer>().enabled = false;
            transform.GetChild(1).gameObject.SetActive(true);
        }
        immortal = false;
    }
    void Nothing() { }
}