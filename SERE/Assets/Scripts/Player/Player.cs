using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    new void Update()
    {
        base.Update();
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
            transform.eulerAngles += new Vector3(Sensertivity, 0, 0);


        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && gap >= BulletGap)
        {
            Fire(BulletMass, BulletInitialSpeed);
            gap = 0;
        }



    }
}
