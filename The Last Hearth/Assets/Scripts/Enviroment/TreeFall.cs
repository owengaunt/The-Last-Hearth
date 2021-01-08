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
    public bool isTreeFallen = false;

    public int speed = 14;

    public Rigidbody rb;
    public Vector3 position;

    void Awake()
    {
        position = new Vector3(UnityEngine.Random.Range(-1.0f, 1), 0, UnityEngine.Random.Range(-1.0f, 1.0f));
    }
    
    public void Start()
    {
        tree = this.gameObject;
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(transform.forward * speed);
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(7);

        Destroy(tree);

        if(isTreeFallen == false)
        {
            Instantiate(log, tree.transform.position + new Vector3(0, 0, 0) + position, Quaternion.identity);
            Instantiate(log, tree.transform.position + new Vector3(2, 2, 0) + position, Quaternion.identity);
            Instantiate(log, tree.transform.position + new Vector3(5, 5, 0) + position, Quaternion.identity);
            StartCoroutine(TreeReset());
        }

        isTreeFallen = true;
    }
   
    public IEnumerator TreeReset()
    {
        if(isTreeFallen == true)
        {
            yield return new WaitForSeconds(4);
            isTreeFallen = false;
        }
    }

}
