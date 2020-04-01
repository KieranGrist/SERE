using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DogCalculateSearchPatern : Node
{
    Dog agent;
    public DogCalculateSearchPatern(Dog Bt, string name) : base(Bt, name)
    {
        agent = Bt;
        NodeName = name;
    }


    public override NodeStatus Execute()
    {
        var NumberOfSearchPoints = agent.search.SearchPoints.Count;
        float Degress = 0;
        var Increase = 360 / NumberOfSearchPoints;
        agent.search.SearchDistance = 1000;
        agent.search.SearchInsideCicle = true;


        for (int i = 0; i < NumberOfSearchPoints; i++)
        {

            agent.search.SearchPoints[i].transform.eulerAngles = new Vector3(0, Degress, 0);
            agent.search.SearchPoints[i].transform.position =new Vector3(2000,0,2000);


            if (agent.search.SearchInsideCicle)
                agent.search.SearchPoints[i].transform.position += agent.search.SearchPoints[i].transform.forward * Random.Range(1, agent.search.SearchDistance);
            else
                agent.search.SearchPoints[i].transform.position += agent.search.SearchPoints[i].transform.forward * agent.search.SearchDistance;

            Vector3 pos = agent.search.SearchPoints[i].transform.position;
            pos.y = Terrain.activeTerrain.SampleHeight(agent.search.SearchPoints[i].transform.position);
            agent.search.SearchPoints[i].transform.position = pos;
            Degress += Increase;
        }
        return NodeStatus.SUCCESS;
    }
}
[System.Serializable]
public class DogExecuteSearch : Node
{
    Dog agent;
    int i = 0;
    public DogExecuteSearch(Dog Bt, string name) : base(Bt, name)
    {
        agent = Bt;
        NodeName = name;
    }
    public override NodeStatus Execute()
    {

        BT_Move bT_Move = new BT_Move(agent, "Move");

        agent.MoveToLocation = agent.search.SearchPoints[i].transform.position;


        if (agent.SmellPlayer)
        {
            agent.search.Searching = false;
            agent.search.SearchedGrids.Clear();
            AIRadioMessage<BrainInformation> SearchMessage = new AIRadioMessage<BrainInformation>();
            SearchMessage.Transmit(agent, agent.AgentsTeam.teamLeader, agent.AIRadio, agent.brain, "Hello I can sense the player" + agent.brain.PlayersLastKnownLocation);
            return NodeStatus.SUCCESS;

        }


        if (bT_Move.Execute() == NodeStatus.SUCCESS)
        {

            i++;
            if (i == 29)
            {
                i = 0;
                agent.search.OrderedSearch = false;
                agent.search.SearchInsideCicle = false;
                return NodeStatus.SUCCESS;
            }

            return NodeStatus.RUNNING;
        }
        else
            return NodeStatus.RUNNING;
    }
}
[System.Serializable]
public class DogSearch : Sequence
{
    Dog agent;
    public DogSearch(Dog Bt, string NodeName) : base(Bt, NodeName)
    {
        agent = Bt;
      this.NodeName = NodeName;
        AddChild(new DogCalculateSearchPatern(agent, "Calculate Search Pattern"));
        AddChild(new DogExecuteSearch(agent, "Execute Search"));
    }
}
[System.Serializable]
public class Hunt : Node
{
    Dog dog;
    public Hunt(Dog Bt, string NodeName) : base(Bt, NodeName)
    {
    }

    public override NodeStatus Execute()
    {
        float Distance = float.MaxValue;
      var  DistanceToPosition = Vector3.Distance(dog.transform.position, dog.MoveToLocation);



        if (!dog.scentSphere)
        {
            Entity player = null;
            Distance = float.MaxValue;
            foreach (var item in Physics.OverlapSphere(dog.transform.position, dog.SmellRadius, LayerMask.GetMask("Enemy")))
            {
                if (Vector3.Distance(dog.transform.position, item.transform.position) < Distance)
                {
                    dog.SmellPlayer = true;
                    Distance = Vector3.Distance(dog.transform.position, item.transform.position);
                    player = dog.GetComponent<Entity>();
                    dog.Position = item.transform.position;
                }
            }
            Distance = float.MaxValue;
            if (!player)
                foreach (var item in Physics.OverlapSphere(dog.transform.position, dog.SmellRadius, LayerMask.GetMask("Scent")))
                {
                    if (Vector3.Distance(dog.transform.position, item.transform.position) < Distance)
                    {
                        Distance = Vector3.Distance(dog.transform.position, item.transform.position);
                        dog.scentSphere = item.GetComponent<ScentSphere>();
                        dog.Position = dog.scentSphere.transform.position;
                    }

                }

        } 
        else
        {

            if (Vector3.Distance(dog.transform.position, dog.Position) < dog.StopDistance + 2)
            {
                dog.transform.forward = dog.scentSphere.TravelingDirection;
                dog.Position = dog.transform.position + dog.transform.forward * 100;
            }
        }
        dog.MoveTo(dog.Position);
        return NodeStatus.SUCCESS;
    }
}
[System.Serializable]
public class DogChase : Node
{
    Dog dog;
    public DogChase(Dog Bt, string NodeName) : base(Bt, NodeName)
    {
        dog = Bt;
        this.NodeName = NodeName;
    }

    public override NodeStatus Execute()
    {

        dog.MoveTo(dog.Position);
        return NodeStatus.SUCCESS;
    }
}
[System.Serializable]
public class DogHuntDecorator : ConditionalDecorator
{
    Dog dog;
    public DogHuntDecorator(Node WrappedNode, Dog bb, string name) : base(WrappedNode, bb, name)
    {
        dog = bb;
    }

    public override bool CheckStatus()
    {
        return dog.SmellPlayer;
    }
}
[System.Serializable]
public class DogSearchDecorator : ConditionalDecorator
{
    Dog dog;
    public DogSearchDecorator(Node WrappedNode, Dog bb, string name) : base(WrappedNode, bb, name)
    {
        dog = bb;
    }

    public override bool CheckStatus()
    {
        return !dog.SmellPlayer;
    }
}
[System.Serializable]
public class BT_Dog : Selector
{
    Dog dog;
    public BT_Dog(Dog Bt, string name) : base(Bt, name)
    {
        DogSearch dogSearch = new DogSearch(Bt, "Search For Player");
        Hunt HuntNode = new Hunt(Bt, "Hunt For Player");

        DogHuntDecorator huntDecorator = new DogHuntDecorator(HuntNode, Bt, "Hunt Decorator");
        DogSearchDecorator searchDecorator = new DogSearchDecorator(dogSearch, Bt, "Search Decorator ");
        AddChild(huntDecorator);
        AddChild(searchDecorator);
        dog = Bt;

    }

    public override NodeStatus Execute()
    {

        return base.Execute();
    }
}
