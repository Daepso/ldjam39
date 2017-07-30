using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 10;

    GameObject battery;
    BatteryPower batteryPower;

    EnemyHealth enemyHealth;

    bool batteryInRange;

    void Awake ()
    {
        battery = GameObject.FindGameObjectWithTag("Battery");
        batteryPower = battery.GetComponent<BatteryPower>();
        enemyHealth = GetComponent<EnemyHealth>();
        batteryInRange = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == battery)
        {
            batteryInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == battery)
        {
            batteryInRange = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(batteryInRange && enemyHealth.currentHealth > 0)
        {
            Sucide();
        }
	}

    void Sucide()
    {
        if (batteryPower.currentPower > 0)
        {
            batteryPower.TakeDamage(attackDamage);
            enemyHealth.Death();
        }
    }
}
