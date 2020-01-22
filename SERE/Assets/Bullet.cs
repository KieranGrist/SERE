using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Timer;
    public Rigidbody b_Rigidbody;
    float MaxTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        b_Rigidbody = GetComponent<Rigidbody>();
        tag = "Bullet";
    }

    // Update is called once per frame
    void Update()
    {
     
        Timer += Time.deltaTime;
        if (Timer >= MaxTime)
            Destroy(gameObject);
    }
}
