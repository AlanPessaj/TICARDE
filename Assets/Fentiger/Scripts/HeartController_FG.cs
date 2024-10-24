using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController_FG : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !collision.gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            collision.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
