using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController_FG : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.transform.GetComponent<UIManager_FG>().AddXP(25))
            {
                //SONIDO
                Destroy(gameObject);
            }
        }
    }
}
