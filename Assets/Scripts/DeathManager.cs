using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DeathManager: MonoBehaviour
{
    [SerializeField]
    private Text deathInfo;
    
    public static DeathManager deathManager;

    void Awake()
    {
        if(deathManager == null)
        {
            deathManager = this;
        } else
            Destroy(this);

        deathInfo.enabled = false;
    }
    public void Died()
    {
        deathInfo.enabled = true;
        GameManager.Instance.bPlaying = false;
    }
}
