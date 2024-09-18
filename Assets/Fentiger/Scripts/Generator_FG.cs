using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator_FG : MonoBehaviour
{
    public GameObject[] sections;
    public GameObject grass;
    public int distance = 0;
    public int difficulty = 1;
    public int despawnRadius;
    bool rapidGeneration = false;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateZones();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            InvokeRepeating(nameof(GenerateZones), 0, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            CancelInvoke();
        }
        if (rapidGeneration)
        {
            GenerateZones();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            rapidGeneration = true;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            rapidGeneration = false;
        }
    }
    public void GenerateZones()
    {
        NextZone();
        DespawnZones();
    }
    GameObject lastSection;
    void NextZone()
    {
        Instantiate(grass, new Vector3(distance, 0, 0), Quaternion.identity);
        distance++;
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
