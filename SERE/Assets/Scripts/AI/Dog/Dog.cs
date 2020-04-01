using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dog : Agent
{
    [Header("Dog")]
    public bool SmellPlayer = false;
    public float SmellRadius = 50;
    public Vector3 Position;
    public ScentSphere scentSphere = null;
    public BT_Dog DogAIAgent;
    public  float delay = 10;
    public Dog() : base ()
    {
        DogAIAgent = new BT_Dog(this, "Dog Controller");
        combat.PrimaryWeapon = new EmptyWeapon();
        combat.SecondaryWeapon = new EmptyWeapon();
        combat.TertiaryWeapon = new EmptyWeapon();
        brain.AbillityToSmell = true;
    }



    public override void Restart()
    {
        base.Restart();
        DogAIAgent = new BT_Dog(this, "Dog Controller");
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
        base.Start();
        DogAIAgent = new BT_Dog(this, "Dog Controller");
        RootNode = DogAIAgent;
    }
    public override void Update()
    {

        DogAIAgent.Execute();

        Entity player = null;
        SmellPlayer = false;
     var   Distance = float.MaxValue;
        foreach (var item in Physics.OverlapSphere(transform.position, SmellRadius, LayerMask.GetMask("Enemy")))
        {
            if (Vector3.Distance(transform.position, item.transform.position) < Distance)
            {
                SmellPlayer = true;
                Distance = Vector3.Distance(transform.position, item.transform.position);
                brain.PlayersLastKnownLocation = item.transform.position;
                brain.PlayersTravelingDirection = item.transform.forward;
                player = GetComponent<Entity>();
                Position = item.transform.position;
            }
        }
    }

}