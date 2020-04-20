using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Search_BT : MonoBehaviour
{

}

class SearchDecorator : ConditionalDecorator
{
    public SearchDecorator(Node WrappedNode, Agent agent, string name) : base(WrappedNode, agent, name)
    {
        base.agent = agent;
    }

    public override bool CheckStatus()
    {
        return agent.brain.Searching;
    }
}


class CalculateSearchPattern : Node
{
    public CalculateSearchPattern(Agent agent, string NodeName) : base(agent, NodeName)
    {
    }

    public override NodeStatus Execute()
    {
        agent.brain.Searching = true;
        var WaypointsNeeded = 30;
        var Angle = 0;
        var IncreaseAngle = 360 / WaypointsNeeded;
        for (int i = 0; i < WaypointsNeeded; i++)
        {
            var EndPoint = agent.transform.position;
            var EulerAngle = new Vector3(0, Angle, 0);

            float Pitch = EulerAngle.x * Mathf.Deg2Rad; //Set pitch to be euler angle x in radians
            float Yaw = EulerAngle.y * Mathf.Deg2Rad; //Set yaw to be euler angle y in radians
            Vector3 RV = new Vector3
            {

                z = Mathf.Cos(Yaw) * Mathf.Cos(Pitch), //z = cos of yaw * cos of pitch
                y = Mathf.Sin(Pitch), //Y = sin of pitch
                x = Mathf.Cos(Pitch) * Mathf.Sin(Yaw) //x = cos of pitch * sin of yaw
            };


            EndPoint += RV * agent.search.SearchDistance;
         //   agent.WayPoints.Add(EndPoint);
            Angle += IncreaseAngle;
            if (i == WaypointsNeeded - 1)
            {
         //       agent.WayPoints.Add(agent.WayPoints[0]);       
            }
        }
        agent.brain.Searching = true;
        return NodeStatus.SUCCESS;
    }

}


