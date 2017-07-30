using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy1;
    public int enemyPerWave1 = 3;
    public int waveScale1 = 2;
    public GameObject enemy2;
    public int enemyPerWave2 = 1;
    public int waveScale2 = 1;

    public float startTime = 2f;
    public float spawnTime = 2f;

    BatteryPower batteryPower;
    Vector3 bounds;
    float border = 2f;
    Vector3 center;

    private void Awake()
    {
        GameObject battery = GameObject.FindGameObjectWithTag("Battery");
        batteryPower = battery.GetComponent<BatteryPower>();
        GameObject area = GameObject.FindGameObjectWithTag("Spawn");
        Collider[] colliders = area.GetComponentsInChildren<Collider>();
        Bounds b = new Bounds();
        foreach (Collider c in colliders)
        {
            if (!c.isTrigger)
            {
                b.Encapsulate(c.bounds);
            }
        }
        bounds = b.extents/2f;
        bounds -= Vector3.one * border;
        bounds.y = 0;

        center = area.transform.position;
        center.y = 0;
    }

    // Use this for initialization
    void Start () {
        InvokeRepeating("Spawn", startTime, spawnTime);
	}
	
	void Spawn () {
        if (batteryPower.currentPower <= 0)
            return;
        
        for(int i =0;i<enemyPerWave1;i++)
        {
            Vector3 spawnPosition = center;
            spawnPosition.x += Random.Range(-bounds.x, bounds.x);
            spawnPosition.z += Random.Range(-bounds.z, bounds.z);            

            Instantiate(enemy1, spawnPosition, Quaternion.identity);
        }
        for (int i = 0; i < enemyPerWave2; i++)
        {
            Vector3 spawnPosition = center;
            spawnPosition.x += Random.Range(-bounds.x, bounds.x);
            spawnPosition.z += Random.Range(-bounds.z, bounds.z);

            Instantiate(enemy2, spawnPosition, Quaternion.identity);
        }
        enemyPerWave1 += waveScale1;
        waveScale1 += 1;

        enemyPerWave2 += waveScale2;
        waveScale2 += 1;
    }
}
