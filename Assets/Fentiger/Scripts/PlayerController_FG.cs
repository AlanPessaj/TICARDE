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
    FrogController_FG rana;
    public Vector3 raycastPos;
    // Start is called before the first frame update
    void Start()
    {
        isPlayer1 = name == "Player1";
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("onFrog: " + onFrog);
        if (isPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.W) && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x <= otherPlayer.transform.position.x) && !facingTreeDown)
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
            }
            if (Input.GetKeyDown(KeyCode.S) && generator.distance - generator.despawnRadius < transform.position.x && transform.position.x > 0 && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x >= otherPlayer.transform.position.x) && !facingTreeUp)
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
            }
            if (Input.GetKeyDown(KeyCode.A) && transform.position.z < 12f && !facingTreeRight)
            {
                if (!onLog && !onFrog)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) + 1);
                }
                else if(onLog)
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
            }
            if (Input.GetKeyDown(KeyCode.D) && transform.position.z > -12f && !facingTreeLeft)
            {
                if (!onLog && !onFrog)
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
                }
                else
                {
                    if (!rana.isJumping)
                    {
                        transform.rotation = Quaternion.identity;
                        //transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) - 1, transform.position.y, transform.position.z);
                        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) - 1);

                        onFrog = false;
                    }
                }
            }

            if (transform.position.z > 13f || transform.position.z < -13f)
            {
                //Perder vida
                SceneManager.LoadScene("MENU");
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x <= otherPlayer.transform.position.x) && !facingTreeDown)
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
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && generator.distance - generator.despawnRadius < transform.position.x && transform.position.x > 0 && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x >= otherPlayer.transform.position.x) && !facingTreeUp)
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
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.z < 12f && !facingTreeRight)
            {
                if (!onLog && !onFrog)
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
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.z > -12f && !facingTreeLeft)
            {
                if (!onLog && !onFrog)
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
                }
                else
                {
                    if (!rana.isJumping)
                    {
                        transform.rotation = Quaternion.identity;
                        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) - 1);
                        //transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) - 1, transform.position.y, transform.position.z);
                        onFrog = false;
                    }
                }
            }
        }
        CheckTile();
    }

    void CheckTile()
    {
        //Debug.DrawRay(transform.position + Vector3.up * 2, Vector3.down, Color.red, 0.5f, false);
        Physics.Raycast(transform.position + Vector3.up * 2, Vector3.down, out RaycastHit hit, 10f, Physics.AllLayers - LayerMask.GetMask("Tree", "Player"));
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Transport"))
            {
                if (hit.collider.gameObject.name == "LillyPad(Clone)")
                {
                    if (hit.transform.childCount == 1)
                    {
                        transform.parent = hit.transform;
                        transform.localPosition = new Vector3(0, 1, 0);
                    }
                }
                else if (hit.collider.gameObject.name == "Frog(Clone)")
                {
                    if (!hit.transform.gameObject.GetComponent<FrogController_FG>().isJumping && hit.transform.childCount == 1)
                    {
                        rana = hit.transform.gameObject.GetComponent<FrogController_FG>();
                        transform.parent = hit.transform;
                        transform.localPosition = new Vector3(0, 1.5f, -0.5f);
                        onFrog = true;
                    }
                }
                else
                {
                    transform.parent = hit.transform;
                    onLog = true;
                    transform.position = new Vector3(transform.position.x, -1.5f, transform.position.z);
                }
                return;
            }
            else
            {
                transform.parent = null;
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), -2, Mathf.RoundToInt(transform.position.z));

                onLog = false;
            }
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Out") && !immortal)
            {
                //perder vida
                SceneManager.LoadScene("MENU");
                return;
            }
        }
        raycastPos = transform.TransformPoint(Vector3.zero);
        Debug.DrawRay(raycastPos, Vector3.forward * 1.3f, Color.red, 1, false);   // Derecha
        Debug.DrawRay(raycastPos, -Vector3.forward * 1.3f, Color.blue, 1, false); // Izquierda
        Debug.DrawRay(raycastPos, Vector3.right * 1.3f, Color.green, 1, false);   // Abajo
        Debug.DrawRay(raycastPos, -Vector3.right * 1.3f, Color.yellow, 1, false);  // Arriba

        if (Physics.Raycast(raycastPos, Vector3.forward, 1.3f, LayerMask.GetMask("Tree")))
        {
            facingTreeRight = true;
            //Derecha
        }
        else
        {
            facingTreeRight = false;
        }
        if (Physics.Raycast(raycastPos, -Vector3.forward, 1.3f, LayerMask.GetMask("Tree")))
        {
            facingTreeLeft = true;
            //Izquierda
        }
        else
        {
            facingTreeLeft = false;
        }
        if (Physics.Raycast(raycastPos, Vector3.right, 1.3f, LayerMask.GetMask("Tree")))
        {
            facingTreeDown = true;
            //Abajo
        }
        else
        {
            facingTreeDown = false;
        }
        if (Physics.Raycast(raycastPos, -Vector3.right, 1.3f, LayerMask.GetMask("Tree")))
        {
            facingTreeUp = true;
            //Arriba
        }
        else
        {
            facingTreeUp = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Out") && !immortal)
        {
            //perder vida
            SceneManager.LoadScene("MENU");
            //BORRAME >:(
        }
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Seagull") && !immortal)
        {
            //perder vida
            SceneManager.LoadScene("MENU");
            Destroy(other.gameObject);
            //BORRAME >:(
        }
    }
}