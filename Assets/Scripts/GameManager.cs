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


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !bPlaying)            //Start level function to test level generation - for debug purposes only
            LevelManager.Instance.StartLevel();
    }
}

