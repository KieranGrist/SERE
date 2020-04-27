using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class ScentSphere : MonoBehaviour
{
    public Vector3 TravelingDirection;
    public Vector3 TravelingLocation;
    public float Age = 120;
    private void Start()
    {
        Age = 120;
    }
    // Update is called once per frame
    void Update()
    {
        Age -= Time.deltaTime;
        if (Age < 0)
            Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5);
    }
    private void OnTriggerEnter(Collider other)
    {
        var smell = other.GetComponent<Agent>();

        if (smell)
        {
            smell.brain.Enemy = other.GetComponent<Entity>();
           
            Destroy(gameObject);
        }
    }
}
