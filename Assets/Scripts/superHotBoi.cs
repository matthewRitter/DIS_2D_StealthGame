using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class superHotBoi : MonoBehaviour
{

    public float patrolSpeed;
    public int rayCount;
    public float lookDistance;

    Vector2 playerPosition;
    private bool playerInSight;
    private int layerMask;
    private int layerMask2;
    private List<RaycastHit2D> rays;
    private int maxViewAngle;
    private int minViewAngle;



    // Start is called before the first frame update
    void Start()
    {
        rays = new List<RaycastHit2D>(rayCount);

        int viewAngle = 360;
        maxViewAngle = viewAngle;
        minViewAngle = -viewAngle;
        layerMask = 1 << 9;
        layerMask = ~layerMask;
        layerMask2 = 1 << 8;
        layerMask2 = ~layerMask2;

    }

    // Update is called once per frame
    void Update()
    {
        CreateRays();
        CheckRays();

        if (playerInSight)
        {
            if (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.DownArrow)) || 
                (Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || 
                (Input.GetKey(KeyCode.D)))
            {
                MoveTowards();
            }
        }
        
    }

    private void CheckRays()
    {
        foreach (RaycastHit2D hit in rays)
        {

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Player")
                {
                    playerInSight = true;
                    playerPosition = hit.point;
                }
            }
        }

        rays.Clear();
    }

    private void CreateRays()
    {

        float increment = 360 / rayCount;
        float angle1 = 0;
        float angle2 = 0;

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

            rays.Add(Physics2D.Raycast(transform.position, dir1, lookDistance, layerMask, layerMask2));
            rays.Add(Physics2D.Raycast(transform.position, dir2, lookDistance, layerMask, layerMask2));
        }

    }

    private void MoveTowards()
    {
        var step = patrolSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);

        transform.right = (Vector3)playerPosition - transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("kill Player");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
