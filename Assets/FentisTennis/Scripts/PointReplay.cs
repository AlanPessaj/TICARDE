using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointReplay : MonoBehaviour
{
    public static PointReplay instance;
    public List<Frame> replay = new List<Frame>();
    public KeyCode[] codes;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Frame currentFrame = new Frame();
        foreach (KeyCode item in codes) if (Input.GetKey(item)) currentFrame.keys.Add(item.ToString());
        replay.Add(currentFrame);
    }
}

public class Frame
{
    public List<string> keys;
}
