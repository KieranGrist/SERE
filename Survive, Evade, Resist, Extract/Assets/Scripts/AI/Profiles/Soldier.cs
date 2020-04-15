using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Agent
{
    public override void Restart()
    {
        base.Restart();
        Selector root = new Selector(this, "Root Node");
        RootNode = root;
        Hunt_BT hunt_BT = new Hunt_BT(this, "Hunt Behaviour Tree");
        root.AddChild(hunt_BT);

    }
    public override void Start()
    {
        Restart();
    }


    public override void Update()
    {
        base.Update();

        brain.WhatIAmDoing = "I am checking my senses";
        if (brain.HasSensedPlayer == false)           
          brain.HasSensedPlayer = brain.CheckSenses();


        if (brain.HasSensedPlayer == true && brain.CheckSenses() == false)
        {
            brain.Hunting = true;
            brain.Searching = false;
            brain.Combat = false;

        }
    }

}
