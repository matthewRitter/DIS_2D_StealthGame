using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RayEnemyDetect : MonoBehaviour
{
    public float viewAngle;
    public int rayCount;
    public float lookDistance;
    public GameObject rayRenderer;
    public int rayRenderDensity;
    public int trackingFrameCount;
    public float alpha = 0.01f;
    public bool renderLight = true;
    public GameObject cameraObj;

    private LineRenderer viewLineRenderer;
    private List<RaycastHit2D> rays;
    private List<GameObject> rayRenderersList;
    private WaypointPatrol patrolScript;
    private ScreenShake screenShake;
    private float minViewAngle;
    private float maxViewAngle;
    private int layerMask;
    private bool updatePlayerPos;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        rays = new List<RaycastHit2D>(rayCount);
        rayRenderersList = new List<GameObject>();
        patrolScript = GetComponent<WaypointPatrol>();
        screenShake = cameraObj.GetComponent<ScreenShake>();


        viewLineRenderer = GetComponent<LineRenderer>();
        viewLineRenderer.positionCount = rayCount;

        updatePlayerPos = true;
        active = true;

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
        if(active)
            CheckRays(CreateRays());
    }

    private void CheckRays(List<Vector3> renderVectors)
    {

        patrolScript.SetAlertState(false);

        int count = 0;

        foreach (GameObject obj in rayRenderersList)
            Destroy(obj);

        rayRenderersList.Clear();

        foreach (RaycastHit2D hit in rays)
        {

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Player")
                {
                    patrolScript.SetAlertState(true);
                    float baseShakeMag = 1/((Vector3)hit.point - transform.position).magnitude;
                    screenShake.Shake(0.1f, 0.3f*baseShakeMag, 1.0f);

                    if (updatePlayerPos)
                    {
                        patrolScript.SetPlayerPosition(hit.point);
                        StartCoroutine(TrackingCooldown());
                    }
              
                }
            }

            if (count % rayRenderDensity == 0 && renderLight)
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

                }
                else
                {
                    templine.SetPosition(1, renderVectors[count]);
                }

                Gradient grad = new Gradient();
                grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
                             new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f) } );

                templine.colorGradient = grad;
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

            rays.Add(Physics2D.Raycast(transform.position, dir1, lookDistance));
            rays.Add(Physics2D.Raycast(transform.position, dir2, lookDistance));

            renderVectors.Add(transform.position + (dir1 * lookDistance));
            renderVectors.Add(transform.position + (dir2 * lookDistance));

        }

        return renderVectors;

    }


    public void SetActivityState(bool activeState)
    {
        active = activeState;
        if(!active)
        {
            rays.Clear();
            foreach (GameObject obj in rayRenderersList)
                Destroy(obj);
        }
    }


    IEnumerator TrackingCooldown()
    {
        updatePlayerPos = false;

        for (int i = 0; i < trackingFrameCount; i++)
            yield return new WaitForEndOfFrame();

        updatePlayerPos = true;
    }


}
