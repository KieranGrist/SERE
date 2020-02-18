using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side
{
    Friendly,
    Enemy,
    Civilian
}
public class Entity : MonoBehaviour
{
    public Side Affiliation = Side.Friendly;
    public Inventory inventory;

    [Header("Movement")]
    public Vector3 TravelingDirection;
    [Header("Stats")]
    public float Health = 100;
    public float Sensertivity = 1;
    public float Speed = 5;
    [Header("Combat")]
    public Weapon PrimaryWeapon;
    public Weapon SecondaryWeapon;
    public Weapon TertiaryWeapon;
    public Weapon CurrentWeapon;
     Weapon PreviousPrimaryWeapon;
     Weapon PreviousSecondaryWeapon;
     Weapon PreviousTerriaryWeapon;
    public virtual void DealDamage(float Damage)
    {
        Health -= Damage;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        Health = 100;
        foreach (var item in inventory.inventoryItems)
        {
            item.Start();
        }
        PrimaryWeapon.LoadPrefabs();
        SecondaryWeapon.LoadPrefabs();
        TertiaryWeapon.LoadPrefabs();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        inventory.CalculateWeight();
  
        TravelingDirection = transform.forward;
        PrimaryWeapon.UpdateGap(Time.deltaTime);
        SecondaryWeapon.UpdateGap(Time.deltaTime);
        TertiaryWeapon.UpdateGap(Time.deltaTime);

        if (PrimaryWeapon != PreviousPrimaryWeapon)
            PrimaryWeapon.LoadPrefabs();     
        PreviousPrimaryWeapon = PrimaryWeapon;

        if (SecondaryWeapon != PreviousSecondaryWeapon)
            SecondaryWeapon.LoadPrefabs();
        PreviousSecondaryWeapon = SecondaryWeapon;

        if (TertiaryWeapon != PreviousTerriaryWeapon)
            TertiaryWeapon.LoadPrefabs();
        PreviousTerriaryWeapon = TertiaryWeapon;
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
            Health = 0;
        }
  
  
    }
    public void Respawn(Vector3 RespawnPosition)
    {
        transform.position = RespawnPosition;
    }



}
