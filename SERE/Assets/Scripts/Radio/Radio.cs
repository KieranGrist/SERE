using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public  class Radio : InventoryItem
{
    [Header("Radio")]
    public int Frequency;
    public float RadioTransmistionDistance;
    public string Message;



     void HearMessage(Entity HearingMessage,string Message)
    {
        Debug.Log(HearingMessage.name + " Has Heard Radio Message: " + Message);
    }
    public void Transmit(Agent From, string message)
    {
        Message = message;
        Debug.Log(Message);
        foreach (var item in Physics.OverlapSphere(From.transform.position, RadioTransmistionDistance))
        {
            if (item.tag == "HasRadio")
            {
                var entity = item.GetComponent<Agent>();
                if (entity.AIRadio.Frequency == Frequency)
                    entity.AIRadio.HearMessage(entity, Message);
            }
        }

    }

    public void Transmit(Agent From,Agent To)
    {

        Message = To.name + " I need you to do x";
        Debug.Log(Message);
        foreach (var item in Physics.OverlapSphere(From.transform.position, RadioTransmistionDistance))
        {
            if (item.tag == "HasRadio")
            {
                var entity = item.GetComponent<Agent>();
                if (entity.AIRadio.Frequency == Frequency)
                    entity.AIRadio.HearMessage(entity, Message);
            }
        }

    }

    public void Transmit(Vector3 Origin,Team From, Team To)
    {

        Message = To.TeamName + " This is " + From.TeamName + "Message as follows: Enemy spotted in treeline at 019, 304";
        Debug.Log(Message);
        foreach(var item in Physics.OverlapSphere(Origin, RadioTransmistionDistance))
        {
 
            if(item.tag == "HasRadio")
            {

                var entity = item.GetComponent<Agent>();             
                if (entity.AIRadio.Frequency == Frequency)
                    entity.AIRadio.HearMessage(entity, Message);
            }                        
        }

    }
}
