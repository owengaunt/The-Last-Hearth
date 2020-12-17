using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{


    //Attributes
    public float maxHunger, maxThirst, maxStamina, maxColdness;
    public float hunger, thirst, stamina, coldness;

    private bool running;

    void Start()
    {

        hunger = maxHunger;
        thirst = maxThirst;
        stamina = maxStamina;
        coldness = maxColdness;
    }

    void Update()
    {
        if(hunger > 0)
            hunger -= 0.3f * Time.deltaTime;

        if (thirst > 0)
            thirst -= 0.45f * Time.deltaTime;

        if(hunger >= maxHunger || thirst >= maxThirst)
            Die();

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            stamina -= 5 * Time.deltaTime;
        }
        else
        {
            StartCoroutine(RegainStamina());
        }

        if(stamina <= 0)
            print("Too Tired");

        coldness -= 1 * Time.deltaTime;
    }
    
    public void Ingestion()
    {
        //if(Attributes.Hunger == itembuff.value 80)
        {
            hunger = hunger + 80;
        }

        
        {
            thirst = thirst + 50;
        }

    }

    public IEnumerator RegainStamina()
    {
        yield return new WaitForSeconds(2f);
        {
            if (stamina < maxStamina)
                stamina += 2.5f * Time.deltaTime;
        }
    }

    public void OnTriggerEnter(Collider other)
    {

    }

    public void OnTriggerExit(Collider other)
    {

    }
    
    void Die()
    {
        print("DEAD");
    }

}
