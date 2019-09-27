using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayEnemyDetect : MonoBehaviour
{
    public float viewangle;
    public int raycount;


    private List<RaycastHit2D> rays;
    // Start is called before the first frame update
    void Start()
    {
        rays = new List<RaycastHit2D>(raycount);
    }

    // Update is called once per frame
    void Update()
    {
        CreateRays();
    }

    private void CreateRays()
    {

        for (int i = 0; i < raycount; i++)
        {
            print("ran");
            var increment = viewangle / raycount;
            var fowardangle = transform.eulerAngles.z;

            Vector2 dir1 = DegreeToVector2(fowardangle + ((viewangle - increment) * (i / 2)));
            Vector2 dir2 = DegreeToVector2(fowardangle - ((viewangle + increment) * (i / 2)));

            if (Vector2.Angle(transform.right, dir1) > viewangle)
                continue;


            Debug.DrawRay(transform.position, dir1);
            Debug.DrawRay(transform.position, dir2);
        }
    }


    private Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    private Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}
