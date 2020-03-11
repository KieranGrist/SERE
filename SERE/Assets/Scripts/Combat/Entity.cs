using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public enum Side
{
    Friendly,
    Enemy,
    Civilian
}
[System.Serializable]
public class EntityStats
{
    [Header("Stats")]
    public float Health = 100;
    public float Sensertivity = 1;
    public float Speed = 5;
}
[System.Serializable]
public class Combat
{
    [Header("Combat")]
    public Weapon PrimaryWeapon = new EmptyWeapon();
    public Weapon SecondaryWeapon = new EmptyWeapon();
    public Weapon TertiaryWeapon = new EmptyWeapon();
    public Weapon CurrentWeapon = new EmptyWeapon();
    [System.NonSerialized]
    public Weapon PreviousPrimaryWeapon = new EmptyWeapon();
    [System.NonSerialized]
    public Weapon PreviousSecondaryWeapon = new EmptyWeapon();
    [System.NonSerialized]
    public Weapon PreviousTerriaryWeapon = new EmptyWeapon();
}
[System.Serializable]
public class Entity : MonoBehaviour
{
    public Entity()
    {
        inventory = new Inventory();
        agentStats = new EntityStats();
        combat = new Combat(); 
    }
    public Side Affiliation = Side.Friendly;
    public Inventory inventory;
    public EntityStats agentStats;
    [Header("Movement")]
    public Vector3 TravelingDirection;
    public Combat combat;
    public virtual void DealDamage(float Damage)
    {
        agentStats.Health -= Damage;
    }
    public virtual void Restart()
    {
       
        agentStats.Health = 100;
        inventory = new Inventory();
        agentStats = new EntityStats();
        combat = new Combat();
        foreach (var item in inventory.inventoryItems)
        {
            item.Start();
        }
        combat.PrimaryWeapon.LoadPrefabs();
        combat.SecondaryWeapon.LoadPrefabs();
        combat.TertiaryWeapon.LoadPrefabs();
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        agentStats.Health = 100;
        foreach (var item in inventory.inventoryItems)
        {
            item.Start();
        }
      combat.PrimaryWeapon.LoadPrefabs();
        combat.SecondaryWeapon.LoadPrefabs();
        combat.TertiaryWeapon.LoadPrefabs();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        inventory.CalculateWeight();
        TravelingDirection = transform.forward;
        combat.PrimaryWeapon.UpdateGap(Time.deltaTime);
        combat.SecondaryWeapon.UpdateGap(Time.deltaTime);
        combat.TertiaryWeapon.UpdateGap(Time.deltaTime);

        if (combat.PrimaryWeapon != combat.PreviousPrimaryWeapon)
            combat.PrimaryWeapon.LoadPrefabs();
        combat.PreviousPrimaryWeapon = combat.PrimaryWeapon;

        if (combat.SecondaryWeapon != combat.PreviousSecondaryWeapon)
            combat.SecondaryWeapon.LoadPrefabs();
        combat.PreviousSecondaryWeapon = combat.SecondaryWeapon;

        if (combat.TertiaryWeapon != combat.PreviousTerriaryWeapon)
            combat.TertiaryWeapon.LoadPrefabs();
        combat.PreviousTerriaryWeapon = combat.TertiaryWeapon;
        switch(Affiliation)
        {
            case Side.Civilian:
                gameObject.layer = 12;
                break;
            case Side.Enemy:
                gameObject.layer = 10;
                break;
            case Side.Friendly:
                gameObject.layer = 11;
                break;
        }


        if (transform.position.y < -1)
        {
            agentStats.Health = 0;
        }
  
  
    }
    public void Respawn(Vector3 RespawnPosition)
    {
        transform.position = RespawnPosition;
    }



}
