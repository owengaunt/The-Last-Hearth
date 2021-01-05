using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{   
    PlayerINV playerINV; 
    CharacterInput characterInput;

    public bool inventoryEnabled = false;
    public bool hasEaten = false;
    public bool hasDrinken = false;

    //Attributes
    public float maxHunger, maxThirst, maxStamina, maxColdness;
    public float hunger, thirst, stamina, coldness;

    private bool running;

    void Start()
    {   
        playerINV = GetComponent<PlayerINV>();
        characterInput = GetComponent<CharacterInput>();

        hunger = maxHunger;
        thirst = maxThirst;
        stamina = maxStamina;
        coldness = maxColdness;
    }

    void Update()
    {
        //Inventory Open

        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryEnabled = !inventoryEnabled;
        }


        //stat changes
        if (hunger > 0)
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
        if ((hunger < 100) && (inventoryEnabled == false) && (playerINV.isLookingatObj == false))
            if (Input.GetKeyDown(KeyCode.F))
                    if (playerINV.isfoodEquipped == true)
                    {
                        print("YOU ATE");
                        hunger = maxHunger;
                        hasEaten = true;
                        StartCoroutine(NoEat());
                    }
        
    }   

    public void Drink()
    {

        if ((hunger < 100) && (inventoryEnabled == false) && (playerINV.isLookingatObj == false))
            if (Input.GetKeyDown(KeyCode.F))
                    if (playerINV.isdrinkEquipped == true)
                    {
                        print("YOU DRANK");
                        thirst = maxThirst;
                        hasDrinken = true;
                        StartCoroutine(NoDrink());
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

    public IEnumerator NoEat()
    {
        yield return new WaitForSeconds(0.3f);
        {
            hasEaten = false;
        }
    }


    public IEnumerator NoDrink()
    {
        yield return new WaitForSeconds(0.3f);
        {
            hasDrinken = false;
        }
    }
}
