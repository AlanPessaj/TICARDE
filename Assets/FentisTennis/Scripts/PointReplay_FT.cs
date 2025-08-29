using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PointReplay_FT : MonoBehaviour
{
    public const int REPLAY_LENGTH = 250;
    public static PointReplay_FT instance;
    public CircularBuffer<Frame> frameBuffer = new CircularBuffer<Frame>(REPLAY_LENGTH);
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
    bool firstTime = true;
    bool skippingReplay;
    public static Frame[] replay;
    public static int replayFrameIndex;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        ogCameras = new Camera[cameras.Length - 1];
        System.Array.Copy(cameras, 1, ogCameras, 0, ogCameras.Length);
        RandomizeCameras();
    }

    public void Update()
    {
        if (PlayerController_FT.inReplay && (Input.GetButtonDown("A") || Input.GetButtonDown("A2")))
        {
            skippingReplay = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (exitingReplay) { PlayerController_FT.inReplay = false; exitingReplay = false; }
        if (showReplay && !GameManager_FT.instance.transition.activeSelf)
        {
            cameraIndex = Random.Range(1, cameras.Length);
            cameras[0].gameObject.SetActive(false);
            cameras[cameraIndex].gameObject.SetActive(true);
            ball.GetComponent<MeshRenderer>().enabled = true;
            showReplay = false;
            replay = frameBuffer.ToArray();
            ball.transform.position = frameBuffer.GetFirst().ballPos;
            GameManager_FT.instance.player1.transform.position = frameBuffer.GetFirst().p1Pos;
            GameManager_FT.instance.player2.transform.position = frameBuffer.GetFirst().p2Pos;
            PlayerController_FT.inReplay = true;
            frameBuffer = new CircularBuffer<Frame>(REPLAY_LENGTH);
            ball.GetComponent<TrailRenderer>().emitting = true;
        }
        if (PlayerController_FT.inReplay)
        {
            if (skippingReplay)
            {
                firstTime = true;
                replay = null;
                replayFrameIndex = 0;
                cameras[0].gameObject.SetActive(true);
                cameras[cameraIndex].gameObject.SetActive(false);
                exitingReplay = true;
                RandomizeCameras();
                GameManager_FT.instance.AddPoint(scorer);
                skippingReplay = false;
                return;
            }
            if (replayFrameIndex >= replay.Length)
            {
                ball.GetComponent<TrailRenderer>().emitting = false;
                cameras[cameraIndex].gameObject.SetActive(false);
                if (cameraIndex < cameras.Length - 1) cameraIndex++; else cameraIndex = 1;
                cameras[cameraIndex].gameObject.SetActive(true);
                replayFrameIndex = 0;
                ball.GetComponent<TrailRenderer>().emitting = true;
            }
            cameras[cameraIndex].transform.LookAt(ball.transform);
            if (cameras[cameraIndex].name.Contains("FollowX")) cameras[cameraIndex].transform.position = new Vector3(ball.transform.position.x, cameras[cameraIndex].transform.position.y, cameras[cameraIndex].transform.position.z);
            if (cameras[cameraIndex].name.Contains("FollowZ")) cameras[cameraIndex].transform.position = new Vector3(cameras[cameraIndex].transform.position.x, cameras[cameraIndex].transform.position.y, ball.transform.position.z);
            if (iDirection == 0 && cameras[cameraIndex].name.Contains("FollowZ")) if (ball.transform.position.z >= 0) cameras[cameraIndex].transform.position += Vector3.forward; else cameras[cameraIndex].transform.position -= Vector3.forward;
            GameManager_FT.instance.player1.transform.position = replay[replayFrameIndex].p1Pos;
            GameManager_FT.instance.player2.transform.position = replay[replayFrameIndex].p2Pos;
            // Usar racket y racketPivot del PlayerController_FT
            GameManager_FT.instance.player1.GetComponent<PlayerController_FT>().racket.transform.rotation = replay[replayFrameIndex].p1RaqRot;
            GameManager_FT.instance.player2.GetComponent<PlayerController_FT>().racket.transform.rotation = replay[replayFrameIndex].p2RaqRot;
            GameManager_FT.instance.player1.GetComponent<PlayerController_FT>().racket.transform.localPosition = replay[replayFrameIndex].p1RaqLocPos;
            GameManager_FT.instance.player2.GetComponent<PlayerController_FT>().racket.transform.localPosition = replay[replayFrameIndex].p2RaqLocPos;
            GameManager_FT.instance.player1.GetComponent<PlayerController_FT>().racketPivot.transform.rotation = replay[replayFrameIndex].p1RaqPivRot;
            GameManager_FT.instance.player2.GetComponent<PlayerController_FT>().racketPivot.transform.rotation = replay[replayFrameIndex].p2RaqPivRot;
            ball.transform.position = replay[replayFrameIndex].ballPos;
            if (replay[replayFrameIndex].p1PlayedHit) GameManager_FT.instance.player1.GetComponent<AudioSource>().Play();
            if (replay[replayFrameIndex].p2PlayedHit) GameManager_FT.instance.player2.GetComponent<AudioSource>().Play();
            if (replay[replayFrameIndex].p1PlayedWoosh) GameManager_FT.instance.player1.GetComponent<AudioSource>().PlayOneShot(GameManager_FT.instance.player1.GetComponent<PlayerController_FT>().wooshSFX);
            if (replay[replayFrameIndex].p2PlayedWoosh) GameManager_FT.instance.player2.GetComponent<AudioSource>().PlayOneShot(GameManager_FT.instance.player1.GetComponent<PlayerController_FT>().wooshSFX);
            replayFrameIndex++;
            return;
        }
        if (showReplay) return;
        Frame currentFrame = new Frame();
        currentFrame.p1PlayedHit = GameManager_FT.instance.player1.GetComponent<PlayerController_FT>().playedHit;
        currentFrame.p2PlayedHit = GameManager_FT.instance.player2.GetComponent<PlayerController_FT>().playedHit;
        currentFrame.p1PlayedWoosh = GameManager_FT.instance.player1.GetComponent<PlayerController_FT>().playedWoosh;
        currentFrame.p2PlayedWoosh = GameManager_FT.instance.player2.GetComponent<PlayerController_FT>().playedWoosh;
        currentFrame.p1Pos = GameManager_FT.instance.player1.transform.position;
        currentFrame.p2Pos = GameManager_FT.instance.player2.transform.position;
        currentFrame.p1RaqRot = GameManager_FT.instance.player1.GetComponent<PlayerController_FT>().racket.transform.rotation;
        currentFrame.p2RaqRot = GameManager_FT.instance.player2.GetComponent<PlayerController_FT>().racket.transform.rotation;
        currentFrame.p1RaqLocPos = GameManager_FT.instance.player1.GetComponent<PlayerController_FT>().racket.transform.localPosition;
        currentFrame.p2RaqLocPos = GameManager_FT.instance.player2.GetComponent<PlayerController_FT>().racket.transform.localPosition;
        currentFrame.p1RaqPivRot = GameManager_FT.instance.player1.GetComponent<PlayerController_FT>().racketPivot.transform.rotation;
        currentFrame.p2RaqPivRot = GameManager_FT.instance.player2.GetComponent<PlayerController_FT>().racketPivot.transform.rotation;
        currentFrame.ballPos = ball.transform.position;
        frameBuffer.Add(currentFrame);
        GameManager_FT.instance.player1.GetComponent<PlayerController_FT>().playedHit = false;
        GameManager_FT.instance.player2.GetComponent<PlayerController_FT>().playedHit = false;
        GameManager_FT.instance.player1.GetComponent<PlayerController_FT>().playedWoosh = false;
        GameManager_FT.instance.player2.GetComponent<PlayerController_FT>().playedWoosh = false;
    }

    bool emergency;

    void RandomizeCameras()
    {
        Camera[] temp = new Camera[cameras.Length];
        temp[0] = cameras[0];
        for (int i = 0; i < ogCameras.Length; i++)
        {
            int times = 0;
            Camera camera = ogCameras[i];
            retry:
            times++;
            if (times > 100)
            {
                temp = cameras;
                break;
            }
            int index = Random.Range(1, temp.Length);
            if (emergency) break;
            if (temp[index] != null) goto retry;
            if (temp[index - 1] != null && temp[index - 1].name.Contains(camera.name.Split(' ')[0])) goto retry;
            if (index < temp.Length - 1 && temp[index + 1] != null && temp[index + 1].name.Contains(camera.name.Split(' ')[0])) goto retry;
            temp[index] = camera;
        }
        cameras = temp;
    }

    public void ShowReplay(GameObject scorer)
    {
        ball.GetComponent<TrailRenderer>().emitting = false;
        if (firstTime)
        {
            GetComponents<AudioSource>()[1].Play();
            GAMEMANAGER.Instance.GetComponent<LedsController>().FullRound("BLUE");
            firstTime = false;
        }
        if (PlayerController_FT.inReplay || showReplay) return; 
        this.scorer = scorer;
        GameManager_FT.instance.transition.SetActive(true);
        ball.transform.position = Vector3.up * 15;
        ball.GetComponent<MeshRenderer>().enabled = false;
        ball.active = false;
        showReplay = true;
    }

    void OnDestroy()
    {
        PlayerController_FT.inReplay = false;
    }
}

public class Frame
{
    public Vector3 p1Pos;
    public Vector3 p2Pos;
    public bool p1PlayedHit;
    public bool p2PlayedHit;
    public bool p1PlayedWoosh;
    public bool p2PlayedWoosh;
    public Quaternion p1RaqRot;
    public Quaternion p2RaqRot;
    public Vector3 p1RaqLocPos;
    public Vector3 p2RaqLocPos;
    public Quaternion p1RaqPivRot;
    public Quaternion p2RaqPivRot;
    public Vector3 ballPos;
}

public class CircularBuffer<T>
{
    private readonly T[] buffer;
    private int currentIndex;
    private bool filled;

    public int Length => buffer.Length;

    public CircularBuffer(int size)
    {
        buffer = new T[size];
        currentIndex = 0;
        filled = false;
    }

    public void Add(T item)
    {
        buffer[currentIndex] = item;
        currentIndex = (currentIndex + 1) % buffer.Length;

        if (currentIndex == 0)
            filled = true;
    }

    public T this[int index]
    {
        get => buffer[index];
        set => buffer[index] = value;
    }

    public T GetFirst()
    {
        if (!filled) return buffer[0];
        return buffer[currentIndex];
    }

    public T GetLast()
    {
        int lastIndex = (currentIndex - 1 + buffer.Length) % buffer.Length;
        return buffer[lastIndex];
    }

    public T[] ToArray()
    {
        if (!filled)
        {
            // Si todavía no se llenó, simplemente devolvemos los elementos cargados en orden natural
            T[] result = new T[currentIndex];
            System.Array.Copy(buffer, result, currentIndex);
            return result;
        }

        // Si ya está lleno, hay que rotar para que 0 sea el más viejo
        T[] ordered = new T[buffer.Length];
        int j = 0;

        for (int i = currentIndex; i < buffer.Length; i++)
            ordered[j++] = buffer[i];

        for (int i = 0; i < currentIndex; i++)
            ordered[j++] = buffer[i];

        return ordered;
    }

    public void Clear()
    {
        currentIndex = 0;
        filled = false;
        System.Array.Clear(buffer, 0, buffer.Length);
    }
}


