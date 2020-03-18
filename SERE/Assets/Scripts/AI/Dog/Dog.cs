using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Agent
{
    public bool SmellPlayer = false;
    public Dog() : base ()
    {
        combat.PrimaryWeapon = new EmptyWeapon();
        combat.SecondaryWeapon = new EmptyWeapon();
        combat.TertiaryWeapon = new EmptyWeapon();
        brain.AbillityToSmell = true;
    }



    public override void Restart()
    {
        base.Restart();
    }

    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
    }

}