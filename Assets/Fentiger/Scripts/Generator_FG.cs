using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator_FG : MonoBehaviour
{
    public GameObject[] sections;
    public GameObject grass;
    bool? lastSection;
    public bool side = false;
    public int distance = 0;
    public int difficulty = 1;
    public int despawnRadius;
    public int difficultyScalar;
    bool initialSpawn = true;
    public Level_FG[] Levels;
    public int Level = 0;
    public Transform camara;
    // Start is called before the first frame update
    void Start()
    {
        while (distance < despawnRadius)
        {
            GenerateZones();
        }
        initialSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        difficulty = (int)Mathf.Clamp(Mathf.Floor(camara.position.x / difficultyScalar), 1f, Mathf.Infinity);
        if (difficulty >= 10 && ((difficulty.ToString()[1] == '0' && difficulty.ToString().Length == 2) || difficulty == 100))
        {
            Level = Mathf.Clamp(difficulty / 10, 0, 9);
        }
    }
    public void GenerateZones()
    {
        NextZone();
        DespawnZones();
    }
    bool? Percenter(float[] percentages)
    {
        float percentage1Interval = percentages[0];
        float percentage2Interval = percentages[0] + percentages[1];
        float percentage3Interval = percentage2Interval + percentages[2];
        float number = Random.Range(1f, 100f);
        if (number <= percentage1Interval)
        {
            return null;
        }
        else if (number <= percentage2Interval)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    void NextZone()
    {
        /*
         * SECCIONES:
         * [0] = Cars               
         * [1] = Logs               \
         * [2] = LilyPads            ) -> Water
         * [3] = BrokenLogs         /
         * [4] = Lions              \
         * [5] = Froggs              ) -> Field
         * [6] = Kangaroo           /
         */
        List<GameObject> section = new List<GameObject>();
        bool? winner = Percenter(Levels[Level].tiles);
        bool isRepated = winner == lastSection;
        float tileValue = 0;
        if (!isRepated)
        {
            Instantiate(grass, new Vector3(distance, 0, 0), Quaternion.identity);
            distance++;
            lastSection = winner;
            switch (winner)
            {
                case null:
                    tileValue = Random.Range(Levels[Level].maxSize[0], Levels[Level].minSize[0]);
                    for (int i = 0; i < tileValue; i++)
                    {
                        switch (Percenter(Levels[Level].enemies))
                        {
                            case null:
                                section.Add(sections[4]);
                            break;
                            case false:
                                section.Add(sections[5]);
                            break;
                            case true:
                                section.Add(sections[6]);
                            break;
                        }
                    }
                break;
                case false:
                    tileValue = Random.Range(Levels[Level].maxSize[1], Levels[Level].minSize[1]);
                    for (int i = 0; i < tileValue; i++)
                    {
                        section.Add(sections[0]);
                    }
                break;
                case true:
                    tileValue = Random.Range(Levels[Level].maxSize[2], Levels[Level].minSize[2]);
                    for (int i = 0; i < tileValue; i++)
                    {
                        switch (Percenter(Levels[Level].floaters))
                        {
                            case null:
                                section.Add(sections[1]);
                            break;
                            case false:
                                section.Add(sections[2]);
                            break;
                            case true:
                                section.Add(sections[3]);
                            break;
                        }
                    }
                break;
            }
            GenerateSection(section);
        }
        else
        {
            NextZone();
        }
    }

    void GenerateSection(List<GameObject> section)
    {
        for (int i = 0; i < section.Count; i++)
        {
            if (!initialSpawn || distance < despawnRadius)
            {
                Instantiate(section[i], new Vector3(distance, 0, 0), Quaternion.identity);
                distance++;
            }
        }
    }

    void DespawnZones()
    {
        GameObject[] zones = GameObject.FindGameObjectsWithTag("Zone");
        foreach (GameObject zone in zones)
        {
            if (zone.transform.position.x < distance - despawnRadius)
            {
                Destroy(zone);
            }
        }
    }
}
