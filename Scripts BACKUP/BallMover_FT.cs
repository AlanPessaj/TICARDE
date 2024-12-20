﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMover_FT : MonoBehaviour
{
    public ShotManager_FT shot;
    public Transform sPoint;
    public Transform ePoint;
    public float height;
    public float stepSize = 0.1f;
    public float step = 0f;
    public float vCOR;
    public float hCOR;
    public float minheight;
    public bool rolling;
    public float aproxAccuracy;
    public float aproxThreshold;
    public bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float a;
    float r1;
    // Update is called once per frame
    public void Update()
    {
        if (active)
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
            transform.position = new Vector3(Mathf.LerpUnclamped(sPoint.position.x, ePoint.position.x, step), transform.position.y, Mathf.LerpUnclamped(sPoint.position.z, ePoint.position.z, step));
            step += stepSize * Time.deltaTime;
            float x = Vector3.Distance(sPoint.position, new Vector3(transform.position.x, sPoint.position.y, transform.position.z));
            float ballY = F(x);
            transform.position = new Vector3(transform.position.x, ballY + sPoint.position.y, transform.position.z);
        }
    }
    float BuildAndRun(float x, float r1)
    {
        CreateQuadratic();
        return F(x);
    }
    public void UpdateQuadratic(bool smash = false)
    {
        if (smash)
        {
            Vector3 crudeSpoint = sPoint.position + ((ePoint.position - sPoint.position).normalized * -Vector3.Distance(sPoint.position, new Vector3(ePoint.position.x, sPoint.position.y, ePoint.position.z)));
            sPoint.position = new Vector3(crudeSpoint.x, ePoint.position.y, crudeSpoint.z);
            step = 0.5f;
        }
        if (sPoint.position.y == ePoint.position.y)
        {
            r1 = Vector3.Distance(sPoint.position, ePoint.position);
            CreateQuadratic();
        }
        else
        {
            ApproximateQuadratic();
        }
    }

    void CreateQuadratic()
    {
        if (r1 != 0)
        {
            Vector2 intermedio = new Vector2(r1 / 2, height - sPoint.position.y);
            a = intermedio.y / ((intermedio.x - r1) * intermedio.x);
        }
        else
        {
            //BORRAME
            shot.FindShot(0, 0, ShotType.drive, gameObject, true);
        }
    }
    public bool failSafe;
    void ApproximateQuadratic()
    {
        int counter = 0;
        r1 = Vector3.Distance(sPoint.position, new Vector3(ePoint.position.x, sPoint.position.y, ePoint.position.z));
        Vector2 qEPoint = new Vector2(r1, ePoint.position.y - sPoint.position.y);
        do
        {
            counter++;
            float currentValue = BuildAndRun(qEPoint.x, r1);
            if (BuildAndRun(qEPoint.x, r1) <  qEPoint.y)
            {
                r1 += aproxAccuracy * (qEPoint.y - BuildAndRun(qEPoint.x, r1));
            }
            else
            {
                r1 -= aproxAccuracy * (BuildAndRun(qEPoint.x, r1) - qEPoint.y);
            }
            if(counter > 10000 && failSafe)
            {
                Destroy(gameObject);
                break;
            }
        } while (Mathf.Abs(qEPoint.y - BuildAndRun(qEPoint.x, r1)) > aproxThreshold);
        CreateQuadratic();
    }

    public float F(float x)
    {
        return a * (x - r1) * x;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            height *= vCOR;
            float distance = Mathf.Clamp(Vector3.Distance(sPoint.position, ePoint.position) - hCOR * (1 + vCOR) * height, 0f, Mathf.Infinity);
            Vector3 direction = (ePoint.position - sPoint.position).normalized;
            sPoint.position = new Vector3(transform.position.x, other.transform.position.y + 0.5f, transform.position.z);
            Vector3 displacement = direction * distance;
            ePoint.position = new Vector3(displacement.x + sPoint.position.x, other.transform.position.y + 0.5f, displacement.z + sPoint.position.z);
            UpdateQuadratic();
            step = 0f;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Out"))
        {
            //BORRAME
            shot.FindShot(0, 0, ShotType.drive, gameObject, true);
        }
    }
}
