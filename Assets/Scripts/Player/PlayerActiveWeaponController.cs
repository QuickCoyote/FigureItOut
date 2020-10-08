using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveWeaponController : MonoBehaviour
{
    public List<GameObject> weaponList = new List<GameObject>();
    public GameObject activeWeapon = null;

    public void ActivateWeapon(int position)
    {
        for(int i = 0; i < weaponList.Count; i++)
        {
            if(i == position)
            {
                weaponList[i].SetActive(true);
                activeWeapon = weaponList[i];
            }
            else
            {
                weaponList[i].SetActive(false);
            }
        }
    }
}
