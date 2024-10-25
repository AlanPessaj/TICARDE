using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController_FG : MonoBehaviour
{
    public float jumpHeight;
    public float jumpSpeed;
    public float jumpDelay;
    public float rotationSpeed;
    public GameObject checker;
    public bool leftSpawn;
    public int maxAttempts;
    int attemptCount = 0;
    float jumpProgress = 0f;
    float delayTimer = 0f;
    public bool isJumping = false;
    bool isRotating = false;
    int distance;
    int direction;
    Vector3 startPos;
    Vector3 targetPos;
    GameObject[] players = new GameObject[2];
    public GameObject outCollider;
    public GameObject ghost;

    private void Start()
    {
        checker.transform.parent = null;
    }

    private void Update()
    {
        if (isJumping && jumpProgress >= 0.5f)
        {
            outCollider.layer = LayerMask.NameToLayer("Out");
        }
        else
        {
            outCollider.layer = LayerMask.NameToLayer("Default");
        }
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
                    //Debug.DrawRay(targetPos + new Vector3(0, 2, 0), Vector3.down * 5, Color.red, 1);
                    if (targetPos.z < -12.1f || targetPos.z > 12.1f || !Physics.Raycast(targetPos + new Vector3(0, 2, 0), Vector3.down, out RaycastHit hit, 5, Physics.AllLayers, QueryTriggerInteraction.Collide))
                    {
                        attemptCount++;
                        continue;
                    }
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Field"))
                    {
                        //Debug.DrawRay(transform.position, (targetPos - transform.position).normalized, Color.green, Vector3.Distance(targetPos, transform.position));
                        foundValidSpot = !Physics.Raycast(transform.position, (targetPos - transform.position).normalized, out hit, Vector3.Distance(targetPos, transform.position), Physics.AllLayers, QueryTriggerInteraction.Collide);
                    }
                    checker.transform.position = targetPos;
                    attemptCount++;
                }
                attemptCount = 0;
                if (!foundValidSpot)
                {
                    //DELETE ME
                    Debug.LogWarning($"Posicion invalida en {transform.localPosition}");
                    return;
                }

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
        float finalY = startPos.y + arc;
        Vector3 finalPosition = new Vector3(horizontalPosition.x, finalY, horizontalPosition.z);
        transform.position = finalPosition;
        if (jumpProgress >= 1f)
        {
            isJumping = false;
            transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
        }
    }

    private void Awake()
    {
        players[0] = GameObject.Find("Player1");
        players[1] = GameObject.Find("Player2");
    }

    private void OnDestroy()
    {
        Destroy(checker.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Seagull") && Application.IsPlaying(this))
        {
            Instantiate(ghost, transform.position, Quaternion.identity).GetComponent<DieScript_FG>().playerGhost = false;
            Destroy(gameObject);
        }
    }
}
