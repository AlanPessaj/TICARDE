using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Level_FG : ScriptableObject
{
    //{Field, Cars, Water}
    public float[] tiles = new float[3];
    //{Lions, Sapos, Canguros}
    public float[] enemys = new float[3];
    //{Logs, LilyPads, BrokenLogs}
    public float[] foaters = new float[3];
    //{Seagull, Lasers, Portal}
    public float[] special = new float[3];
}
