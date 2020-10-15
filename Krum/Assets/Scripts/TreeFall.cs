using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFall : MonoBehaviour
{
    AxeHit axeHit;

    public float health = 50f;
    public float thrust = 1.0f;


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
        Destroy(gameObject);
    }

}
