using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMover_FG : MonoBehaviour
{
    public float speed;
    private float rotation;
    public float time = 3;
    public float initialTime;
    public bool movingForward;
    public Generator_FG generator;
    public LinearSpawner_FG spawner;
    public bool hippoRotating;
    Vector3 hippoInitialPosition;
    Quaternion hippoFinalRotation;
    Vector3 hippoFinalPosition;
    Quaternion hippoInitialRotation;
    float hippoTime;
    public bool hippoResuming;
    public AudioClip breakSound;
    bool hippoCollision;
    bool firstSound = true;
    bool secondSound = true;
    bool playerOn;
    int logChildCount;

    void Start()
    {
        speed = generator.Levels[generator.Level].speed;

        rotation = Random.Range(-1f, 1f);
        if (gameObject.name == "Seagull(Clone)")
        {
            movingForward = !transform.GetComponent<SeagullController_FG>().leftSide;
        }
        else
        {
            movingForward = !spawner.changedSide;

        }

        if (gameObject.name == "LillyPad(Clone)")
        {
            time = 5 / speed;
        }
        else if (gameObject.name == "BrokenLog(Clone)")
        {
            time = Random.Range((5 / speed) - 0.2f, (5 / speed) + 0.5f);
        }
        hippoTime = 0;
        initialTime = time;
    }
    void Update()
    {
        if (hippoCollision)
        {
            speed = generator.Levels[generator.Level].speed * 2;
        }
        else
        {
            speed = generator.Levels[generator.Level].speed;
        }

        if (hippoRotating)
        {
            if (hippoTime < 1f)
            {
                hippoTime += Time.deltaTime * speed/2;
                transform.position = Vector3.Lerp(hippoInitialPosition, hippoFinalPosition, hippoTime);
                transform.rotation = Quaternion.Lerp(hippoInitialRotation, hippoFinalRotation, hippoTime);
                return;
            }
            else
            {
                hippoRotating = false;
                hippoResuming = true;
                hippoTime = 0;
            }
        }

        if (hippoResuming)
        {
            if (hippoTime < 1f)
            {
                hippoTime += Time.deltaTime * speed / 2;
                transform.position = Vector3.Lerp(hippoFinalPosition, hippoInitialPosition + Vector3.right, hippoTime);
                transform.rotation = Quaternion.Lerp(hippoFinalRotation, hippoInitialRotation * Quaternion.Euler(0, 180, 180), hippoTime);
                return;
            }
            else
            {
                hippoResuming = false;
                movingForward = !movingForward;
            }
        }

        
        if (movingForward)
        {
            if (transform.position.z < 18)
            {
                if (gameObject.name.Contains("Hippo") && transform.position.z >= 12)
                {
                    if (Physics.Raycast(new Vector3(transform.position.x + 1, 1, 0), Vector3.down, out RaycastHit hit, 10f, LayerMask.GetMask("Out")))
                    {
                        hippoRotating = true;
                        hippoInitialPosition = transform.position;
                        hippoInitialRotation = transform.rotation;
                        hippoFinalPosition = new Vector3(transform.position.x + 0.5f, transform.position.y, 14);
                        hippoFinalRotation = Quaternion.Euler(transform.eulerAngles.x, -90, transform.eulerAngles.z);

                        hippoTime = 0;
                        return;
                    }
                }

                if (gameObject.name != "Seagull(Clone)")
                {
                    transform.Translate(0, 0, speed * Time.deltaTime, Space.World);
                }
                else
                {
                    transform.Translate(0, 0, 5 * Time.deltaTime, Space.World);
                }
            }
            else
            {
                Destroy(gameObject);
            }

        }
        else
        {
            if (transform.position.z > -18)
            {
                if (gameObject.name.Contains("Hippo") && transform.position.z <= -12)
                {
                    if (Physics.Raycast(new Vector3(transform.position.x + 1, 1, 0), Vector3.down, out RaycastHit hit, 10f, LayerMask.GetMask("Out")))
                    {
                        hippoRotating = true;
                        hippoInitialPosition = transform.position;
                        hippoInitialRotation = transform.rotation;
                        hippoFinalPosition = new Vector3(transform.position.x + 0.5f, transform.position.y, -14);
                        hippoFinalRotation = Quaternion.Euler(transform.eulerAngles.x, -90, transform.eulerAngles.z);

                        hippoTime = 0;
                        return;
                    }
                }

                if (gameObject.name != "Seagull(Clone)")
                {
                    transform.Translate(0, 0, speed * -Time.deltaTime, Space.World);
                }
                else
                {
                    transform.Translate(0, 0, 5 * -Time.deltaTime, Space.World);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (gameObject.name == "Log(Clone)" || gameObject.name == "Hippo(Clone)")
        {
            if (logChildCount != transform.childCount - 1)
            {
                if (logChildCount < transform.childCount - 1)
                {
                    GetComponent<AudioSource>().Play();
                }
                logChildCount = transform.childCount - 1;
            }
        }
        else if (gameObject.name == "LillyPad(Clone)")
        {
            transform.Rotate(0, rotation, 0);
            if (transform.childCount > 1)
            {
                time -= Time.deltaTime;
                if (!playerOn)
                {
                    GetComponents<AudioSource>()[1].Play();
                    playerOn = true;
                }
                if (time <= (initialTime / 3) * 2 && firstSound)
                {
                    GetComponent<AudioSource>().Play();
                    firstSound = false;
                }
                else if(time <= initialTime / 3 && secondSound)
                {
                    GetComponent<AudioSource>().Play();
                    secondSound = false;
                }
            }
            else
            {
                playerOn = false;
            }
        }
        else if (gameObject.name == "BrokenLog(Clone)")
        {
            if (transform.childCount > 1)
            {
                time -= Time.deltaTime;
                if (time <= (initialTime / 3) * 2 && firstSound)
                {
                    GetComponents<AudioSource>()[1].Play();
                    firstSound = false;
                }
                else if (time <= initialTime / 3 && secondSound)
                {
                    GetComponents<AudioSource>()[1].Play();
                    secondSound = false;
                }
            }

            if (logChildCount < transform.childCount - 1)
            {
                GetComponent<AudioSource>().Play();
            }
            logChildCount = transform.childCount - 1;
        }

        if (time <= 0)
        {
            generator.GetComponent<SoundManager_FG>().PlaySound(breakSound);
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.name.Contains("Hippo") && (collision.gameObject.name.Contains("Log(Clone)") || collision.gameObject.name.Contains("LillyPad")))
        {
            GetComponents<AudioSource>()[1].PlayOneShot(GetComponents<AudioSource>()[1].clip);
            Destroy(collision.gameObject);
        }

        if (gameObject.name != "Hippo(Clone)" && collision.transform.position.x == transform.position.x && (collision.gameObject.name == "LillyPad(Clone)" || collision.gameObject.name == "Log(Clone)" || collision.gameObject.name == "Car(Clone)"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (gameObject.name.Contains("Hippo") && transform.childCount > 0)
        {
            transform.GetChild(0).GetComponent<PlayerController_FG>().enabled = true;
            transform.GetChild(0).parent = null;
        }
        else if(transform.childCount > 1)
        {
            transform.GetChild(1).GetComponent<PlayerController_FG>().enabled = true;
            transform.GetChild(1).parent = null;
            if (transform.childCount > 1)
            {
                transform.GetChild(1).GetComponent<PlayerController_FG>().enabled = true;
                transform.GetChild(1).parent = null;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (gameObject.name.Contains("Hippo") && collision.gameObject.name.Contains("Hippo"))
        {
            if (movingForward)
            {
                hippoCollision = transform.position.z > collision.transform.position.z;
            }
            else
            {
                hippoCollision = transform.position.z < collision.transform.position.z;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (gameObject.name.Contains("Hippo") && collision.gameObject.name.Contains("Hippo")) hippoCollision = false;
    }
}