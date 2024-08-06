using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballmover : MonoBehaviour
{
    public Transform puntoI;
    public Transform puntoF;
    public float altura;
    public float stepSize = 0.1f;
    float step = 0f;
    public float vCOR;
    public float hCOR;
    public float minAltura;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool ignoredCollision = false;
        if (altura < minAltura)
        {
            altura = 0f;
            stepSize -= hCOR * Time.deltaTime;
            stepSize = Mathf.Clamp(stepSize, 0f, Mathf.Infinity);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            puntoI.position = new Vector3(0, 1, 0);
            puntoF.position = new Vector3(5, 1, 5);
            stepSize = 3f;
            altura = 2f;
            step = 0f;
        }
        if (transform.position.y < 0)
        {
            ignoredCollision = true;
        }
        transform.position = new Vector3(Mathf.LerpUnclamped(puntoI.position.x, puntoF.position.x, step), puntoI.position.y, Mathf.LerpUnclamped(puntoI.position.z, puntoF.position.z, step));
        step += stepSize * Time.deltaTime;
        float x = Vector3.Distance(puntoI.position, new Vector3(transform.position.x, puntoI.position.y, transform.position.z));
        float r1 = Vector3.Distance(puntoI.position, puntoF.position);
        if (r1 != 0)
        {
            Vector2 intermedio = new Vector2(r1/2, altura - puntoI.position.y);
            float a = intermedio.y/((intermedio.x-r1)*intermedio.x);
            float pelotaY = a*(x-r1)*x;
            transform.position = new Vector3(transform.position.x, pelotaY + puntoI.position.y, transform.position.z);
        }
        else
        {
            puntoI.position = new Vector3(0, 1, 0);
            puntoF.position = new Vector3(5, 1, 5);
            stepSize = 3f;
            altura = 2f;
            step = 0f;
        }
        if (ignoredCollision)
        {
            transform.position = new Vector3(transform.position.x, 20, transform.position.z);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("piso"))
        {
            altura *= vCOR;
            float distance = Mathf.Clamp(Vector3.Distance(puntoI.position, puntoF.position) - hCOR*(1+vCOR)*altura, 0f, Mathf.Infinity);
            Vector3 direction = (puntoF.position - puntoI.position).normalized;
            puntoI.position = new Vector3(transform.position.x, other.transform.position.y + 0.5f, transform.position.z);
            Vector3 displacement = direction * distance;
            puntoF.position = new Vector3(displacement.x + puntoI.position.x, other.transform.position.y + 0.5f, displacement.z + puntoI.position.z);
        }
        step = 0f;
    }
}
