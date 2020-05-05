using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public ExtractionArea MyArea;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("agent"))
        {
            collision.gameObject.GetComponent<LearningSoldier>().AddReward(-2f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        MyArea = GetComponentInParent<ExtractionArea>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
