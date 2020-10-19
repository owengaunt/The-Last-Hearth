using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Drinkables : MonoBehaviour
{
    PlayerStats playerStats;

    public Camera fpscam;
    public float range;

    public float thirst;

    public Slider thirstBar;
    public float thirstOverTime;

    public float minAmount = 0f;




    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        
        thirstBar.maxValue = thirst;

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
            Drink();
        }
    }

        public void updateUI()
    {

        thirst = Mathf.Clamp(thirst, 0, 100f);
        thirstBar.value = thirst;
      
       
    }

    public void Drink()
    {
        

        RaycastHit hit;
        if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
        {
            UnityEngine.Debug.Log(hit.transform.name);

            if (gameObject.tag == "liquid")
            {
                thirst += thirstOverTime * Time.deltaTime;
            }
            else
            {
                thirst -= thirstOverTime * Time.deltaTime;
            }

        }
    }
}
