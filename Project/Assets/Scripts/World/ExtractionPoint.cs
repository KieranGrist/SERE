using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionPoint : MonoBehaviour
{
    public int PlayersInScene;
    public int PlayersInExtractionPoint;
    public ExtractionArea MyArea;
    private void Update()
    {

        foreach (var item in Physics.OverlapBox(transform.position, new Vector3(2, 2, 2)))
            if (item.GetComponent<Player>())
            {
                MyArea.PlayerWins++;
                MyArea.Restart();
            }
    
        

    }

}
