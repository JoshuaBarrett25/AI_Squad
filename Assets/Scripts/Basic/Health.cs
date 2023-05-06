using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Image healthbar;
    HealthManager healthmanager;
    private int currentHealth;

   
    private void Start()
    {
        currentHealth = maxHealth;
    }

    //Damage function; will result in a normalised negative change to currentHealth
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        float nDamage = normalise(damage);
        float nHealth = normalise(currentHealth);
        nHealth -= nDamage;

        healthbar.fillAmount = nHealth;
    }

    //Heal function; will result in a normalised positive change to currentHealth
    void Heal(int heal)
    {
        currentHealth += heal;

        float nHeal = normalise(heal);
        float nHealth = normalise(currentHealth);
        if (nHealth != 1.0f)
        { 
            nHealth += nHeal; 
        }
        if (nHealth > 1.0f)
        {
            currentHealth = maxHealth;
            nHealth = 1.0f;
        }

        healthbar.fillAmount = nHealth;
    }


    //Function used to normalise damage values between 0 and 1.
    //currentHealth can then be used by fillamount for the healthbar fill
    float normalise(int integer)
    {
        float normalised = ((float)integer - 0) / ((float)maxHealth - 0);
        return normalised;
    }

   
}
