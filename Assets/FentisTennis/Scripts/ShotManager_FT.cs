using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotManager_FT : MonoBehaviour
{
    PlayerController_FT player;
    public BallMover_FT ball;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController_FT>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("B"))
        {
            ShotFinder();
        }
    }

    public void ShotFinder()
    {
        float initX = Mathf.Lerp(0, 51, Random.value); 
        float initZ = Mathf.Lerp(-31, 31, Random.value);
        float finX = Mathf.Lerp(-50.5f, 0, Random.value); 
        float finZ = Mathf.Lerp(-31, 31, Random.value);
        ball.sPoint.position = new Vector3(initX, 6, initZ);
        ball.ePoint.position = new Vector3(finX, -1, finZ);
        ball.step = 0f;
        ball.height = 12f;
        ball.stepSize = 0.7f;
    }

}
