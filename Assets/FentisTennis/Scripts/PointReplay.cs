using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointReplay : MonoBehaviour
{
    public static PointReplay instance;
    public List<Frame> replay = new List<Frame>();
    [HideInInspector] public Vector3 iSPoint;
    [HideInInspector] public Vector3 iEPoint;
    [HideInInspector] public Vector3 iP1Pos;
    [HideInInspector] public Vector3 iP2Pos;
    [HideInInspector] public ShotType shot;
    public BallMover_FT ball;
    public KeyCode[] codes;
    public string[] buttons;
    GameObject scorer;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController_FT.inReplay)
        {
            if (PlayerController_FT.frameIndex >= PlayerController_FT.replay.Count)
            {
                PlayerController_FT.replay = null;
                PlayerController_FT.inReplay = false;
                GameManager_FT.instance.AddPoint(scorer);
                PlayerController_FT.frameIndex = 0;
                return;
            }
            PlayerController_FT.currentFrame = PlayerController_FT.replay[PlayerController_FT.frameIndex];
            PlayerController_FT.frameIndex++;
            return;
        }
        Frame currentFrame = new Frame();
        foreach (KeyCode item in codes) if (Input.GetKey(item)) currentFrame.keys.Add(item);
        foreach (string item in buttons) if (Input.GetButton(item)) currentFrame.buttons.Add(item);
        foreach (string item in buttons) if (Input.GetButtonDown(item)) currentFrame.buttonDowns.Add(item);
        foreach (string item in buttons) if (Input.GetButtonUp(item)) currentFrame.buttonUps.Add(item);
        replay.Add(currentFrame);
    }

    public void ShowReplay(GameObject scorer)
    {
        if (PlayerController_FT.inReplay) return; 
        this.scorer = scorer;
        PlayerController_FT.replay = replay;
        ball.sPoint.position = iSPoint;
        ball.ePoint.position = iEPoint;
        GameManager_FT.instance.player1.transform.position = iP1Pos;
        GameManager_FT.instance.player2.transform.position = iP2Pos;
        ball.step = 0;
        PlayerController_FT.inReplay = true;
        replay = new List<Frame>();
    }
}

public class Frame
{
    public List<KeyCode> keys = new List<KeyCode>();
    public List<string> buttons = new List<string>();
    public List<string> buttonDowns = new List<string>();
    public List<string> buttonUps = new List<string>();
}
