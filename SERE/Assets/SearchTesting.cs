using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class SearchTesting : MonoBehaviour
{
    public GameObject Cube;
 public   List<GameObject> SearchPoints = new List<GameObject>();
    public int NumberOfSearchPoints = 12;
    public Vector3 SearchLocation = new Vector3();

    public float SearchDistance = 100;
    public bool InnerCircleEnabled = false;
    Vector3 PreviousSearchLocation = new Vector3(23, 2313, 23);
    float PreviousSearchDistance = 0;
    int PreviousSearchPoint = 0;
    bool PreviousInnerCircleEnabled = true;
    // Start is called before the first frame update
    void Start()
    {

    }
    bool CheckIfChanged()
    {
        if (NumberOfSearchPoints != PreviousSearchPoint)
            return true;
        if (SearchLocation != PreviousSearchLocation)
            return true;
        if (SearchDistance != PreviousSearchDistance)
            return true;
        if (InnerCircleEnabled != PreviousInnerCircleEnabled)
            return true;

        return false;
    }
    private void OnDrawGizmos()
    {

        int i = 0;
        foreach (var item in SearchPoints)
        {
            i++;
            Handles.Label(item.transform.position, " " + i.ToString());
        }
    }
    // Update is called once per frame
    void Update()
    {
        NumberOfSearchPoints = Mathf.Clamp(NumberOfSearchPoints, 1, 30);
        if (NumberOfSearchPoints != PreviousSearchPoint)
        {   
            foreach (var item in SearchPoints)
                Destroy(item);
            SearchPoints.Clear();
            for (int i = 0; i < NumberOfSearchPoints; i++)
            {
                GameObject go = Instantiate(Cube, transform.position, transform.rotation);
                SearchPoints.Add(go);
            }
        }


        SearchLocation = transform.position;
        var position = SearchLocation;
        float Degress = 0;
        var Increase = 360 / NumberOfSearchPoints;
        for (int i = 0; i < NumberOfSearchPoints; i++)
        {
            if (i == NumberOfSearchPoints - 1)
                Debug.DrawLine(SearchPoints[i].transform.position, SearchPoints[0].transform.position);
            else
                Debug.DrawLine(SearchPoints[i].transform.position, SearchPoints[i + 1].transform.position);
        }

        if (CheckIfChanged())
            for (int i = 0; i < NumberOfSearchPoints; i++)
            {

                SearchPoints[i].transform.eulerAngles = new Vector3(0, Degress, 0);
                SearchPoints[i].transform.position = transform.position;


                if (InnerCircleEnabled)
                    SearchPoints[i].transform.position += SearchPoints[i].transform.forward * Random.Range(1, SearchDistance) + new Vector3(0, 2, 0);
                else
                    SearchPoints[i].transform.position += SearchPoints[i].transform.forward * SearchDistance + new Vector3(0, 2, 0);
                SearchPoints[i].GetComponent<Renderer>().material.color = Color.red;
                Degress += Increase;


            }
        PreviousSearchLocation = SearchLocation;
        PreviousSearchDistance = SearchDistance;
        PreviousInnerCircleEnabled = InnerCircleEnabled;
        PreviousSearchPoint = NumberOfSearchPoints;
    }
}