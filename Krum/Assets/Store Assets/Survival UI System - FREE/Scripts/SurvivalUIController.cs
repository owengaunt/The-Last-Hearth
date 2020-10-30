using UnityEngine;
using UnityEngine.UI;

namespace SurvivalUISystem
{
    public class SurvivalUIController : MonoBehaviour
    {
        [Header("Health Controls")]
        public float playerHealth;
        [SerializeField] private float maxHealth;

        [Header("Stamina Controls")]
        public float playerStamina;
        public float maxStamina;
        [SerializeField] private float staminaDrain;
        [SerializeField] private float staminaRegen;
        [SerializeField] private float jumpCost;

        [Header("Hunger Controls")]
        [SerializeField] private float playerHunger;
        [SerializeField] private float maxHunger;
        [SerializeField] private float hungerDrain;

        [Header("Thirst Controls")]
        [SerializeField] private float playerThirst;
        [SerializeField] private float maxThirst;
        [SerializeField] private float thirstDrain;

        [Header("Thirst / Hunger Damage Controls")]
        [SerializeField] private int damageMult;
        [SerializeField] private float hungerDamageRate;
        [SerializeField] private float thirstDamageRate;

        [Header("Player Reference")]
        [SerializeField] private Image healthImage;
        [SerializeField] private Image staminaImage;
        [SerializeField] private Image hungerImage;
        [SerializeField] private Image thirstImage;
        [HideInInspector] public bool weAreSprinting;

        private void Update()
        {
            if (!weAreSprinting)
            {
                if (playerStamina <= maxStamina)
                {
                    playerStamina += Time.deltaTime * staminaRegen;
                    UpdateStamina("StaminaUpdate", 0);
                }
            }

            if (playerHealth >= 0)
            {
                if (playerHunger >= 0)
                {
                    playerHunger -= Time.deltaTime * hungerDrain;
                }
                else
                {
                    UpdateHealth("NoVitals", damageMult);
                }

                if (playerThirst >= 0)
                {
                    playerThirst -= Time.deltaTime * thirstDrain;
                }
                else
                {
                    UpdateHealth("NoVitals", damageMult);
                }

                UpdateVitals("UpdateVitals", 0);
            }
        }

        public void UpdateVitals(string vitalType, int value)
        {
            if (vitalType == "Hunger")
            {
                playerHunger += value;

                if (playerHunger >= maxHunger)
                {
                    playerHunger = maxHunger;
                }
            }

            if (vitalType == "Thirst")
            {
                playerThirst += value;

                if (playerThirst >= maxThirst)
                {
                    playerThirst = maxThirst;
                }
            }

            if (vitalType == "UpdateVitals")
            {
                hungerImage.fillAmount = playerHunger / maxHunger;
                thirstImage.fillAmount = playerThirst / maxThirst;
            }
        }

        public void Sprinting()
        {
            weAreSprinting = true;
            playerStamina -= Time.deltaTime * staminaDrain;
            UpdateStamina("StaminaUpdate", 0);
        }

        public void StaminaJump()
        {
            playerStamina -= jumpCost;
            UpdateStamina("StaminaUpdate", 0);
        }

        public void UpdateStamina(string staminaItemType, int value)
        {
            if (staminaItemType == "StaminaUpdate")
            {
                staminaImage.fillAmount = playerStamina / maxStamina;
            }

            if (staminaItemType == "StaminaItem")
            {
                playerStamina += value;
                UpdateStamina("StaminaUpdate", 0);

                if (playerStamina >= maxStamina)
                {
                    playerStamina = maxStamina;
                }
            }
        }

        public void UpdateHealth(string healthItemType, int value)
        {
            if (healthItemType == "UpdateHealth")
            {
                healthImage.fillAmount = playerHealth / maxHealth;
            }

            if (healthItemType == "NoVitals")
            {
                if (playerHealth >= 0)
                {
                    playerHealth -= value / (hungerDamageRate + thirstDamageRate);
                    UpdateHealth("UpdateHealth", 0);
                }
            }

            if (healthItemType == "Damage")
            {
                if (playerHealth >= 0)
                {
                    playerHealth -= value;
                    UpdateHealth("UpdateHealth", 0);

                    if (playerHealth <= 0)
                    {
                        Death();
                    }
                }
            }

            if (healthItemType == "Heal")
            {
                if (playerHealth >= 0)
                {
                    playerHealth += value;
                    UpdateHealth("UpdateHealth", 0);

                    if (playerHealth >= maxHealth)
                    {
                        playerHealth = maxHealth;
                    }
                }
            }
        }

        void Death()
        {
            Debug.Log("You died!");
        }
    }
}

