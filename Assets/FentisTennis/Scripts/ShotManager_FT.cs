using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotManager_FT : MonoBehaviour
{
    public BallMover_FT ball;
    GameManager_FT gameManager;
    float lateralDistance = 12.4f;
    public float driveHeight;
    public float lobHeight;
    public float stepSize;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager_FT>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ShotFinder(0, 0, false, gameObject, true);
        }
    }

    public void ShotFinder(int direction, float power, bool lob, GameObject player, bool random = false)
    {
        float initX = 0;
        float initZ = 0;
        float finX = 0;
        float finZ = 0;
        direction *= -1;
        power += lateralDistance * 2;
        if (random)
        {
            initX = Mathf.Lerp(0, 51, Random.value);
            initZ = Mathf.Lerp(-31, 31, Random.value);
            finX = initX - Random.Range(1f, 100f);
            finZ = (Random.Range(-2, 3) * 12.4f) + initZ;
        }
        else
        {
            initX = player.transform.position.x;
            initZ = player.transform.position.z;
            finX = initX + Mathf.Sqrt(Mathf.Pow(power, 2) - Mathf.Pow(direction * lateralDistance, 2));
            finZ = initZ + (direction * lateralDistance);
        }
        if (!lob)
        {
            ball.height = driveHeight;
        }
        else
        {
            ball.height = lobHeight;
        }
        gameManager.EndServe();
        ball.sPoint.position = new Vector3(initX, 6, initZ);
        ball.ePoint.position = new Vector3(finX, 0, finZ);
        ball.step = 0f;
        ball.stepSize = stepSize;
        ball.rolling = false;
        ball.enabled = true;
        ball.UpdateQuadratic();
    }

}
