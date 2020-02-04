using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Movement")]
    public Vector3 TravelingDirection;
    [Header("Stats")]
    public float Health = 100;
    public float Sensertivity = 1;
    public float Speed = 5;
    [Header("Combat")]
    public float NextTracer = 0;
    protected float gap = 0;
    public float BulletGap = 0.2f;

    protected GameObject Tracer;
    protected GameObject Bullet;
    public float BulletMass = 10;
    public float BulletInitialSpeed = 5700;
    // Start is called before the first frame update
    public virtual void Start()
    {
        BulletInitialSpeed = 7000;
        Tracer = Resources.Load("Tracer", typeof(GameObject)) as GameObject;
        Bullet = Resources.Load("Bullet", typeof(GameObject)) as GameObject;    
        Health = 100;
    }

    // Update is called once per frame
    public virtual void Update()
    {

   




        TravelingDirection = transform.forward;
        gap += Time.deltaTime;
        if (transform.position.y < -1)
        {
            Health = 0;
        }
  
  
    }
    public void Respawn(Vector3 RespawnPosition)
    {
        transform.position = RespawnPosition;
    }
    public void Fire(float Mass, float InitialSpeed)
    {
        
        GameObject go;
        NextTracer++;
        if (NextTracer >= 2)
        {
            NextTracer = 0;
            go = Instantiate(Tracer, transform.position, transform.rotation);
        }
        else
        {
            go = Instantiate(Bullet, transform.position, transform.rotation);
        }

        go.transform.position += transform.forward * 6.1f;
        go.transform.position += new Vector3(0, 0, 0);
        go.AddComponent<Rigidbody>();
        var rb = go.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * InitialSpeed);
        rb.mass = Mass;
        
    }


}
