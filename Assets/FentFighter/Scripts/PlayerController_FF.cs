using System.Collections;
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
    public bool punchHit;
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
    public float movDirection = 0;
    // Update is called once per frame
    void FixedUpdate()
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
        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.D))
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
            if (Input.GetKey(KeyCode.A))
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
            animator.SetBool("holdCrouch", Input.GetKey(KeyCode.S));
            if (Input.GetKey(KeyCode.S))
            {
                if (hitManager.blocking)
                {
                    movDirection = 0;
                }
                movDirection /= 2;
                colliders.SetActive(false);
                cColliders.SetActive(true);
                GetComponent<CapsuleCollider>().height = 0.72f;
                GetComponent<CapsuleCollider>().center = new Vector3(0, 0.36f, 0);
            }
            else
            {
                colliders.SetActive(true);
                cColliders.SetActive(false);
                GetComponent<CapsuleCollider>().height = 0.95f;
                GetComponent<CapsuleCollider>().center = new Vector3(0, 0.475f, 0);
            }
            if (hitManager.blocking)
            {
                movDirection = movDirection / 2;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (((animator.GetCurrentAnimatorStateInfo(0).IsName("punch") && !animator.IsInTransition(0)) || animator.GetNextAnimatorStateInfo(0).IsName("punch")) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.33f && !punchHit)
                {
                    animator.SetBool("UpperCut", true);
                }
                else if (!airborne)
                {
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(movementSpeed * movDirection, 0, 0);
                    gameObject.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
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
                colliders.SetActive(false);
                cColliders.SetActive(true);
                GetComponent<CapsuleCollider>().height = 0.72f;
                GetComponent<CapsuleCollider>().center = new Vector3(0, 0.36f, 0);
            }
            else
            {
                colliders.SetActive(true);
                cColliders.SetActive(false);
                GetComponent<CapsuleCollider>().height = 0.95f;
                GetComponent<CapsuleCollider>().center = new Vector3(0, 0.475f, 0);
            }
            if (hitManager.blocking)
            {
                movDirection /= 2;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (((animator.GetCurrentAnimatorStateInfo(0).IsName("punch") && !animator.IsInTransition(0)) || animator.GetNextAnimatorStateInfo(0).IsName("punch")) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.33f && !punchHit)
                {
                    animator.SetBool("UpperCut", true);
                }
                else if (!airborne)
                {
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(movementSpeed * movDirection, 0, 0);
                    gameObject.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
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
            gameObject.GetComponent<Rigidbody>().AddForce(movementForce * movDirection, 0, 0);
        }
        movDirection = 0;
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
                            temp.GetComponent<Damage_FF>().damage *= 2;
                            temp.GetComponent<Damage_FF>().type = DamageType.Ulti;
                            temp.GetComponent<Damage_FF>().owner = gameObject;
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
                        GameObject temp = Instantiate(proyectile, transform.position + new Vector3(0, 1, 0), Quaternion.Euler(proyectile.transform.eulerAngles + transform.eulerAngles - new Vector3(0, 90, 0)), transform);
                        temp.transform.parent = null;
                        temp.GetComponent<Damage_FF>().owner = gameObject;
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
