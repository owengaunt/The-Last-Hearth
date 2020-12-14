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
        stamina = maxStamina;
    }

    void Update()
    {
        if(hunger < maxHunger)
            hunger += 0.15f * Time.deltaTime;

        if (thirst < maxThirst)
            thirst += 0.25f * Time.deltaTime;

        if(hunger >= maxHunger || thirst >= maxThirst)
            Die();

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
            stamina -= 5 * Time.deltaTime;

        if(stamina <= 0)
            print("Too Tired");

        coldness += 1 * Time.deltaTime;
    }
    
    public void Ingestion(GameObject obj)
    {
        if(obj.tag == "Food")
        {
            hunger = hunger - 50;
        }

        if(obj.tag == "Drink")
        {
            thirst = thirst - 25;
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
