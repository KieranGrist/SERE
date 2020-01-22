using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
  public  float gap;
    public float BulletGap = 0.05f;
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
        go.transform.position += new Vector3(0, 1, 0);
        go.AddComponent<Rigidbody>();
        var rb = go.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * InitialSpeed);
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

        transform.eulerAngles += new Vector3(Sensertivity * Input.GetAxis("Mouse Y"), -Sensertivity * Input.GetAxis("Mouse X"), 0);


        if (Input.GetKey(KeyCode.UpArrow))
            transform.eulerAngles += new Vector3(Sensertivity , 0, 0);

        gap += Time.deltaTime;
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && gap >= BulletGap)
        {
            Fire(BulletMass, BulletInitialSpeed);
            gap = 0;
        }



    }
}
