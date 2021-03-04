using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { get; private set; }

    [Header("Level Info")]
    public int LevelNumber;

    [Header("Level Settings")]
    public float PlayerGravity;
    public float PlayerSpeed;
    public float PlayerJumpHeight;


    public GameObject PlayerPrefab;
    private GameObject Player;


    void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;
    }

    public void StartLevel()
    {
        Player = Instantiate(PlayerPrefab);
        LevelGenerator.Instance.GenerateLevel(); //Instantiates the level on start.
        MenuManager.Instance.ToggleRoundOverlay();
        GameManager.Instance.bPlaying = true;       //Sets bool bPlaying to true (If game is in running state)
    }

    public void LevelComplete()
    {
        GameManager.Instance.bPlaying = false;
    }

    public void RestartLevel(GameObject LevelFailedMenu)
    {
        LevelGenerator.Instance.DestroyLevel();
        Destroy(Player);
        MenuManager.Instance.ToggleLevelFailedMenu();
        LeanTween.moveLocal(Camera.main.transform.gameObject, new Vector3(0,0,-10), 0.5f).setOnComplete(() =>
        {
            StartLevel();
        });
    }
}
