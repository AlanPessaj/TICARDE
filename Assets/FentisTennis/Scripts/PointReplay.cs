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

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (showReplay && !GameManager_FT.instance.transition.activeSelf)
        {
            cameras[0].gameObject.SetActive(false);
            cameras[1].gameObject.SetActive(true);
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
            if (PlayerController_FT.frameIndex >= PlayerController_FT.replay.Count)
            {
                PlayerController_FT.replay = null;
                PlayerController_FT.inReplay = false;
                GameManager_FT.instance.AddPoint(scorer);
                PlayerController_FT.frameIndex = 0;
                cameras[0].gameObject.SetActive(true);
                cameras[1].gameObject.SetActive(false);
                return;
            }
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
