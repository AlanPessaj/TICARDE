using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShotManager_FT : MonoBehaviour
{
    const float noHit = 200;
    public float ballHit = noHit;
    public BallMover_FT ball;
    GameManager_FT gameManager;
    float lateralDistance = 12.4f;
    public float driveHeight;
    public float lobHeight;
    public float stepSize;
    public bool finishedHit;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager_FT>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log(PredictShot(GameObject.Find("Player1")));
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            FindShot(0, 0, ShotType.drive, gameObject, true);
        }
    }

    public float[] PredictShot(GameObject player)
    {
        Scene simulationScene = SceneManager.CreateScene("simulation " + player.name);
        Physics.autoSimulation = false;
        float[] results = new float[2];
        GameObject cPlayer = Instantiate(player, player.transform.position + new Vector3(100, 100, 100), player.transform.rotation);
        GameObject cBall;
        Transform cSPoint = ball.sPoint;
        Transform cEPoint = ball.ePoint;
        cSPoint.position = cSPoint.position + new Vector3(100, 100, 100);
        cEPoint.position = cEPoint.position + new Vector3(100, 100, 100);
        SceneManager.MoveGameObjectToScene(cPlayer, simulationScene);
        if (gameManager.serve == int.Parse(player.name.Substring(player.name.Length - 1)))
        {
            cBall = cPlayer.transform.GetChild(2).gameObject;
        }
        else
        {
            cBall = Instantiate(ball.gameObject, ball.transform.position + new Vector3(100, 100, 100), ball.transform.rotation);
            SceneManager.MoveGameObjectToScene(cBall, simulationScene);
        }
        cBall.GetComponent<BallMover_FT>().sPoint = cSPoint;
        cBall.GetComponent<BallMover_FT>().ePoint = cEPoint;
        Destroy(cPlayer.GetComponent<MeshRenderer>());
        cPlayer.name = player.name;
        cPlayer.GetComponent<PlayerController_FT>().simShot = this;
        cPlayer.GetComponent<PlayerController_FT>().shot = null;
        Destroy(cPlayer.GetComponent<Rigidbody>());
        Destroy(cBall.GetComponent<MeshRenderer>());
        for (int i = 1; i < 100; i++)
        {
            simulationScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
            cBall.GetComponent<BallMover_FT>().Update();
            cPlayer.GetComponent<PlayerController_FT>().Update();
            cPlayer.transform.GetChild(0).gameObject.GetComponent<HitManager_FT>().Update();
            if (ballHit != noHit)
            {
                ballHit = noHit;
                SceneManager.UnloadSceneAsync(simulationScene);
                Physics.autoSimulation = true;
                results[0] = i;
                results[1] = ballHit;
                return results;
            }
            if (finishedHit)
            {
                break;
            }
        }
        finishedHit = false;
        SceneManager.UnloadSceneAsync(simulationScene);
        Physics.autoSimulation = true;
        return null;
    }

    public void FindShot(int direction, float power, ShotType type, GameObject player, bool random = false)
    {
        int directionModifier = 1;
        if (random)
        {
            player.transform.position = new Vector3(23, 0, 0);
            direction = 0;
            //player.transform.position = new Vector3(Random.Range(0f, 51f), 0, Random.Range(-31, 31));
            player.transform.eulerAngles = new Vector3(0, 180, 0);
            //direction = Random.Range(-2, 3);
            power = Random.Range(40f, 80f);
            type = ShotType.lob;
        }
        else
        {
            int a = 2;
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
