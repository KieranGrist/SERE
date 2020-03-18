using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[System.Serializable]
public class Soldier : Agent
{

   public Soldier() : base ()
    {
       combat. PrimaryWeapon = new L85A2();
        combat.SecondaryWeapon = new Glock17();
        combat.CurrentWeapon = combat.PrimaryWeapon;
        inventory.AddItem(combat.CurrentWeapon);
        inventory.AddItem(combat.SecondaryWeapon);
        ANPRC152 aNPRC152 = new ANPRC152();
        AIRadio = aNPRC152;
        inventory.AddItem(aNPRC152);
        for (int i = 0; i < 4; i++)
            inventory.AddItem(new NATO30TracerMag());
        for (int i = 0; i < 6; i++)
            inventory.AddItem(new NATO30StandardMag());
        for (int i = 0; i < 2; i++)
        {
            inventory.AddItem(new NATO17StandardMag());
            inventory.AddItem(new NATO17TracerMag());
        }

        inventory.CalculateWeight();
    }
    public virtual new void Restart()
    {
        base.Restart();
        combat.PrimaryWeapon = new L85A2();
        combat.SecondaryWeapon = new Glock17();
        combat.CurrentWeapon = combat.PrimaryWeapon;
        inventory.AddItem(combat.CurrentWeapon);
        inventory.AddItem(combat.SecondaryWeapon);
        ANPRC152 aNPRC152 = new ANPRC152();
        AIRadio = aNPRC152;
        inventory.AddItem(aNPRC152);
        for (int i = 0; i < 4; i++)
            inventory.AddItem(new NATO30TracerMag());
        for (int i = 0; i < 6; i++)
            inventory.AddItem(new NATO30StandardMag());
        for (int i = 0; i < 2; i++)
        {
            inventory.AddItem(new NATO17StandardMag());
            inventory.AddItem(new NATO17TracerMag());
        }

        inventory.CalculateWeight();

    }
    public Vector3 EndPosition;
    // Start is called before the first frame update
   public new virtual void Start()
    {
        base.Start();
        if (AIRadio != null)
            tag = "HasRadio";        
        AINavAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    public new virtual void  Update()
    {
        
        base.Update();     
 
        EndPosition = AINavAgent.pathEndPosition;
        if (AIRadio != null)
            tag = "HasRadio";

        //   AINavAgent.SetDestination(PlayersLastKnownLocation);




    }


}
