using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glad0s_FG : MonoBehaviour
{
    public GameObject redPrefab;
    public GameObject redPortal;
    int randomX;
    int randomZ;
    // Start is called before the first frame update
    void Start()
    {
        randomX = Random.Range(30, 71);
        int tries = 1;
        bool foundGrass = false;

        while (!foundGrass)
        {
            if (Physics.Raycast(new Vector3(randomX, transform.position.y, 0) + new Vector3(tries, 0, 0), Vector3.down, out RaycastHit hit, 10f, LayerMask.GetMask("Grass")))
            {
                if (hit.collider != null)
                {
                    foundGrass = true;
                    redPortal = Instantiate(redPrefab, new Vector3(tries + randomX, transform.position.y, Random.Range(-12, 12)), Quaternion.Euler(0, 0, 90));
                }
            }
            tries++;
        }
    }
}