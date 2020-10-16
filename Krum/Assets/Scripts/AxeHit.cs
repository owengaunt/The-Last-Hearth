using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AxeHit : MonoBehaviour
{
    Animator anim;
    public Camera fpscam;
    public GameObject impactEffect;

    public bool AxeTrue = true;
    public float range = 1.5f;
    public float Damage = 10f;
    public float swingRate = 1f;


    private float nextTimetoSwing = 1f;

    public void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimetoSwing)
        {
            nextTimetoSwing = Time.time + 1f / swingRate;
            StartCoroutine(Swing());
            Hit();
        }
    }


    public void Hit()
    {
        anim.SetTrigger("Active");

    }


    public IEnumerator Swing()
    {
        yield return new WaitForSeconds(1.4f);

        RaycastHit hit;
        if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
        {
            UnityEngine.Debug.Log(hit.transform.name);

            TreeFall treeFall = hit.transform.GetComponent<TreeFall>();
            if (treeFall != null)
            {
                if (AxeTrue == true)
                {
                    treeFall.TakeDamage(Damage);
                }

                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }

        }

    }


        public void SetAxe()
        {
            if (AxeTrue == false)
            {
                AxeTrue = true;
            }
            if (AxeTrue == true)
            {
                AxeTrue = false;
            }
        }


    }

