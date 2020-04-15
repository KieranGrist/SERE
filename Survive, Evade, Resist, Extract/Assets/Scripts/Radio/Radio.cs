using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public static class AIRadioMessage<t>
{

    static void  HearMessage(Agent HearingMessage, t Data, string Message = "You need to fill this in to see it within debug")
    {
        Debug.Log(HearingMessage.name + " Has heard the message:\n" + Message);
        HearingMessage.AIRadio.LastTransmitedMessage = Message;
        HearingMessage.AIRadio.AllHeardMessages.Add(Message);

        //Handling the AI Searching within the teams
        if (Data is SearchInformation S)
        {

            if (HearingMessage.AgentsTeam.teamLeader == HearingMessage)
            {
                bool contains = false;

                foreach (var item in HearingMessage.AgentsTeam.SearchedGrids)
                {
                    if (item.ID == S.CurrentSearchGrid.ID)
                    {
                        contains = true;
                        break;
                    }
                }
                if (!contains)
                    HearingMessage.AgentsTeam.SearchedGrids.Add(S.CurrentSearchGrid);
            }
        }


        //Handling the AI Finding the player and informing their team
        if (Data is BrainInformation)
        {
            HearingMessage.AgentsTeam.SearchedGrids.Clear();
            HearingMessage.search.Searching = false;
            HearingMessage.search.SearchedGrids.Clear();
            //     HearingMessage.brain.PlayersLastKnownLocation = B.PlayersLastKnownLocation;
            //      HearingMessage.brain.PlayersTravelingDirection = B.PlayersTravelingDirection;
        }

        //Handling the AI Engaging the player 



    }
    public static void Transmit(Agent From, Agent To, Radio radio, t Data, string Message )
    {
        Debug.Log(From.name + " is transmiting their radio" + To.name +  " is the intended reciepient" +"\n Message as follows:" + Message);
        From.AIRadio.LastTransmitedMessage = Message;
        From.AIRadio.AllTransmitedMessages.Add(Message);
        foreach (var item in Physics.OverlapSphere(From.transform.position, radio.RadioTransmistionDistance))
        {
            if (item.tag == "HasRadio")
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
    
    public void TransmitMoving()
    {

    }
    public void TransmitSearching()
    {

    }
    public void TransmitInCombat()
    {

    }
    public void TransmitEnemySeen()
    {

    }
}
