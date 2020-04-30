using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[ExecuteInEditMode]
public class ShootingRange : MonoBehaviour
{
    public int NumberTargetsNeeded;
    int PreviousOfNumberTargetsNeeded = int.MaxValue;
    public float Radius;
    public float TimeScale = 1;
    public bool FirstTime = false;
    public GameObject TargetToSpawn;
    public List<ShootingTarget> Targets = new List<ShootingTarget>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(Radius * 2, Radius * 2, Radius * 2));
    }
    // Start is called before the first frame update
    void Start()
    {

        name = "Shooting Range";
    }

    // Update is called once per frame
    void Update()
    {

        Targets.Clear();
        Targets.AddRange(FindObjectsOfType<ShootingTarget>());
        if (Application.isPlaying == false)
        {
            FirstTime = true;
            for (int i = Targets.Count - 1; i >= 0; i--)
            {
                if (Targets[i] == null)
                {
                    Targets.RemoveAt(i);
                }
            }
            Time.timeScale = TimeScale;
            int NumOfTargets = Targets.Count;
            if (NumberTargetsNeeded != PreviousOfNumberTargetsNeeded || NumberTargetsNeeded != NumOfTargets)
            {

                foreach (var item in Targets)
                {
                    DestroyImmediate(item.gameObject);
                }
                Targets = new List<ShootingTarget>();

                for (int i = 0; i < NumberTargetsNeeded; i++)
                {

                    GameObject go = Instantiate(TargetToSpawn, transform.position, transform.rotation);
                    go.name = "Target " + i;
                    Targets.Add(go.GetComponent<ShootingTarget>());
                }
                PreviousOfNumberTargetsNeeded = NumberTargetsNeeded;

                var NavMesh = FindObjectOfType<NavMeshSurface>();
                NavMesh.BuildNavMesh();
            }
        }

        else
        {
            if (FirstTime)
            {
                foreach (var go in Targets)
                {
                    var spawnpoint = GameManager.GenerateRandomPoint(transform.position, Radius);
                    go.AINavMeshAgent.Warp(spawnpoint);

                }
                FirstTime = false;
            }
        }

    }
}