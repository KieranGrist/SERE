using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody b_Rigidbody;
    public float MaxTime = 200;
    public float Damage;
    public float LifeTime;
    // Start is called before the first frame update
    void Start()
    {
        MaxTime = 30;
        b_Rigidbody = GetComponent<Rigidbody>();
        tag = "Bullet";
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != gameObject)
        {
            var Hit = collision.gameObject.GetComponent<AIPlayer>();
            if (Hit)
            {
                Hit.entityStats.Health -= 20;
                LearningAIArea learningAIArea = Hit.GetComponentInParent<LearningAIArea>();
                if (learningAIArea)
                    learningAIArea.SoldierAgent.AddReward(-2f);
                var rb = Hit.GetComponent<Rigidbody>();       
            }
            var wall = collision.gameObject.GetComponent<Wall>();
            if (wall)
            {
                LearningAIArea learningAIArea = wall.GetComponentInParent<LearningAIArea>();
                if (learningAIArea)
                    learningAIArea.SoldierAgent.AddReward(-2f);
            }
            var floor = collision.gameObject.GetComponent<Floor>();
            if (floor)
            {
                LearningAIArea learningAIArea = floor.GetComponentInParent<LearningAIArea>();
                if (learningAIArea)
                    learningAIArea.SoldierAgent.AddReward(-2f);
            }

        }
        if(!collision.gameObject.GetComponent<Bullet>())
        Destroy(gameObject);
    }
    private void Update()
    {
        if (transform.position.y <= -10)
            Destroy(gameObject);
        if (LifeTime >= MaxTime)
            Destroy(gameObject);
        LifeTime += Time.deltaTime;
    }
}
