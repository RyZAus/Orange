using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RileyMcGowan
{
    public class Health : MonoBehaviour
    {
        //Private Vars
        private float currentHealth;
        private bool passiveHealActive;
        private bool canBeDamaged;

        //Public Vars
        [Tooltip("Expects 100, both the starting and max health.")]
        public float maxHealth;
        [Tooltip("This let's the object heal relevant to below variables until max health.")]
        public bool canPassiveHeal;
        [Tooltip("How long until the object starts to initially heal <Seconds>")]
        public float initialHealDelay = 10f;
        [Tooltip("Health to increase per heal, this is used multiple times to lerp the health.")]
        public float healthForHeal = 2f;
        [Tooltip("This is the delay between heals.")]
        public float delayBetweenHeal = .5f;

        private void Start()
        {
            passiveHealActive = false;
            ResetMaxHealth();
        }
        
        /// <summary>
        /// Mostly for editor but resets the health to maximum
        /// </summary>
        public void ResetMaxHealth()
        {
            CurrentHealth = maxHealth;
        }

        /// <summary>
        /// Deals damage to the current health of the object and if it can, passively heals
        /// </summary>
        public void DoDamage(float damageToDeal)
        {
            if (canBeDamaged == true)
            {
                if (passiveHealActive == true && canPassiveHeal == true) //If the heal is already active cancel it
                {
                    StopCoroutine(PassiveHeal()); //Stop the passive heal
                    CurrentHealth -= damageToDeal;
                    StartCoroutine(PassiveHeal(initialHealDelay, delayBetweenHeal, healthForHeal)); //Start the passive heal
                }
                else if (passiveHealActive == false && canPassiveHeal == true)
                {
                    CurrentHealth -= damageToDeal;
                    StartCoroutine(PassiveHeal(initialHealDelay, delayBetweenHeal, healthForHeal));
                }
                else
                {
                    CurrentHealth -= damageToDeal;
                }
            }
        }

        /// <summary>
        /// Can heal the object
        /// </summary>
        public void DoHeal(float healthToHeal)
        {
            CurrentHealth += healthToHeal;
        }

        /// <summary>
        /// Passively heal the object over time after a delay
        /// </summary>
        IEnumerator PassiveHeal(float delayStartHeal = default(float), float timeBetweenHeal = default(float), float healthPerTick = default(float))
        {
            passiveHealActive = true;
            yield return new WaitForSeconds(delayStartHeal);
            while (CurrentHealth < maxHealth)
            {
                yield return new WaitForSeconds(timeBetweenHeal);
                DoHeal(healthPerTick);
            }
            passiveHealActive = false;
        }
        
        /// <summary>
        /// Current health for counter boundaries
        /// </summary>
        public float CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            set
            {
                currentHealth = value;
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }

                if (currentHealth < 0)
                {
                    currentHealth = 0;
                }
            }
        }
    }
}