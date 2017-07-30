using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour {
    public float damage = 2f;
    public float cooldown = 0.5f;
    public float range = 20f;

    float timer;
    int enemyMask;
    List<GameObject> enemyInRange;
    GameObject target;
    Ray shootRay = new Ray();
    RaycastHit shootHit;

    //Effects
    AudioSource gunAudio;
    Light gunLight;
    LineRenderer gunLine;
    float effectsDisplayTime = 0.2f;

    // Use this for initialization
    void Awake ()
    {
        enemyMask = LayerMask.GetMask("Enemy");
        enemyInRange = new List<GameObject>();
        GetComponent<SphereCollider>().radius = range;
        gunLight = GetComponent<Light>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (enemyMask == (enemyMask | (1 << other.gameObject.layer)))
        {
            enemyInRange.Add(other.gameObject);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (enemyMask == (enemyMask | (1 << other.gameObject.layer)))
        {
            enemyInRange.Remove(other.gameObject);
            if (target == other.gameObject)
                target = null;
        }
    }
    
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (target == null || !ValidTarget(target))
        {
            ChooseTarget();
        }
        if (target != null)
        {
            if (timer >= cooldown)
            {
                Shoot();
            }
        }
        if (timer >= cooldown * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    void ChooseTarget()
    {
        target = null;
        if (enemyInRange.Count == 0)
        {
            return;
        }

        enemyInRange.RemoveAll(item => item == null);

        float minDist = float.MaxValue;
        foreach (GameObject o in enemyInRange)
        {
            if (ValidTarget(o))
            {
                float dist = (o.transform.position - transform.position).sqrMagnitude;
                if (minDist > dist)
                {
                    target = o;
                    minDist = dist;
                }
            }
        }
    }

    bool ValidTarget(GameObject o)
    {
        EnemyHealth health = o.GetComponent<EnemyHealth>();
        if (health.currentHealth <= 0)
        {
            return false;
        }
        return true;
    }

    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        shootRay.origin = transform.position;
        shootRay.direction = target.transform.position - transform.position;

        gunLight.enabled = true;

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        if (Physics.Raycast(shootRay, out shootHit, range, enemyMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}
