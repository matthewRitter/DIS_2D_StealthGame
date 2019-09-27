using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class circleManPatrol : MonoBehaviour
{

    public float circleRadius;
    public float patrolSpeed;
    public bool clockwise;

    private float timeCounter;
    private Vector2 origin;
    private float originX;
    private float originY;

    public Sprite[] sprites;

    private SpriteRenderer sprite;


    Rigidbody2D rb;
    //BoxCollider2D collision;


    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        originX = origin.x + circleRadius;
        originY = origin.y + circleRadius;

        timeCounter = 0;

        rb = GetComponent<Rigidbody2D>();
        //collision = GetComponent<BoxCollider2D>();


        sprite = GetComponent<SpriteRenderer>();


        if (clockwise)
        {
            patrolSpeed = -patrolSpeed;
        }

    }

    // Update is called once per frame
    void Update()
    {
  

        if (Time.deltaTime != 0)
        {
            timeCounter += Time.deltaTime * patrolSpeed; 
        }
        else
        {
            timeCounter += patrolSpeed;
        }
        float x = -Mathf.Cos(timeCounter) * circleRadius + originX;
        float y = -Mathf.Sin(timeCounter) * circleRadius + originY;
        transform.position = new Vector2(x, y);


        float cos = -Mathf.Cos(timeCounter);
        float sin = -Mathf.Sin(timeCounter);

        if (patrolSpeed > 0)
        {
            if (sin > 0)
            {
                if ((cos >= -1) && (cos < -.5))
                {
                    sprite.sprite = sprites[7];
                }
                else if ((cos >= -.5) && (cos < 0))
                {
                    sprite.sprite = sprites[6];
                }
                else if ((cos >= 0) && (cos < .5))
                {
                    sprite.sprite = sprites[1];
                }
                else
                {
                    sprite.sprite = sprites[2];
                }
            }
            else
            {
                if ((cos >= -1) && (cos < -.5))
                {
                    sprite.sprite = sprites[7];
                }
                else if ((cos >= -.5) && (cos < 0))
                {
                    sprite.sprite = sprites[6];
                }
                else if ((cos >= 0) && (cos < .5))
                {
                    sprite.sprite = sprites[1];
                }
                else
                {
                    sprite.sprite = sprites[2];
                }
            }
        }
        else
        {
            if (sin > 0)
            {
                if ((cos >= -1) && (cos < -.5))
                {
                    sprite.sprite = sprites[6];
                }
                else if ((cos >= -.5) && (cos < 0))
                {
                    sprite.sprite = sprites[7];
                }
                else if ((cos >= 0) && (cos < .5))
                {
                    sprite.sprite = sprites[1];
                }
                else
                {
                    sprite.sprite = sprites[2];
                }
            }
            else
            {
                if ((cos >= -1) && (cos < -.5))
                {
                    sprite.sprite = sprites[5];
                }
                else if ((cos >= -.5) && (cos < 0))
                {
                    sprite.sprite = sprites[6];
                }
                else if ((cos >= 0) && (cos < .5))
                {
                    sprite.sprite = sprites[3];
                }
                else
                {
                    sprite.sprite = sprites[2];
                }
            }
        }


        print("X is:   " + cos);
        //print("Y is:   " + sin);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            patrolSpeed = -patrolSpeed;
        }
    }
}
