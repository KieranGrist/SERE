using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMe : MonoBehaviour
{
    public Vector3 Position;
    public Vector3 Rotation;
    public GameObject Parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent = Parent.transform;
        transform.position = Parent.transform.position;
        transform.position += Position;
        transform.eulerAngles += Rotation;
    }
}
