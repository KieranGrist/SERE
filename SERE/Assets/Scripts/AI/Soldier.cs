using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Soldier : Agent
{
   public Soldier()
    {
        PrimaryWeapon = new L85A2();
        SecondaryWeapon = new Glock17();
        CurrentWeapon = PrimaryWeapon;
        inventory = new Inventory();
        inventory.AddItem(CurrentWeapon);
        inventory.AddItem(SecondaryWeapon);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, CombatDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, VisionDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AIRadio.RadioTransmistionDistance);

    }
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        AINavAgent = GetComponent<NavMeshAgent>();
        name = _firstNames[Random.Range(0, _firstNames.Length)];
    }

    // Update is called once per frame
  new  void  Update()
    {

        base.Update();
       
        RadioManagement();

        EndPosition = AINavAgent.pathEndPosition;


        //   AINavAgent.SetDestination(PlayersLastKnownLocation);

        if (Health < 0)
        {
            transform.position = new Vector3(0, 0, 0);
            enabled = false;
        }

        Combat();

    }

    private void RadioManagement()
    {
        if (inventory.ItemInInventory("ANPRC152")) tag = "HasRadio";
        if (SquadTransmitRadio)
        {
            AIRadio.Transmit(this, "I have taken contact at " + transform.position.ToString());
            SquadTransmitRadio = false;
        }
    }

    private void Combat()
    {

        float ClosestDistance = float.MaxValue;
        GameObject target = null;
        if (Physics.CheckSphere(transform.position, VisionDistance))
            AINavAgent.speed = 8;
        else
            AINavAgent.speed = 5;

        foreach (var item in Physics.OverlapSphere(transform.position, CombatDistance, LayerMask.GetMask("Enemy")))
        {
            if (item.gameObject != gameObject)
                if (Vector3.Distance(transform.position, item.transform.position) < ClosestDistance)
                {
                    ClosestDistance = Vector3.Distance(transform.position, item.transform.position);
                    target = item.gameObject;
                }
        }
       

        if (target)
        {
            if (CurrentWeapon.CurrentMagazine.BulletsInMag <= 0)
            {
             StartCoroutine(   CurrentWeapon.Reload(inventory));
            }
           
            transform.LookAt(target.transform);

            CurrentWeapon.Fire(transform);
        }
        Vector2 velocitySum = Vector2.zero;
    }
}
