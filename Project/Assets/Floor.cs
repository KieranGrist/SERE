using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public ExtractionArea MyArea;
    // Start is called before the first frame update
    void Start()
    {
        MyArea = GetComponentInParent<ExtractionArea>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
