using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    public Player()
    {
        Affiliation = Side.Enemy;
        combat.PrimaryWeapon = new L129a1();
        combat.SecondaryWeapon = new Glock17();
        combat.TertiaryWeapon = new L85A2();
        combat.CurrentWeapon = combat.PrimaryWeapon;
        inventory = new Inventory();
        inventory.AddItem(combat.CurrentWeapon);
        inventory.AddItem(combat.SecondaryWeapon);
        inventory.AddItem(combat.TertiaryWeapon);
        for (int i = 0; i < 4; i++)
            inventory.AddItem(new NATO20APMag());
        for (int i = 0; i < 6; i++)
            inventory.AddItem(new NATO20HVMag());
        for (int i = 0; i < 4; i++)
            inventory.AddItem(new NATO30TracerMag());
        for (int i = 0; i < 2; i++)
        {
            inventory.AddItem(new NATO17StandardMag());
            inventory.AddItem(new NATO17TracerMag());
        }
        inventory.CalculateWeight();
    }
    public override void Start()
    {
        base.Start();
        combat.CurrentWeapon.LoadPrefabs();
    }
    public override void Restart()
    {
        base.Restart();
        combat.PrimaryWeapon = new L129a1();
        combat.SecondaryWeapon = new Glock17();
        combat.TertiaryWeapon = new L85A2();
        Affiliation = Side.Enemy; 
        combat.CurrentWeapon = combat.PrimaryWeapon;
        inventory = new Inventory();
        inventory.AddItem(combat.CurrentWeapon);
        inventory.AddItem(combat.SecondaryWeapon);
        inventory.AddItem(combat.TertiaryWeapon);

        for (int i = 0; i < 4; i++)
            inventory.AddItem(new NATO20APMag());
        for (int i = 0; i < 6; i++)
            inventory.AddItem(new NATO20HVMag());
        for (int i = 0; i < 4; i++)
            inventory.AddItem(new NATO30TracerMag());
        for (int i = 0; i < 2; i++)
        {
            inventory.AddItem(new NATO17StandardMag());
            inventory.AddItem(new NATO17TracerMag());
        }
        inventory.CalculateWeight();
        combat.CurrentWeapon.LoadPrefabs();
    }
    public override void Update()
    {
        base.Update();

        Movement();

        Combat();


    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            if (combat.PrimaryWeapon != null)
                combat.CurrentWeapon = combat.PrimaryWeapon;
        if (Input.GetKey(KeyCode.Alpha2))
            if (combat.SecondaryWeapon != null) combat.CurrentWeapon = combat.SecondaryWeapon;
        if (Input.GetKey(KeyCode.Alpha3))
            if (combat.TertiaryWeapon != null) combat.CurrentWeapon = combat.TertiaryWeapon;
        if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward * Time.deltaTime * agentStats.Speed;
        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right * Time.deltaTime * agentStats.Speed;
        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward * Time.deltaTime * agentStats.Speed;
        if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right * Time.deltaTime * agentStats.Speed;

        transform.eulerAngles += new Vector3(agentStats.Sensertivity * Input.GetAxis("Mouse Y"), -agentStats.Sensertivity * Input.GetAxis("Mouse X"), 0);
    }

    private void Combat()
    {
        switch (combat.CurrentWeapon.WeaponFireRate)
        {
            case RateOfFire.Automatic:
                if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
                    combat.CurrentWeapon.Fire(transform);
                break;
            case RateOfFire.Burst:
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(combat.CurrentWeapon.BurstFire(transform));
                }
                break;
            case RateOfFire.Single:
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                    combat.CurrentWeapon.Fire(transform);
                break;
            case RateOfFire.Saftey:
                break;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            agentStats.Speed = 9;
        }
        else
        {
            agentStats.Speed = 4;
        }

        if (Input.GetKey(KeyCode.R))
            StartCoroutine(combat.CurrentWeapon.Reload(inventory));
        if (Input.GetKeyDown(KeyCode.F))
            combat.CurrentWeapon.SwitchFireRate();
    }
}