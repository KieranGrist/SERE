using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Soldier : AdHOCAgent
{
    public Soldier() : base()
    {
        RootNode = new Selector(this, "Soldier Selector Node");

        Hunt_BT hunt_BT = new Hunt_BT(this, "Hunt Behaviour Tree");
        Search_BT search_BT = new Search_BT(this, "Search Behaviour Tree");
        Combat_BT combat_BT = new Combat_BT(this, "Combat Behaviour Tree");

        RootNode.AddChild(search_BT);
        RootNode.AddChild(hunt_BT);
        RootNode.AddChild(combat_BT);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, entityStats.sightRange);
    }

    public override void Restart()
    {
        WayPoints = new List<Vector3>();
        RootNode = new Selector(this, "Soldier Selector Node");

        Hunt_BT hunt_BT = new Hunt_BT(this, "Hunt Behaviour Tree");
        Search_BT search_BT = new Search_BT(this, "Search Behaviour Tree");
        Combat_BT combat_BT = new Combat_BT(this, "Combat Behaviour Tree");
        AINavAgent = GetComponent<NavMeshAgent>();
        RootNode.AddChild(search_BT);
        RootNode.AddChild(hunt_BT);
        RootNode.AddChild(combat_BT);

        base.Restart();
   //    LoadoutGenerator.GenerateRandomLoadout(this);
    }
    public override void Update()
    {
        base.Update();

        if (!Control)
        {
            SensesSystem();
                brain.HasSensedPlayer = brain.CheckSenses();


            if (brain.HasSensedPlayer == true && brain.CheckSenses() == false)
            {
                brain.Hunting = true;
                brain.Searching = false;
                brain.Combat = false;

            }
            if (brain.HasSensedPlayer == false && brain.CheckSenses() == false)
            {
                brain.Hunting = false;
                brain.Searching = true;
                brain.Combat = false;
            }

            if (brain.HasSensedPlayer == true && brain.CheckSenses() == true)
            {
                brain.Hunting = false;
                brain.Searching = false;
                brain.Combat = true;
            }
        }
    }

}
