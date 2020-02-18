using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Player()
    {
        PrimaryWeapon = new L85A2();
        SecondaryWeapon = new Glock17();
        CurrentWeapon = new L85A2();
        inventory = new Inventory();
        inventory.AddItem(CurrentWeapon);
        inventory.AddItem(new Glock17());
        for (int i = 0; i < 4; i++)

            inventory.AddItem(new NATO30TracerMag());



        for (int i = 0; i < 4; i++)

            inventory.AddItem(new NATO30StandardMag());

        for (int i = 0; i < 1; i++)

            inventory.AddItem(new NATO30NoTracerMag());


        for (int i = 0; i < 4; i++)
        {
            inventory.AddItem(new NATO17StandardMag());
            inventory.AddItem(new NATO17TracerMag());
        }

        inventory.CalculateWeight();

    }
    new void Start()
    {
        base.Start();
        CurrentWeapon.LoadPrefabs();
    }
    new void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.Alpha1))
            CurrentWeapon = PrimaryWeapon;
        if (Input.GetKey(KeyCode.Alpha2))
            CurrentWeapon = SecondaryWeapon;

            if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward * Time.deltaTime * Speed;
        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right * Time.deltaTime * Speed;
        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward * Time.deltaTime * Speed;
        if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right * Time.deltaTime * Speed;

        transform.eulerAngles += new Vector3(Sensertivity * Input.GetAxis("Mouse Y"), -Sensertivity * Input.GetAxis("Mouse X"), 0);
        if (Input.GetKey(KeyCode.UpArrow))
            transform.eulerAngles += new Vector3(Sensertivity, 0, 0);


        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)))
        {
            CurrentWeapon.Fire(transform);
        }

        if (Input.GetKey(KeyCode.R))
            CurrentWeapon.Reload(inventory);

    }
}
