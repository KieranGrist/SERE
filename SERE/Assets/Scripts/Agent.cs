using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : Entity
{
    float MaxSpeed = 10;
    public GameObject target;
    public Vector2 Waypoint;
    public Vector2 Velocity;



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 100);
    }
    new void Start()
    {
        base.Start();
        Waypoint = GenerateWaypoint();
    }
    // Update is called once per frame
    new  void Update()
    {
        base.Update();
        float ClosestDistance = float.MaxValue;
        target = null;
        foreach (var item in Physics.OverlapSphere(transform.position, 100,LayerMask.GetMask("Agent")))
        {
            if (item.gameObject != gameObject)
            if (Vector3.Distance(transform.position,item.transform.position) < ClosestDistance)
            {
                ClosestDistance = Vector3.Distance(transform.position, item.transform.position);
                target = item.gameObject;
            }
        }
        if (target)
        {
            transform.LookAt(target.transform);

            if (gap > BulletGap)
            {
                Fire(BulletMass, BulletInitialSpeed);
            }
        }
        Vector2 velocitySum = Vector2.zero;
        if (Vector3.Distance(transform.position, Waypoint) > 10)
        {
            velocitySum=  Seek(Waypoint);

        }
        else
             Waypoint= GenerateWaypoint();
        if (target)
        {
            velocitySum= Seek(new Vector2(target.transform.position.x, target.transform.position.z));
        }

        Velocity += velocitySum;
        Velocity = Vector2.ClampMagnitude(Velocity, MaxSpeed);
        transform.position += new Vector3(Velocity.x, 0, Velocity.y) * Time.deltaTime;
    }
    private Vector2 Seek(Vector2 targetPos)
    {

        Vector2 desiredVelocity = ((targetPos - new Vector2(transform.position.x, transform.position.z)).normalized) * 15;

        return (desiredVelocity - Velocity);
    }

    Vector2 GenerateWaypoint()
    {
        var MaxX = transform.position.x + RespawnHandler.respawn.RespawnRadius;

        var MaxZ = transform.position.z + RespawnHandler.respawn.RespawnRadius;
        var MinX = transform.position.x - RespawnHandler.respawn.RespawnRadius;

        var MinZ = transform.position.z - RespawnHandler.respawn.RespawnRadius;

        var RandX = Random.Range(MinX, MaxX);
        var RandZ = Random.Range(MinZ, MaxZ);
        return new Vector2(RandX, RandZ);
    }
}
