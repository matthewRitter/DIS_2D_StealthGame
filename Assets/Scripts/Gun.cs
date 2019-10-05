using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject casing;
    public int casingVeclocity;
    public GameObject bullet;
    public int bulletVeclocity = 20;
    public bool isPlayer = true;

    public void shoot()
    {
        
        GameObject casingDrop = (GameObject)Instantiate(casing, transform.position, Quaternion.identity);
        casingDrop.transform.rotation = Random.rotation;
        Vector3 euler = transform.eulerAngles;
        euler.z = Random.Range(0f, 360f);
        casingDrop.transform.eulerAngles = euler;
        casingDrop.GetComponent<Rigidbody2D>().velocity = transform.right * casingVeclocity;

        
        if (isPlayer)
        {
            GameObject bulletObj = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            bulletObj.transform.up = transform.up;
            bulletObj.GetComponent<Rigidbody2D>().velocity = transform.up * bulletVeclocity;
        }
        else
        {
            Vector3 pos = transform.parent.position + transform.parent.right;
            GameObject bulletObj = (GameObject)Instantiate(bullet, pos, Quaternion.identity);
            bulletObj.transform.up = transform.up;
            bulletObj.GetComponent<Rigidbody2D>().velocity = transform.up * bulletVeclocity;
        }

    }

}
