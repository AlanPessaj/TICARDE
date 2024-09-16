using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_FG : MonoBehaviour
{
    public Generator_FG generator;
    bool isPlayer1;
    public bool immortal;
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
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                if (generator.distance <= transform.position.x + (generator.despawnRadius / 2))
                {
                    generator.GenerateZones();
                }
            }
            if (Input.GetKeyDown(KeyCode.S) && generator.distance - generator.despawnRadius < transform.position.x && transform.position.x > 0)
            {
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.A) && transform.position.z < 12f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            }
            if (Input.GetKeyDown(KeyCode.D) && transform.position.z > -12f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                if (generator.distance <= transform.position.x + (generator.despawnRadius / 2))
                {
                    generator.GenerateZones();
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && generator.distance - generator.despawnRadius < transform.position.x && transform.position.x > 0)
            {
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.z < 12f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.z > -12f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            }
        }
        CheckTile();
    }

    void CheckTile()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 10f, LayerMask.GetMask("Out")) && !immortal)
        {
            //perder vida
            SceneManager.LoadScene(gameObject.scene.name);
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
