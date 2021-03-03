using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
        LeanTween.scale(GameObject.Find("RoundOverlay"), new Vector3(0, 0, 0), 0.3f);
        LeanTween.scale(deathInfo, new Vector3(1, 1, 1), 0.3f);
        LeanTween.moveLocalY(Camera.main.transform.gameObject, Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.width * 1.5f, 0)).y, 0.5f);
        GameManager.Instance.bPlaying = false;
    }
}
