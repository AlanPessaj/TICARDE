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
    // Start is called before the first frame update
    void Start()
    {
        isPlayer1 = name == "Player1";
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.W) && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x <= otherPlayer.transform.position.x) && !facingTreeDown)
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) + 1, transform.position.y, transform.position.z);
                if (generator.distance <= transform.position.x + (generator.despawnRadius / 2))
                {
                    generator.GenerateZones();
                }
            }
            if (Input.GetKeyDown(KeyCode.S) && generator.distance - generator.despawnRadius < transform.position.x && transform.position.x > 0 && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x >= otherPlayer.transform.position.x) && !facingTreeUp)
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) - 1, transform.position.y, transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.A) && transform.position.z < 12f && !facingTreeRight)
            {
                if (!onLog)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) + 1);
                }
                else
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
            }
            if (Input.GetKeyDown(KeyCode.D) && transform.position.z > -12f && !facingTreeLeft)
            {
                if (!onLog)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) - 1);
                }
                else
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
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) + 1, transform.position.y, transform.position.z);
                if (generator.distance <= transform.position.x + (generator.despawnRadius / 2))
                {
                    generator.GenerateZones();
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && generator.distance - generator.despawnRadius < transform.position.x && transform.position.x > 0 && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x >= otherPlayer.transform.position.x) && !facingTreeUp)
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) - 1, transform.position.y, transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.z < 12f && !facingTreeRight)
            {
                if (!onLog)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) + 1);
                }
                else
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
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.z > -12f && !facingTreeLeft)
            {
                if (!onLog)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) - 1);
                }
                else
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
            }
        }
        CheckTile();
    }

    void CheckTile()
    {
        //Debug.DrawRay(transform.position, Vector3.down * 10, Color.red, 0.5f, false);
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10f);
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
                transform.position = new Vector3(transform.position.x, -2, transform.position.z);
                onLog = false;
            }
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Out") && !immortal)
            {
                //perder vida
                SceneManager.LoadScene("MENU");
                return;
            }
        }


        if (Physics.Raycast(transform.position, transform.forward, 1f, LayerMask.GetMask("Tree")))
        {
            Debug.Log("Derecha");
            facingTreeRight = true;
            //Derecha
        }
        else
        {
            facingTreeRight = false;
        }
        if (Physics.Raycast(transform.position, -transform.forward, 1f, LayerMask.GetMask("Tree")))
        {
            Debug.Log("Izquierda");
            facingTreeLeft = true;
            //Izquierda
        }
        else
        {
            facingTreeLeft = false;
        }
        if (Physics.Raycast(transform.position, transform.right, 1f, LayerMask.GetMask("Tree")))
        {
            Debug.Log("Abajo");
            facingTreeDown = true;
            //Abajo
        }
        else
        {
            facingTreeDown = false;
        }
        if (Physics.Raycast(transform.position, -transform.right, 1f, LayerMask.GetMask("Tree")))
        {
            Debug.Log("Arriba");
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