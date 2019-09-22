using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroltype1 : MonoBehaviour
{
    public float walldetectdistance = 0;
    public float speed = 3;


    Rigidbody2D rb;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = rb.velocity;

        velocity.x = speed;

        rb.velocity = velocity;

        WallCheck();
    }

    bool WallCheck()
    {
        Vector2 origin = transform.position;
        origin.x += 1;
        Vector2 direction = (transform.right - transform.position);
        direction.Normalize();

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, walldetectdistance);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Wall"))
            {
                print("HIT WALL");
                var down = transform.up * -1.0f;
                //transform.rotation = Quaternion.LookRotation(down);
                transform.rotation = Quaternion.AngleAxis(-90, Vector3.right);
                return true;
            }

        }

        return false;
    }
}
