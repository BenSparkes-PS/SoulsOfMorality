using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSection : MonoBehaviour
{

    public bool isBottom = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (isBottom && (transform.position.x + GetComponent<Renderer>().bounds.size.x) < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
        {
            LevelManager.Instance.CreateNewLevelSection(false);
            LevelManager.Instance.DestroyFirstBottom();
        }
        else if (!isBottom && (transform.position.x + GetComponent<Renderer>().bounds.size.x) < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
        {
            LevelManager.Instance.DestroyFirstTop();
        }
    }
}
