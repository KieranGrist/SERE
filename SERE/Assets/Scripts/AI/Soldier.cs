using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Soldier : Agent
{
    public Vector3 EndPosition;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, CombatDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, VisionDistance);


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
        EndPosition = AINavAgent.pathEndPosition;
        base.Update();
        if (FindObjectOfType<Target>())
            PlayersLastKnownLocation = FindObjectOfType<Target>().transform.position;
        AINavAgent.SetDestination(PlayersLastKnownLocation);

        if (Health < 0)
        {
            transform.position = new Vector3(0, 0, 0);
            enabled = false;
        }

        float ClosestDistance = float.MaxValue;
       GameObject target = null;


       if (Physics.CheckSphere(transform.position, VisionDistance))
        AINavAgent.speed = 8;
       else
                    AINavAgent.speed = 5;
        
        foreach (var item in Physics.OverlapSphere(transform.position, CombatDistance, LayerMask.GetMask("Player")))
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
