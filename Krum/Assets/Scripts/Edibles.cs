using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Edibles : MonoBehaviour
{
    PlayerStats playerStats;

    public Camera fpscam;

    public float range; 
    public float hunger;

    public Slider hungerBar;
    public float hungerOverTime;

  
    public float minAmount = 0f;



    void Start()
    {
        playerStats = GetComponent<PlayerStats>();

        hungerBar.maxValue = hunger;

        updateUI();
    }

    void Update()
    {
            CalculateValues();
    }

    void CalculateValues()
    {


        if (Input.GetKeyDown("f"))
        {
            Eat();
        }
    }

    public void updateUI()
    {
        hunger = Mathf.Clamp(hunger, 0, 100f);
        hungerBar.value = hunger;
      
    }

    public void Eat()
    {
       

        RaycastHit hit;
        if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
        {
            UnityEngine.Debug.Log(hit.transform.name);

            
            if(gameObject.tag == "solid")
            {
                hunger += hungerOverTime * Time.deltaTime;
            }
            else
            {
                hunger -= hungerOverTime * Time.deltaTime;
            }

        }
    }

}
