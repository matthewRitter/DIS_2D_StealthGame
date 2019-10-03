using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class superHotBoi : MonoBehaviour
{

    public float patrolSpeed;
    public float areaRadius;

    Rigidbody2D rb2d;
    CircleCollider2D sightDistance;
    Vector2 playerPosition;
    private bool playerInSight;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sightDistance = gameObject.AddComponent<CircleCollider2D>();

        sightDistance.radius = areaRadius;
        sightDistance.isTrigger = true;
        playerInSight = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = new Vector2(0, 0);
        if (playerInSight)
        {
            if (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.DownArrow)) || 
                (Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || 
                (Input.GetKey(KeyCode.D)))
            {
                Vector2 movement = new Vector2(0,0);


                if ((playerPosition.y > gameObject.transform.position.y) && (canMoveUp()))
                {
                    movement.y = patrolSpeed;
                }
                else if ((playerPosition.y < gameObject.transform.position.y) && (canMoveDown()))
                {
                    movement.y = -patrolSpeed;
                }

                if (playerPosition.x > gameObject.transform.position.x)
                {
                    movement.x = patrolSpeed;
                }
                else if (playerPosition.x < gameObject.transform.position.x)
                {
                    movement.x = -patrolSpeed;
                }

                rb2d.velocity = movement;
                transform.right = rb2d.velocity.normalized;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //playerPosition = collision.transform.position;
            print("kill Player");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("Enter Trigger");
            playerPosition = collision.transform.position;
            print(playerPosition);
            playerInSight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("Exit Trigger");
            playerInSight = false;
        }
    }

    private bool canMoveUp()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.up, (float)1.5);

        if (hit.collider != null && (hit.collider.tag == "wall"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool canMoveDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, (float)1.5);

        if (hit.collider != null && (hit.collider.tag == "wall"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool canMoveRight()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.right, (float)1.5);

        if (hit.collider != null && (hit.collider.tag == "wall"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool canMoveLeft()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.left, (float)1.5);

        if (hit.collider != null && (hit.collider.tag == "wall"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }


}
