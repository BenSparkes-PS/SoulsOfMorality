using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourFlipper : MonoBehaviour
{
    public static ColourFlipper Instance { get; private set; }


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
        }
    }
}
