using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Agent
{
    [Header("Dog")]
    public bool SmellPlayer = false;
    public float SmellRadius = 50;
    public Vector3 Position;
    public float DistanceToPosition = 0;
    public bool traveling = false;
    public ScentSphere scentSphere = null;
    public  float delay = 10;
    public Dog() : base ()
    {
        combat.PrimaryWeapon = new EmptyWeapon();
        combat.SecondaryWeapon = new EmptyWeapon();
        combat.TertiaryWeapon = new EmptyWeapon();
        brain.AbillityToSmell = true;
    }



    public override void Restart()
    {
        combat.PrimaryWeapon = new EmptyWeapon();
        combat.SecondaryWeapon = new EmptyWeapon();
        combat.TertiaryWeapon = new EmptyWeapon();
        brain.AbillityToSmell = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, SmellRadius);

        Gizmos.DrawLine(transform.position, Position);
        Gizmos.DrawCube(Position, new Vector3(1, 1, 1));
    }
    public override void Start()
    {
       
    }
    public override void Update()
    {

        float Distance = float.MaxValue;    
        DistanceToPosition = Vector3.Distance(transform.position, Position);
        MoveTo(Position);
        if (!traveling)
        {
            if (scentSphere)
            {

                if (Vector3.Distance(transform.position, Position) < StopDistance + 2)
                {
                    transform.forward = scentSphere.TravelingDirection;
                    Position = transform.position + transform.forward * 100;

                    traveling = true;

                    delay = 10;
                }
            }

        }
        else
        {
            delay -= Time.deltaTime;
            if (delay < 0)
            {
                scentSphere = null;
                traveling = false;
                delay = 10;
            }
        }
        if (!scentSphere)
        {

            foreach (var item in Physics.OverlapSphere(transform.position, SmellRadius, LayerMask.GetMask("Scent")))
            {
                if (Vector3.Distance(transform.position, item.transform.position) < Distance)
                {
                    Distance = Vector3.Distance(transform.position, item.transform.position);
                    scentSphere = item.GetComponent<ScentSphere>();
                    Position = scentSphere.transform.position;
                }

            }

        }
    }

}