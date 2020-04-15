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
    [Header("Entity")]
    public Side Affiliation = Side.Friendly;
    public Inventory inventory;
    [Header("Movement")]
    public Vector3 TravelingDirection;

    /// <summary>
    /// Contains all data for the entitys statistics such as health
    /// </summary>
    public EntityStats entityStats;
     


    [Header("Combat")]
    public Weapon PrimaryWeapon = new EmptyWeapon();
    public Weapon SecondaryWeapon = new EmptyWeapon();
    public Weapon TertiaryWeapon = new EmptyWeapon();
    public Weapon CurrentWeapon = new EmptyWeapon();

     Weapon PreviousPrimaryWeapon = new EmptyWeapon();
    Weapon PreviousSecondaryWeapon = new EmptyWeapon();
     Weapon PreviousTerriaryWeapon = new EmptyWeapon();
    public Entity()
    {
        inventory = new Inventory();
    }
    public virtual void OnDrawGizmos()
    {
        if (Affiliation == Side.Friendly)
            Gizmos.color = Color.blue;
        if (Affiliation == Side.Enemy)
            Gizmos.color = Color.red;
        if (Affiliation == Side.Civilian)
            Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 15);
    }
    public virtual void DealDamage(float Damage)
    {
        entityStats.Health -= Damage;
    }
    public virtual void Restart()
    {
        name = GameManager.GM.GenerateName();
        switch (Affiliation)
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

        entityStats = new EntityStats();



        inventory = new Inventory();
        PrimaryWeapon = new EmptyWeapon();
        SecondaryWeapon = new EmptyWeapon();
        TertiaryWeapon = new EmptyWeapon();
        CurrentWeapon = new EmptyWeapon();          

        PrimaryWeapon.LoadPrefabs();
        SecondaryWeapon.LoadPrefabs();
        TertiaryWeapon.LoadPrefabs();
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        Restart();
    }

    public void Sprint()
    {
        if (entityStats.Stamina > 0)
        {
            entityStats.Stamina--;
            entityStats.CurrentSpeed *= 2;
        }
        else
        {
            entityStats.CurrentSpeed = entityStats.DefaultSpeed;
        }
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
        switch (Affiliation)
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
            entityStats.Health = 0;
        }


    }
    public void Respawn(Vector3 RespawnPosition)
    {
        transform.position = RespawnPosition;
    }


}
