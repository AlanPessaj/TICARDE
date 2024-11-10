using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointReplay : MonoBehaviour
{
    public static PointReplay instance;
    public List<Frame> replay = new List<Frame>();
    [HideInInspector] public int iDirection;
    [HideInInspector] public ShotType shot;
    [HideInInspector] public bool wasPlayer1;
    [HideInInspector] public bool wasServe;
    [HideInInspector] public Vector3 iBallPos;
    [HideInInspector] public Vector3 iP1Pos;
    [HideInInspector] public Vector3 iP2Pos;
    public BallMover_FT ball;
    public KeyCode[] codes;
    public string[] buttons;
    GameObject scorer;
    bool showReplay;
    public Camera[] cameras;
    public float speed;
    bool exitingReplay;
    int cameraIndex;
    Camera[] ogCameras;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        System.Array.Copy(cameras, 1, ogCameras, 0, cameras.Length - 1);
        RandomizeCameras();
    }

    // Update is called once per frame
    void Update()
    {
        if (exitingReplay) { PlayerController_FT.inReplay = false; exitingReplay = false; }
        if (showReplay && !GameManager_FT.instance.transition.activeSelf)
        {
            cameraIndex = Random.Range(1, cameras.Length);
            cameras[0].gameObject.SetActive(false);
            cameras[cameraIndex].gameObject.SetActive(true);
            ball.GetComponent<MeshRenderer>().enabled = true;
            ball.active = true;
            showReplay = false;
            PlayerController_FT.replay = replay;
            ball.transform.position = iBallPos;
            GetComponent<ShotManager_FT>().FindShot(iDirection, shot, wasPlayer1, wasServe);
            GameManager_FT.instance.player1.transform.position = iP1Pos;
            GameManager_FT.instance.player2.transform.position = iP2Pos;
            PlayerController_FT.inReplay = true;
            replay = new List<Frame>();
        }
        if (PlayerController_FT.inReplay)
        {
            if (Input.GetButtonDown("A") || Input.GetButtonDown("A2"))
            {
                PlayerController_FT.replay = null;
                GameManager_FT.instance.AddPoint(scorer);
                PlayerController_FT.frameIndex = 0;
                cameras[0].gameObject.SetActive(true);
                cameras[cameraIndex].gameObject.SetActive(false);
                exitingReplay = true;
                RandomizeCameras();
                return;
            }
            if (PlayerController_FT.frameIndex >= PlayerController_FT.replay.Count)
            {
                cameras[cameraIndex].gameObject.SetActive(false);
                if (cameraIndex < cameras.Length - 1) cameraIndex++; else cameraIndex = 1;
                cameras[cameraIndex].gameObject.SetActive(true);
                PlayerController_FT.frameIndex = 0;
                ball.transform.position = iBallPos;
                GetComponent<ShotManager_FT>().FindShot(iDirection, shot, wasPlayer1, wasServe);
                GameManager_FT.instance.player1.transform.position = iP1Pos;
                GameManager_FT.instance.player2.transform.position = iP2Pos;
            }
            cameras[cameraIndex].transform.LookAt(ball.transform);
            if (cameras[cameraIndex].name.Contains("FollowX")) cameras[cameraIndex].transform.position = new Vector3(ball.transform.position.x, cameras[cameraIndex].transform.position.y, cameras[cameraIndex].transform.position.z);
            if (cameras[cameraIndex].name.Contains("FollowZ")) cameras[cameraIndex].transform.position = new Vector3(cameras[cameraIndex].transform.position.x, cameras[cameraIndex].transform.position.y, ball.transform.position.z);
            PlayerController_FT.currentFrame = PlayerController_FT.replay[PlayerController_FT.frameIndex];
            PlayerController_FT.frameIndex++;
            return;
        }
        if (showReplay) return;
        Frame currentFrame = new Frame();
        foreach (KeyCode item in codes) if (Input.GetKey(item)) currentFrame.keys.Add(item);
        foreach (string item in buttons) if (Input.GetButton(item)) currentFrame.buttons.Add(item);
        foreach (string item in buttons) if (Input.GetButtonDown(item)) currentFrame.buttonDowns.Add(item);
        foreach (string item in buttons) if (Input.GetButtonUp(item)) currentFrame.buttonUps.Add(item);
        replay.Add(currentFrame);
    }

    void RandomizeCameras()
    {
        Camera[] temp = new Camera[cameras.Length];
        temp[0] = cameras[0];
        int[] takenInts = new int[ogCameras.Length];
        for (int i = 0; i < ogCameras.Length; i++)
        {

        }
    }

    public void ShowReplay(GameObject scorer)
    {
        if (PlayerController_FT.inReplay || showReplay) return; 
        this.scorer = scorer;
        GameManager_FT.instance.transition.SetActive(true);
        ball.transform.position = Vector3.up * 15;
        ball.GetComponent<MeshRenderer>().enabled = false;
        ball.active = false;
        showReplay = true;
    }
}

public class Frame
{
    public List<KeyCode> keys = new List<KeyCode>();
    public List<string> buttons = new List<string>();
    public List<string> buttonDowns = new List<string>();
    public List<string> buttonUps = new List<string>();
}
