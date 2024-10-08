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
    public bool takenSpot = true;
    public float jumpHeight;
    public float jumpSpeed;
    public float jumpDelay;
    public float rotationSpeed;
    private float jumpProgress = 0f;
    private float delayTimer = 0f;
    private bool isJumping = false;
    private bool isRotating = false;
    public GameObject checker;
    public bool leftSpawn;
    public int attemptCount = 0;
    public int maxAttempts = 10;

    private void Start()
    {
        checker.transform.parent = null;
    }

    void FixedUpdate()
    {
        if (!isJumping && !isRotating)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= jumpDelay)
            {
                bool foundValidSpot = false;
                while (!foundValidSpot && attemptCount < maxAttempts)
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

                    checker.transform.position = targetPos;
                    foundValidSpot = FreeSpot();

                    attemptCount++;
                }
                if (!foundValidSpot)
                {
                    //DELETE ME
                    this.enabled = false;
                    return;
                }

                startPos = transform.position;
                isRotating = true;
                jumpProgress = 0f;
                delayTimer = 0f;
                attemptCount = 0;
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
        float finalY = startPos.y + arc;
        Vector3 finalPosition = new Vector3(horizontalPosition.x, finalY, horizontalPosition.z);
        transform.position = finalPosition;
        if (jumpProgress >= 1f)
        {
            isJumping = false;
            transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
        }
    }

    bool FreeSpot()
    {
        StartCoroutine(WaitForCollisionDetection());
        return !takenSpot;
    }

    IEnumerator WaitForCollisionDetection()
    {
        // Espera un frame
        yield return new WaitForEndOfFrame();
    }

    private void Awake()
    {
        players[0] = GameObject.Find("Player1");
        players[1] = GameObject.Find("Player2");
    }
}
