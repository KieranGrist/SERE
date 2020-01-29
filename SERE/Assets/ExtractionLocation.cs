using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionLocation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Target>())
        {
            GameManager.GM.NewGame();
        }
    }
}
