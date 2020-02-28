using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Helicopter : Agent
{
    [Header("Helicopter Stats")]
    public GameObject ObjectToFlyTo;



    
    public Helicopter ()
    {
        PrimaryWeapon = new EmptyWeapon();
        SecondaryWeapon = new EmptyWeapon();
        TertiaryWeapon = new EmptyWeapon();
        CurrentWeapon = PrimaryWeapon;
        inventory = new Inventory();
        inventory.AddItem(CurrentWeapon);
        inventory.AddItem(SecondaryWeapon);
        inventory.AddItem(TertiaryWeapon);

        ANPRC119 aNPRC119 = new ANPRC119();
        AIRadio = aNPRC119;
        inventory.AddItem(aNPRC119);
        inventory.MaxiumWeight = 0;
        

        AIRadio.Frequency = 7;
       


        inventory.CalculateWeight();
        VisionDistance = 1000;
        CombatDistance = 0;
    }
    private new void Start()
    {
        base.Start();
        AINavAgent = GetComponent<NavMeshAgent>();
        name = "Ugly 1";
        AIRadio.Frequency = 7;
        
    }
    private new void Update()
    {
        base.Update();
     var TargetPosition =    ObjectToFlyTo.transform.position + new Vector3(0,50,0);
       



    }
}
