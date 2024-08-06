using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldBallMover : MonoBehaviour
{
    public Transform puntoI;
    public Transform puntoF;
    public float altura;
    public float stepSize = 0.1f;
    float step = 0f;
    public float COR;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            step = 0f;
        }
        if (transform.position.y < -5f)
        {
            step = 0f;
        }
        transform.position = new Vector3(Mathf.LerpUnclamped(puntoI.position.x, puntoF.position.x, step), puntoI.position.y, Mathf.LerpUnclamped(puntoI.position.z, puntoF.position.z, step));
        step += stepSize * Time.deltaTime;
        float x = Vector3.Distance(puntoI.position, new Vector3(transform.position.x, puntoI.position.y, transform.position.z));
        float r1 = Vector3.Distance(puntoI.position, puntoF.position);
        Vector2 intermedio = new Vector2(r1/2, altura);
        float a = intermedio.y/((intermedio.x-r1)*intermedio.x);
        float pelotaY = a*(x-r1)*x;
        transform.position = new Vector3(transform.position.x, pelotaY + puntoI.position.y, transform.position.z);
    }

    void OnCollisionEnter(Collision other)
    {
        float distance = Vector3.Distance(puntoI.position, puntoF.position) * COR;
        if (other.gameObject.layer == LayerMask.NameToLayer("piso"))
        {
            float angle = CalculateAngle();
            float finalX = 0;
            float finalZ = 0;
            switch (angle)
            {
                case 0:
                    finalZ = distance;
                break;
                case 90:
                    finalX = distance;
                break;
                case 180:
                    finalZ = -distance;
                break;
                case 270:
                    finalX = -distance;
                break;
                default:
                if (angle < 90)
                {
                    finalX = Mathf.Sin(angle)*distance;
                    finalZ = Mathf.Cos(angle)*distance;
                }
                else if (angle < 180)
                {
                    finalX = Mathf.Sin(angle)*distance;
                    finalZ = -Mathf.Cos(angle)*distance;
                }else if (angle < 270)
                {
                    finalX = -Mathf.Sin(angle)*distance;
                    finalZ = -Mathf.Cos(angle)*distance;
                }else
                {
                    finalX = -Mathf.Sin(angle)*distance;
                    finalZ = Mathf.Cos(angle)*distance;
                }
                break;
            }
            puntoI.position = new Vector3(puntoF.position.x, other.transform.position.y, puntoF.position.z);
            puntoF.position = new Vector3(puntoI.position.x + finalX, other.transform.position.y, puntoI.position.z + finalZ);
            
        }
        step = 0f;
    }
    
    float CalculateAngle()
    {
        float distance = Vector3.Distance(puntoI.position, puntoF.position);
        float angle = 2000;
        if (puntoF.position.z > puntoI.position.z)
        {
            if (puntoF.position.x > puntoI.position.x)
            {
                angle = Mathf.Acos((puntoF.position.z - puntoI.position.z)/distance) * Mathf.Rad2Deg;
            }
            else if (puntoF.position.x < puntoI.position.x)
            {
                angle = Mathf.Asin((puntoF.position.x - puntoI.position.x)/distance) * Mathf.Rad2Deg;
                angle += 360;
            }
            else
            {
                angle = 0;
            }
        }
        else if (puntoF.position.z < puntoI.position.z)
        {
            if (puntoF.position.x > puntoI.position.x)
            {
                angle = Mathf.Asin((puntoI.position.x - puntoF.position.x)/distance) * Mathf.Rad2Deg;
                angle += 180;
            }
            else if (puntoF.position.x < puntoI.position.x)
            {
                angle = Mathf.Acos((puntoI.position.z - puntoF.position.z)/distance) * Mathf.Rad2Deg;
                angle += 180;
            }
            else
            {
                angle = 180;
            }
        }
        else
        {
            if (puntoF.position.x > puntoI.position.x)
            {
                angle = 90;
            }
            else if (puntoF.position.x < puntoI.position.x)
            {
                angle = 270;
            }
            else
            {
                angle = 30000;
            }
        }
        return angle;
    }
}
