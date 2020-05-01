using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Combat
{
    [Header("Primary Weapon")]
    public Weapon PrimaryWeapon = null;
    public bool PrimaryWeaponHasMags = false;
    Weapon PreviousPrimaryWeapon = null;

    [Header("Secondary Weapon")]
    public Weapon SecondaryWeapon = null;
    public bool SecondaryWeaponHasMags = false;
    Weapon PreviousSecondaryWeapon = null;

    [Header("Current Weapon")]
    public Weapon CurrentWeapon = null;
    public bool Weapon1IsCurrent;
    public bool Weapon2IsCurrent;
    public bool Weapon3IsCurrent;
    public bool CurrentWeaponHasMags;



    public Combat()
    {
        PrimaryWeapon = null;
        SecondaryWeapon = null;
        CurrentWeapon = null;
    }
    public Combat(Weapon Weapon1, Weapon Weapon2)
    {
        PrimaryWeapon = Weapon1;
        SecondaryWeapon = Weapon2;
    }

    public void SwitchCurrentWeapon(int WeaponSlot)
    {
        switch (WeaponSlot)
        {
            case 1:
                CurrentWeapon = PrimaryWeapon;
                break;
            case 2:
                CurrentWeapon = SecondaryWeapon;
                break;

        }
    }

    public void SwitcWeapon(int WeaponSlot, Weapon weapon)
    {
        switch (WeaponSlot)
        {
            case 1:
                PrimaryWeapon = weapon;
                break;
            case 2:
                SecondaryWeapon = weapon;
                break;
 
        }

    }
    public bool CheckCurrentWeaponHasMags(Inventory inventory)
    {
        foreach (var item in CurrentWeapon.CompatableMagazines)
        {
            if (inventory.ItemInInventory(item))
            {
                CurrentWeaponHasMags = true;
                return true;
            }
        }
        return false;
    }
    public bool CheckWeaponHasMags(Inventory inventory, int WeaponSlot)
    {
        switch (WeaponSlot)
        {
            case 1:
                PrimaryWeaponHasMags = false;
                foreach (var item in PrimaryWeapon.CompatableMagazines)
                {
                    if (inventory.ItemInInventory(item))
                    {
                        PrimaryWeaponHasMags = true;
                        return true;    
                    }
                }
                break;
            case 2:
                SecondaryWeaponHasMags = false;
                foreach (var item in SecondaryWeapon.CompatableMagazines)
                {
                    if (inventory.ItemInInventory(item))
                    {
                        SecondaryWeaponHasMags = true;
                        return true;      
                    }
                }
                break;
        }
        return false;
    }


    public void Update()
    {
        CheckCurrentWeapon();
        CheckWeaponUpdated();
        UpdateGaps();

    }

    private void UpdateGaps()
    {
        if (PrimaryWeapon != null)
            PrimaryWeapon.UpdateGap(Time.deltaTime);
        if (SecondaryWeapon != null)
            SecondaryWeapon.UpdateGap(Time.deltaTime);
  
    }

    private void CheckWeaponUpdated()
    {
        if (PrimaryWeapon != PreviousPrimaryWeapon)
            PrimaryWeapon.LoadPrefabs();
        PreviousPrimaryWeapon = PrimaryWeapon;

        if (SecondaryWeapon != PreviousSecondaryWeapon)
            SecondaryWeapon.LoadPrefabs();
        PreviousSecondaryWeapon = SecondaryWeapon;

    }

    private void CheckCurrentWeapon()
    {
        Weapon1IsCurrent = false;
        Weapon2IsCurrent = false;
        Weapon3IsCurrent = false;
        if (CurrentWeapon == PrimaryWeapon)
            Weapon1IsCurrent = true;
        if (CurrentWeapon == SecondaryWeapon)
            Weapon2IsCurrent = true;

    }
}