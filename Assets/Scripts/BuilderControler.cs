using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class BuilderControler : MonoBehaviour
{
    GameObject construction;

    float camRayLength = 100f;
    int floorMask;
    Vector3 halfExtents;
    int unbuildableMask;
    BatteryPower batteryPower;
    float cost;

    // Use this for initialization
    void Awake ()
    {
        floorMask = LayerMask.GetMask("Floor");
        unbuildableMask = LayerMask.GetMask("Enemy","Ally");
        GameObject battery = GameObject.FindGameObjectWithTag("Battery");
        batteryPower = battery.GetComponent<BatteryPower>();
    }

    public void SetCost(float c)
    {
        cost = c;
    }

    public void SetConstruction(GameObject construc)
    {
        construction = construc;
        GameObject dummy = Instantiate(construction, Vector3.zero, Quaternion.identity);
        Collider[] colliders = dummy.GetComponentsInChildren<Collider>();
        Bounds b = new Bounds();
        foreach (Collider c in colliders)
        {
            if (!c.isTrigger)
            {
                b.Encapsulate(c.bounds);
            }
        }
        halfExtents = b.extents;
        Destroy(dummy);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (construction != null && Input.GetButton("Fire1") && !EventSystem.current.IsPointerOverGameObject())
        {
            Construct();
        }
    }

    void Construct()
    {
        if(batteryPower.currentPower <= cost)
        {
            return;
        }

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            if(ValidEmplacement(floorHit.point))
            {
                Instantiate(construction, floorHit.point, Quaternion.identity);
                batteryPower.TakeDamage(cost);
            }
        }
        else
        {
            print("Error ray");
        }
    }

    bool ValidEmplacement(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapBox(position, halfExtents, Quaternion.identity, unbuildableMask, QueryTriggerInteraction.Ignore);
        foreach(Collider other in hitColliders)
        {
            if (unbuildableMask == (unbuildableMask | (1 << other.gameObject.layer)))
            {
                return false;
            }
        }
        return true;
    }
}
