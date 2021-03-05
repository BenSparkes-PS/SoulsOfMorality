using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { get; private set; }

    [Header("Level Info")]
    public int LevelNumber = 1;
    public float LevelTimer = 0.0f;
    private int _seconds;
    private bool _RunComplete = false;
    public bool LevelCompleteSections;

    [Header("Level Settings")]
    public float PlayerGravity;
    public float PlayerSpeed;
    public float PlayerJumpHeight;
    private float LevelTime;

    public GameObject PlayerPrefab;
    public GameObject Player;

    public bool isFlipped = false;

    void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;
    }


    void Start()
    {
        LevelTime = 10 + (LevelNumber * 5);
        PlayerSpeed = 10 + (LevelNumber / 4);
    }

    void Update()
    {
        if (!LevelCompleteSections && GameManager.Instance.bPlaying)
        {
            LevelTimer += Time.deltaTime;
            _seconds = (int)LevelTimer % 60;
        }
        if (_seconds == LevelTime && _RunComplete == false)
        {
            LevelGenerator.Instance.CreateNewLevelSection(false, 6, 0);
            LevelCompleteSections = true;
            _RunComplete = true;
        }
    }

    public void StartLevel()
    {
        LevelTimer = 0;
        _seconds = 0;
        Player = Instantiate(PlayerPrefab);
        LevelGenerator.Instance.GenerateLevel(); //Instantiates the level on start.
        MenuManager.Instance.ToggleRoundOverlay();
        GameManager.Instance.bPlaying = true;       //Sets bool bPlaying to true (If game is in running state)
        LevelCompleteSections = false;
    }
    public void StartNextLevel()
    {
        MenuManager.Instance.ToggleRoundComplete();
        LevelTime = 10 + (LevelNumber * 5);
        PlayerSpeed = 10 + (LevelNumber / 4);
        LevelCompleteSections = false;
        _RunComplete = false;
    }
    public void LevelComplete()
    {
        LevelTimer = 0;
        _seconds = 0;
        LevelNumber++;
        MenuManager.Instance.ToggleRoundComplete();
        Debug.Log("Complete");
    }

    public void RestartLevel(GameObject LevelFailedMenu)
    {
        LevelTime = 10 + (LevelNumber * 5);
        PlayerSpeed = 10 + (LevelNumber / 4);
        LevelTimer = 0;
        _seconds = 0;
        LevelGenerator.Instance.DestroyLevel();
        Destroy(Player);
        MenuManager.Instance.ToggleLevelFailedMenu();
        LeanTween.moveLocal(Camera.main.transform.gameObject, new Vector3(0, 0, -10), 0.5f).setOnComplete(() =>
          {
              StartLevel();
          });
    }
}
