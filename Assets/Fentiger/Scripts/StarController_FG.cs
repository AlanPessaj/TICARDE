using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController_FG : MonoBehaviour
{
    public AudioClip pickUp;
    private void Update()
    {
        
        if (Physics.Raycast(transform.position + Vector3.up * 2f, Vector3.down, out RaycastHit hit, 3f, LayerMask.GetMask("Player")) && hit.transform.GetComponent<UIManager_FG>().AddXP(25))
        {
            hit.transform.GetComponent<PlayerController_FG>().generator.GetComponent<SoundManager_FG>().PlaySound(pickUp);
            StartCoroutine(hit.transform.GetComponent<PlayerController_FG>().generator.GetComponent<LedsController>().SingleBlink(hit.transform.name == "Player1", "BLUE", GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>().initialMultiplayer));
            Destroy(gameObject);
        }
    }
}
