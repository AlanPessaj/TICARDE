using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator_FG : MonoBehaviour
{
   /*
    * Characters:
    * [0] = Rabino
    * [1] = Martin Fierro
    * [2] = Messi
    * [3] = Peron
    */
    public Material[] characters;
    public bool multiplayer;
    public bool initialMultiplayer = false;
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
    public Transform ovniSpawn;
    public GameObject[] players = new GameObject[2];
    public GameObject[] specials;
    public GameObject loadScreen;
    public int treeSeparator;
    public bool treeSpawn;
    public BakeNavMesh_FG baker;
    public int startingLevel;
    public List<GameObject> section = new List<GameObject>();
    public bool isTherePlayer1 = false;
    public bool isTherePlayer2 = false;
    public bool player1Alive;
    public float player1Score;
    public float player2Score;
    public string player1Name = "Player1";
    public string player2Name = "Player2";

    void Start()
    {
        multiplayer = GameData.name2 != "";
        player1Name = GameData.name1;
        player2Name = GameData.name2;
        players[0].GetComponent<Renderer>().material = characters[GameData.char1];
        players[0].GetComponent<PlayerController_FG>().enabled = false;
        if (multiplayer)
        {
            players[1].GetComponent<Renderer>().material = characters[GameData.char2];
            players[1].GetComponent<PlayerController_FG>().enabled = false;
            initialMultiplayer = true;
        }
        else
        {
            Destroy(players[1]);
        }
        Level = startingLevel;
        while (distance < despawnRadius)
        {
            GenerateZones();
        }
        initialSpawn = false;
        StartCoroutine(TimeSprint(30, 50));
    }
    
    private IEnumerator TimeSprint(float seconds, float rate)
    {
        Time.timeScale = rate;
        yield return new WaitForSeconds(seconds);
        Time.timeScale = 1;
        loadScreen.SetActive(false);
        players[0].GetComponent<PlayerController_FG>().enabled = true;
        if (initialMultiplayer) players[1].GetComponent<PlayerController_FG>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        isTherePlayer1 = GameObject.Find("Player1") != null;
        isTherePlayer2 = GameObject.Find("Player2") != null;
        if (isTherePlayer1 && isTherePlayer2)
        {
            multiplayer = true;
        }
        else if (isTherePlayer1)
        {
            multiplayer = false;
            player1Alive = true;

        }
        else
        {
            multiplayer = false;
            player1Alive = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Portal
            //Instantiate(specials[2], new Vector3((int)camara.position.x + 25.5f, -1.5f, Random.Range(-12, 13)), Quaternion.Euler(0, 0, 90));

            
            // Ovni
            /*if (camara.GetChild(0).childCount < 1)
            {
                Instantiate(specials[1], ovniSpawn.position + Vector3.right * 3, Quaternion.identity, ovniSpawn);
            }*/


            //Gaviota
            /*if (multiplayer)
            {
                Instantiate(specials[0], new Vector3(Mathf.Max(players[0].transform.position.x, players[1].transform.position.x), 3, 18), Quaternion.identity);
            }
            else if (player1Alive)
            {
                Instantiate(specials[0], new Vector3(players[0].transform.position.x, 3, 18), Quaternion.identity);
            }
            else
            {
                Instantiate(specials[0], new Vector3(players[1].transform.position.x, 3, 18), Quaternion.identity);
            }*/
        }

        float difficultyPosition = 0; //reemplazo de camara.position.x
        if (difficulty >= 10 && difficulty % 10 == 0)
        {
            Level = Mathf.Clamp(difficulty / 10, 0 + startingLevel, 9);
        }
        if (isTherePlayer1 || isTherePlayer2)
        {
            if (multiplayer)
            {
                difficultyPosition = (players[0].transform.position.x + players[1].transform.position.x) / 2;
            }
            else
            {
                if (player1Alive)
                {
                    difficultyPosition = players[0].transform.position.x;
                }
                else
                {
                    difficultyPosition = players[1].transform.position.x;
                }
            }
        }
        difficulty = (int)Mathf.Clamp(Mathf.Floor(difficultyPosition / difficultyScalar), 1f, Mathf.Infinity);
    }
    public void GenerateZones()
    {
        NextZone();
        SpecialSpawner();
        DespawnZones();
    }
    void SpecialSpawner()
    {
        /*
         * Especiales:
         * [0] = Gaviota
         * [1] = Laser
         * [2] = Portales
         * [3] = Corazon
         * [4] = Estrella
         */
        float number = Random.Range(0, 101);
        float number2 = Random.Range(0, 101);
        float number3 = Random.Range(0, 101);
        if (number <= Levels[Level].special[0]) Instantiate(specials[0], new Vector3(distance, 3, 18), Quaternion.identity);
        if (number2 <= Levels[Level].special[1] && camara.GetChild(0).childCount < 1) Instantiate(specials[1], ovniSpawn.position + Vector3.right * 3, Quaternion.identity, ovniSpawn);
        if (number3 <= Levels[Level].special[2]) Instantiate(specials[2], new Vector3((int)camara.position.x + 25.5f, -1.5f, Random.Range(-12, 13)), Quaternion.Euler(0, 0, 90));
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
         * [5] = Froggs              \ -> Field
         * [6] = Kangaroos           /
         * [7] = EmptyFields        /
         */
        bool? winner = Percenter(Levels[Level].tiles);
        bool isRepated = winner == lastSection;
        float tileValue;
        if (!isRepated)
        {
            section.Add(grass);
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
                        section.Add(sections[7]);
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
            GenerateSection();
        }
        else
        {
            NextZone();
        }
    }
    List<GameObject> toBake = new List<GameObject>();

    void GenerateSection()
    {
        int pickable = Random.Range(1, 101);
        if (pickable == 1)
        {
            //Vida
            Instantiate(specials[3], new Vector3(distance, -1.5f, Random.Range(-11,12)), Quaternion.identity);
        }
        else if(pickable > 95)
        {
            //XP
            Instantiate(specials[4], new Vector3(distance, -1.5f, Random.Range(-11, 12)), Quaternion.identity);
        }
        bool isField = false;
        while ((!initialSpawn || distance < despawnRadius) && section.Count != 0)
        {
            isField = section[0] == sections[4] || section[0] == sections[5] || section[0] == sections[6] || section[0] == sections[7];
            if (isField)
            {
                toBake.Add(Instantiate(section[0], new Vector3(distance, 0, 0), Quaternion.identity).transform.GetChild(0).gameObject);
            }
            else
            {
                Instantiate(section[0], new Vector3(distance, 0, 0), Quaternion.identity);
            }
            bool doBreak = section[0] == grass;
            section.RemoveAt(0);
            distance++;
            if (doBreak) break;
        }
        if (isField)
        {
            baker.Bake(toBake.ToArray());
        }
        if ((!initialSpawn || distance < despawnRadius) && section.Count != 0)
        {
            GenerateSection();
        }
    }

    void DespawnZones()
    {
        GameObject[] zones = GameObject.FindGameObjectsWithTag("Zone");
        foreach (GameObject zone in zones)
        {
            if (zone.transform.position.x < distance - despawnRadius)
            {
                if (zone.name == "Lions(Clone)" || zone.name == "Kangaroos(Clone)" || zone.name == "Frogs(Clone)" || zone.name == "EmptyField(Clone)")
                {
                    toBake.Remove(zone.transform.GetChild(0).gameObject);
                }
                Destroy(zone);
            }
        }
    }
    private void Awake()
    {
        players[0] = GameObject.Find("Player1");
        players[1] = GameObject.Find("Player2");
    }
}
