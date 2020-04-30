﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Soldier : Agent
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

    public override void Restart()
    {
        RootNode = new Selector(this, "Soldier Selector Node");

        Hunt_BT hunt_BT = new Hunt_BT(this, "Hunt Behaviour Tree");
        Search_BT search_BT = new Search_BT(this, "Search Behaviour Tree");
        Combat_BT combat_BT = new Combat_BT(this, "Combat Behaviour Tree");

        RootNode.AddChild(search_BT);
        RootNode.AddChild(hunt_BT);
        RootNode.AddChild(combat_BT);

        base.Restart();

    }
    public override void Update()
    {
        base.Update();

        if (!Control)
        {
            if (brain.HasSensedPlayer == false)
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
