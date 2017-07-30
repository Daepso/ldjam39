using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public float powerValue = 5;
    public float startingHealth = 10;
    public float currentHealth;

    bool isDead;
    BatteryPower batteryPower;
        AudioSource enemyAudio;

    private void Awake()
    {
        isDead = false;
        currentHealth = startingHealth;
        GameObject battery = GameObject.FindGameObjectWithTag("Battery");
        batteryPower = battery.GetComponent<BatteryPower>();
        enemyAudio = GetComponent<AudioSource>();
    }
    
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(float amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        isDead = true;

        enemyAudio.Play();

        if (currentHealth <= 0)
            batteryPower.GainPower(powerValue);

        currentHealth = 0;

        Destroy(gameObject,0.5f);
    }
}
