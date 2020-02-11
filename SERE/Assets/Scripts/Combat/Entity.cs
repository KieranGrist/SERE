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
    public Weapon CurrentWeapon;
    Weapon PreviousWeapon;
    // Start is called before the first frame update
    public virtual void Start()
    {
        Health = 100;
        foreach (var item in FindObjectsOfType<Magazine>())
        {
            item.Start();
        }
        CurrentWeapon.LoadPrefabs();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        inventory.CalculateWeight();
  
        TravelingDirection = transform.forward;
        CurrentWeapon.UpdateGap(Time.deltaTime);
        if (CurrentWeapon != PreviousWeapon)
        {
            CurrentWeapon.LoadPrefabs();
        }
        PreviousWeapon = CurrentWeapon;
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
