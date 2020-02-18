using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

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
    public Weapon TerriaryWeapon;
    public Weapon CurrentWeapon;
     Weapon PreviousPrimaryWeapon;
     Weapon PreviousSecondaryWeapon;
     Weapon PreviousTerriaryWeapon;
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
        TerriaryWeapon.LoadPrefabs();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        inventory.CalculateWeight();
  
        TravelingDirection = transform.forward;
        PrimaryWeapon.UpdateGap(Time.deltaTime);
        SecondaryWeapon.UpdateGap(Time.deltaTime);
        TerriaryWeapon.UpdateGap(Time.deltaTime);

        if (PrimaryWeapon != PreviousPrimaryWeapon)
            PrimaryWeapon.LoadPrefabs();     
        PreviousPrimaryWeapon = PrimaryWeapon;

        if (SecondaryWeapon != PreviousSecondaryWeapon)
            SecondaryWeapon.LoadPrefabs();
        PreviousSecondaryWeapon = SecondaryWeapon;

        if (TerriaryWeapon != PreviousTerriaryWeapon)
            TerriaryWeapon.LoadPrefabs();
        PreviousTerriaryWeapon = TerriaryWeapon;



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
