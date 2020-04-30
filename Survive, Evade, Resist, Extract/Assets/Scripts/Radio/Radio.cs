using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

 static class AIRadioMessage<t>
{

    static void  HearMessage(Agent HearingMessage, t Data, string Message = "You need to fill this in to see it within debug")
    {
        
        HearingMessage.AIRadio.LastTransmitedMessage = Message;
        HearingMessage.AIRadio.AllHeardMessages.Add(Message);

        //Handling the AI Searching within the teams
        if (Data is Grid S)
        {

            if (HearingMessage.AgentsTeam.teamLeader == HearingMessage)
            {
                bool contains = false;

                foreach (var item in HearingMessage.GM.SearchedGrids)
                {
                    if (item.ID == S.ID)
                    {
                        contains = true;
                        break;
                    }
                }
                if (!contains)
                    HearingMessage.GM.SearchedGrids.Add(S);
            }
        }


        //Handling the AI Finding the player and informing their team
        if (Data is Entity e)
        {
  
            HearingMessage.brain.UpdateEnemy(e);
        }

        //Handling the AI Engaging the player 



    }
   public  static void Transmit(Agent From, Agent _1, Radio radio, t Data, string Message )
    {
        
        From.AIRadio.LastTransmitedMessage = Message;
        From.AIRadio.AllTransmitedMessages.Add(Message);

        foreach (var item in Physics.OverlapSphere(From.transform.position, radio.RadioTransmistionDistance))
        {
            if (item.CompareTag("HasRadio"))
            {
                var entity = item.GetComponent<Agent>();
                if (entity.AIRadio.Frequency == radio.Frequency)
                {     
                    HearMessage(entity, Data, Message);
                }
            }
        }

    }

}
[System.Serializable]
public class Radio : InventoryItem
{
    [Header("Radio")]
    public int Frequency;
    public float RadioTransmistionDistance;
    public string LastTransmitedMessage = "Nothing Transmited yet";
    public string LastHearMessage = "Nothing Heard Yet";
    public List<string> AllHeardMessages = new List<string>();
    public List<string> AllTransmitedMessages = new List<string>();
    public void TransmitSearchingGrid(Agent From, Grid grid)
    {
        var To = From.AgentsTeam.teamLeader;

        AIRadioMessage<Grid>.Transmit(From, To, this, grid, "Hello " + To.name + "I am searching grid " + grid.ID);
    }

    public void TransmitInCombat(Agent From)
    {
        var To = From.AgentsTeam.teamLeader;

        AIRadioMessage<Entity>.Transmit(From, To, this, From.brain.Enemy, "Hello " + To.name + "I am engaging the enemy " + From.brain.Enemy.name);
    }
    public void TransmitEnemySeen(Agent From)
    {
        var To = From.AgentsTeam.teamLeader;

        AIRadioMessage<Entity>.Transmit(From, To, this, From.brain.Enemy, "Hello " + To.name + "I see the enemy " + From.brain.Enemy.name);
    }
}