﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLightRays : MonoBehaviour
{
    public float viewAngle;
    public int rayCount;
    public float lookDistance;
    public GameObject rayRenderer;
    public int rayRenderDensity;


    private LineRenderer viewLineRenderer;
    private List<RaycastHit2D> rays;
    private List<GameObject> rayRenderersList;
    private float minViewAngle;
    private float maxViewAngle;
    private int layerMask;
    // Start is called before the first frame update
    void Start()
    {
        rays = new List<RaycastHit2D>(rayCount);
        rayRenderersList = new List<GameObject>();

        viewLineRenderer = GetComponent<LineRenderer>();
        viewLineRenderer.positionCount = rayCount;

        maxViewAngle = viewAngle;
        minViewAngle = -viewAngle;
        rayCount /= 2;


        layerMask = 1 << 8;
        layerMask = ~layerMask;

        if (rayRenderDensity % 2 == 0)
            rayRenderDensity += 1;

    }

    // Update is called once per frame
    void Update()
    {
        CheckRays(CreateRays());
    }

    private void CheckRays(List<Vector3> renderVectors)
    {

        int count = 0;

        foreach (GameObject obj in rayRenderersList)
            Destroy(obj);

        rayRenderersList.Clear();

        foreach (RaycastHit2D hit in rays)
        {

            if (hit.collider != null)
            {
                //Collision Logic goes here if you want any
                
            }

            if (count % rayRenderDensity == 0)
            {
                GameObject rayRendererObject = Instantiate(rayRenderer, transform);
                rayRendererObject.transform.SetParent(transform);
                rayRenderersList.Add(rayRendererObject);


                LineRenderer templine = rayRendererObject.GetComponent<LineRenderer>();
                templine.startWidth = 0.10f * rayRenderDensity;
                templine.endWidth = 0.10f * rayRenderDensity;
                templine.SetPosition(0, transform.position);

                if (hit.collider != null)
                {
                    if (count % rayRenderDensity == 0)
                        templine.SetPosition(1, hit.point);

                    print(hit.collider.tag);
                }
                else
                {
                    templine.SetPosition(1, renderVectors[count]);
                }

            }
            count++;



        }

        rays.Clear();
    }

    private List<Vector3> CreateRays()
    {

        float increment = viewAngle / rayCount;
        float angle1 = 0;
        float angle2 = 0;

        List<Vector3> renderVectors = new List<Vector3>();

        for (int i = 0; i < rayCount; i++)
        {

            angle1 += increment;
            angle2 -= increment;

            if (angle1 > maxViewAngle)
                angle1 = maxViewAngle;
            if (angle2 < minViewAngle)
                angle2 = minViewAngle;

            Vector3 dir1 = Quaternion.AngleAxis(angle1, transform.forward) * transform.right;
            Vector3 dir2 = Quaternion.AngleAxis(angle2, transform.forward) * transform.right;

            rays.Add(Physics2D.Raycast(transform.position, dir1, lookDistance)); //, layerMask));
            rays.Add(Physics2D.Raycast(transform.position, dir2, lookDistance)); //, layerMask));

            renderVectors.Add(transform.position + (dir1 * lookDistance));
            renderVectors.Add(transform.position + (dir2 * lookDistance));


            //Debug.DrawRay(transform.position, dir1, Color.green);
            //Debug.DrawRay(transform.position, dir2, Color.red);
        }

        return renderVectors;

    }

}