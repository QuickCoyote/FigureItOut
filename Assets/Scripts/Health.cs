using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;
    public int currentHealth = 5;

    public int entityID = 0;

    public void Initialize()
    {
        currentHealth = maxHealth;
        GlobalManager.Instance.entityDictionary.Add(entityID, gameObject);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth < 0)
        {
            GlobalManager.Instance.entityDictionary.Remove(entityID);
            Destroy(gameObject);
        }
    }
}
