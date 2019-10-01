﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poofOnDeath : MonoBehaviour
{
    public GameObject poofPrefab;
    public int poofQuantity;
    public int radius;

    // Start is called before the first frame update
    void OnDestroy()
    {
        Vector2 start = transform.position;
        for (int i = 360 / poofQuantity; i <= 360; i += 360 / poofQuantity)
        {
            Debug.Log(i);
            //Make a point in unity and subtract 
            Vector2 cirPoint = new Vector2(radius * Mathf.Cos(Mathf.Deg2Rad*i), radius * Mathf.Sin(Mathf.Deg2Rad * i));
            Vector2 circleDir = cirPoint - start;
            start += circleDir;
            Instantiate(poofPrefab, transform);
            GameObject obj = (GameObject)Instantiate(poofPrefab, transform.position, Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().velocity = start * 1;
        }
    }
}
