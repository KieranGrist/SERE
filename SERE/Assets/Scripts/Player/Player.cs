using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    public Player()
    {
        Affiliation = Side.Enemy;
        PrimaryWeapon = new L129a1();
        SecondaryWeapon = new Glock17();
        TertiaryWeapon = new L85A2();
        CurrentWeapon = PrimaryWeapon;
        inventory = new Inventory();
        inventory.AddItem(CurrentWeapon);
        inventory.AddItem(SecondaryWeapon);
        inventory.AddItem(TertiaryWeapon);
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
        CurrentWeapon.LoadPrefabs();
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
            if (PrimaryWeapon != null)
                CurrentWeapon = PrimaryWeapon;
        if (Input.GetKey(KeyCode.Alpha2))
            if (SecondaryWeapon != null) CurrentWeapon = SecondaryWeapon;
        if (Input.GetKey(KeyCode.Alpha3))
            if (TertiaryWeapon != null) CurrentWeapon = TertiaryWeapon;
        if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward * Time.deltaTime * Speed;
        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right * Time.deltaTime * Speed;
        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward * Time.deltaTime * Speed;
        if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right * Time.deltaTime * Speed;

        transform.eulerAngles += new Vector3(Sensertivity * Input.GetAxis("Mouse Y"), -Sensertivity * Input.GetAxis("Mouse X"), 0);
    }

    private void Combat()
    {
        switch (CurrentWeapon.WeaponFireRate)
        {
            case RateOfFire.Automatic:
                if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
                    CurrentWeapon.Fire(transform);
                break;
            case RateOfFire.Burst:
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(CurrentWeapon.BurstFire(transform));
                }
                break;
            case RateOfFire.Single:
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                    CurrentWeapon.Fire(transform);
                break;
            case RateOfFire.Saftey:
                break;
        }


        if (Input.GetKey(KeyCode.R))
            StartCoroutine(CurrentWeapon.Reload(inventory));
        if (Input.GetKeyDown(KeyCode.F))
            CurrentWeapon.SwitchFireRate();
    }
}