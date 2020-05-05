using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RateOfFire
{
    Saftey,
    Single,
    Burst,
    Automatic,
    Max


}

[System.Serializable]
public class Weapon : InventoryItem
{
    protected GameObject Tracer;
    protected GameObject Bullet;
    [Header("Weapon")]
    public RateOfFire WeaponFireRate = RateOfFire.Saftey;
    public float FiringSpeed = 5700;
    public float FiringRate;
    public List<Magazine> CompatableMagazines = new List<Magazine>();
    public Magazine CurrentMagazine;
    float ReloadTime = .05f;
    float ReloadGap;
    float gap;


      public bool NeedsReloading()
    {
        return CurrentMagazine.BulletsInMag <= 0;
    }
    public IEnumerator Reload(Inventory inventory)
    {
        yield  return new WaitForSeconds(1);
        if (ReloadGap > ReloadTime)
        {
      
            Magazine MagToReload = null;
            foreach (var item in CompatableMagazines)
            {
                var FoundItem = inventory.SearchAndReturn(item);
                if (FoundItem != null)
                    MagToReload = (Magazine)FoundItem;
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

    }
    public void LoadPrefabs()
    {
        Tracer = Resources.Load("Tracer", typeof(GameObject)) as GameObject;
        Bullet = Resources.Load("Bullet", typeof(GameObject)) as GameObject;
        CurrentMagazine.CalculateWeight();
    }
     public void Fire(Transform transform, Vector3 Rotation)
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
                go = SEREArea.Clone(Tracer, transform);
            }
            else

                go = SEREArea.Clone(Bullet, transform);
            
            go.GetComponent<Bullet>().Damage = CurrentMagazine.BulletDamage;
            go.transform.position += Rotation * 1.1f;
            var rb = go.GetComponent<Rigidbody>();
            rb.velocity = Rotation * FiringSpeed;
            rb.mass = CurrentMagazine.BulletWeight;
            CurrentMagazine.CalculateWeight();
        }
    }
    public IEnumerator BurstFire(Transform transform, Vector3 Rotation)
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(FiringRate);
            Fire(transform, Rotation);
        }
    }
    public void UpdateGap(float DeltaTime)
    {
        gap += DeltaTime;
        ReloadGap += DeltaTime;
    }
    public virtual void SwitchFireRate()
    {
        WeaponFireRate++;
        if (WeaponFireRate == RateOfFire.Max)
            WeaponFireRate = RateOfFire.Saftey;
    }
}