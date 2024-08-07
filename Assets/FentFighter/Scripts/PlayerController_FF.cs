using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_FF : MonoBehaviour
{
    public GameObject otherPlayer;
    public int movementForce;
    public int movementSpeed;
    public int jumpForce;
    bool airborne;
    bool isColliding;
    // Start is called before the first frame update
    void Start()
    {
        isPlayer1 = gameObject.name == "Player1";
    }
    bool facingLeft;
    bool isPlayer1;
    // Update is called once per frame
    void Update()
    {
        if (otherPlayer.transform.position.x > transform.position.x && facingLeft)
        {
            //Cambiar a derecha
            transform.Rotate(180, 0, 0);
            facingLeft = false;
        }
        else if(otherPlayer.transform.position.x < transform.position.x && !facingLeft)
        {
            //Cambiar a izquierda
            transform.Rotate(180, 0, 0);
            facingLeft = true;
        }
        int lateralSpeed = 0;
        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (airborne)
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(movementForce, 0, 0);
                }
                else
                {
                    lateralSpeed += 1;
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (airborne)
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(-movementForce, 0, 0);
                }
                else
                {
                    lateralSpeed -= 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (!airborne)
                {
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(movementSpeed * lateralSpeed * Time.deltaTime, 0, 0);
                    gameObject.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                //agacharse
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (airborne)
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(movementForce, 0, 0);
                }
                else
                {
                    lateralSpeed += 1;
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (airborne)
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(-movementForce, 0, 0);
                }
                else
                {
                    lateralSpeed -= 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!airborne)
                {
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(movementSpeed * lateralSpeed * Time.deltaTime, 0, 0);
                    gameObject.GetComponent<Rigidbody>().AddForce(movementSpeed * lateralSpeed, jumpForce, 0, ForceMode.Impulse);
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //agacharse
            }
        }
        transform.Translate(lateralSpeed * movementSpeed * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            airborne = false;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            airborne = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GetComponent<Rigidbody>().isKinematic = false;
            isColliding = false;
        }
    }
}
