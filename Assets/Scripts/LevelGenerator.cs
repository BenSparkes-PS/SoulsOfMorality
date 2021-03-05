using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public static LevelGenerator Instance { get; private set; }

    [Header("Obstacle Prefabs")]
    public GameObject[] ObstaclesPrefabsLight;
    public GameObject[] ObstaclesPrefabsDark;
    public GameObject GoodSoulPrefab;
    public GameObject BadSoulPrefab;
    public List<GameObject> LevelObstaclesBottomLight = new List<GameObject>();
    public List<GameObject> LevelObstaclesTopLight = new List<GameObject>();
    public List<GameObject> LevelObstaclesBottomDark = new List<GameObject>();
    public List<GameObject> LevelObstaclesTopDark = new List<GameObject>();

    //Level Generation variables
    public Vector2 BottomLeftScreenReference;
    public Vector2 TopLeftScreenReference;
    private Transform ObstaclesParentBottom;
    private Transform ObstaclesParentTop;
    private Transform CollectablesParent;
    private int LastPrefabBottom;
    private int LastPrefabTop;


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


    public void GenerateLevel()
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
            float RandDouble = Random.Range(0.0f, 1.0f);
            if (RandDouble < 1)
                RandPrefabBottom = 0;
            if (RandDouble < 0.7)
                RandPrefabBottom = 2;
            if (RandDouble < 0.5)
                RandPrefabBottom = 3;
            if (RandDouble < 0.3)
                RandPrefabBottom = 1;
            if (RandDouble < 0.15)
                RandPrefabBottom = 5;
            if (RandDouble < 0.1)
                RandPrefabBottom = 4;

            while (RandPrefabBottom == LastPrefabBottom)
            {
                RandPrefabBottom = Random.Range(0, ObstaclesPrefabsLight.Length - 1);
            }
            SoulCanBePlaced = true;

        }
        if (RandPrefabTop == -1)
        {
            float RandDouble = Random.Range(0.0f, 1.0f);
            if (RandDouble < 1)
                RandPrefabTop = 0;
            if (RandDouble < 0.7)
                RandPrefabTop = 2;
            if (RandDouble < 0.5)
                RandPrefabTop = 3;
            if (RandDouble < 0.3)
                RandPrefabTop = 1;
            if (RandDouble < 0.15)
                RandPrefabTop = 5;
            if (RandDouble < 0.1)
                RandPrefabTop = 4;
            SoulCanBePlaced = true;
            while (RandPrefabTop == LastPrefabTop)
            {
                RandPrefabTop = Random.Range(0, ObstaclesPrefabsLight.Length - 1);
            }
        }

        if((LastPrefabBottom == 4 || LastPrefabTop == 4) && RandPrefabBottom != 6)
        {
            RandPrefabBottom = 0;
            RandPrefabTop = 0;
        }
        if(RandPrefabBottom == 4)
        {
            RandPrefabTop = 0;
        }
        if (RandPrefabTop == 4)
        {
            RandPrefabBottom = 0;
        }

        LastPrefabBottom = RandPrefabBottom;
        LastPrefabTop = RandPrefabTop;

        float xPos;
        float yPos;
        Vector2 SpawnPos;
        //Bottom positioning
        if (!first)
        {
            xPos = LevelObstaclesBottomLight[LevelObstaclesBottomLight.Count - 1].transform.position.x + (LevelObstaclesBottomLight[LevelObstaclesBottomLight.Count - 1].GetComponent<Renderer>().bounds.size.x / 2) + ObstaclesPrefabsLight[RandPrefabBottom].GetComponent<Renderer>().bounds.size.x / 2;
            yPos = LevelObstaclesBottomLight[LevelObstaclesBottomLight.Count - 1].transform.position.y;
            SpawnPos = new Vector2(xPos, yPos);
        }
        else
        {
            SpawnPos = new Vector2(BottomLeftScreenReference.x + (ObstaclesPrefabsLight[RandPrefabBottom].GetComponent<Renderer>().bounds.size.x / 2), BottomLeftScreenReference.y + (ObstaclesPrefabsLight[RandPrefabBottom].GetComponent<Renderer>().bounds.size.y / 2));
        }
        LevelObstaclesBottomLight.Add(Instantiate(ObstaclesPrefabsLight[RandPrefabBottom], SpawnPos, Quaternion.identity, ObstaclesParentBottom));
        LevelObstaclesBottomLight[LevelObstaclesBottomLight.Count - 1].GetComponent<LevelSection>().isBottom = true;
        LevelObstaclesBottomDark.Add(Instantiate(ObstaclesPrefabsDark[RandPrefabBottom], SpawnPos, Quaternion.identity, ObstaclesParentBottom));
        LevelObstaclesBottomDark[LevelObstaclesBottomDark.Count - 1].GetComponent<LevelSection>().isBottom = true;
        //Top positioning
        if (!first)
        {
            xPos = LevelObstaclesTopLight[LevelObstaclesTopLight.Count - 1].transform.position.x + (LevelObstaclesTopLight[LevelObstaclesTopLight.Count - 1].GetComponent<Renderer>().bounds.size.x / 2) + ObstaclesPrefabsLight[RandPrefabTop].GetComponent<Renderer>().bounds.size.x / 2;
            yPos = LevelObstaclesTopLight[LevelObstaclesTopLight.Count - 1].transform.position.y;
            SpawnPos = new Vector2(xPos, yPos);
        }
        else
        {
            SpawnPos = new Vector2(TopLeftScreenReference.x + (ObstaclesPrefabsLight[RandPrefabTop].GetComponent<Renderer>().bounds.size.x / 2), TopLeftScreenReference.y - (ObstaclesPrefabsLight[RandPrefabTop].GetComponent<Renderer>().bounds.size.y * 2));
        }
        LevelObstaclesTopLight.Add(Instantiate(ObstaclesPrefabsLight[RandPrefabTop], SpawnPos, Quaternion.identity, ObstaclesParentTop));
        LevelObstaclesTopLight[LevelObstaclesTopLight.Count - 1].transform.localScale = new Vector3(LevelObstaclesTopLight[LevelObstaclesTopLight.Count - 1].transform.localScale.x, -1, 1);
        LevelObstaclesTopDark.Add(Instantiate(ObstaclesPrefabsDark[RandPrefabTop], SpawnPos, Quaternion.identity, ObstaclesParentTop));
        LevelObstaclesTopDark[LevelObstaclesTopDark.Count - 1].transform.localScale = new Vector3(LevelObstaclesTopDark[LevelObstaclesTopDark.Count - 1].transform.localScale.x, -1, 1);


        if (LevelManager.Instance.isFlipped)
        {
            LevelObstaclesBottomLight[LevelObstaclesBottomLight.Count - 1].SetActive(false);
            LevelObstaclesTopLight[LevelObstaclesTopLight.Count - 1].SetActive(false);
        }
        else
        {
            LevelObstaclesBottomDark[LevelObstaclesBottomDark.Count - 1].SetActive(false);
            LevelObstaclesTopDark[LevelObstaclesTopDark.Count - 1].SetActive(false);
        }



        if (SoulCanBePlaced)
        {
            //Soul Pickup Spawning
            float DoSpawnSoul = Random.Range(0.0f, 1.0f);
            float SoulType = Random.Range(0.0f, 1.0f);
            GameObject NewSoul;
            if (DoSpawnSoul > 0.6f && LevelObstaclesTopLight[LevelObstaclesTopLight.Count - 1].transform.Find("SoulSpawnPoint"))
            {
                if (SoulType > 0.5f)
                {
                    NewSoul = Instantiate(GoodSoulPrefab, LevelObstaclesTopLight[LevelObstaclesTopLight.Count - 1].transform.Find("SoulSpawnPoint").position, Quaternion.identity, CollectablesParent);
                    NewSoul.GetComponent<Souls>().SoulType = 1;
                }
                else
                    Instantiate(BadSoulPrefab, LevelObstaclesTopLight[LevelObstaclesTopLight.Count - 1].transform.Find("SoulSpawnPoint").position, Quaternion.identity, CollectablesParent);
            }
            DoSpawnSoul = Random.Range(0.0f, 1.0f);
            SoulType = Random.Range(0.0f, 1.0f);
            if (DoSpawnSoul > 0.6f && LevelObstaclesBottomLight[LevelObstaclesBottomLight.Count - 1].transform.Find("SoulSpawnPoint"))
            {
                if (SoulType > 0.5f)
                {
                    NewSoul = Instantiate(GoodSoulPrefab, LevelObstaclesBottomLight[LevelObstaclesBottomLight.Count - 1].transform.Find("SoulSpawnPoint").position, Quaternion.identity, CollectablesParent);
                    NewSoul.GetComponent<Souls>().SoulType = 1;
                }
                else
                    Instantiate(BadSoulPrefab, LevelObstaclesBottomLight[LevelObstaclesBottomLight.Count - 1].transform.Find("SoulSpawnPoint").position, Quaternion.identity, CollectablesParent);
            }
        }
    }

    public void DestroyFirstBottom()
    {
        Destroy(LevelObstaclesBottomLight[0].gameObject);
        LevelObstaclesBottomLight.RemoveAt(0);
        Destroy(LevelObstaclesBottomDark[0].gameObject);
        LevelObstaclesBottomDark.RemoveAt(0);
    }
    public void DestroyFirstTop()
    {
        Destroy(LevelObstaclesTopLight[0].gameObject);
        LevelObstaclesTopLight.RemoveAt(0);
        Destroy(LevelObstaclesTopDark[0].gameObject);
        LevelObstaclesTopDark.RemoveAt(0);
    }

    public void DestroyLevel()
    {
        Destroy(ObstaclesParentBottom.gameObject);
        Destroy(ObstaclesParentTop.gameObject);
        Destroy(CollectablesParent.gameObject);
        LevelObstaclesBottomLight.Clear();
        LevelObstaclesBottomDark.Clear();
        LevelObstaclesTopLight.Clear();
        LevelObstaclesTopDark.Clear();
    }
}
