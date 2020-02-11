using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "InventoryItems/Weapon", order = 1)]

[System.Serializable]
public class Weapon : InventoryItem
{
    protected GameObject Tracer;
    protected GameObject Bullet;
    [Header("Weapon")]
    public float FiringSpeed =5700;
    public float FiringRate;
    public List<Magazine> CompatableMagazines;
    public Magazine CurrentMagazine;
    float gap;

    public void Reload(Inventory inventory)
    {
        Magazine MagToReload = null;
        foreach (var item in CompatableMagazines)
        {

            MagToReload =  (Magazine)inventory.SearchAndReturn(item) ;
            if (MagToReload)
                break;
                    }
        if (MagToReload)
        {
            CurrentMagazine = MagToReload;

        }
    }
    public void LoadPrefabs()
    {
        Tracer = Resources.Load("Tracer", typeof(GameObject)) as GameObject;
        Bullet = Resources.Load("Bullet", typeof(GameObject)) as GameObject;
        CurrentMagazine.CalculateWeight();
    }
    public void Fire(Transform transform)
    {
        if (CurrentMagazine.RunTimeBulletsInMag > 0)
        {
            CurrentMagazine.RunTimeBulletsInMag--;
            GameObject go;
            CurrentMagazine.RunTimeNextTracer++;
            if (CurrentMagazine.NextTracer >= CurrentMagazine.Tracergap)
            {
                CurrentMagazine.RunTimeNextTracer = 0;
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
            rb.AddForce(transform.forward * FiringSpeed);
            rb.mass = CurrentMagazine.BulletWeight;
            CurrentMagazine.CalculateWeight();
        }
    }
    public void UpdateGap(float DeltaTime)
    {
        gap += DeltaTime;
    }

}
