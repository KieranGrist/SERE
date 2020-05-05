using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public SEREArea MyArea;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("agent"))
        {
            LearningSoldier learningSoldier = collision.gameObject.GetComponent<LearningSoldier>();
            if (learningSoldier)
                collision.gameObject.GetComponent<LearningSoldier>().AddReward(-2f);

        }

    }
    // Start is called before the first frame update
    void Start()
    {
        MyArea = GetComponentInParent<SEREArea>();
    }
}
