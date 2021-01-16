using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float Health { get; private set; }

    private float healthMin = 0;
    private float healthMax = 100;

    private float regenTimer = 0;
    private float regenTimerMax = 1; 

    public Slider healthBar;

    private void Awake()
    {
        Health = 100;
        UpdateHealth();
    }

    void Update()
    {
        HealthRegen();
    }

    public void affectHealth(float Quantity)
    {
        Health += Quantity;

        UpdateHealth();

        Debug.Log("Health: " + Health);

        if (Health < healthMin)
        {
            Death();
        }

        if(Health > healthMax)
        {
            Health = healthMax;
        }
    }

    void HealthRegen()
    {
        if (Health < healthMax)
        {
            if (regenTimer < regenTimerMax)
            {
                regenTimer += Time.deltaTime;
            }
            else
            {
                Health += 2;

                UpdateHealth();

                regenTimer = 0;
            }
        }
    }

    void UpdateHealth()
    {
        healthBar.value = Health / 100;
    }

    void Death()
    {
        SceneManager.LoadScene(0);
    }
}
