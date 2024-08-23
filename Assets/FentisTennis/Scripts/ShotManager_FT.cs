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
        float finX = initX - Random.Range(1f, 100f);
        float finZ = (Random.Range(-2, 3) * 12.4f) + initZ;
        ball.sPoint.position = new Vector3(initX, 6, initZ);
        ball.ePoint.position = new Vector3(finX, 0, finZ);
        ball.step = 0f;
        ball.height = 12f;
        ball.stepSize = 0.7f;
        ball.rolling = false;
        ball.UpdateQuadratic();
    }

}
