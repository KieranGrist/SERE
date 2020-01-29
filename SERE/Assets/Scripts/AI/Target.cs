using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Target : Entity
{
    NavMeshAgent Target_agent;
    new void Start()
    {
        base.Start();
        Target_agent = GetComponent<NavMeshAgent>();
  
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        Target_agent.SetDestination(GameManager.GM.ExtractionLocation);

        float ClosestDistance = float.MaxValue;
        GameObject target = null;
        foreach (var item in Physics.OverlapSphere(transform.position, 100, LayerMask.GetMask("Agent")))
        {
            if (item.gameObject != gameObject)
                if (Vector3.Distance(transform.position, item.transform.position) < ClosestDistance)
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

    }
}
