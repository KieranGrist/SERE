using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionPoint : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Player>())

            GameManager.GM.NewGame();

    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.GetComponent<Player>())

            GameManager.GM.NewGame();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())

            GameManager.GM.NewGame();

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())

            GameManager.GM.NewGame();

    }
    private void Update()
    {
        foreach (var item in Physics.OverlapBox(transform.position, new Vector3(2, 2, 2)))
        {
            if (item.GetComponent<Player>())
                GameManager.GM.NewGame();
        }

    }

}
