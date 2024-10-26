using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glad0s_FG : MonoBehaviour
{
    public GameObject redPrefab;
    public GameObject redPortal;
    // Start is called before the first frame update
    void Start()
    {
        redPortal = Instantiate(redPrefab, new Vector3(transform.position.x + Random.Range(30, 71), transform.position.y, Random.Range(-12, 12)), Quaternion.Euler(0,0,90));
    }
}
