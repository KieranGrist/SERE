using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionPoint : MonoBehaviour
{
    public int PlayersInScene;
    public int PlayersInExtractionPoint;
    public GameManager GM;
    private void Update()
    {
        PlayersInScene = GM.Alive;
        PlayersInExtractionPoint = 0;
        foreach (var item in Physics.OverlapBox(transform.position, new Vector3(2, 2, 2)))
            if (item.GetComponent<Player>())
                PlayersInExtractionPoint++;
        if (PlayersInExtractionPoint == PlayersInScene)  
            GM.Restart();
    
        

    }

}
