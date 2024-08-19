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
    bool rolling;
    public float aproxAccuracy;
    public float aproxThreshold;
    // Start is called before the first frame update
    void Start()
    {
        UpdateQuadratic();
        sPointi = sPoint.position;
        ePointi = ePoint.position;
        heighti = height;
        stepi = stepSize;
    }
    float a;
    float r1;
    // Update is called once per frame
    void Update()
    {
        if (height < minheight && !rolling)
        {
            height = 0f;
            UpdateQuadratic();
            rolling = true;
        }
        if (rolling)
        {
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
            UpdateQuadratic();
        }
        transform.position = new Vector3(Mathf.LerpUnclamped(sPoint.position.x, ePoint.position.x, step), transform.position.y, Mathf.LerpUnclamped(sPoint.position.z, ePoint.position.z, step));
        step += stepSize * Time.deltaTime;
        float x = Vector3.Distance(sPoint.position, new Vector3(transform.position.x, sPoint.position.y, transform.position.z));
        float ballY = F(x);
        transform.position = new Vector3(transform.position.x, ballY + sPoint.position.y, transform.position.z);
    }
    float BuildAndRun(float x, float r1)
    {
        return F(x, CreateQuadratic(r1));
    }
    void UpdateQuadratic()
    {
        if (sPoint.position.y == ePoint.position.y)
        {
            CreateQuadratic();
        }
        else
        {
            ApproximateQuadratic();
        }
    }

    void CreateQuadratic()
    {
        r1 = Vector3.Distance(sPoint.position, ePoint.position);
        if (r1 != 0)
        {
            Vector2 intermedio = new Vector2(r1 / 2, height - sPoint.position.y);
            a = intermedio.y / ((intermedio.x - r1) * intermedio.x);
        }
        else
        {
            sPoint.position = sPointi;
            ePoint.position = ePointi;
            stepSize = stepi;
            height = heighti;
            step = 0f;
            UpdateQuadratic();
        }
    }

    float CreateQuadratic(float r1)
    {
        if (r1 != 0)
        {
            Vector2 intermedio = new Vector2(r1 / 2, height - sPoint.position.y);
            return intermedio.y / ((intermedio.x - r1) * intermedio.x);
        }
        else
        {
            return 0;
        }
    }

    void ApproximateQuadratic()
    {
        float r1 = Vector3.Distance(sPoint.position, new Vector3(ePoint.position.x, sPoint.position.y, ePoint.position.x));
        do
        {
            /*if (BuildAndRun(r1, r1) == 0)
            {
                sPoint.position = sPointi;
                ePoint.position = ePointi;
                stepSize = stepi;
                height = heighti;
                step = 0f;
                break;
            }*/
            if (BuildAndRun(r1, r1) < 0 - ePoint.position.y)
            {
                r1 += aproxAccuracy * (ePoint.position.y - BuildAndRun(r1, r1));
            }
            else
            {
                r1 -= aproxAccuracy * (BuildAndRun(r1, r1) - ePoint.position.y);
            }
        } while (Mathf.Abs(ePoint.position.y - BuildAndRun(r1, r1)) > aproxThreshold);
        this.r1 = r1;
        CreateQuadratic();
    }

    float F(float x)
    {
        return a * (x - r1) * x;
    }
    float F(float x, float a)
    {
        return a * (x - r1) * x;
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
            UpdateQuadratic();
            Debug.Log("BOING");
        }
        step = 0f;
    }
}
