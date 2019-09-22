using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backAndForthPatrol : MonoBehaviour
{


    public float direction;
    //0 is left
    //1 is up
    //2 is right
    //3 is down


    public float wallDetectionDistance;

    public float patrolSpeed;


    Rigidbody2D rb2d;

    Vector2 directionFacing;
    

    // Start is called before the first frame update
    void Start()
    {
        print(direction);
        rb2d = GetComponent<Rigidbody2D>();


        switch (direction)
        {
            case 0:
                directionFacing = Vector2.left;
                break;
            case 1:
                directionFacing = Vector2.up;
                break;
            case 2:
                directionFacing = Vector2.right;
                break;
            case 3:
                directionFacing = Vector2.down;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 origin = transform.position;

        if (direction == 0)
        {
            origin.x -= 1;
        }
        else if (direction == 1)
        {
            origin.y += 1;
        }
        else if (direction == 2)
        {
            origin.x += 1;
        }
        else if (direction == 3)
        {
            origin.y -= 1;
        }

        RaycastHit2D wall = Physics2D.Raycast(origin, directionFacing, wallDetectionDistance);

        if (wall.collider != null)
        {
            print(wall.collider.tag);
        }


        if ((wall.collider != null) && (wall.collider.tag == "wall"))
        {
            changeDirection();
        }
        else
        {
            Vector2 movement = new Vector2(0,0);
            if (direction == 0)
            {
                movement = new Vector2(-patrolSpeed, 0);
                //print("Going Left");
            }
            else if (direction == 1)
            {
                movement = new Vector2(0, patrolSpeed);
            }
            else if (direction == 2)
            {
                movement = new Vector2(patrolSpeed, 0);
            }
            else if (direction == 3)
            {
                movement = new Vector2(0, -patrolSpeed);
            }
            rb2d.velocity = movement;
        }

    }


    public void changeDirection()
    {

        if (direction == 0)
        {
            direction = 2;
        }
        else if (direction == 2)
        {
            direction = 0;
        }
        else if (direction == 1)
        {
            direction = 3;
        }
        else if (direction == 3)
        {
            direction = 1;
        }

        switch (direction)
        {
            case 0:
                directionFacing = Vector2.left;
                break;
            case 1:
                directionFacing = Vector2.up;
                break;
            case 2:
                directionFacing = Vector2.right;
                break;
            case 3:
                directionFacing = Vector2.down;
                break;
        }
    }


}
