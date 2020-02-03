﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody b_Rigidbody;
    public float MaxTime = 200;
    public float LifeTime;
    // Start is called before the first frame update
    void Start()
    {
        MaxTime = 200;
        b_Rigidbody = GetComponent<Rigidbody>();
        tag = "Bullet";
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != gameObject)
        {
            var Hit = collision.gameObject.GetComponent<Entity>();
            if (Hit)
            {
                Hit.Health -= 50;

                Destroy(gameObject);
            }

 
        }
    }
    private void Update()
    {
        if (transform.position.y <= -10)
            Destroy(gameObject);
        if (LifeTime >= MaxTime)
            Destroy(gameObject);
        LifeTime += Time.deltaTime;
    }
}
