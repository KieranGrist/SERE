using UnityEngine;
public class AIPlayer : Entity
{
    public float AgentSpeed;

    // Start is called before the first frame update
    public override void Restart()
    {
        entityStats.Health = 100;
        MyArea = GetComponentInParent<SEREArea>();

    }
    void Start()
    {
        Restart();
        entityStats.Health = 100;
    }
    // Update is called once per frame
    public override void Update()
    {
        Affiliation = Side.Enemy;
        gameObject.layer = 10;
        
        transform.LookAt(MyArea.ExtractionGameObject.transform);
        Vector3 moveVector = AgentSpeed * transform.forward * Time.fixedDeltaTime;
        transform.position += moveVector;
        if (entityStats.Health <= 0)
        {
            LearningAIArea learningAIArea = GetComponentInParent<LearningAIArea>();
            if (learningAIArea && learningAIArea.SoldierAgent)  
                learningAIArea.SoldierAgent.AddReward(10f);
            MyArea.RemovePlayer(this);

        }
    }
}
