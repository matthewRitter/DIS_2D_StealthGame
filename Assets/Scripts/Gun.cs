using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject casing;
    public int casingVeclocity;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot()
    {
        
        GameObject casingDrop = (GameObject)Instantiate(casing, transform.position, Quaternion.identity);
        casingDrop.transform.rotation = Random.rotation;
        Vector3 euler = transform.eulerAngles;
        euler.z = Random.Range(0f, 360f);
        casingDrop.transform.eulerAngles = euler;
        casingDrop.GetComponent<Rigidbody2D>().velocity = transform.right * casingVeclocity;
    }
}
