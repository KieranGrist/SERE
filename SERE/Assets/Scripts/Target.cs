using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Entity
{
    private new  void Start()
    {
        base.Start();
        rend = GetComponent<Renderer>();
        rend.material.color = Color.green;
    }
    private new void Update()
    {

        base.Update();
        switch (Health)
        {
            case 100:
                rend.material.color = Color.green;
                break;
            case 80:
                rend.material.color = new Color(0, 255, 0);
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
    }
}
