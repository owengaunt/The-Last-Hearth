using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    CharacterInput characterinput;

    CharacterController character;

    public float health;
    public float healthOverTime;

    public float thirst;
    public float thirstOverTime;

    public float hunger;
    public float hungerOverTime;

    public float stamina;
    public float staminaOverTime;
    public float staminaChopOverTime;

    
    public Slider healthBar;
    public Slider thirstBar;
    public Slider hungerBar;
    public Slider staminaBar;

    public float minAmount = 5f;

   CharacterController myBody;
  

    // Start is called before the first frame update
    private void Start()
    {
        myBody = GetComponent<CharacterController>();
        characterinput = GetComponent<CharacterInput>();

        healthBar.maxValue = health;
        thirstBar.maxValue = thirst;
        hungerBar.maxValue = hunger;
        staminaBar.maxValue = stamina;

        updateUI();
    }

    // Update is called once per frame
    private void Update()
    {
        CalculateValues();
    }

    private void CalculateValues()
    {
        hunger -= hungerOverTime * Time.deltaTime;
        thirst -= thirstOverTime * Time.deltaTime;

        if(hunger <= minAmount || thirst <= minAmount)
    {
            health -= healthOverTime * Time.deltaTime;
            stamina -= staminaOverTime * Time.deltaTime;
    }
       

        if (Input.GetMouseButton(0))
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


    private void updateUI()
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
