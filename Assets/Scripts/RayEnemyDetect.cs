using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RayEnemyDetect : MonoBehaviour
{
    public float viewAngle;
    public int rayCount;
    public float lookDistance;


    private LineRenderer viewLineRenderer;
    private List<RaycastHit2D> rays;
    private float minViewAngle;
    private float maxViewAngle;
    private int layerMask;
    // Start is called before the first frame update
    void Start()
    {
        rays = new List<RaycastHit2D>(rayCount);

        viewLineRenderer = GetComponent<LineRenderer>();
        viewLineRenderer.positionCount = rayCount;

        maxViewAngle = viewAngle;
        minViewAngle = -viewAngle;
        rayCount /= 2;


        layerMask = 1 << 8;
        layerMask = ~layerMask;

    }

    // Update is called once per frame
    void Update()
    {
        CheckRays(CreateRays());
    }

    private void CheckRays(List<Vector3> renderVectors)
    {

        int count = 0;

        foreach (RaycastHit2D hit in rays)
        {
            if(hit.collider != null)
            {
                if (hit.collider.tag == "Player")
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                renderVectors[count++] = hit.point;

                //for(int i = 0; i < 5; i++)
                //{
                //    if (!(count + i > renderVectors.Count - 1))
                //        renderVectors[count + i] = hit.point;

                //    if (!(count - 1 < 0))
                //        renderVectors[count - i] = hit.point;

                //    count++;
                //}
                

            }
        }

        viewLineRenderer.SetPositions(renderVectors.ToArray());
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

            rays.Add(Physics2D.Raycast(transform.position, dir1, lookDistance, layerMask));
            rays.Add(Physics2D.Raycast(transform.position, dir2, lookDistance, layerMask));

            renderVectors.Add(transform.position + (dir1 * lookDistance));
            renderVectors.Add(transform.position + (dir2 * lookDistance));


            Debug.DrawRay(transform.position, dir1, Color.green);
            Debug.DrawRay(transform.position, dir2, Color.red);
        }

        return renderVectors;

        //print(renderVectors[0]);
        //viewLineRenderer.SetPosition(0, transform.position);
        //viewLineRenderer.SetPosition(1, renderVectors[0]);
        //viewLineRenderer.SetPosition(2, transform.position);
        //viewLineRenderer.SetPosition(3, renderVectors[1]);
        //viewLineRenderer.SetPosition(4, transform.position);


        //viewLineRenderer.SetPositions(renderVectors);

    }

}
