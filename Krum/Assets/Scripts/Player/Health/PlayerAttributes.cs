using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{

    CharacterInput characterInput;

    //Attributes
    public float maxHunger, maxThirst, maxStamina, maxColdness;
    public float hunger, thirst, stamina, coldness;

    private bool running;

    void Start()
    {

        characterInput = GetComponent<CharacterInput>();

        hunger = maxHunger;
        thirst = maxThirst;
        stamina = maxStamina;
        coldness = maxColdness;
    }

    void Update()
    {
        if(hunger > 0)
            hunger -= 0.2f * Time.deltaTime;

        if (thirst > 0)
            thirst -= 0.25f * Time.deltaTime;

        if(hunger <= 1 || thirst <= 1)
            Die();

        if (Input.GetKey(KeyCode.LeftShift) && (stamina > 0) && (coldness < 100))
        {
            stamina -= 5 * Time.deltaTime;
            coldness += 3 * Time.deltaTime;
            hunger -= 1 * Time.deltaTime;
        }
        else
        {
            StartCoroutine(RegainStamina());
        }

        if(stamina <= 1)
            print("Too Tired");

        coldness -= 1 * Time.deltaTime;

        if (hunger < 100)
        {
             Eat();
        }

        if(thirst < 100)
        {
             Drink();
        }
    }

    public void Eat()
    {

        if (hunger < 100)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                print("YOU ATE");
                hunger = maxHunger;
            }
        }

    }   

    public void Drink()
    {
        if (thirst < 100)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                print("YOU DRANK");
                thirst = maxThirst;
            }
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
