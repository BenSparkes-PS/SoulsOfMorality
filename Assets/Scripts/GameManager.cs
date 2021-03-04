using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public bool bPlaying;


    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        Application.targetFrameRate = 60;
    }


    void Start()
    {
    }

    public void PlayLevels()
    {
        MenuManager.Instance.ToggleMainMenu();
        LevelManager.Instance.StartLevel();
    }


}

