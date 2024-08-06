using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_FF : MonoBehaviour
{
    public GameObject otherPlayer;
    public int movementForce;
    public int jumpForce;
    bool airborne;
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
        if(otherPlayer.transform.position.x > transform.position.x && facingLeft)
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

        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.A))
            {
                gameObject.GetComponent<Rigidbody>().AddForce(-movementForce, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                gameObject.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
            }
            if (Input.GetKey(KeyCode.D))
            {
                gameObject.GetComponent<Rigidbody>().AddForce(movementForce, 0, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                //agacharse
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                gameObject.GetComponent<Rigidbody>().AddForce(-movementForce, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                gameObject.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                gameObject.GetComponent<Rigidbody>().AddForce(movementForce, 0, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //agacharse
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            airborne = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        airborne = other.gameObject.layer == LayerMask.NameToLayer("Floor");
    }
}
