using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    Transform batteryTransform;
    BatteryPower batteryPower;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;

    private void Awake()
    {
        GameObject battery = GameObject.FindGameObjectWithTag("Battery");
        batteryTransform = battery.transform;
        batteryPower = battery.GetComponent<BatteryPower>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
	
	// Update is called once per frame
	void Update () {
        if (batteryPower.currentPower > 0 && enemyHealth.currentHealth > 0)
        {
            nav.SetDestination(batteryTransform.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
