using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_FF : MonoBehaviour
{
    public GameObject proyectile;
    public GameObject fist;
    public GameObject foot;
    public GameObject otherPlayer;
    public HitManager_FF hitManager;
    public int movementForce;
    public int movementSpeed;
    public int jumpForce;
    bool airborne;
    bool isColliding;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        isPlayer1 = gameObject.name == "Player1";
        animator = GetComponent<Animator>();
        facingLeft = otherPlayer.transform.position.x < transform.position.x;
    }
    bool facingLeft;
    bool isPlayer1;
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
        else if(otherPlayer.transform.position.x < transform.position.x && !facingLeft)
        {
            //Cambiar a izquierda
            transform.Rotate(0, 180, 0, Space.World);
            facingLeft = true;
            animator.SetTrigger("turnAround");
        }
        int movDirection = 0;
        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (airborne)
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(movementForce, 0, 0);
                }
                else
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
                if (airborne)
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(-movementForce, 0, 0);
                }
                else
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
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (!airborne)
                {
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(movementSpeed * movDirection, 0, 0);
                    gameObject.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
                    animator.SetTrigger("jump");
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                movDirection = 0;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (airborne)
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(movementForce, 0, 0);
                }
                else
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
                if (airborne)
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(-movementForce, 0, 0);
                }
                else
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
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!airborne)
                {
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(movementSpeed * movDirection, 0, 0);
                    gameObject.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
                    animator.SetTrigger("jump");
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                movDirection = 0;
            }
        }
        CheckButtons();
        if (isColliding)
        {
            if (transform.position.x > otherPlayer.transform.position.x)
            {
                movDirection = (int)Mathf.Clamp(movDirection, 0, Mathf.Infinity);
            }
            else
            {
                movDirection = (int)Mathf.Clamp(movDirection, Mathf.NegativeInfinity, 0);
            }
        }
        transform.Translate(movDirection * movementSpeed * Time.deltaTime, 0, 0, Space.World);
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
                    if (Input.GetButtonDown("A" + player) || Input.GetButtonDown("B" + player))
                    {
                        if (GetComponent<UIManager_FF>().RemoveXP(100))
                        {
                            Debug.Log("Ulti");
                            GameObject temp = Instantiate(proyectile, transform.position + new Vector3(0, 1, 0), Quaternion.Euler(proyectile.transform.eulerAngles + transform.eulerAngles - new Vector3(0, 90, 0)), transform);
                            temp.transform.parent = null;
                            temp.transform.localScale *= 2.5f;
                            temp.GetComponent<Properties_FF>().damage *= 2;
                            temp.GetComponent<Properties_FF>().type = DamageType.Ulti;
                        }
                    }
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
                    if ((animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && !animator.IsInTransition(0)) || animator.GetNextAnimatorStateInfo(0).IsName("idle"))
                    {
                        fist.SetActive(true);
                        animator.SetTrigger("Punch");
                    }
                }
            }

        }
        if (Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            if (Input.GetButton("C" + player))
            {
                //B + C
                if (Input.GetButtonDown("B" + player) || Input.GetButtonDown("C" + player))
                {
                    if (GetComponent<UIManager_FF>().RemoveXP(25))
                    {
                        Debug.Log("Habilidad");
                        Instantiate(proyectile, transform.position + new Vector3(0, 1, 0), Quaternion.Euler(proyectile.transform.eulerAngles + transform.eulerAngles - new Vector3(0, 90, 0)), transform).transform.parent = null;
                    }
                }
            }
            else
            {
                //B
                if (Input.GetButtonDown("B" + player))
                {
                    if ((animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && !animator.IsInTransition(0)) || animator.GetNextAnimatorStateInfo(0).IsName("idle"))
                    {
                        foot.SetActive(true);
                        animator.SetTrigger("Kick");
                    }
                }
            }
        }
        if (Input.GetButton("C" + player) && !Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            //C
            if (Input.GetButtonDown("C" + player))
            {
                if ((animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && !animator.IsInTransition(0)) || animator.GetNextAnimatorStateInfo(0).IsName("idle"))
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
