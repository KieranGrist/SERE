using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class ScentSphere : MonoBehaviour
{
    public Vector3 TravelingDirection;
    public Vector3 TravelingLocation;
    public float Age = 30;
    // Start is called before the first frame update
    void Start()
    {
        Age = 30;
    }

    // Update is called once per frame
    void Update()
    {
        Age -= Time.deltaTime;
        if (Age < 0)
            Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        var smell = other.GetComponent<Agent>();
        
        if (smell&&smell.brain.AbillityToSmell)
        {
            smell.brain.PlayersLastKnownLocation = TravelingDirection;
            smell.brain.PlayersTravelingDirection = TravelingLocation;
            Destroy(gameObject);
        }
    }
}
