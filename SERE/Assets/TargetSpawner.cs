using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public float Radius = 50;
    public float TargetAmmount = 5;
    public int TargetsAlive;
    
    public GameObject target;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
    void SpawnTarget()
    {
        var MaxX = transform.position.x + Radius;

        var MaxZ = transform.position.z + Radius;
        var MinX = transform.position.x - Radius;

        var MinZ = transform.position.z - Radius;

        var RandX = Random.Range(MinX, MaxX);
        var RandZ = Random.Range(MinZ, MaxZ);
        var GO = Instantiate(target, new Vector3(RandX, 4, RandZ),transform.rotation);


        

    }
    private void Update()
    {
        TargetsAlive= FindObjectsOfType<Target>().Length;
        if (TargetsAlive < TargetAmmount)
        {
            SpawnTarget();
        }
    }

}
