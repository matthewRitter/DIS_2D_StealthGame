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
            timeCounter = patrolSpeed;
        }
        float x = -Mathf.Cos(timeCounter) * circleRadius + originX;
        float y = -Mathf.Sin(timeCounter) * circleRadius + originY;
        transform.position = new Vector2(x, y);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            patrolSpeed = -patrolSpeed;
        }
    }
}
