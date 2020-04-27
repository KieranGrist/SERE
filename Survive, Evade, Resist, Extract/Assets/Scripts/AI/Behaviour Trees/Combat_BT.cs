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
    public Combat_BT(Agent agent, string NodeName) : base(agent, NodeName)
    {
        node = new CombatDecorator(new EngageEnemy(agent, " Engage Enemy"), agent, " Combat Decorator");
    }

    /// <summary>
    /// Executes Combat Node
    /// </summary>
    /// <returns></returns>
    public override NodeStatus Execute()
    {
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
    public CombatDecorator(Node WrappedNode, Agent agent, string name) : base(WrappedNode, agent, name)
    {
        base.agent = agent;
    }

    /// <summary>
    /// Checks the status of the decorator
    /// </summary>
    /// <returns></returns>
    public override bool CheckStatus()
    {
        return agent.brain.Combat;
    }

}

/// <summary>
/// Node that engages the closest enemy to the agent
/// </summary>
class EngageEnemy : Node
{
    public EngageEnemy(Agent agent, string NodeName) : base(agent, NodeName)
    {
    }

    public override NodeStatus Execute()
    {
        agent.brain.Searching = false;
        agent.search.SearchedGrids.Clear(); //Clear the searched grids so the agent can search the same grids again if they loose the enemy 
        agent.SensesSystem(); //Checks the senses system incase the agent has lost the enemy



        if (agent.brain.Enemy)
        {       
            agent.brain.UpdateEnemyInfo(); //Updates the enemy information 

            //Handle the AI needing to switch weapons
            //Check if the current weapon has mags left 
            if (agent.combat.CheckCurrentWeaponHasMags(agent.inventory) == false)
            {
                //If it doesnt check if the other weapons have any mags left
                agent.combat.CheckWeaponHasMags(agent.inventory, 1);
                agent.combat.CheckWeaponHasMags(agent.inventory, 2);
                agent.combat.CheckWeaponHasMags(agent.inventory, 3);

                //Check if the weapon has magazines to reload, then checks if the weapon already been switched, if it has skip the other weapons. 
                bool WeaponSwitched = false;
                if (agent.combat.PrimaryWeaponHasMags && WeaponSwitched == false)
                {
                    agent.combat.SwitchCurrentWeapon(1);
                    WeaponSwitched = true;
                }
                if (agent.combat.SecondaryWeaponHasMags && WeaponSwitched == false)
                {
                    agent.combat.SwitchCurrentWeapon(2);
                    WeaponSwitched = true;
                }

                if (agent.combat.TertiaryWeaponHasMags && WeaponSwitched == false)
                {
                    agent.combat.SwitchCurrentWeapon(3);
                    WeaponSwitched = true;
                }

            }
            //Checks if the weapon needs reloading first
            if (agent.combat.CurrentWeapon.NeedsReloading())
                agent.Reload();
          
            //look at the enemy 
            agent.transform.LookAt(agent.brain.Enemy.transform);

            //Transmit on the radio that the agent is seeing the enemy and about to engage
            agent.AIRadio.TransmitEnemySeen();
            agent.AIRadio.TransmitInCombat();
            agent.WayPoints.Clear();
            agent.WayPoints.Add(new Vector3());
            //Set the waypoint to the enemys location so the agent is moving and firing
            agent.WayPoints[0] = agent.brain.Enemy.transform.position;

            //The AI will always be on Automatic rate of fire as the brain would have no way of knowing when to use what fire rate and each weapon is different
            agent.combat.CurrentWeapon.WeaponFireRate = RateOfFire.Automatic;

            //Fire the weapon
            agent.combat.CurrentWeapon.Fire(agent.transform);
        }
        else
            return NodeStatus.FAILURE;

        //If the enemy is still alive keep the combat behaviour tree running
        if (agent.brain.Enemy.entityStats.Health > 0)
            return NodeStatus.RUNNING;
        else
            return NodeStatus.SUCCESS;
    }
}