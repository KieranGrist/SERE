using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Chase : Node
{
    Agent agent;
    public BT_Chase(Agent Bt) : base(Bt)
    {
        agent = Bt;
    }

    public override NodeStatus Execute()
    {
     var Target =   agent.PerceptionSystem();
        agent.search.Searching = false;
        agent.search.SearchedGrids.Clear();
        if ( Target)
        {
            agent.MoveToLocation = Target.transform.position;
            BT_Move bT_Move = new BT_Move(agent);
            AIRadioMessage<BrainInformation> SearchMessage = new AIRadioMessage<BrainInformation>();
            SearchMessage.Transmit(agent, agent.AgentsTeam.TeamLeader, agent.AIRadio, agent.brain, "Hello I can see the player" + agent.brain.PlayersLastKnownLocation);
            bT_Move.Execute();
            if (Vector3.Distance(agent.transform.position, agent.brain.PlayersLastKnownLocation) < agent.brain.CombatDistance)
                return NodeStatus.RUNNING;
            else
                return NodeStatus.FAILURE;
        }
        else
        {
            agent.MoveToLocation = agent.brain.PlayersLastKnownLocation;
            if (Vector3.Distance(agent.transform.position, agent.brain.PlayersLastKnownLocation) < agent.search.SearchDistance)
            {
                agent.search.OrderedSearch = true;
                agent.search.SearchLocation = agent.brain.PlayersLastKnownLocation;
                agent.search.SearchInsideCicle = true;
                agent.search.Searching = true;
            }
        }
        return NodeStatus.FAILURE;
    }
}

public class ChaseDecorator : ConditionalDecorator
{
    Agent agent;
    public ChaseDecorator(Node WrappedNode, Agent bb) : base(WrappedNode, bb)
    {
        agent = bb;
    }

    public override bool CheckStatus()
    {
        if (agent.search.OrderedSearch)
        {
            return false;
        }
        return Vector3.Distance(agent.transform.position, agent.brain.PlayersLastKnownLocation) > agent.brain.CombatDistance;
    }
}