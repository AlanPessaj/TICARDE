using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController_FG : MonoBehaviour
{
    public AudioClip pickUp;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.transform.GetComponent<UIManager_FG>().AddXP(25))
            {
                collision.transform.GetComponent<PlayerController_FG>().generator.GetComponent<SoundManager_FG>().PlaySound(pickUp);
                Destroy(gameObject);
            }
        }
    }
}
