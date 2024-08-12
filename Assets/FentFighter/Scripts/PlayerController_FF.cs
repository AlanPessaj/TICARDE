using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_FF : MonoBehaviour
{
    public GameObject otherPlayer;
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
            if (Input.GetButtonDown("A"))
            {
                if (Input.GetButtonDown("B"))
                {
                    if (Input.GetButtonDown("C"))
                    {
                        //A + B + C
                    }
                    else
                    {
                        //A + B
                    }
                }
                else if (Input.GetButtonDown("C"))
                {
                    //A + C
                }
                else
                {
                    //A
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                    {
                        transform.GetChild(0).gameObject.SetActive(true);
                        animator.SetTrigger("Punch");
                    }
                }
                
            }
            if (Input.GetButtonDown("B"))
            {
                if (!Input.GetButtonDown("A"))
                {
                    if (Input.GetButtonDown("C"))
                    {
                        //B + C
                    }
                    else
                    {
                        //B
                        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                        {
                            transform.GetChild(1).gameObject.SetActive(true);
                            animator.SetTrigger("Kick");
                        }
                    }
                }
            }
            if (Input.GetButtonDown("C"))
            {
                if (!Input.GetButtonDown("B") && !Input.GetButtonDown("A"))
                {
                    //C
                    animator.SetBool("holdBlock", true);
                }
            }
            else
            {
                animator.SetBool("holdBlock", false);
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
            if (Input.GetButtonDown("A2"))
            {
                if (Input.GetButtonDown("B2"))
                {
                    if (Input.GetButtonDown("C2"))
                    {
                        //A + B + C
                    }
                    else
                    {
                        //A + B
                    }
                }
                else if (Input.GetButtonDown("C2"))
                {
                    //A + C
                }
                else
                {
                    //A
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                    {
                        transform.GetChild(0).gameObject.SetActive(true);
                        animator.SetTrigger("Punch");
                    }
                }

            }
            if (Input.GetButtonDown("B2"))
            {
                if (!Input.GetButtonDown("A2"))
                {
                    if (Input.GetButtonDown("C2"))
                    {
                        //B + C
                    }
                    else
                    {
                        //B
                        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                        {
                            transform.GetChild(1).gameObject.SetActive(true);
                            animator.SetTrigger("Kick");
                        }
                    }
                }
            }
            if (Input.GetButtonDown("C2"))
            {
                if (!Input.GetButtonDown("B2") && !Input.GetButtonDown("A2"))
                {
                    //C
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                    {
                        animator.SetBool("holdBlock", true);
                    }
                }
            }
            else
            {
                animator.SetBool("holdBlock", false);
            }
        }
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

    private void OnCollisionEnter(Collision other)
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
