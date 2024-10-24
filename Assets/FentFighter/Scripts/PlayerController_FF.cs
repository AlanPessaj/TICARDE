﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_FF : MonoBehaviour
{
    public GameObject proyectile;
    public GameObject fist;
    public GameObject foot;
    public GameObject otherPlayer;
    public GameObject colliders;
    public GameObject cColliders;
    public HitManager_FF hitManager;
    public int movementForce;
    public int movementSpeed;
    public int jumpForce;
    public float comboTime;
    public bool punchHit;
    public bool airborne;
    bool isColliding;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        isPlayer1 = gameObject.name == "Player1";
        animator = GetComponent<Animator>();
        facingLeft = otherPlayer.transform.position.x < transform.position.x;
    }
    public bool facingLeft;
    public bool isPlayer1;
    public float movDirection = 0;
    // Update is called once per frame
    void Update()
    {
        if (otherPlayer.transform.position.x > transform.position.x && facingLeft)
        {
            //Cambiar a derecha
            transform.Rotate(0, 180, 0, Space.World);
            facingLeft = false;
            animator.SetTrigger("turnAround");
        }
        else if (otherPlayer.transform.position.x < transform.position.x && !facingLeft)
        {
            //Cambiar a izquierda
            transform.Rotate(0, 180, 0, Space.World);
            facingLeft = true;
            animator.SetTrigger("turnAround");
        }
        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (facingLeft)
                {
                    animator.SetBool("runb", true);
                    movDirection += 1;
                }
                else
                {
                    animator.SetBool("run", true);
                    movDirection += 1;
                }
            }
            else
            {
                if (facingLeft)
                {
                    animator.SetBool("runb", false);
                }
                else
                {
                    animator.SetBool("run", false);
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (!facingLeft)
                {
                    animator.SetBool("runb", true);
                    movDirection -= 1;
                }
                else
                {
                    animator.SetBool("run", true);
                    movDirection -= 1;
                }
            }
            else
            {
                if (!facingLeft)
                {
                    animator.SetBool("runb", false);
                }
                else
                {
                    animator.SetBool("run", false);
                }
            }
            animator.SetBool("holdCrouch", Input.GetKey(KeyCode.S));
            if (Input.GetKey(KeyCode.S))
            {
                if (hitManager.blocking)
                {
                    movDirection = 0;
                }
                movDirection /= 2;
                if (Input.GetKeyDown(KeyCode.S))
                {
                    DetectCombo("s", "A", "", Smash, false);
                }
                if (colliders.activeSelf)
                {
                    colliders.SetActive(false);
                    cColliders.SetActive(true);
                    GetComponent<CapsuleCollider>().height = 0.72f;
                    GetComponent<CapsuleCollider>().center = new Vector3(0, 0.36f, 0);
                }
            }
            else
            {
                if (cColliders.activeSelf)
                {
                    colliders.SetActive(true);
                    cColliders.SetActive(false);
                    GetComponent<CapsuleCollider>().height = 0.95f;
                    GetComponent<CapsuleCollider>().center = new Vector3(0, 0.475f, 0);
                }
            }
            if (hitManager.blocking)
            {
                movDirection = movDirection / 2;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (!airborne)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(movementSpeed * movDirection, 0, 0);
                    GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
                    animator.SetTrigger("jump");
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movDirection += 1;
                if (facingLeft)
                {
                    animator.SetBool("runb", true);
                }
                else
                {
                    animator.SetBool("run", true);
                }
            }
            else
            {
                if (facingLeft)
                {
                    animator.SetBool("runb", false);
                }
                else
                {
                    animator.SetBool("run", false);
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movDirection -= 1;
                if (!facingLeft)
                {
                    animator.SetBool("runb", true);
                }
                else
                {
                    animator.SetBool("run", true);
                }
            }
            else
            {
                if (!facingLeft)
                {
                    animator.SetBool("runb", false);
                }
                else
                {
                    animator.SetBool("run", false);
                }
            }
            animator.SetBool("holdCrouch", Input.GetKey(KeyCode.DownArrow));
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (hitManager.blocking)
                {
                    movDirection = 0;
                }
                movDirection /= 2;
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    DetectCombo("down", "A", "2", Smash, false);
                }
                if (colliders.activeSelf)
                {
                    colliders.SetActive(false);
                    cColliders.SetActive(true);
                    GetComponent<CapsuleCollider>().height = 0.72f;
                    GetComponent<CapsuleCollider>().center = new Vector3(0, 0.36f, 0);
                }
            }
            else
            {
                if (cColliders.activeSelf)
                {
                    colliders.SetActive(true);
                    cColliders.SetActive(false);
                    GetComponent<CapsuleCollider>().height = 0.95f;
                    GetComponent<CapsuleCollider>().center = new Vector3(0, 0.475f, 0);
                }
            }
            if (hitManager.blocking)
            {
                movDirection /= 2;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!airborne)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(movementSpeed * movDirection, 0, 0);
                    GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
                    animator.SetTrigger("jump");
                }
            }
        }
        CheckButtons();
        if (isColliding)
        {
            if (transform.position.x > otherPlayer.transform.position.x)
            {
                movDirection = Mathf.Clamp(movDirection, 0, Mathf.Infinity);
            }
            else
            {
                movDirection = Mathf.Clamp(movDirection, Mathf.NegativeInfinity, 0);
            }
        }
        if (!airborne)
        {
            transform.Translate(movDirection * movementSpeed * Time.deltaTime, 0, 0, Space.World);
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(movementForce * movDirection, 0, 0);
        }
        movDirection = 0;
        UpdateCombo();
    }

    void CheckButtons()
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
                    if (Input.GetButtonDown("A" + player) && Input.GetButtonDown("B" + player))
                    {
                        Ulti();
                    }
                }
            }
            else if (Input.GetButton("C" + player))
            {
                //A + C
                if (Input.GetButtonDown("A" + player) && Input.GetButtonDown("C" + player))
                {
                    UpperCut();
                }
            }
            else
            {
                //A
                if (Input.GetButtonDown("A" + player))
                {
                    if (isPlayer1)
                    {
                        DetectCombo("A", "s", "", Smash, isBtn2: false);
                    }
                    else
                    {
                        DetectCombo("A", "down", "2", Smash, isBtn2: false);
                    }
                    DetectCombo("A", "B", player, Ulti);
                    DetectCombo("A", "C", player, UpperCut);
                    if (InState("idle"))
                    {
                        animator.SetTrigger("punch");
                    }
                }
            }

        }
        if (Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            if (Input.GetButton("C" + player))
            {
                //B + C
                if (Input.GetButtonDown("B" + player) && Input.GetButtonDown("C" + player))
                {
                    Ability();
                }
            }
            else
            {
                //B
                if (Input.GetButtonDown("B" + player))
                {
                    DetectCombo("B", "C", player, Ability);
                    DetectCombo("B", "A", player, Ulti);
                    if (InState("idle") || InState("crouching") || InState("crouch") || InState("uncrouch"))
                    {
                        animator.SetTrigger("kick");
                    }
                }
            }
        }
        if (Input.GetButton("C" + player) && !Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            //C
            if (Input.GetButtonDown("C" + player))
            {
                DetectCombo("C", "B", player, Ability);
                DetectCombo("C", "A", player, UpperCut);
                if (InState("idle"))
                {
                    animator.SetBool("holdBlock", true);
                    hitManager.blocking = true;
                }
            }
        }
        else
        {
            animator.SetBool("holdBlock", false);
            hitManager.blocking = false;
        }
        if (!Input.GetButton("C" + player) && !Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            //NONE
        }
    }

    void UpdateCombo()
    {
        Queue<string[]> removeQueue = new Queue<string[]>();
        foreach (var item in combos)
        {
            if (float.Parse(item[2]) <= 0)
            {
                removeQueue.Enqueue(item);
                continue;
            }
            if (bool.Parse(item[4]))
            {
                if (bool.Parse(item[5]))
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
                if (bool.Parse(item[5]))
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
    List<string[]> combos = new List<string[]>();
    void DetectCombo(string button1, string button2, string player, System.Action func, bool isBtn1 = true, bool isBtn2 = true)
    {
        if (isBtn1)
            button1 += player;
        if (isBtn2)
            button2 += player;
        if (!combos.Contains(new string[] { button1, button2, comboTime.ToString(), func.Method.Name, isBtn1.ToString(), isBtn2.ToString() })) 
            combos.Add(new string[] { button1, button2, comboTime.ToString(), func.Method.Name, isBtn1.ToString(), isBtn2.ToString() });
    }

    void Smash()
    {
        if (airborne)
            animator.SetTrigger("smash");
    }

    void UpperCut()
    {
        if ((InState("idle") || InState("jumping")) && (!airborne || (airborne && GetComponent<Rigidbody>().velocity.y > 9)))
            animator.SetTrigger("upperCut");
    }

    void Ability()
    {
        if (GetComponent<UIManager_FF>().RemoveXP(25))
        {
            GameObject temp = Instantiate(proyectile, transform.position + new Vector3(0, 1, 0), Quaternion.Euler(proyectile.transform.eulerAngles + transform.eulerAngles - new Vector3(0, 90, 0)), transform);
            temp.transform.parent = null;
            temp.GetComponent<Damage_FF>().owner = gameObject;
        }
    }

    void Ulti()
    {
        if (GetComponent<UIManager_FF>().RemoveXP(100))
        {
            GameObject temp = Instantiate(proyectile, transform.position + new Vector3(0, 1, 0), Quaternion.Euler(proyectile.transform.eulerAngles + transform.eulerAngles - new Vector3(0, 90, 0)), transform);
            temp.transform.parent = null;
            temp.transform.localScale *= 2.5f;
            temp.GetComponent<Damage_FF>().damage *= 2;
            temp.GetComponent<Damage_FF>().type = DamageType.Ulti;
            temp.GetComponent<Damage_FF>().owner = gameObject;
        }
    }

    public bool InState(string name)
    {
        return (animator.GetCurrentAnimatorStateInfo(0).IsName(name) && !animator.IsInTransition(0)) || animator.GetNextAnimatorStateInfo(0).IsName(name);
    }

    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            airborne = false;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            airborne = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isColliding = false;
        }
    }
}
