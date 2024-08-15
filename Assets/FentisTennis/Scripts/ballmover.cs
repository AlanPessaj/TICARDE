using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour
{
    public Transform sPoint;
    public Transform ePoint;
    public float height;
    public float stepSize = 0.1f;
    float step = 0f;
    public float vCOR;
    public float hCOR;
    public float minheight;
    Vector3 sPointi;
    Vector3 ePointi;
    float heighti;
    float stepi;
    // Start is called before the first frame update
    void Start()
    {
        sPointi = sPoint.position;
        ePointi = ePoint.position;
        heighti = height;
        stepi = stepSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (height < minheight)
        {
            height = 0f;
            stepSize -= hCOR * Time.deltaTime;
            stepSize = Mathf.Clamp(stepSize, 0f, Mathf.Infinity);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sPoint.position = sPointi;
            ePoint.position = ePointi;
            stepSize = stepi;
            height = heighti;
            step = 0f;
        }
        transform.position = new Vector3(Mathf.LerpUnclamped(sPoint.position.x, ePoint.position.x, step), transform.position.y, Mathf.LerpUnclamped(sPoint.position.z, ePoint.position.z, step));
        step += stepSize * Time.deltaTime;
        float x = Vector3.Distance(sPoint.position, new Vector3(transform.position.x, sPoint.position.y, transform.position.z));
        float r1 = Vector3.Distance(sPoint.position, ePoint.position);
        if (r1 != 0)
        {
            Vector2 intermedio = new Vector2(r1/2, height - sPoint.position.y);
            float a = intermedio.y/((intermedio.x-r1)*intermedio.x);
            float ballY = a*(x-r1)*x;
            transform.position = new Vector3(transform.position.x, ballY + sPoint.position.y, transform.position.z);
        }
        else
        {
            sPoint.position = sPointi;
            ePoint.position = ePointi;
            stepSize = stepi;
            height = heighti;
            step = 0f;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            height *= vCOR;
            float distance = Mathf.Clamp(Vector3.Distance(sPoint.position, ePoint.position) - hCOR*(1+vCOR)*height, 0f, Mathf.Infinity);
            Vector3 direction = (ePoint.position - sPoint.position).normalized;
            sPoint.position = new Vector3(transform.position.x, other.transform.parent.position.y + 0.5f, transform.position.z);
            Vector3 displacement = direction * distance;
            ePoint.position = new Vector3(displacement.x + sPoint.position.x, other.transform.parent.position.y + 0.5f, displacement.z + sPoint.position.z);
        }
        step = 0f;
    }
}
