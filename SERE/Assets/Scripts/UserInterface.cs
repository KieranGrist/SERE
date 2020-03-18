using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UserInterface : MonoBehaviour
{
    public Player player;
    public Image P1, P2, P3, P4, P5, P6;
    public Text CurrentWeapon;
    public Text WeaponInfo;

    // Start is called before the first frame update
    void Start()
    {
        P2.transform.position = P1.transform.position + new Vector3(30, 0, 0);
        P3.transform.position = P2.transform.position + new Vector3(30, 0, 0);
        P4.transform.position = P3.transform.position + new Vector3(30, 0, 0);
        P5.transform.position = P4.transform.position + new Vector3(30, 0, 0);
        P6.transform.position = P5.transform.position + new Vector3(30, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {



        if (player)
        {
            if (player.combat.CurrentWeapon != null)
            {
                CurrentWeapon.text = player.combat.CurrentWeapon.Name;

                P1.color = Color.white;
                P2.color = Color.white;
                P3.color = Color.white;
                P4.color = Color.white;
                P5.color = Color.white;
                P6.color = Color.white;

                switch  (player.combat.CurrentWeapon.WeaponFireRate)
                {
                    case RateOfFire.Automatic:
                        P1.color = Color.white;
                        P2.color = Color.white;
                        P3.color = Color.white;
                        P4.color = Color.white;
                        P5.color = Color.white;
                        P6.color = Color.white;
                        break;
                    case RateOfFire.Burst:
                        P1.color = Color.white;
                        P2.color = Color.white;
                        P3.color = Color.white;
                        P4.color = Color.black;
                        P5.color = Color.black;
                        P6.color = Color.black;
                        break;
                    case RateOfFire.Single:
                        P1.color = Color.white;
                        P2.color = Color.black;
                        P3.color = Color.black;
                        P4.color = Color.black;
                        P5.color = Color.black;
                        P6.color = Color.black;
                        break;
                    case RateOfFire.Saftey:
                        P1.color = Color.black;
                        P2.color = Color.black;
                        P3.color = Color.black;
                        P4.color = Color.black;
                        P5.color = Color.black;
                        P6.color = Color.black;
                        break;
                }

                int CompatableMags = 0;
                foreach (var item in player.inventory.inventoryItems)
                {

                    if (item.GetType().BaseType.ToString() == "Magazine")
                    {
                        var Mag = (Magazine)item;
                        foreach (var magazine in player.combat.CurrentWeapon.CompatableMagazines)
                            if (item.Name == magazine.Name)
                                CompatableMags++;
                    }
                }


                WeaponInfo.text = player.combat.CurrentWeapon.CurrentMagazine.BulletsInMag + (" | ") + CompatableMags;
            }
        }
    }
}
