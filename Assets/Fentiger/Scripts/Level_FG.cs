using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Level_FG : ScriptableObject
{
    //{Field, Cars, Water}
    public float[] tiles = new float[3];
    //{Field, Cars, Water}
    public float[] minSize = new float[3];
    //{Field, Cars, Water}
    public float[] maxSize = new float[3];
    //{Lions, Sapos, Canguros}
    public float[] enemies = new float[3];
    //{Logs, LilyPads, BrokenLogs}
    public float[] floaters = new float[3];
    //{Seagull, Lasers, Portal}
    public float[] special = new float[3];
    //{Logs, LilyPads, Cars}
    public float[] spawnRate = new float[3];
}
