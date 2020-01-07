using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float Sensertivity;
    public GameObject Bullet;
    public float BulletMass, BulletInitialSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Fire(float Mass, float InitialSpeed)
    {
        var go = Instantiate(Bullet, transform.position, transform.rotation);
        go.transform.position += transform.forward * 1.1f;
        go.AddComponent<Rigidbody>();
        var rb = go.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 10000);
        rb.mass = Mass;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward * Time.deltaTime * Speed;
        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right * Time.deltaTime * Speed;
        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward * Time.deltaTime * Speed;
        if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right * Time.deltaTime * Speed;

        transform.eulerAngles += new Vector3(Sensertivity * Input.GetAxis("Mouse X"), Sensertivity * Input.GetAxis("Mouse Y"), 0);


        if (Input.GetKey(KeyCode.UpArrow))
            transform.eulerAngles += new Vector3(Sensertivity , 0, 0);
        if (Input.GetMouseButton(0)|| Input.GetKey(KeyCode.Space))
            Fire(BulletMass, BulletInitialSpeed);


    }
}
