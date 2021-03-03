using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public bool bPlaying;

    [Header("Menu References")]
    public GameObject MainMenu;

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
        LeanTween.scale(MainMenu, new Vector3(1, 1, 1), 0.3f);
    }

    public void PlayLevels()
    {
        LevelManager.Instance.StartLevel();
        LeanTween.scale(MainMenu, new Vector3(0, 0, 0), 0.3f);
    }


}

