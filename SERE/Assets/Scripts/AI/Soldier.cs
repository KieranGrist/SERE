using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Soldier : Agent
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 100);
    }
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        AINavAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
  new  void  Update()
    {
        base.Update();
        if (FindObjectOfType<Target>())
            PlayersLastKnownLocation = FindObjectOfType<Target>().transform.position;
        AINavAgent.SetDestination(PlayersLastKnownLocation);



        float ClosestDistance = float.MaxValue;
       GameObject target = null;
        foreach (var item in Physics.OverlapSphere(transform.position, 100, LayerMask.GetMask("Player")))
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
