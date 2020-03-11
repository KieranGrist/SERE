using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class AIRadioMessage<t>
{

    void HearMessage(Agent HearingMessage, t Data)
    {
        var S = Data as Search;
        if (S != null)
        {
            if (HearingMessage.AgentsTeam.TeamLeader == HearingMessage)
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
        var B = Data as BrainInformation;
        if (B != null)
        {
            HearingMessage.AgentsTeam.SearchedGrids.Clear();
            HearingMessage.search.Searching = false;
            HearingMessage.search.SearchedGrids.Clear();
            HearingMessage.brain.PlayersLastKnownLocation = B.PlayersLastKnownLocation;
            HearingMessage.brain.PlayersTravelingDirection = B.PlayersTravelingDirection;
        }

    }
    public void Transmit(Agent From, Agent To, Radio radio, t Data, string Message = "Hello how are you")
    {
        foreach (var item in Physics.OverlapSphere(From.transform.position, radio.RadioTransmistionDistance))
        {
            if (item.tag == "HasRadio")
            {
                var entity = item.GetComponent<Agent>();
                if (entity.AIRadio.Frequency == radio.Frequency)
                {     
                    HearMessage(entity, Data);
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
    public string Message;
}
