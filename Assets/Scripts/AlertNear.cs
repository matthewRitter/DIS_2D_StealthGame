using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertNear : MonoBehaviour
{

    public float AlertRadius;

    private CircleCollider2D alertArea;

    // Start is called before the first frame update
    void Start()
    {
        alertArea = gameObject.AddComponent<CircleCollider2D>();
        alertArea.radius = AlertRadius;
        alertArea.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        print("Running stay");
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<WaypointPatrol>() != null)
            {
                collision.gameObject.GetComponent<WaypointPatrol>().SetAlertState(true);
            }
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<WaypointPatrol>() != null)
            {
                collision.gameObject.GetComponent<WaypointPatrol>().SetAlertState(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<WaypointPatrol>() != null)
            {
                collision.gameObject.GetComponent<WaypointPatrol>().SetAlertState(true);
            }
        }
    }




}
