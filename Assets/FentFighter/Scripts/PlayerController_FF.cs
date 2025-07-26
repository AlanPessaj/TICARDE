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
    public float comboTime;
    public bool punchHit;
    public bool airborne;
    bool isColliding;
    Animator animator;
    public float slideKickCooldown;
    public AudioClip abilitySound;
    // Start is called before the first frame update
    void Start()
    {
        isPlayer1 = gameObject.name == "Player1";
        animator = GetComponent<Animator>();
        facingLeft = otherPlayer.transform.position.x < transform.position.x;
    }
    public bool facingLeft;
    public bool isPlayer1;
    public float pMovDirection;
    // Update is called once per frame
    void Update()
    {
        if (slideKickCooldown > 0) slideKickCooldown -= Time.deltaTime;
        float movDirection = 0;
        facingLeft = otherPlayer.transform.position.x < transform.position.x;
        if (((facingLeft && Mathf.Approximately(transform.eulerAngles.y, 90)) || (!facingLeft && Mathf.Approximately(transform.eulerAngles.y, 270))) && !InState("TurnAround") && !InState("CrouchedTurnAround")) animator.SetTrigger("turnAround");
        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.D) && !InState("Death") && !InState("HitSlideKick"))
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
            if (Input.GetKey(KeyCode.A) && !InState("Death") && !InState("HitSlideKick"))
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
            animator.SetBool("holdCrouch", Input.GetKey(KeyCode.S) && !InState("Death") && !InState("HitSlideKick"));
            if (Input.GetKey(KeyCode.S) && !InState("Death") && !InState("HitSlideKick"))
            {
                movDirection /= 2;
                if (InState("Blocking")) movDirection = 0;
                if (Input.GetKeyDown(KeyCode.S)) DetectCombo("s", "A", "", Smash, false);
                if (colliders.activeSelf)
                {
                    colliders.SetActive(false);
                    cColliders.SetActive(true);
                    GetComponent<CapsuleCollider>().height = 1.692f / transform.localScale.y;
                    GetComponent<CapsuleCollider>().center = Vector3.up * (0.846f / transform.localScale.y);
                }
            }
            else
            {
                if (cColliders.activeSelf)
                {
                    colliders.SetActive(true);
                    cColliders.SetActive(false);
                    GetComponent<CapsuleCollider>().height = 2.2325f / transform.localScale.y;
                    GetComponent<CapsuleCollider>().center = Vector3.up * (1.11625f / transform.localScale.y);
                }
            }
            if (InState("Blocking")) movDirection /= 2;
            if (Input.GetKeyDown(KeyCode.W) && !InState("Death") && !InState("HitSlideKick"))
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
            if (Input.GetKey(KeyCode.RightArrow) && !InState("Death") && !InState("HitSlideKick"))
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
            if (Input.GetKey(KeyCode.LeftArrow) && !InState("Death") && !InState("HitSlideKick"))
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
            animator.SetBool("holdCrouch", Input.GetKey(KeyCode.DownArrow) && !InState("Death") && !InState("HitSlideKick"));
            if (Input.GetKey(KeyCode.DownArrow) && !InState("Death") && !InState("HitSlideKick"))
            {
                if (InState("Blocking"))
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
                    GetComponent<CapsuleCollider>().height = 1.692f / transform.localScale.y;
                    GetComponent<CapsuleCollider>().center = Vector3.up * (0.846f / transform.localScale.y); 
                }
            }
            else
            {
                if (cColliders.activeSelf)
                {
                    colliders.SetActive(true);
                    cColliders.SetActive(false);
                    GetComponent<CapsuleCollider>().height = 2.2325f / transform.localScale.y;
                    GetComponent<CapsuleCollider>().center = Vector3.up * (1.11625f / transform.localScale.y);
                }
            }
            if (InState("Blocking"))
            {
                movDirection /= 2;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && !InState("Death") && !InState("HitSlideKick"))
            {
                if (!airborne)
                {
                    GetComponent<Rigidbody>().AddForce(movementSpeed * movDirection, jumpForce, 0, ForceMode.Impulse);
                    animator.SetTrigger("jump");
                }
            }
        }
        if (!InState("Death") && !InState("HitSlideKick")) CheckButtons();
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
            transform.Translate(Vector3.right * movDirection * movementSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(movementForce * movDirection, 0, 0);
        }
        if (!InState("Death") && !InState("HitSlideKick")) UpdateCombo();
        pMovDirection = movDirection;
    }

    void CheckButtons()
    {
        string player = isPlayer1 ? "" : "2";
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
                        UpperCut();
                    }
                }
            }
            else if (Input.GetButton("C" + player))
            {
                //A + C
                if (Input.GetButtonDown("A" + player) && Input.GetButtonDown("C" + player))
                {
                    Ability();
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
                    DetectCombo("A", "C", player, Ability);
                    DetectCombo("A", "B", player, UpperCut);
                    if (InState("Idle", 1)) animator.SetTrigger("punch");
                }
            }

        }
        if (Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            if (Input.GetButton("C" + player))
            {
                //B + C
                if (Input.GetButtonDown("B" + player) && Input.GetButtonDown("C" + player)) Ulti();
            }
            else
            {
                //B
                if (Input.GetButtonDown("B" + player))
                {
                    DetectCombo("B", "C", player, Ulti);
                    DetectCombo("B", "A", player, UpperCut);
                    if ((InState("Idle", 1) && !(InState("Crouched") || InState("CrouchedRunFowards") || InState("CrouchedRunBackwards"))) || (slideKickCooldown <= 0 && (InState("Crouched") || InState("CrouchedRunFowards") || InState("CrouchedRunBackwards")) && !InState("CrouchedRunBackwards"))) animator.SetTrigger("kick");
                }
            }
        }
        if (Input.GetButton("C" + player) && !Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            //C
            if (Input.GetButtonDown("C" + player))
            {
                DetectCombo("C", "B", player, Ulti);
                DetectCombo("C", "A", player, Ability);
            }
            if (InState("Idle", 1))
            {
                animator.SetBool("holdBlock", true);
                animator.SetFloat("speed", 0.5f);
            }
        }
        else
        {
            animator.SetBool("holdBlock", false);
            animator.SetFloat("speed", 1f);
        }
        if (!Input.GetButton("C" + player) && !Input.GetButton("B" + player) && !Input.GetButton("A" + player))
        {
            //NONE
        }
    }

    struct Combo
    {
        public string Button1 { get; set; }
        public string Button2 { get; set; }
        public float TimeRemaining { get; set; }
        public string ActionName { get; set; }
        public bool IsBtn1 { get; set; }
        public bool IsBtn2 { get; set; }

        public Combo(string button1, string button2, float timeRemaining, string actionName, bool isBtn1, bool isBtn2)
        {
            Button1 = button1;
            Button2 = button2;
            TimeRemaining = timeRemaining;
            ActionName = actionName;
            IsBtn1 = isBtn1;
            IsBtn2 = isBtn2;
        }
    }
    List<Combo> combos = new List<Combo>();

    void DetectCombo(string button1, string button2, string player, System.Action func, bool isBtn1 = true, bool isBtn2 = true)
    {
        if (isBtn1)
            button1 += player;
        if (isBtn2)
            button2 += player;

        if (!combos.Exists(combo => combo.Button1 == button1 && combo.Button2 == button2 && combo.ActionName == func.Method.Name && combo.IsBtn1 == isBtn1 && combo.IsBtn2 == isBtn2))
        {
            combos.Add(new Combo(button1, button2, comboTime, func.Method.Name, isBtn1, isBtn2));
        }
    }

    void UpdateCombo()
    {
        Queue<Combo> removeQueue = new Queue<Combo>();
        for (int i = 0; i < combos.Count; i++)
        {
            Combo combo = combos[i];
            if (combo.TimeRemaining <= 0)
            {
                removeQueue.Enqueue(combo);
                continue;
            }

            if (combo.IsBtn1)
            {
                if (combo.IsBtn2)
                {
                    if (Input.GetButton(combo.Button1) && Input.GetButton(combo.Button2))
                    {
                        Invoke(combo.ActionName, 0);
                        removeQueue.Enqueue(combo);
                        continue;
                    }
                }
                else
                {
                    if (Input.GetButton(combo.Button1) && Input.GetKey(combo.Button2))
                    {
                        Invoke(combo.ActionName, 0);
                        removeQueue.Enqueue(combo);
                        continue;
                    }
                }
            }
            else
            {
                if (combo.IsBtn2)
                {
                    if (Input.GetKey(combo.Button1) && Input.GetButton(combo.Button2))
                    {
                        Invoke(combo.ActionName, 0);
                        removeQueue.Enqueue(combo);
                        continue;
                    }
                }
                else
                {
                    if (Input.GetKey(combo.Button1) && Input.GetKey(combo.Button2))
                    {
                        Invoke(combo.ActionName, 0);
                        removeQueue.Enqueue(combo);
                        continue;
                    }
                }
            }
            combo.TimeRemaining -= Time.deltaTime;
            combos[i] = combo;
        }

        while (removeQueue.Count > 0)
        {
            combos.Remove(removeQueue.Dequeue());
        }
    }
    void Smash()
    {
        if (airborne)
        {
            animator.ResetTrigger("punch");
            // if (InState("Punch")) animator.CrossFade("Smash", 0.25f);
            // if (InState("UpperCut")) animator.SetBool("cutToSmash", true);
            if (InState("Idle", 1) || InState("Punch") || InState("UpperCut")) animator.SetTrigger("smash");
        }
    }

    void UpperCut()
    {
        // if (InState("Kick") || InState("Punch")) animator.CrossFade("UpperCut", 0.25f);
        if (InState("Idle", 1) || InState("Kick") || InState("Punch")) animator.SetTrigger("upperCut");
    }

    void Ability()
    {
        if (GetComponent<UIManager_FF>().RemoveXP(25))
        {
            animator.SetTrigger("ability");
            GetComponent<AudioSource>().PlayOneShot(abilitySound);
            StartCoroutine(GAMEMANAGER.Instance.GetComponent<LedsController>().SingleBlink(isPlayer1, "BLUE"));
            GameObject temp = Instantiate(proyectile, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            temp.GetComponent<LinearMover_FF>().goingLeft = facingLeft;
            temp.GetComponent<Damage_FF>().owner = gameObject;
        }
    }

    void Ulti()
    {
        if (GetComponent<UIManager_FF>().RemoveXP(100))
        {
            animator.SetTrigger("ability");
            GetComponent<AudioSource>().PlayOneShot(abilitySound);
            StartCoroutine(GAMEMANAGER.Instance.GetComponent<LedsController>().SideBlink(isPlayer1, "MAGENTA"));
            GameObject temp = Instantiate(proyectile, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            temp.GetComponent<LinearMover_FF>().goingLeft = facingLeft;
            temp.transform.GetChild(1).gameObject.SetActive(true);
            temp.GetComponent<Damage_FF>().damage *= 2;
            temp.GetComponent<Damage_FF>().type = DamageType.Ulti;
            temp.GetComponent<Damage_FF>().owner = gameObject;
        }
    }

    public bool InState(string name, int animatorLayer = -1)
    {
        if (animatorLayer == -1)
        {
            for (int i = 0; i < animator.layerCount; i++)
            {
                if ((animator.GetCurrentAnimatorStateInfo(i).IsName(name) && !animator.IsInTransition(i)) || animator.GetNextAnimatorStateInfo(i).IsName(name))
                    return true;
            }
            return false;
        }
        else return animator.GetCurrentAnimatorStateInfo(animatorLayer).IsName(name) && !animator.IsInTransition(animatorLayer) || animator.GetNextAnimatorStateInfo(animatorLayer).IsName(name);
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
