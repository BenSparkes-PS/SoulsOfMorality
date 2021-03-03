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


    [Header("Obstacle Prefabs")]
    public GameObject[] ObstaclesPrefabs;
    public GameObject SoulPrefab;
    private List<GameObject> LevelObstaclesBottom = new List<GameObject>();
    private List<GameObject> LevelObstaclesTop = new List<GameObject>();

    public GameObject PlayerPrefab;
    //Level Generation variables
    public Vector2 BottomLeftScreenReference;
    public Vector2 TopLeftScreenReference;
    private Transform ObstaclesParentBottom;
    private Transform ObstaclesParentTop;
    private Transform CollectablesParent;
    private int LastPrefabBottom;
    private int LastPrefabTop;
    private GameObject Player;


    void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        BottomLeftScreenReference = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        TopLeftScreenReference = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

    }
    #region RoundFunctions
    public void StartLevel()
    {
        Player = Instantiate(PlayerPrefab);
        GenerateLevel(); //Instantiates the level on start.
        LeanTween.scale(GameObject.Find("RoundOverlay"), new Vector3(1, 1, 1), 0.3f);
        GameManager.Instance.bPlaying = true;       //Sets bool bPlaying to true (If game is in running state)
    }

    public void LevelComplete()
    {
        GameManager.Instance.bPlaying = false;
    }

    public void RestartLevel(GameObject LevelFailedMenu)
    {
        Destroy(ObstaclesParentBottom.gameObject);
        Destroy(ObstaclesParentTop.gameObject);
        Destroy(CollectablesParent.gameObject);
        Destroy(Player);
        LevelObstaclesBottom.Clear();
        LevelObstaclesTop.Clear();
        LeanTween.scale(LevelFailedMenu, new Vector3(0, 0, 0), 0.3f);
        LeanTween.moveLocal(Camera.main.transform.gameObject, new Vector3(0,0,-10), 0.5f).setOnComplete(() =>
        {
            StartLevel();
        });
    }
    #endregion


    #region LevelGeneration
    private void GenerateLevel()
    {
        ObstaclesParentBottom = new GameObject("Obstacles Bottom").transform;
        ObstaclesParentTop = new GameObject("Obstacles Top").transform;
        CollectablesParent = new GameObject("Souls").transform;

        CreateNewLevelSection(true, 0, 0);
        for (int i = 0; i < 6; i++)
        {
            CreateNewLevelSection(false, 0, 0);
        }

    }
    public void CreateNewLevelSection(bool first = false, int RandPrefabBottom = -1, int RandPrefabTop = -1)
    {
        bool SoulCanBePlaced = false;
        //Add randomness here to decide what obstacles
        if (RandPrefabBottom == -1)             //If sections not starting sections (plain floor)
        {
            RandPrefabBottom = Random.Range(0, ObstaclesPrefabs.Length);
            SoulCanBePlaced = true;
            while (RandPrefabBottom == LastPrefabBottom)
            {
                RandPrefabBottom = Random.Range(0, ObstaclesPrefabs.Length);
            }
        }
        if (RandPrefabTop == -1)
        {
            RandPrefabTop = Random.Range(0, ObstaclesPrefabs.Length);
            SoulCanBePlaced = true;
            while (RandPrefabTop == LastPrefabTop)
            {
                RandPrefabTop = Random.Range(0, ObstaclesPrefabs.Length);
            }
        }


        LastPrefabBottom = RandPrefabBottom;
        LastPrefabTop = RandPrefabTop;

        float xPos;
        float yPos;
        Vector2 SpawnPos;
        //Bottom positioning
        if (!first)
        {
            xPos = LevelObstaclesBottom[LevelObstaclesBottom.Count - 1].transform.position.x + (LevelObstaclesBottom[LevelObstaclesBottom.Count - 1].GetComponent<Renderer>().bounds.size.x / 2) + ObstaclesPrefabs[RandPrefabBottom].GetComponent<Renderer>().bounds.size.x / 2;
            yPos = LevelObstaclesBottom[LevelObstaclesBottom.Count - 1].transform.position.y;
            SpawnPos = new Vector2(xPos, yPos);
        }
        else
        {
            SpawnPos = new Vector2(BottomLeftScreenReference.x + (ObstaclesPrefabs[RandPrefabBottom].GetComponent<Renderer>().bounds.size.x / 2), BottomLeftScreenReference.y + (ObstaclesPrefabs[RandPrefabBottom].GetComponent<Renderer>().bounds.size.y / 2));
        }
        LevelObstaclesBottom.Add(Instantiate(ObstaclesPrefabs[RandPrefabBottom], SpawnPos, Quaternion.identity, ObstaclesParentBottom));
        LevelObstaclesBottom[LevelObstaclesBottom.Count - 1].GetComponent<LevelSection>().isBottom = true;
        //Top positioning
        if (!first)
        {
            xPos = LevelObstaclesTop[LevelObstaclesTop.Count - 1].transform.position.x + (LevelObstaclesTop[LevelObstaclesTop.Count - 1].GetComponent<Renderer>().bounds.size.x / 2) + ObstaclesPrefabs[RandPrefabTop].GetComponent<Renderer>().bounds.size.x / 2;
            yPos = LevelObstaclesTop[LevelObstaclesTop.Count - 1].transform.position.y;
            SpawnPos = new Vector2(xPos, yPos);
        }
        else
        {
            SpawnPos = new Vector2(TopLeftScreenReference.x + (ObstaclesPrefabs[RandPrefabTop].GetComponent<Renderer>().bounds.size.x / 2), TopLeftScreenReference.y - (ObstaclesPrefabs[RandPrefabTop].GetComponent<Renderer>().bounds.size.y * 2));
        }
        LevelObstaclesTop.Add(Instantiate(ObstaclesPrefabs[RandPrefabTop], SpawnPos, Quaternion.identity, ObstaclesParentTop));
        LevelObstaclesTop[LevelObstaclesTop.Count - 1].transform.localScale = new Vector3(LevelObstaclesTop[LevelObstaclesTop.Count - 1].transform.localScale.x, -1, 1);
        if (SoulCanBePlaced)
        {
            //Soul Pickup Spawning
            float DoSpawnSoul = Random.Range(0.0f, 1.0f);
            float SoulType = Random.Range(0.0f, 1.0f);
            GameObject NewSoul;
            if (DoSpawnSoul > 0.6f && LevelObstaclesTop[LevelObstaclesTop.Count - 1].transform.Find("SoulSpawnPoint"))
            {
                NewSoul =  Instantiate(SoulPrefab, LevelObstaclesTop[LevelObstaclesTop.Count - 1].transform.Find("SoulSpawnPoint").position, Quaternion.identity, CollectablesParent);
                if (SoulType > 0.5f)
                {
                    NewSoul.GetComponent<Souls>().SoulType = 1;
                }
            }
            DoSpawnSoul = Random.Range(0.0f, 1.0f);
            SoulType = Random.Range(0.0f, 1.0f);
            if (DoSpawnSoul > 0.6f && LevelObstaclesBottom[LevelObstaclesBottom.Count - 1].transform.Find("SoulSpawnPoint"))
            {
                NewSoul = Instantiate(SoulPrefab, LevelObstaclesBottom[LevelObstaclesBottom.Count - 1].transform.Find("SoulSpawnPoint").position, Quaternion.identity, CollectablesParent);
                if (SoulType > 0.5f)
                {
                    NewSoul.GetComponent<Souls>().SoulType = 1;
                }
            }
        }
    }

    public void DestroyFirstBottom()
    {
        Destroy(LevelObstaclesBottom[0].gameObject);
        LevelObstaclesBottom.RemoveAt(0);
    }
    public void DestroyFirstTop()
    {
        Destroy(LevelObstaclesTop[0].gameObject);
        LevelObstaclesTop.RemoveAt(0);
    }
    #endregion
}
