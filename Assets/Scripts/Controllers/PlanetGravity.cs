using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    
    Rigidbody rigidbody;
    HashSet<Rigidbody> _objects = new HashSet<Rigidbody>();
    Vector3 directToPlanet;
    float distance;
    float strangth;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        foreach(Rigidbody body in _objects)
        {
            directToPlanet = (transform.position - body.position).normalized;
            distance = (transform.position - body.position).magnitude;
            strangth = body.mass * rigidbody.mass / distance;
            body.AddForce(directToPlanet * strangth);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            _objects.Add(other.attachedRigidbody);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody)
        {
            _objects.Remove(other.attachedRigidbody);
        }
    }
}
