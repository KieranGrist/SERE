using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Combat : Node
{
    Agent agent;
    public BT_Combat(Agent Bt) : base(Bt)
    {
        agent = Bt;
    }

    public override NodeStatus Execute()
    {
        agent.search.Searching = false;
        agent.search.SearchedGrids.Clear();
        Entity target = agent.CombatSystem();

      

        if (target)
        {

            agent.brain.PlayersLastKnownLocation = target.transform.position;
            agent.brain.PlayersTravelingDirection = target.transform.forward;
            if (agent.combat.CurrentWeapon.CurrentMagazine.BulletsInMag <= 0)
            {
                agent.Reload();
            }

            agent.transform.LookAt(target.transform);
            BT_Move bT_Move = new BT_Move(agent);
            AIRadioMessage<BrainInformation> SearchMessage = new AIRadioMessage<BrainInformation>();
            SearchMessage.Transmit(agent, agent.AgentsTeam.TeamLeader, agent.AIRadio, agent.brain, "Hello I can see the player" + agent.brain.PlayersLastKnownLocation);
            agent.MoveToLocation = agent.brain.PlayersLastKnownLocation;
            agent.combat.CurrentWeapon.WeaponFireRate = RateOfFire.Single;
            agent.combat.CurrentWeapon.Fire(agent.transform);
        }

        if (!target)
        {
            agent.search.Searching = true;
            return NodeStatus.FAILURE;
            
        }
        if (target.agentStats.Health > 0)
            return NodeStatus.RUNNING;
        else
            return NodeStatus.SUCCESS;
    }

}
public class CombatDecorator : ConditionalDecorator
{
    Agent agent;
    public CombatDecorator(Node WrappedNode, Agent bb) : base(WrappedNode, bb)
    {
        agent = bb;
    }

    public override bool CheckStatus()
    {
        return Vector3.Distance(agent.transform.position, agent.brain.PlayersLastKnownLocation) < agent.brain.CombatDistance;
    }
}