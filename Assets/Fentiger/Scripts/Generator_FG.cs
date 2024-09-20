using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator_FG : MonoBehaviour
{
    public GameObject[] sections;
    public GameObject grass;
    GameObject lastSection;
    public bool side = false;
    public int distance = 0;
    public int difficulty = 1;
    public int despawnRadius;
    public int difficultyScalar = 100;
    bool initialSpawn = true;
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
        difficulty = (int)Mathf.Clamp(Mathf.Floor(distance / difficultyScalar), 1f, Mathf.Infinity);
    }
    public void GenerateZones()
    {
        NextZone();
        DespawnZones();
    }
    void NextZone()
    {
        Instantiate(grass, new Vector3(distance, 0, 0), Quaternion.identity);
        distance++;
        /* SECCIONES:
         * [0] = BrokenLogs
         * [1] = Cars
         * [2] = Field
         * [3] = LilyPads
         * [4] = Logs
         */
        GameObject section = sections[Random.Range(0, sections.Length)];
        bool isRepated = false;
        do
        {
            isRepated = section == lastSection;
            if (!isRepated)
            {
                GenerateSection(section);
                lastSection = section;
            }
            else
            {
                section = sections[Random.Range(0, sections.Length)];
            }
        } while (isRepated);
    }

    void GenerateSection(GameObject section)
    {
        int amount = Random.Range(difficulty, difficulty + 4);
        for (int i = 0; i < amount; i++)
        {
            if (!initialSpawn || distance < despawnRadius)
            {
                Instantiate(section, new Vector3(distance, 0, 0), Quaternion.identity);
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
