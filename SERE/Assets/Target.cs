using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int Health = 100;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = Color.green;
        Health = 100;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Health -= 20;
            var go = collision.gameObject;
            Destroy(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(Health)
        {
            case 100:
                rend.material.color = Color.green;
                break;
            case 80:
                rend.material.color = new Color(0,255,0);
                break;
            case 60:
                rend.material.color = Color.yellow;
                break;
            case 40:
                rend.material.color = Color.red;
                break;
            case 20:
                rend.material.color = Color.black;
                break;
        }

        if (transform.position.y < -1)
        {
            Health = 0;
        }
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
