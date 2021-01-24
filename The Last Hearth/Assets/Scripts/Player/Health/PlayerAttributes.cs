using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{   
    PlayerINV playerINV; 
    CharacterInput characterInput;

    public bool inventoryEnabled = false;
    public bool hasEaten = false;

    //Attributes
    public float maxHunger, maxStamina, maxColdness;
    public float hunger, stamina, coldness;

    private bool running;

    void Start()
    {   
        playerINV = GetComponent<PlayerINV>();
        characterInput = GetComponent<CharacterInput>();

        hunger = maxHunger;
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

        if(hunger <= 1)
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

}
