using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
public static class LoadoutGenerator
{

    public static void GenerateRandomLoadout(Entity agent)
    {
        var LoadoutList = new List<Loadout>
        {
            new Rifleman(),
            new MachineGunner(),
            new Marksman()
        };

        LoadoutList[Random.Range(0, LoadoutList.Count)].GenerateLoadout(agent);
    }
    public static void GenerateRandomLoadout(LearningSoldier agent)
    {
        var LoadoutList = new List<Loadout>
        {
            new Rifleman(),
            new MachineGunner(),
            new Marksman()
        };


        LoadoutList[Random.Range(0, LoadoutList.Count)].GenerateLoadout(agent);
        agent.combat.PrimaryWeapon.LoadPrefabs();
        agent.combat.SecondaryWeapon.LoadPrefabs();
        agent.inventory.CalculateWeight();
    }
}

abstract class Loadout
{
    public abstract void GenerateLoadout(Entity agent);
    public abstract void GenerateLoadout(LearningSoldier agent);
}
class Rifleman : Loadout
{
    public override void GenerateLoadout(Entity agent)
    {
        agent.combat.PrimaryWeapon = new L85A2();
        agent.combat.SecondaryWeapon = new Glock17();
        agent.combat.CurrentWeapon = agent.combat.PrimaryWeapon;
        agent.inventory.AddItem(agent.combat.CurrentWeapon);
        agent.inventory.AddItem(agent.combat.SecondaryWeapon);

        for (int i = 0; i < 4; i++)
            agent.inventory.AddItem(new NATO30TracerMag());
        for (int i = 0; i < 6; i++)
            agent.inventory.AddItem(new NATO30StandardMag());
        for (int i = 0; i < 2; i++)
        {
            agent.inventory.AddItem(new NATO17StandardMag());
            agent.inventory.AddItem(new NATO17TracerMag());
        }

        agent.inventory.CalculateWeight();
    }

    public override void GenerateLoadout(LearningSoldier agent)
    {
        agent.combat.PrimaryWeapon = new L85A2();
        agent.combat.SecondaryWeapon = new Glock17();
        agent.combat.CurrentWeapon = agent.combat.PrimaryWeapon;
        agent.inventory.AddItem(agent.combat.CurrentWeapon);
        agent.inventory.AddItem(agent.combat.SecondaryWeapon);

        for (int i = 0; i < 4; i++)
            agent.inventory.AddItem(new NATO30TracerMag());
        for (int i = 0; i < 6; i++)
            agent.inventory.AddItem(new NATO30StandardMag());
        for (int i = 0; i < 2; i++)
        {
            agent.inventory.AddItem(new NATO17StandardMag());
            agent.inventory.AddItem(new NATO17TracerMag());
        }

        agent.inventory.CalculateWeight();
    }
}
class Marksman : Loadout
{
    public override void GenerateLoadout(Entity agent)
    {
        agent.combat.PrimaryWeapon = new L129a1();
        agent.combat.SecondaryWeapon = new Glock17();

        agent.combat.CurrentWeapon = agent.combat.PrimaryWeapon;
        agent.inventory.AddItem(agent.combat.CurrentWeapon);
        agent.inventory.AddItem(agent.combat.SecondaryWeapon);

        for (int i = 0; i < 40; i++)
            agent.inventory.AddItem(new NATO20APMag());
        for (int i = 0; i < 6; i++)
            agent.inventory.AddItem(new NATO20HVMag());
        for (int i = 0; i < 4; i++)
            agent.inventory.AddItem(new NATO30TracerMag());
        for (int i = 0; i < 2; i++)
        {
            agent.inventory.AddItem(new NATO17StandardMag());
            agent.inventory.AddItem(new NATO17TracerMag());
        }
        agent.inventory.CalculateWeight();
    }

    public override void GenerateLoadout(LearningSoldier agent)
    {
        agent.combat.PrimaryWeapon = new L129a1();
        agent.combat.SecondaryWeapon = new Glock17();

        agent.combat.CurrentWeapon = agent.combat.PrimaryWeapon;
        agent.inventory.AddItem(agent.combat.CurrentWeapon);
        agent.inventory.AddItem(agent.combat.SecondaryWeapon);


        for (int i = 0; i < 40; i++)
            agent.inventory.AddItem(new NATO20APMag());
        for (int i = 0; i < 6; i++)
            agent.inventory.AddItem(new NATO20HVMag());
        for (int i = 0; i < 4; i++)
            agent.inventory.AddItem(new NATO30TracerMag());
        for (int i = 0; i < 2; i++)
        {
            agent.inventory.AddItem(new NATO17StandardMag());
            agent.inventory.AddItem(new NATO17TracerMag());
        }
        agent.inventory.CalculateWeight();

  }
}
class MachineGunner : Loadout
{
    public override void GenerateLoadout(Entity agent)
    {
        agent.combat.PrimaryWeapon = new L7A2();
        agent.combat.SecondaryWeapon = new Glock17();
        agent.inventory = new Inventory();
        agent.inventory.AddItem(agent.combat.PrimaryWeapon);
        agent.inventory.AddItem(agent.combat.SecondaryWeapon);
        for (int i = 0; i < 4; i++)
            agent.inventory.AddItem(new NATO200BoxMagTracer());
        for (int i = 0; i < 2; i++)
        {
            agent.inventory.AddItem(new NATO17StandardMag());
            agent.inventory.AddItem(new NATO17TracerMag());
        }
        agent.combat.CurrentWeapon = agent.combat.PrimaryWeapon;
        agent.inventory.CalculateWeight();
    }

    public override void GenerateLoadout(LearningSoldier agent)
    {
        agent.combat.PrimaryWeapon = new L7A2();
        agent.combat.SecondaryWeapon = new Glock17();
        agent.inventory = new Inventory();
        agent.inventory.AddItem(agent.combat.PrimaryWeapon);
        agent.inventory.AddItem(agent.combat.SecondaryWeapon);
        for (int i = 0; i < 4; i++)
            agent.inventory.AddItem(new NATO200BoxMagTracer());
        for (int i = 0; i < 2; i++)
        {
            agent.inventory.AddItem(new NATO17StandardMag());
            agent.inventory.AddItem(new NATO17TracerMag());
        }
        agent.combat.CurrentWeapon = agent.combat.PrimaryWeapon;
        agent.inventory.CalculateWeight();
    }
}

