using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

[RequireComponent(typeof(Rigidbody))]
public class LearningSoldier : Agent
{
    // Speed of agent movement.
    public float moveSpeed = 2f;
    // Speed of agent rotation.
    public float turnSpeed = 180;
    //Area
    public ExtractionArea MyArea;
    Rigidbody RB;
    L85A2 l85A2 = new L85A2();
    [Header("Entity")]
    public Side Affiliation = Side.Friendly;
    [Header("Movement")]
    public Vector3 TravelingDirection;
    [Header("Combat")]
    public GameObject Enemy;
    public override void Initialize()
    {
        base.Initialize();
        MyArea = GetComponentInParent<ExtractionArea>();
        Enemy = null;
        RB = GetComponent<Rigidbody>();
        //LoadoutGenerator.GenerateRandomLoadout(this);

    }
    public override void Heuristic(float[] actionsOut)
    {
        if (Input.GetKey(KeyCode.D))
            actionsOut[2] = 2f;
        if (Input.GetKey(KeyCode.W))
            actionsOut[0] = 1f;
        if (Input.GetKey(KeyCode.A))
            actionsOut[2] = 1f;
        if (Input.GetKey(KeyCode.S))
            actionsOut[0] = 2f;
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        float forwardAmount = vectorAction[0];
        float turnAmount = 0f;
        if (vectorAction[1] == 1f)
            turnAmount = -1f;
        else if (vectorAction[1] == 2f)
            turnAmount = 1f;
        if (vectorAction[2] == 0f)
            l85A2.Fire(transform, transform.forward);

        // Apply movement
        RB.MovePosition(transform.position + transform.forward * forwardAmount * moveSpeed * Time.fixedDeltaTime);
        transform.Rotate(transform.up * turnAmount * turnSpeed * Time.fixedDeltaTime);

        // Apply a tiny negative reward every step to encourage action
        if (MaxStep > 0) AddReward(-1f / MaxStep);
    }
    public override void OnEpisodeBegin()
    {
        Enemy = null;
        base.OnEpisodeBegin();

    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // Direction soldier is facing  
        sensor.AddObservation(transform.forward);
    }
    private void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        l85A2.CurrentMagazine = new InfiniteMagazine();
        l85A2.WeaponFireRate = RateOfFire.Single;
        l85A2.LoadPrefabs();
        l85A2.UpdateGap(Time.deltaTime);
        if (transform.position.y < -5)
        {
            AddReward(-10);
            MyArea.ResetArea();
        }

        if (StepCount % 5 == 0)
            RequestDecision();
        else
            RequestAction();
    }
}