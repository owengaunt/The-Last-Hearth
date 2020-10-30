using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFall : MonoBehaviour
{
    AxeHit axeHit;

    public GameObject tree;
    public GameObject log;

    public float health = 50f;
    public float thrust = 1.0f;
    public float distanceOffset = 2f;
    public float distance2Offset = 4f;
    public float distance3Offset = 6f;



    public Rigidbody rb;

    public void Start()
    {
     
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
          
           Die();
        }
    }
    void Die()
    {
        Destroy(tree);

        Instantiate(original : log, position: tree.transform.position + (Vector3.left * distanceOffset), rotation: tree.transform.rotation, parent: null);
        Instantiate(original: log, position: tree.transform.position + (Vector3.left * distance2Offset), rotation: tree.transform.rotation, parent: null);
        Instantiate(original: log, position: tree.transform.position + (Vector3.left * distance3Offset), rotation: tree.transform.rotation, parent: null);


    }

}
