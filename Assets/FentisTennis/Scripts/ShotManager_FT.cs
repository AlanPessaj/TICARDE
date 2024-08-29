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
            ShotFinder(0, 0, ShotType.drive, gameObject, true);
        }
    }

    public void ShotFinder(int direction, float power, ShotType type, GameObject player, bool random = false)
    {
        int directionModifier = 1;
        if (random)
        {
            player.transform.position = new Vector3(Random.Range(0f, 51f), 0, Random.Range(-31, 31));
            player.transform.eulerAngles = new Vector3(0, 180, 0);
            direction = Random.Range(-2, 3);
            power = Random.Range(40f, 80f);
            type = ShotType.lob;
        }
        if (player.transform.eulerAngles.y == 180)
        {
            directionModifier = -1;
        }
        direction *= -1;
        power += lateralDistance * 2;
        float initX = player.transform.position.x;
        float initZ = player.transform.position.z;
        float finX = initX + Mathf.Sqrt(Mathf.Pow(power, 2) - Mathf.Pow(direction * lateralDistance, 2)) * directionModifier;
        float finZ = initZ + (direction * lateralDistance);
        if (type == ShotType.drive)
        {
            ball.height = driveHeight;
        }
        else if (type == ShotType.lob)
        {
            ball.height = lobHeight;
        }
        else
        {
            ball.height = ball.transform.position.y;
        }
        gameManager.EndServe();
        ball.sPoint.position = new Vector3(initX, ball.transform.position.y, initZ);
        ball.ePoint.position = new Vector3(finX, 0, finZ);
        ball.step = 0f;
        ball.stepSize = stepSize;
        ball.rolling = false;
        ball.UpdateQuadratic(type == ShotType.smash);
    }

}

public enum ShotType
{
    drive,
    lob,
    smash
}
