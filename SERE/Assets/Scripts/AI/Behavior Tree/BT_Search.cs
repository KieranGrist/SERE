using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class CalculateSearchLocation : Node
{
    Agent agent;

    public CalculateSearchLocation(Agent agent) : base(agent)
    {
        this.agent = agent;
    }

    public override NodeStatus Execute()
    {
        return NodeStatus.SUCCESS;
    }
}

class CalculateSearchPatern : Node
{
    Agent agent;
    public CalculateSearchPatern(Agent Bt) : base(Bt)
    {
        agent = Bt;
    }

    public override NodeStatus Execute()
    {
     var SearchPoints = new List<GameObject>();
        var NumberOfSearchPoints = 30;
    
       
            SearchPoints.Clear();
            for (int i = 0; i < NumberOfSearchPoints; i++)
            {
            GameObject go = new GameObject();
                SearchPoints.Add(go);
            }
        


       var SearchLocation =agent.transform.position;
        var position = SearchLocation;
        float Degress = 0;
        var Increase = 360 / NumberOfSearchPoints;
        for (int i = 0; i < NumberOfSearchPoints; i++)
        {
            if (i == NumberOfSearchPoints - 1)
                Debug.DrawLine(SearchPoints[i].transform.position, SearchPoints[0].transform.position);
            else
                Debug.DrawLine(SearchPoints[i].transform.position, SearchPoints[i + 1].transform.position);
        }


            for (int i = 0; i < NumberOfSearchPoints; i++)
            {

                SearchPoints[i].transform.eulerAngles = new Vector3(0, Degress, 0);
                SearchPoints[i].transform.position = agent.transform.position;


            if (agent.SearchInsideCicle)
                SearchPoints[i].transform.position += SearchPoints[i].transform.forward * Random.Range(1, agent.SearchDistance) + new Vector3(0, 2, 0);
                else
                    SearchPoints[i].transform.position += SearchPoints[i].transform.forward * agent.SearchDistance + new Vector3(0, 2, 0);
                SearchPoints[i].GetComponent<Renderer>().material.color = Color.red;
                Degress += Increase;


            }
      
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
