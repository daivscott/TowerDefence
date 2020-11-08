using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    public Slider healthSlider;
    public Text healthText;
    public float armour;

    // OnEnable used due to object pooling
    void OnEnable()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.minValue = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = currentHealth;

        healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

        if(Input.GetKeyDown(KeyCode.G))
        {
            LoseHealth(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            GainHealth(5);
        }
    }

    public void GainHealth(float valueIn)
    {
        if (currentHealth >= maxHealth)
        { 
            return;
        }

        currentHealth += valueIn;
    }

    public void LoseHealth(float valueIn)
    {
        if (currentHealth <= 0f)
        {
            return;
        }

        currentHealth -= valueIn;
    }

    public void LoseBlockedDamage(float damageIn)
    {
        currentHealth -= ArmourDamage(damageIn);
    }

    public float ArmourDamage(float damageIn)
    {
        float armourDamage;
        float damageMultiplier = 1 - 0.06f * armour / (1 + 0.06f * armour);
        armourDamage = damageIn * damageMultiplier;
        //Debug.Log(armourDamage);
        return armourDamage;
    }
}
