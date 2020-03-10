using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class CalculateTalkTo : Node
{
    Agent agent;
    public CalculateTalkTo(Agent Bt) : base(Bt)
    {
        agent = Bt;
    }

    public override NodeStatus Execute()
    {
        throw new System.NotImplementedException();
    }
}


public class BT_Communicate : Sequence
{
    Agent agent;
    public BT_Communicate(Agent bb) : base(bb)
    {
        agent = bb;
        AddChild(new CalculateTalkTo(agent));

    }
}
