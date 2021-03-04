using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public static MenuManager Instance { get; private set; }


    [Header("Menu References")]
    public GameObject MainMenu;
    public GameObject RoundOverlay;
    public GameObject LevelFailedMenu;
    public GameObject LevelCompleteMenu;

    private bool _mainMenuActive = false;
    private bool _roundOverlayActive = false;
    private bool _levelCompleteMenuActive = false;
    private bool _levelFailedMenuActive = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;
    }

    void Start()
    {
        ToggleMainMenu();
    }

    public void ToggleMainMenu()
    {
        if (_mainMenuActive)
        {
            LeanTween.scale(MainMenu, new Vector3(0, 0, 0), 0.3f);
        }
        else
        {
            LeanTween.scale(MainMenu, new Vector3(1, 1, 1), 0.3f);
        }
        _mainMenuActive = !_mainMenuActive;
    }
    public void ToggleRoundOverlay()
    {
        if (_roundOverlayActive)
        {
            LeanTween.scale(RoundOverlay, new Vector3(0, 0, 0), 0.3f);
        }
        else
        {
            LeanTween.scale(RoundOverlay, new Vector3(1, 1, 1), 0.3f);
        }
        _roundOverlayActive = !_roundOverlayActive;
    }
    public void ToggleRoundComplete()
    {
        if (_levelCompleteMenuActive)
        {
            LeanTween.scale(LevelCompleteMenu, new Vector3(0, 0, 0), 0.3f);
        }
        else
        {
            LeanTween.scale(LevelCompleteMenu, new Vector3(1, 1, 1), 0.3f);
        }
        _levelCompleteMenuActive = !_levelCompleteMenuActive;
    }
    public void ToggleLevelFailedMenu()
    {
        if (_levelFailedMenuActive)
        {
            LeanTween.moveLocalY(Camera.main.transform.gameObject, Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.width * 1.5f, 0)).y, 0.5f);
            LeanTween.scale(LevelFailedMenu, new Vector3(0, 0, 0), 0.3f);
        }
        else
        {
            LeanTween.scale(LevelFailedMenu, new Vector3(1, 1, 1), 0.3f);
        }
        _levelFailedMenuActive = !_levelFailedMenuActive;
    }
}

