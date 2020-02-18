using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Weapon : InventoryItem
{
    protected GameObject Tracer;
    protected GameObject Bullet;
    [Header("Weapon")]
    public float FiringSpeed =5700;
    public float FiringRate;
    public List<Magazine> CompatableMagazines = new List<Magazine>();
    public Magazine CurrentMagazine;
    public float gap;

    public void Reload(Inventory inventory)
    {
        Magazine MagToReload = null;
        foreach (var item in CompatableMagazines)
        {

            MagToReload = (Magazine)inventory.SearchAndReturn(item);
            if (MagToReload != null)
                break;
        }
        if (CurrentMagazine.BulletsInMag <= 0)
        {
            CurrentMagazine = null;
            CurrentMagazine = new EmptyMag();
        }

        if (MagToReload != null)
        {
         
            if (CurrentMagazine.Name != "No Magazine")
                inventory.AddItem(CurrentMagazine);

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
        if (CurrentMagazine.BulletsInMag > 0 && gap > FiringRate)
        {
            gap = 0;
            CurrentMagazine.BulletsInMag--;
            GameObject go;
            CurrentMagazine.NextTracer++;
        
            if (CurrentMagazine.NextTracer >= CurrentMagazine.Tracergap)
            {
                CurrentMagazine.NextTracer = 0;
                go = GameManager.Clone(Tracer, transform);
            }
            else
            {
                go = GameManager.Clone(Bullet, transform);
            }
            go.GetComponent<Bullet>().Damage = CurrentMagazine.BulletDamage;
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
