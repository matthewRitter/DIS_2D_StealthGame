using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageHitBox : MonoBehaviour
{
    public GameObject poofPrefab;
    public int poofQuantity;
    public int radius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Vector2 start = col.gameObject.transform.position;
            for ( int i = 0; i < 360; i += 360/poofQuantity)
            {
                //Make a point in unity and subtract 
                Vector2 cirPoint = new Vector2(radius * Mathf.Cos(i), radius * Mathf.Cos(i));
                Vector2 circleDir = cirPoint - start;
                start += circleDir;
            }
            Instantiate(poofPrefab, col.gameObject.transform);
            GameObject obj = (GameObject)Instantiate(poofPrefab, col.gameObject.transform);
            obj.GetComponent<Rigidbody2D>().velocity = transform.GetComponent<Rigidbody2D>().velocity;
            Destroy(gameObject);

        }
    }
}
