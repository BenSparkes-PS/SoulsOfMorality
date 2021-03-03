using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField]
    private int playerPickupCount = 0, poolAmount = 10;
    private List<GameObject> pickupPool;
    [SerializeField]
    private GameObject pickup;

    void Start() 
    {
        pickupPool = PoolObjects(pickup, poolAmount);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {                        
            GetPoolObject(pickupPool).SetActive(true);
        }
    }

    public static PickupManager pickupManager;
    void Awake()
    {
       if(pickupManager == null)
        {
            pickupManager = this;
        } else
            Destroy(this); 
    }

    public void PickupCollision(GameObject pickup)
    {
        playerPickupCount++;
        pickup.SetActive(false);
    }

    private List<GameObject> PoolObjects(GameObject poolObject, int amount)
    { 
        List<GameObject> tempPool = new List<GameObject>();
        GameObject temp;       
        for(int i = 0; i < amount; i++)
        {
            temp = Instantiate(poolObject);
            temp.SetActive(false);
            tempPool.Add(temp);
        }
        return tempPool;
    }

    private GameObject GetPoolObject(List<GameObject> objectPool)
    {
        foreach(GameObject x in objectPool)
        {
            if(!x.activeInHierarchy)
            {
                return x;
            }
        }
        Debug.LogError("Unable to find extra object in pool");
        return null;
    }
}
