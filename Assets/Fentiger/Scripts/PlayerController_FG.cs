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
            if (Input.GetKeyDown(KeyCode.W) && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x <= otherPlayer.transform.position.x))
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) + 1, transform.position.y, transform.position.z);
                if (generator.distance <= transform.position.x + (generator.despawnRadius / 2))
                {
                    generator.GenerateZones();
                }
            }
            if (Input.GetKeyDown(KeyCode.S) && generator.distance - generator.despawnRadius < transform.position.x && transform.position.x > 0 && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x >= otherPlayer.transform.position.x))
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) - 1, transform.position.y, transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.A) && transform.position.z < 12f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) + 1);
            }
            if (Input.GetKeyDown(KeyCode.D) && transform.position.z > -12f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) - 1);
            }

            if (transform.position.z > 13f || transform.position.z < -13f)
            {
                //Perder vida
                SceneManager.LoadScene(gameObject.scene.name);
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x <= otherPlayer.transform.position.x))
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) + 1, transform.position.y, transform.position.z);
                if (generator.distance <= transform.position.x + (generator.despawnRadius / 2))
                {
                    generator.GenerateZones();
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && generator.distance - generator.despawnRadius < transform.position.x && transform.position.x > 0 && (Mathf.Abs(transform.position.x - otherPlayer.transform.position.x) <= 15 || transform.position.x >= otherPlayer.transform.position.x))
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) - 1, transform.position.y, transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.z < 12f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) + 1);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.z > -12f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z) - 1);
            }
        }
        CheckTile();
    }

    void CheckTile()
    {
        Debug.DrawRay(transform.position, Vector3.down * 12, Color.red, 1, false);
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10f);
        if (hit.collider == null)
        {
            return;
        }
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
                transform.position = new Vector3(transform.position.x, -1.5f, transform.position.z);
            }
            return;
        }
        else
        {
            transform.parent = null;
            transform.position = new Vector3(transform.position.x, -2, transform.position.z);
        }
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Out") && !immortal)
        {
            //perder vida
            SceneManager.LoadScene(gameObject.scene.name);
            return;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Out") && !immortal)
        {
            //perder vida
            SceneManager.LoadScene("Game(FG)");
            //BORRAME >:(
        }
    }
}
