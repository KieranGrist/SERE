using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class CommandersBrain
{
    public Team Team1, Team2, Team3, Team4;
    public Vector3 PlayersLocation;
    public Vector3 TravelingLocation;
    public List<Grid> SearchedGrids = new List<Grid>();

}
[System.Serializable]
public class Commander : MonoBehaviour
{
    public Commander()
    {
         commandersBrain = new CommandersBrain();
 CommandersRadio = new ANPRC119();
        CommandersRadio.Frequency = 5;
}
    public CommandersBrain commandersBrain = new CommandersBrain();
    public Radio CommandersRadio = new ANPRC119();


}
