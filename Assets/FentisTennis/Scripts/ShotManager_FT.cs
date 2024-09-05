using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShotManager_FT : MonoBehaviour
{
    public bool ballHit;
    public BallMover_FT ball;
    GameManager_FT gameManager;
    float lateralDistance = 8.4f;
    public float driveHeight;
    public float lobHeight;
    public float stepSize;
    public bool finishedHit;
    public GameObject court;
    public float minPower;
    public float maxPower;
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
            Debug.Log(PredictShot(GameObject.Find("Player1"), ShotType.drive));
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            FindShot(0, ShotType.drive, false, true);
        }
    }

    public float[] PredictShot(GameObject player, ShotType type)
    {
        Scene simulationScene = SceneManager.CreateScene("simulation " + player.name);
        Physics.autoSimulation = false;
        float[] results = null;
        player.GetComponent<Rigidbody>().useGravity = false;
        GameObject cPlayer = Instantiate(player, player.transform.position + new Vector3(0, 0, 1000), player.transform.rotation);
        GameObject cCourt = Instantiate(court, court.transform.position + new Vector3(0, 0, 1000), court.transform.rotation);
        GameObject cSPoint = Instantiate(ball.sPoint.gameObject, ball.sPoint.transform.position + new Vector3(0, 0, 1000), ball.sPoint.transform.rotation);
        GameObject cEPoint = Instantiate(ball.ePoint.gameObject, ball.ePoint.transform.position + new Vector3(0, 0, 1000), ball.ePoint.transform.rotation);
        cCourt.transform.localScale = court.transform.lossyScale;
        Destroy(cPlayer.GetComponent<MeshRenderer>());
        Destroy(cPlayer.GetComponent<Rigidbody>());
        Destroy(cCourt.GetComponent<MeshRenderer>());
        player.GetComponent<Rigidbody>().useGravity = true;
        switch (type)
        {
            case ShotType.drive:
                cPlayer.GetComponent<PlayerController_FT>().doingDrive = true;
            break;
            case ShotType.lob:
                cPlayer.GetComponent<PlayerController_FT>().doingLob = true;
                break;
            case ShotType.smash:
                cPlayer.GetComponent<PlayerController_FT>().doingSmash = true;
                break;
        }
        GameObject cBall;
        SceneManager.MoveGameObjectToScene(cPlayer, simulationScene);
        SceneManager.MoveGameObjectToScene(cSPoint, simulationScene);
        SceneManager.MoveGameObjectToScene(cEPoint, simulationScene);
        SceneManager.MoveGameObjectToScene(cCourt, simulationScene);
        if (gameManager.serve == int.Parse(player.name.Substring(player.name.Length - 1)))
        {
            cBall = cPlayer.transform.GetChild(2).gameObject;
        }
        else
        {
            cBall = Instantiate(ball.gameObject, ball.transform.position + new Vector3(0, 0, 1000), ball.transform.rotation);
            SceneManager.MoveGameObjectToScene(cBall, simulationScene);
        }
        cBall.GetComponent<BallMover_FT>().sPoint = cSPoint.transform;
        cBall.GetComponent<BallMover_FT>().ePoint = cEPoint.transform;
        cBall.GetComponent<BallMover_FT>().UpdateQuadratic();
        //cBall.GetComponent<BallMover_FT>().active = false;
        cPlayer.name = player.name;
        cPlayer.GetComponent<PlayerController_FT>().simShot = this;
        cPlayer.GetComponent<PlayerController_FT>().shot = null;
        Destroy(cBall.GetComponent<MeshRenderer>());
        for (int i = 1; i < 100; i++)
        {
            simulationScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
            cBall.GetComponent<BallMover_FT>().Update();
            cPlayer.GetComponent<PlayerController_FT>().Update();
            cPlayer.transform.GetChild(0).gameObject.GetComponent<HitManager_FT>().Update();
            if (ballHit)
            {
                results = new float[3];
                SceneManager.UnloadSceneAsync(simulationScene);
                Physics.autoSimulation = true;
                results[0] = i;
                //results[1] = ballHit[0];
                //results[2] = ballHit[1];
                break;
            }
            if (finishedHit)
            {
                finishedHit = false;
                break;
            }
        }
        SceneManager.UnloadSceneAsync(simulationScene);
        Physics.autoSimulation = true;
        ballHit = false;
        return results;
    }

    public void FindShot(int direction, ShotType type, bool isPlayer1, bool random = false)
    {
        int directionModifier = 1;
        if (random)
        {
            ball.transform.position = new Vector3(23, 0, 0);
            direction = 0;
            //player.transform.position = new Vector3(Random.Range(0f, 51f), 0, Random.Range(-31, 31));
            //direction = Random.Range(-2, 3);
            //power = 40f;
            isPlayer1 = false;
            type = ShotType.lob;
        }
        if (!isPlayer1)
        {
            directionModifier = -1;
        }
        direction *= -1;
        //power += lateralDistance * 2;
        float initX = ball.transform.position.x;
        float initZ = ball.transform.position.z;
        //float finX = initX + Mathf.Sqrt(Mathf.Pow(power, 2) - Mathf.Pow(direction * lateralDistance, 2)) * directionModifier;
        float finX = Random.Range(minPower, maxPower) * directionModifier;
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
