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
    bool onLog = false;
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
    Material material;
    GameObject portal;
    GameObject hippo;
    GameObject log;
    List<string[]> combos = new List<string[]>();
    float comboTime;
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
                DetectCombo("A", "B", "", HeartAbility, HippoAbility);
            }
            if (Input.GetButtonDown("B"))
            {
                DetectCombo("B", "A", "", HeartAbility, PortalAbility);
            }
            if (Input.GetButtonDown("C") && GetComponent<UIManager_FG>().RemoveXP(25)) Invulnerability(true);
            if (Input.GetKeyDown(KeyCode.W))
            {
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
                    portal.GetComponent<AudioSource>().Play();
                }
            }
            if (Input.GetKeyDown(KeyCode.S) && generator.distance - generator.despawnRadius < transform.position.x && transform.position.x > 0 && !facingTreeUp)
            {
                if (generator.multiplayer && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x >= otherPlayer.transform.position.x) || !generator.multiplayer)
                {
                    MoveBackward();
                }
            }
            if (Input.GetKeyDown(KeyCode.A) && transform.position.z < 12f && !facingTreeRight)
            {
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.D) && transform.position.z > -12f && !facingTreeLeft)
            {
                MoveRight();
            }
        }
        else
        {
            if (Input.GetButtonDown("A2"))
            {
                DetectCombo("A", "B", "2", HeartAbility, HippoAbility);
            }
            if (Input.GetButtonDown("B2"))
            {
                DetectCombo("B", "A", "2", HeartAbility, PortalAbility);
            }
            if (Input.GetButtonDown("C2") && GetComponent<UIManager_FG>().RemoveXP(25)) Invulnerability(true);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
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
                    portal.GetComponent<AudioSource>().Play();
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && generator.distance - generator.despawnRadius < transform.position.x && transform.position.x > 0 && !facingTreeUp)
            {
                if (generator.multiplayer && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x >= otherPlayer.transform.position.x) || !generator.multiplayer)
                {
                    MoveBackward();
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.z < 12f && !facingTreeRight)
            {
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.z > -12f && !facingTreeLeft)
            {
                MoveRight();
            }
        }

        if ((transform.position.z > 15f || transform.position.z < -15f) && !immortal)
        {
            //Perder vida
            Die();
        }
        CheckTile();
        UpdateCombo();
    }

    void MoveForward()
    {
        if (!onFrog)
        {
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) + 1, transform.position.y, transform.position.z);
        }
        else
        {
            if (!rana.isJumping)
            {
                transform.rotation = Quaternion.identity;
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) + 1, transform.position.y, transform.position.z);
                onFrog = false;
            }
        }
        if (generator.distance <= transform.position.x + (generator.despawnRadius / 2))
        {
            generator.GenerateZones();
        }
        hasMoved = true;
    }

    void MoveBackward()
    {
        if (!onFrog)
        {
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) - 1, transform.position.y, transform.position.z);
        }
        else
        {
            if (!rana.isJumping)
            {
                transform.rotation = Quaternion.identity;
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) - 1, transform.position.y, transform.position.z);
                onFrog = false;
            }
        }
        hasMoved = true;
    }

    void MoveLeft()
    {
        if (!onLog && !onFrog && !onHippo)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) + 1);
        }
        else if (onLog)
        {
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
        else if (onHippo)
        {
            if (!hippo.GetComponent<LinearMover_FG>().hippoResuming && !hippo.GetComponent<LinearMover_FG>().hippoRotating)
            {
                if (hippo.GetComponent<LinearMover_FG>().movingForward)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) + 2.3f);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) + 1.5f);
                }
            }
        }
        else
        {
            if (!rana.isJumping)
            {
                transform.rotation = Quaternion.identity;
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) + 1);
                onFrog = false;
            }
        }
        hasMoved = true;
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

    void HeartAbility()
    {
        //3
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            if (GetComponent<UIManager_FG>().RemoveXP(75)) transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void HippoAbility()
    {
        //2
        if (Physics.Raycast(transform.position + Vector3.right + Vector3.up, Vector3.down, out RaycastHit hippoCheck, 5f, LayerMask.GetMask("Out")) && hippoCheck.transform.name == "agua")
        {
            if (GetComponent<UIManager_FG>().RemoveXP(50))
            {
                if (hippoCheck.transform.parent.GetComponent<LinearSpawner_FG>().changedSide)
                {
                    Instantiate(hippoCheck.transform.parent.GetComponent<LinearSpawner_FG>().hippo, new Vector3(transform.position.x + 1, -2.7f, transform.position.z + 2), Quaternion.Euler(0, 0, 90), hippoCheck.transform.parent).GetComponent<LinearMover_FG>().spawner = hippoCheck.transform.parent.GetComponent<LinearSpawner_FG>();
                }
                else
                {
                    Instantiate(hippoCheck.transform.parent.GetComponent<LinearSpawner_FG>().hippo,  new Vector3(transform.position.x + 1, -2.7f, transform.position.z - 2), Quaternion.Euler(0, 180, 90), hippoCheck.transform.parent).GetComponent<LinearMover_FG>().spawner = hippoCheck.transform.parent.GetComponent<LinearSpawner_FG>();
                }
            }
        }
    }

    void PortalAbility()
    {
        //2
        if (GetComponent<UIManager_FG>().RemoveXP(50))
        {
            Instantiate(generator.specials[2], new Vector3(transform.position.x + 0.5f, -1.5f, transform.position.z), Quaternion.Euler(0, 0, 90));
        }
    }

    void MoveRight()
    {
        if (!onLog && !onFrog && !onHippo)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) - 1);
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

            log.transform.GetComponent<AudioSource>().PlayOneShot(log.transform.GetComponent<AudioSource>().clip);
        }
        else if (onHippo)
        {
            if (!hippo.GetComponent<LinearMover_FG>().hippoResuming && !hippo.GetComponent<LinearMover_FG>().hippoRotating)
            {
                if (hippo.GetComponent<LinearMover_FG>().movingForward)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) - 1.5f);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) - 2.3f);
                }
            }
        }
        else
        {
            if (!rana.isJumping)
            {
                transform.rotation = Quaternion.identity;
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) - 1);
                onFrog = false;
            }
        }
        hasMoved = true;
    }
    
    void Die()
    {
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            int tries = 1;
            bool foundGrass = false;

            while (tries < 20 && !foundGrass)
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
            if (isPlayer1)
            {
                generator.player1Score = transform.position.x;
            }
            else
            {
                generator.player2Score = transform.position.x;
            }

            Instantiate(ghost, transform.position, Quaternion.identity).GetComponent<DieScript_FG>().playerGhost = true;
            Destroy(gameObject);
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
                    if (hit.transform.childCount == 1)
                    {
                        transform.parent = hit.transform;
                        transform.localPosition = new Vector3(0, 1, 0);
                    }
                }
                else if (hit.collider.gameObject.name == "Frog(Clone)" && !onHippo)
                {
                    if (!hit.transform.gameObject.GetComponent<FrogController_FG>().isJumping && hit.transform.childCount == 1)
                    {
                        rana = hit.transform.gameObject.GetComponent<FrogController_FG>();
                        transform.parent = hit.transform;
                        transform.localPosition = new Vector3(0, 1.5f, -0.5f);
                        onFrog = true;
                    }
                }
                else if (hit.collider.gameObject.name == "Hippo(Clone)" && !onHippo)
                {
                    if (hit.transform.childCount == 0)
                    {
                        transform.parent = hit.transform;
                        transform.localPosition = new Vector3(1.5f, 0, 0.5f);
                        onHippo = true;
                        hippo = hit.collider.gameObject;
                    }
                }
                else if(!onHippo)
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
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), -2, Mathf.RoundToInt(transform.position.z));
                onHippo = false;
                onLog = false;
            }
            if ((hit.collider.gameObject.layer == LayerMask.NameToLayer("Field") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Grass")) && hasMoved)
            {
                generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().step);
                hasMoved = false;
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
                if (hit.transform.parent.name.Contains("Logs") || hit.transform.parent.name == "LilyPads(Clone)")
                {
                    generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().waterFalling);
                }
                else if (hit.collider.gameObject.name == "Car(Clone)")
                {
                    generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().carRunOver);
                }
                else if (hit.collider.gameObject.name == "Out")
                {
                    hit.collider.GetComponent<AudioSource>().Play();
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
        if ((other.collider.gameObject.layer == LayerMask.NameToLayer("Lion") || other.collider.gameObject.layer == LayerMask.NameToLayer("Out")) && !immortal && !onFrog)
        {
            //perder vida
            Die();
            if (other.collider.gameObject.name == "Car(Clone)")
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
            if (other.transform.name.Contains("Shit"))
            {
                Destroy(other.gameObject);
            }
        }
    }

    IEnumerator Invulnerability(bool ability = false)
    {
        immortal = true;
        int index = 4;
        if (ability) index /= 2;
        for (int i = 0; i < index; i++)
        {
            yield return new WaitForSeconds(0.3f);
            GetComponent<Renderer>().material = ghostMaterial;
            yield return new WaitForSeconds(0.3f);
            GetComponent<Renderer>().material = material;
        }
        immortal = false;
    }
    void Nothing() { }
}