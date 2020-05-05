using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using TMPro;
[System.Serializable]
public class LearningAIArea : SEREArea
{
    [Header("Learning AI Management")]
    public LearningSoldier SoldierAgent;
    Vector3 AISpawnLocation;

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(AISpawnLocation, new Vector3(5, 5, 5));

    }
    // Start is called before the first frame update
    void Start()
    {
        ResetArea();
    }
    // Update is called once per frame
    void Update()
    {
        cumulativeRewardText.text = SoldierAgent.GetCumulativeReward().ToString("0.00");
        if (PlayerRemaining <= 0)
            ResetArea();
    }
    // https://www.immersivelimit.com/tutorials/reinforcement-learning-penguins-part-2-unity-ml-agents
    public override void ResetArea()
    {
        RemoveAllAI();
        PlaceSoldier();
        PlaceExtractionPoint();
        ExtractionGameObject.ExtractionRadius = Academy.Instance.EnvironmentParameters.GetWithDefault("Extraction_Radius", 3f);
        SpawnAI(AmmountToSpawn, Academy.Instance.EnvironmentParameters.GetWithDefault("Agent_Speed", 1f));

    }
    void PlaceSoldier()
    {
        SoldierAgent.EndEpisode();
        Rigidbody rb = SoldierAgent.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        AISpawnLocation = ChooseRandomPosition(transform.position, SpawnArea, SpawnArea, SpawnArea, SpawnArea) + Vector3.up * 5f; ;
        SoldierAgent.MyArea = this;
        SoldierAgent.transform.position = AISpawnLocation;
    }
    void SpawnAI(int Ammount, float speed)
    {
        for (int i = 0; i < Ammount; i++)
        {
            GameObject player = Instantiate(EnemyPrefap);
            Vector3 PlayerSpawnLocation = ChooseRandomPosition(transform.position, SpawnArea, SpawnArea, SpawnArea, SpawnArea) + Vector3.up * 5f; ;

            player.transform.position = PlayerSpawnLocation;
            player.transform.parent = transform;
            player.GetComponent<AIPlayer>().AgentSpeed = speed;
            aIPlayers.Add(player.GetComponent<AIPlayer>());
        }
    }
}