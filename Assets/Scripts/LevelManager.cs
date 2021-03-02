using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { get; private set; }


    [Header("Level Settings")]
    public float PlayerGravity;
    public float PlayerSpeed;
    public float PlayerJumpHeight;


    [Header("Obstacle Prefabs")]
    public GameObject[] ObstaclesPrefabs;
    private List<GameObject> LevelObstaclesBottom = new List<GameObject>();
    private List<GameObject> LevelObstaclesTop = new List<GameObject>();


    //Level Generation variables
    public Vector2 BottomLeftScreenReference;
    public Vector2 TopLeftScreenReference;
    private Transform ObstaclesParentBottom;
    private Transform ObstaclesParentTop;



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

        GenerateLevel(); //Instantiates the level on start.
    }
    #region RoundFunctions
    public void StartLevel()
    {
        GameManager.Instance.bPlaying = true;       //Sets bool bPlaying to true (If game is in running state)
        StartCoroutine(CreateNewLevelSection());    //Starts coroutine to instantiate level sections. - Will be changed to when level section is out of screen view.
    }

    public void LevelFailed()
    {
        GameManager.Instance.bPlaying = false;
    }
    #endregion


    #region LevelGeneration
    private void GenerateLevel()
    {
        ObstaclesParentBottom = new GameObject("Obstacles Bottom").transform;
        ObstaclesParentTop = new GameObject("Obstacles Top").transform;
        //Spawn Bottom of level
        Vector2 SpawnPos = new Vector2(BottomLeftScreenReference.x + (ObstaclesPrefabs[0].GetComponent<Renderer>().bounds.size.x / 2), BottomLeftScreenReference.y + (ObstaclesPrefabs[0].GetComponent<Renderer>().bounds.size.y / 2));
        for (int i = 0; i < 5; i++)                     //Change to spawn enough to cover screen width
        {
            if (i != 0)
            {
                float xPos = LevelObstaclesBottom[LevelObstaclesBottom.Count - 1].transform.position.x + ObstaclesPrefabs[0].GetComponent<Renderer>().bounds.size.x; //ObstaclePrefab[0] will need changing to the current obstacle being instantiated (Random)
                float yPos = LevelObstaclesBottom[LevelObstaclesBottom.Count - 1].transform.position.y;
                SpawnPos = new Vector2(xPos, yPos);
            }
            LevelObstaclesBottom.Add(Instantiate(ObstaclesPrefabs[0], SpawnPos, Quaternion.identity, ObstaclesParentBottom));
        }

        //Spawn Top of 
        SpawnPos = new Vector2(TopLeftScreenReference.x + (ObstaclesPrefabs[0].GetComponent<Renderer>().bounds.size.x / 2), TopLeftScreenReference.y - (ObstaclesPrefabs[0].GetComponent<Renderer>().bounds.size.y / 2));
        for (int i = 0; i < 5; i++)
        {
            if (i != 0)
            {
                float xPos = LevelObstaclesTop[LevelObstaclesTop.Count - 1].transform.position.x + ObstaclesPrefabs[0].GetComponent<Renderer>().bounds.size.x; //ObstaclePrefab[0] will need changing to the current obstacle being instantiated
                float yPos = LevelObstaclesTop[LevelObstaclesTop.Count - 1].transform.position.y;
                SpawnPos = new Vector2(xPos, yPos);
            }
            LevelObstaclesTop.Add(Instantiate(ObstaclesPrefabs[0], SpawnPos, Quaternion.identity, ObstaclesParentTop));
        }
    }
    public IEnumerator CreateNewLevelSection()
    {
        //Destroy First Section
        while (GameManager.Instance.bPlaying) //Add end while clause for end of level
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(LevelObstaclesBottom[0].gameObject);
            Destroy(LevelObstaclesTop[0].gameObject);
            LevelObstaclesBottom.RemoveAt(0);
            LevelObstaclesTop.RemoveAt(0);

            float xPos = LevelObstaclesBottom[LevelObstaclesBottom.Count - 1].transform.position.x + ObstaclesPrefabs[0].GetComponent<Renderer>().bounds.size.x; //ObstaclePrefab[0] will need changing to the current obstacle being instantiated (Random)
            float yPos = LevelObstaclesBottom[LevelObstaclesBottom.Count - 1].transform.position.y;
            Vector2 SpawnPos = new Vector2(xPos, yPos);
            LevelObstaclesBottom.Add(Instantiate(ObstaclesPrefabs[0], SpawnPos, Quaternion.identity, ObstaclesParentBottom));

            xPos = LevelObstaclesTop[LevelObstaclesTop.Count - 1].transform.position.x + ObstaclesPrefabs[0].GetComponent<Renderer>().bounds.size.x; //ObstaclePrefab[0] will need changing to the current obstacle being instantiated (Random)
            yPos = LevelObstaclesTop[LevelObstaclesTop.Count - 1].transform.position.y;
            SpawnPos = new Vector2(xPos, yPos);
            LevelObstaclesTop.Add(Instantiate(ObstaclesPrefabs[0], SpawnPos, Quaternion.identity, ObstaclesParentTop));
        }

    }
    #endregion
}
