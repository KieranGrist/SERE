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
    public L85A2 l85A2 = new L85A2();
    /// <summary>
    /// Contains all data for the entitys statistics such as health
    /// </summary>
    public EntityStats entityStats;
    public SEREArea MyArea;
    public Entity()
    {
        entityStats = new EntityStats();
        inventory = new Inventory();
        l85A2 = new L85A2 ();
    }
    public virtual void OnDrawGizmos()
    {
        if (Affiliation == Side.Friendly)
            Gizmos.color = Color.blue;
        if (Affiliation == Side.Enemy)
            Gizmos.color = Color.red;
        if (Affiliation == Side.Civilian)
            Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 2);
    }
    public virtual void DealDamage(float Damage)
    {
        entityStats.Health -= Damage;
    }
    public virtual void Restart()
    {
        Control = false;
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
        l85A2 = new L85A2 ();


    }
 

    // Update is called once per frame
    public virtual void Update()
    {
        l85A2.UpdateGap(Time.deltaTime);
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
        //if (Input.GetKey(KeyCode.Alpha1))
        //    if (combat.PrimaryWeapon != null)
        //        combat.SwitchCurrentWeapon(1);
        //if (Input.GetKey(KeyCode.Alpha2))
        //    if (combat.SecondaryWeapon != null)
        //        combat.SwitchCurrentWeapon(2);   
        if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward * Time.deltaTime * 5;
        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right * Time.deltaTime * 5;
        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward * Time.deltaTime * 5;
        if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right * Time.deltaTime * 5;

        transform.eulerAngles += new Vector3(1 * Input.GetAxis("Mouse Y"), -1 * Input.GetAxis("Mouse X"), 0);
    }
    private void WeaponSystems()
    {
        switch (l85A2.WeaponFireRate)
        {
            case RateOfFire.Automatic:
                if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
                    l85A2.Fire(transform, transform.forward);
                break;
            case RateOfFire.Burst:
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(l85A2.BurstFire(transform, transform.forward));
                }
                break;
            case RateOfFire.Single:
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                    l85A2.Fire(transform, transform.forward);
                break;
            case RateOfFire.Saftey:
                break;
        }


        if (Input.GetKey(KeyCode.R))
            StartCoroutine(l85A2.Reload(inventory));
        if (Input.GetKeyDown(KeyCode.F))
            l85A2.SwitchFireRate();
    }


    public void Respawn(Vector3 RespawnPosition)
    {
        transform.position = RespawnPosition;
    }


}
