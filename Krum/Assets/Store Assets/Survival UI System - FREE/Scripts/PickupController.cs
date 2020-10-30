using UnityEngine;

namespace SurvivalUISystem
{
    public class PickupController : MonoBehaviour
    {
        [SerializeField] private int itemValue;
        [SerializeField] private PickupType myPickups;
        [SerializeField] private SurvivalUIController uiController;

        public enum PickupType { None, DamageObject, HealObject, HungerObject, ThirstObject, StaminaObject }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (myPickups == PickupType.HealObject)
                {
                    uiController.UpdateHealth("Heal", itemValue);
                }

                if (myPickups == PickupType.DamageObject)
                {
                    uiController.UpdateHealth("Damage", itemValue);
                }


                if (myPickups == PickupType.HungerObject)
                {
                    uiController.UpdateVitals("Hunger", itemValue);
                }

                if (myPickups == PickupType.ThirstObject)
                {
                    uiController.UpdateVitals("Thirst", itemValue);
                }

                if (myPickups == PickupType.StaminaObject)
                {
                    uiController.UpdateStamina("StaminaItem", itemValue);
                }

                this.gameObject.SetActive(false);
            }
        }
    }
}
