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
    //private GameObject pickup;

    private SpriteRenderer _currentPlayerSprite;
    [SerializeField]
    private Sprite spriteNeutral, spriteGood, spriteEvil;
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

    void Start() 
    {
        if(spriteNeutral == null || spriteGood == null || spriteEvil == null)
        {
            Debug.LogError("Unassigned Sprites on PickupManager!");
        }
    }

    public void PickupCollision(GameObject pickup)
    {
        Souls soulsInstance = pickup.GetComponent<Souls>();
        if(_currentPlayerSprite == null)
        {
            _currentPlayerSprite = LevelManager.Instance.Player.transform.GetChild(0).GetComponent<SpriteRenderer>();
        }        
        if(soulsInstance != null)
        {
            if(soulsInstance.SoulType == 0)
                playerPickupCount++;
            else
                playerPickupCount--;
            playerPickupCount = Mathf.Clamp(playerPickupCount, -5,5);
        }

        if(playerPickupCount <= -2)
        {
            _currentPlayerSprite.sprite = spriteGood;
        }else if(playerPickupCount >= 2)
        {
            _currentPlayerSprite.sprite = spriteEvil;
        }else
        {
            _currentPlayerSprite.sprite = spriteNeutral;
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
