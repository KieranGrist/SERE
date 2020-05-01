using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    int scentSphere = 0;
    public Player()
    {
    }
    public virtual void Start()
    {
        Restart();
       combat.CurrentWeapon.LoadPrefabs();  
    }
    public virtual new void Restart()
    {
        base.Restart();
        LoadoutGenerator.GenerateRandomLoadout(this);
        Control = true;
        Affiliation = Side.Enemy;
    }
  
}