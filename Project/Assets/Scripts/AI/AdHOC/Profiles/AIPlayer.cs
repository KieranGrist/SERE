using UnityEngine;
public class AIPlayer : MonoBehaviour
{
    public ExtractionArea MyArea;
    public float Health;
    public float AgentSpeed;

    // Start is called before the first frame update
    public void Restart()
    {
        Health = 100;
        MyArea = GetComponentInParent<ExtractionArea>();

    }
    void Start()
    {
        Restart();
        Health = 100;
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(MyArea.ExtractionGameObject.transform);
        Vector3 moveVector = AgentSpeed * transform.forward * Time.fixedDeltaTime;
        transform.position += moveVector;
        if (Health <= 0)
        {
            if (MyArea.SoldierAgent)
            {
                MyArea.RemovePlayer(this);
                MyArea.SoldierAgent.AddReward(10f);
            }
     
        }
    }

}
