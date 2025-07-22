using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glad0s_FG : MonoBehaviour
{
    public GameObject redPrefab;
    public GameObject redPortal;
    float randomX;
    // Start is called before the first frame update
    void Start()
    {
        randomX = Random.Range(30, 71) + 0.5f;
        int tries = 1;
        bool foundGrass = false;

        while (!foundGrass)
        {
            Debug.DrawRay(new Vector3(randomX + transform.position.x, transform.position.y + 10, 0), Vector3.down * 10f, Color.green, 10f);
            Debug.DrawRay(new Vector3((randomX - tries) + transform.position.x, transform.position.y+10, 0), Vector3.down * 10f, Color.red, 10f);
            if (Physics.Raycast(new Vector3((randomX - tries) + transform.position.x , transform.position.y, 0), Vector3.down, out RaycastHit hit, 10f, LayerMask.GetMask("Grass")))
            {
                if (hit.collider != null)
                {
                    foundGrass = true;
                    redPortal = Instantiate(redPrefab, new Vector3(randomX + 0.5f - tries + transform.position.x - 1, transform.position.y, Random.Range(-12, 12)), Quaternion.Euler(0, 0, 90));
                }
            }
            tries++;
            if (tries > 100)
            {
                Destroy(gameObject);
            }
        }

    }
}