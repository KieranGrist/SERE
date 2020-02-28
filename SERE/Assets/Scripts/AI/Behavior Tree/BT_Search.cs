using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CalculateSearchLocations : Node
{
    Agent agent;
    public CalculateSearchLocations(Agent Bt) : base(Bt)
    {
        agent = Bt;
    }

    public override NodeStatus Execute()
    {
        var position = agent.SearchLocation;
          
        float Degress =0; 
   position =         new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        position = position.normalized;
        position *= agent.SearchDistance;
        position += agent.SearchLocation;
            return NodeStatus.SUCCESS;
    }
}
public class BT_Search :Sequence
{
    bool FoundPlayer;
    Agent agent;
    List<Transform> SearchLocations = new List<Transform>();
    


public BT_Search(Agent bb) : base(bb)
    {
        agent = bb;
    }
}
