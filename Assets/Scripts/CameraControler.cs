using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour {
    public float smoothing = 5f;
    public float speed = 50f;


    Vector3 target;
    Vector3 bounds;
    Vector3 forward;
    float dist = 50f;

	// Use this for initialization
	void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Battery").transform.position;
        GameObject area = GameObject.FindGameObjectWithTag("CameraArea");
        Collider[] colliders = area.GetComponentsInChildren<Collider>();
        Bounds b = new Bounds();
        foreach (Collider c in colliders)
        {
            if (!c.isTrigger)
            {
                b.Encapsulate(c.bounds);
            }
        }
        bounds = b.extents;

        forward = transform.forward;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        target += movement;
        target.x = Mathf.Clamp(target.x, -bounds.x, bounds.x);
        target.z = Mathf.Clamp(target.z, -bounds.z, bounds.z);

        Vector3 targetCamPos = target - forward * dist;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
