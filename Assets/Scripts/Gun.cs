using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject casing;
    public int casingVeclocity;
    public GameObject bullet;
    public int bulletVeclocity;

    public void shoot()
    {
        
        GameObject casingDrop = (GameObject)Instantiate(casing, transform.position, Quaternion.identity);
        casingDrop.transform.rotation = Random.rotation;
        Vector3 euler = transform.eulerAngles;
        euler.z = Random.Range(0f, 360f);
        casingDrop.transform.eulerAngles = euler;
        casingDrop.GetComponent<Rigidbody2D>().velocity = transform.right * casingVeclocity;

        GameObject bulletSpawn = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
        bulletSpawn.transform.up = transform.up; 
        bulletSpawn.GetComponent<Rigidbody2D>().velocity = transform.up * bulletVeclocity;
    }
}
