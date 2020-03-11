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
        combat.PrimaryWeapon = new EmptyWeapon();
        combat.SecondaryWeapon = new EmptyWeapon();
        combat.TertiaryWeapon = new EmptyWeapon();
        combat.CurrentWeapon = combat.PrimaryWeapon;
        inventory = new Inventory();
        inventory.AddItem(combat.CurrentWeapon);
        inventory.AddItem(combat.SecondaryWeapon);
        inventory.AddItem(combat.TertiaryWeapon);

        ANPRC119 aNPRC119 = new ANPRC119();
        AIRadio = aNPRC119;
        inventory.AddItem(aNPRC119);
        inventory.MaxiumWeight = 0;
        

        AIRadio.Frequency = 7;
       


        inventory.CalculateWeight();
        brain.VisionDistance = 1000;
        brain.CombatDistance = 0;
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
