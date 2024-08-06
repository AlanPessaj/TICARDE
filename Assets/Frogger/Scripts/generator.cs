using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generator : MonoBehaviour
{
    public GameObject[] secciones;
    public GameObject pastito;
    public GameObject camara;
    public int distancia = 0;
    public int dificultad = 1;
    public int rangoDespawneo;
    bool rapidGeneration = false;
    public int difficultyScalar = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dificultad = (int)Mathf.Floor(distancia / difficultyScalar);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerarZonas();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            InvokeRepeating(nameof(GenerarZonas), 0, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            CancelInvoke();
        }
        if (rapidGeneration)
        {
            GenerarZonas();
        }if (Input.GetKeyDown(KeyCode.K))
        {
            rapidGeneration = true;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            rapidGeneration = false;
        }
    }
    void GenerarZonas()
    {
        SiguienteZona();
        DespawnearZonas();
    }
    GameObject lastSeccion;
    void SiguienteZona()
    {
        Instantiate(pastito, new Vector3(distancia, 0, 0), Quaternion.identity);
        distancia++;
        //camara.transform.position = new Vector3(camara.transform.position.x + 1, camara.transform.position.y, camara.transform.position.z);
        GameObject seccion = secciones[Random.Range(0, secciones.Length)];
        bool isRepated = false;
        do
        {
            isRepated = seccion == lastSeccion;
            if (!isRepated)
            {
                GenerarSeccion(seccion);
                lastSeccion = seccion;
            }
            else
            {
                seccion = secciones[Random.Range(0, secciones.Length)];
            }
        } while (isRepated);
    }

    void GenerarSeccion(GameObject seccion)
    {
        int cantidad = Random.Range(dificultad, dificultad + 4);
        for (int i = 0; i < cantidad; i++)
        {
            Instantiate(seccion, new Vector3(distancia, 0, 0), Quaternion.identity);
            distancia++;
            //camara.transform.position = new Vector3(camara.transform.position.x + 1, camara.transform.position.y, camara.transform.position.z);
        }
    }

    void DespawnearZonas()
    {
        GameObject[] zonas = GameObject.FindGameObjectsWithTag("zona");
        foreach (GameObject zona in zonas)
        {
            if (zona.transform.position.x < distancia - rangoDespawneo)
            {
                Destroy(zona);
            }
        }
    }
}
