using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController_FG : MonoBehaviour
{
    GameObject[] players = new GameObject[2];
    int distance;
    int direction;
    Vector3 startPos;
    Vector3 targetPos;
    public bool takenSpot;
    public float jumpHeight;
    public float jumpSpeed;
    public float jumpDelay;
    public float rotationSpeed;
    private float jumpProgress = 0f;
    private float delayTimer = 0f;
    private bool isJumping = false;
    private bool isRotating = false;
    public GameObject checker;

    private int attemptCount = 0; // Contador de intentos
    private const int maxAttempts = 10; // Máximo de intentos antes de destruir el script

    private void Start()
    {
        checker.transform.parent = null;
    }

    void Update()
    {
        if (!isJumping && !isRotating)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= jumpDelay)
            {
                attemptCount = 0; // Reiniciar el contador al empezar a buscar un nuevo salto
                do
                {
                    distance = Random.Range(2, 4);
                    direction = Random.Range(0, 4);
                    switch (direction)
                    {
                        case 0:
                            targetPos = transform.position + transform.forward * distance;
                            break;
                        case 1:
                            targetPos = transform.position + transform.right * distance;
                            break;
                        case 2:
                            targetPos = transform.position + transform.forward * -distance;
                            break;
                        case 3:
                            targetPos = transform.position + transform.right * -distance;
                            break;
                    }

                    attemptCount++; // Incrementar el contador de intentos

                    if (attemptCount >= maxAttempts)
                    {
                        // Destruir el script si no se encuentra un lugar después de 10 intentos
                        Destroy(this);
                        return; // Salir del método para evitar hacer más intentos
                    }
                }
                while (!FreeSpot(targetPos)); // Solo llama a FreeSpot una vez

                startPos = transform.position;
                isRotating = true;
                jumpProgress = 0f;
                delayTimer = 0f;
            }
        }
        else if (isRotating)
        {
            RotateTowardsChecker();
        }
        else if (isJumping)
        {
            JumpToPosition(targetPos);
        }
    }

    void RotateTowardsChecker()
    {
        Vector3 directionToChecker = (checker.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToChecker);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            isRotating = false;
            isJumping = true;
        }
    }

    void JumpToPosition(Vector3 targetPosition)
    {
        jumpProgress += Time.deltaTime * jumpSpeed;
        Vector3 horizontalPosition = Vector3.Lerp(startPos, targetPosition, jumpProgress);
        float arc = Mathf.Sin(Mathf.PI * jumpProgress) * jumpHeight;
        Vector3 finalPosition = new Vector3(horizontalPosition.x, horizontalPosition.y + arc, horizontalPosition.z);
        transform.position = finalPosition;
        if (jumpProgress >= 1f)
        {
            isJumping = false;
        }
    }

    bool FreeSpot(Vector3 pos)
    {
        checker.transform.position = pos;
        return !takenSpot; // Retorna true si no está tomado
    }

    private void Awake()
    {
        players[0] = GameObject.Find("Player1");
        players[1] = GameObject.Find("Player2");
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController_FG : MonoBehaviour
{
    GameObject[] players = new GameObject[2];
    int distance;
    int direction;
    Vector3 startPos;
    Vector3 targetPos;
    public bool takenSpot;
    public float jumpHeight;
    public float jumpSpeed;
    public float jumpDelay;
    public float rotationSpeed;
    private float jumpProgress = 0f;
    private float delayTimer = 0f;
    private bool isJumping = false;
    private bool isRotating = false;
    public GameObject checker;

    private void Start()
    {
        checker.transform.parent = null;
    }

    void Update()
    {
        if (!isJumping && !isRotating)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= jumpDelay)
            {
                do
                {
                    distance = Random.Range(2, 4);
                    direction = Random.Range(0, 4);
                    switch (direction)
                    {
                        case 0:
                            targetPos = transform.position + transform.forward * distance;
                            break;
                        case 1:
                            targetPos = transform.position + transform.right * distance;
                            break;
                        case 2:
                            targetPos = transform.position + transform.forward * -distance;
                            break;
                        case 3:
                            targetPos = transform.position + transform.right * -distance;
                            break;
                    }
                }
                while (!FreeSpot(targetPos));

                startPos = transform.position;
                isRotating = true;
                jumpProgress = 0f;
                delayTimer = 0f;
            }
        }
        else if (isRotating)
        {
            RotateTowardsChecker();
        }
        else if (isJumping)
        {
            JumpToPosition(targetPos);
        }
    }

    void RotateTowardsChecker()
    {
        Vector3 directionToChecker = (checker.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToChecker);
        float angle = Vector3.SignedAngle(transform.forward, directionToChecker, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            isRotating = false;
            isJumping = true;
        }
    }

    void JumpToPosition(Vector3 targetPosition)
    {
        jumpProgress += Time.deltaTime * jumpSpeed;
        Vector3 horizontalPosition = Vector3.Lerp(startPos, targetPosition, jumpProgress);
        float arc = Mathf.Sin(Mathf.PI * jumpProgress) * jumpHeight;
        Vector3 finalPosition = new Vector3(horizontalPosition.x, horizontalPosition.y + arc, horizontalPosition.z);
        transform.position = finalPosition;
        if (jumpProgress >= 1f)
        {
            isJumping = false;
        }
    }

    bool FreeSpot(Vector3 pos)
    {
        checker.transform.position = pos;
        return !takenSpot;
    }


    private void Awake()
    {
        players[0] = GameObject.Find("Player1");
        players[1] = GameObject.Find("Player2");
    }
}
*/