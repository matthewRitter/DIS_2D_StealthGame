using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunItem : MonoBehaviour
{
    // Start is called before the first frame update
    private int bulletCount;
    private bool triggered;
    private GameObject player;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            player = col.gameObject;
            triggered = true;
            Debug.Log("triggereed");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            triggered = false;
        }
    }


    private void Update()
    {
        if (triggered)
        {
            player.GetComponent<PlayerController>().gunActive = true;
            player.GetComponent<PlayerController>().knifeActive = false;
            Destroy(gameObject);
        }
    }
}
