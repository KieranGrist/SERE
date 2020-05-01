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
    public bool Control;
    [Header("Entity")]
    public Side Affiliation = Side.Friendly;
    public Inventory inventory;
    [Header("Movement")]
    public Vector3 TravelingDirection;
    [Header("Combat")]
    public Combat combat = new Combat();

    /// <summary>
    /// Contains all data for the entitys statistics such as health
    /// </summary>
    public EntityStats entityStats;
    public ExtractionArea MyArea;




    public Entity()
    {
        entityStats = new EntityStats();
        inventory = new Inventory();
        combat = new Combat();
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
        Control = false;
        if(MyArea)
        name = MyArea.GenerateName();
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
        combat = new Combat();


    }
 

    // Update is called once per frame
    public virtual void Update()
    {
        combat.Update();
        inventory.CalculateWeight();

       
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
        if (Control)
        {
            PlayerInput();
            WeaponSystems();
            Camera.main.transform.parent = transform;
            Camera.main.transform.position = transform.position;
            Camera.main.transform.rotation = transform.rotation;
        }

    }
    private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            if (combat.PrimaryWeapon != null)
                combat.SwitchCurrentWeapon(1);
        if (Input.GetKey(KeyCode.Alpha2))
            if (combat.SecondaryWeapon != null)
                combat.SwitchCurrentWeapon(2);   
        if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward * Time.deltaTime * 5;
        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right * Time.deltaTime * 5;
        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward * Time.deltaTime * 5;
        if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right * Time.deltaTime * 5;

        transform.eulerAngles += new Vector3(entityStats.Sensertivity * Input.GetAxis("Mouse Y"), -entityStats.Sensertivity * Input.GetAxis("Mouse X"), 0);
    }
    private void WeaponSystems()
    {
        switch (combat.CurrentWeapon.WeaponFireRate)
        {
            case RateOfFire.Automatic:
                if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
                    combat.CurrentWeapon.Fire(transform, transform.forward);
                break;
            case RateOfFire.Burst:
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(combat.CurrentWeapon.BurstFire(transform, transform.forward));
                }
                break;
            case RateOfFire.Single:
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                    combat.CurrentWeapon.Fire(transform, transform.forward);
                break;
            case RateOfFire.Saftey:
                break;
        }


        if (Input.GetKey(KeyCode.R))
            StartCoroutine(combat.CurrentWeapon.Reload(inventory));
        if (Input.GetKeyDown(KeyCode.F))
            combat.CurrentWeapon.SwitchFireRate();
    }


    public void Respawn(Vector3 RespawnPosition)
    {
        transform.position = RespawnPosition;
    }


}
