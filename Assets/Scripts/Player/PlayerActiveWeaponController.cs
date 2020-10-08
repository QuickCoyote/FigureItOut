using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveWeaponController : MonoBehaviour
{
    public List<GameObject> weaponList = new List<GameObject>();

    public void ActivateWeapon(int position)
    {
        for(int i = 0; i < weaponList.Count; i++)
        {
            if(i == position)
            {
                weaponList[i].SetActive(true);
            }
            else
            {
                weaponList[i].SetActive(false);
            }
        }
    }
}
