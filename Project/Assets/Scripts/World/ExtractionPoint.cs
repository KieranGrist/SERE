﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionPoint : MonoBehaviour
{
    public int PlayersInScene;
    public int PlayersInExtractionPoint;
    public SEREArea MyArea;
    public float ExtractionRadius;
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        foreach (var item in Physics.OverlapSphere(transform.position, ExtractionRadius))
            if (item.GetComponent<AIPlayer>())
            {
                MyArea.RemovePlayer(item.GetComponent<AIPlayer>());
                LearningAIArea learningAIArea = GetComponentInParent<LearningAIArea>();
                    if (learningAIArea)
                    learningAIArea.SoldierAgent.AddReward(-2f);        
            }
    
        

    }

}
