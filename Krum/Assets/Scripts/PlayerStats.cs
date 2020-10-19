using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    Edibles edibles;
    Drinkables drinkables;
    AxeHit axeHit;
    CharacterInput characterinput;
    CharacterController character;

    public float swingRate = 1f;
    private float nextTimetoSwing = 2f;

    public float health;
    public float healthOverTime;

    public float thirst;
    public float thirstOverTime;
    public float drinkOverTime;

    public float hunger;
    public float hungerOverTime;
    public float eatOverTime;

    public float stamina;
    public float staminaOverTime;
    public float staminaChopOverTime;
   

    
    public Slider healthBar;
    public Slider thirstBar;
    public Slider hungerBar;
    public Slider staminaBar;

    public float minAmount = 0f;

   CharacterController myBody;
  

    // Start is called before the first frame update
    private void Start()
    {
        edibles = GetComponent<Edibles>();
        drinkables = GetComponent<Drinkables>();
        axeHit = GetComponent<AxeHit>();
        myBody = GetComponent<CharacterController>();
        characterinput = GetComponent<CharacterInput>();

        healthBar.maxValue = health;
        thirstBar.maxValue = thirst;
        hungerBar.maxValue = hunger;
        staminaBar.maxValue = stamina;

        updateUI();
    }

    // Update is called once per frame
    public void Update()
    {
        CalculateValues();
    }

    public void CalculateValues()
    {

        if (hunger <= minAmount || thirst <= minAmount)
    {
            health -= healthOverTime * Time.deltaTime;
            stamina -= staminaOverTime * Time.deltaTime;
    }


        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimetoSwing)
        {
            StartCoroutine(Swinging());
        }

if (Input.GetKey(characterinput.sprintkey))
        {
            stamina -= staminaOverTime * Time.deltaTime;
            hunger -= hungerOverTime * Time.deltaTime;
            thirst -= thirstOverTime * Time.deltaTime;
        }
        else
        {
            stamina += staminaOverTime * Time.deltaTime;
        }

        if(health <= 0)
        {
            print("PLAYER HAS DIED");
        }

        updateUI();
    }


    public void updateUI()
    {
       health = Mathf.Clamp(health, 0, 100f);
       thirst = Mathf.Clamp(thirst, 0, 100f);
       hunger = Mathf.Clamp(hunger, 0, 100f);
       stamina = Mathf.Clamp(stamina, 0, 100f);

        healthBar.value = health;
        thirstBar.value = thirst;
        hungerBar.value = hunger;
        staminaBar.value = stamina;
    }

    public void TakeDamage(float amnt)
    {
        health -= amnt;

        updateUI();
    }

    public IEnumerator Swinging()
    {
        yield return new WaitForSeconds(1.3f);

        stamina -= staminaChopOverTime * Time.deltaTime;

    }
//end of class
}
