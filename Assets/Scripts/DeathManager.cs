using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField]
    private GameObject deathInfo;

    public static DeathManager deathManager;

    void Awake()
    {
        if (deathManager == null)
        {
            deathManager = this;
        }
        else
            Destroy(this);

    }
    public void Died()
    {

        //SAVE / GET SCORE INFO HERE AND SET ON DEATH INFO
        MenuManager.Instance.ToggleLevelFailedMenu();
        MenuManager.Instance.ToggleRoundOverlay();
        GameManager.Instance.bPlaying = false;
    }
}
