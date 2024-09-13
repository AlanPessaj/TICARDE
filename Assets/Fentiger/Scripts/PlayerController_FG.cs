using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_FG : MonoBehaviour
{
    public Generator_FG generator;
    bool avoidFront;
    bool avoidBack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !avoidFront)
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            if (generator.distance <= transform.position.x + (generator.despawnRadius / 2))
            {
                generator.GenerateZones();
            }
            CheckTile();
        }
        if (Input.GetKeyDown(KeyCode.S) && generator.distance - generator.despawnRadius < transform.position.x && !avoidBack)
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            CheckTile();
        }
        if (Input.GetKeyDown(KeyCode.A) && transform.position.z < 17f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            CheckTile();
        }
        if (Input.GetKeyDown(KeyCode.D) && transform.position.z > -17f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            CheckTile();
        }
    }

    void CheckTile()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 10f, LayerMask.GetMask("Out")))
        {
            //perder vida
            SceneManager.LoadScene(gameObject.scene.name);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Out"))
        {
            //perder vida
            SceneManager.LoadScene("Game(FG)");
            //BORRAME >:(
        }
    }
}
