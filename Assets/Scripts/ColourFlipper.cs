using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourFlipper : MonoBehaviour
{
    public static ColourFlipper Instance { get; private set; }

    public GameObject EvilBackground;
    public GameObject GoodBackground;

    void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;
    }


    public void FlipColour(bool flipped)
    {
        if (flipped)
        {
            foreach (GameObject section in LevelGenerator.Instance.LevelObstaclesBottomLight)
            {
                section.SetActive(false);
            }
            foreach (GameObject section in LevelGenerator.Instance.LevelObstaclesTopLight)
            {
                section.SetActive(false);
            }
            foreach (GameObject section in LevelGenerator.Instance.LevelObstaclesBottomDark)
            {
                section.SetActive(true);
            }
            foreach (GameObject section in LevelGenerator.Instance.LevelObstaclesTopDark)
            {
                section.SetActive(true);
            }
            EvilBackground.transform.localPosition = new Vector3(EvilBackground.transform.position.x, EvilBackground.transform.position.y, 0);
            GoodBackground.transform.localPosition = new Vector3(EvilBackground.transform.position.x, EvilBackground.transform.position.y, 1);

        }
        else
        {
            foreach (GameObject section in LevelGenerator.Instance.LevelObstaclesBottomLight)
            {
                section.SetActive(true);
            }
            foreach (GameObject section in LevelGenerator.Instance.LevelObstaclesTopLight)
            {
                section.SetActive(true);
            }
            foreach (GameObject section in LevelGenerator.Instance.LevelObstaclesBottomDark)
            {
                section.SetActive(false);
            }
            foreach (GameObject section in LevelGenerator.Instance.LevelObstaclesTopDark)
            {
                section.SetActive(false);
            }
            EvilBackground.transform.localPosition = new Vector3(EvilBackground.transform.position.x, EvilBackground.transform.position.y, 1);
            GoodBackground.transform.localPosition = new Vector3(EvilBackground.transform.position.x, EvilBackground.transform.position.y, 0);
        }
    }
}
