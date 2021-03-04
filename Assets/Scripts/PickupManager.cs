using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public int playerPickupCount = 0;
    [SerializeField]
    private int poolAmount = 10;
    private List<GameObject> pickupPool;
    [SerializeField]
    private GameObject pickup;

    public static PickupManager pickupManager;
    void Awake()
    {
        if (pickupManager == null)
        {
            pickupManager = this;
        }
        else
            Destroy(this);
    }

    public void PickupCollision(GameObject pickup)
    {        
        Souls soulsInstance = pickup.GetComponent<Souls>();
        if(soulsInstance != null)
        {
            if(soulsInstance.SoulType == 0)
                playerPickupCount++;
            else
                playerPickupCount--;
            playerPickupCount = Mathf.Clamp(playerPickupCount, -5,5);
        }
        pickup.SetActive(false);
    }

    private List<GameObject> PoolObjects(GameObject poolObject, int amount)
    {
        List<GameObject> tempPool = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < amount; i++)
        {
            temp = Instantiate(poolObject);
            temp.SetActive(false);
            tempPool.Add(temp);
        }
        return tempPool;
    }

    private GameObject GetPoolObject(List<GameObject> objectPool)
    {
        foreach (GameObject x in objectPool)
        {
            if (!x.activeInHierarchy)
            {
                return x;
            }
        }
        Debug.LogError("Unable to find extra object in pool");
        return null;
    }
}
