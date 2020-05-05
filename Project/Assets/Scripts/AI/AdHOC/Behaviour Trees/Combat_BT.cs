using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Combat_BT : Node
{
    /// <summary>
    /// Node to execute
    /// </summary>
    public Node node;

    /// <summary>
    /// Creates a new Combat Behaviour Tree
    /// </summary>
    /// <param name="agent"></param>
    /// <param name="NodeName"></param>
    public Combat_BT(AdHOCAgent agent, string NodeName) : base(agent, NodeName)
    {
        node = new CombatDecorator(new EngageEnemy(agent, " Engage Enemy"), agent, " Combat Decorator");
    }

    /// <summary>
    /// Executes Combat Node
    /// </summary>
    /// <returns></returns>
    public override NodeStatus Execute()
    {
        agent.CurrentExecutingNode = this;
        return node.Execute();
    }
}
/// <summary>
/// Class to check if the agent is in combat or not
/// </summary>
class CombatDecorator : ConditionalDecorator
{
    /// <summary>
    /// Creates a new combat decorator
    /// </summary>
    /// <param name="WrappedNode"></param>
    /// <param name="agent"></param>
    /// <param name="name"></param>
    public CombatDecorator(Node WrappedNode, AdHOCAgent agent, string name) : base(WrappedNode, agent, name)
    {
        base.agent = agent;
    }

    /// <summary>
    /// Checks the status of the decorator
    /// </summary>
    /// <returns></returns>
    public override bool CheckStatus()
    {
        agent.CurrentExecutingNode = this;
        return agent.brain.Combat;
    }

}

/// <summary>
/// Node that engages the closest enemy to the agent
/// </summary>
class EngageEnemy : Node
{
    public EngageEnemy(AdHOCAgent agent, string NodeName) : base(agent, NodeName)
    {
    }

    public override NodeStatus Execute()
    {
        agent.CurrentExecutingNode = this;
        agent.brain.Searching = false;
  
        agent.SensesSystem(); //Checks the senses system incase the agent has lost the enemy



        if (agent.Enemy)
        {

            agent.l85A2.CurrentMagazine = new InfiniteMagazine();
            
            //Handle the AI needing to switch weapons
            //Check if the current weapon has mags left 


            //look at the enemy 
            agent.transform.LookAt(agent.Enemy.transform);

            //Transmit on the radio that the agent is seeing the enemy and about to engage

            agent.WayPoints.Clear();
            agent.WayPoints.Add(agent.Enemy.transform.position);
       

            //The AI will always be on Automatic rate of fire as the brain would have no way of knowing when to use what fire rate and each weapon is different
            agent.l85A2.WeaponFireRate = RateOfFire.Automatic;

            //Fire the weapon
            Vector3 ShootRotation;

            Vector3 InFrontOfEnemy = agent.Enemy.transform.position + agent.Enemy.transform.forward * 1;

            agent.l85A2.LoadPrefabs();

            ShootRotation = (InFrontOfEnemy - agent.transform.position).normalized;
          agent.l85A2.Fire(agent.transform, ShootRotation);
        }
        else
            return NodeStatus.FAILURE;

        //If the enemy is still alive keep the combat behaviour tree running
        if (agent.Enemy.entityStats.Health > 0)
            return NodeStatus.RUNNING;
        else
            return NodeStatus.SUCCESS;
    }
}