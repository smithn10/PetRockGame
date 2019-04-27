using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugLevelChanger : MonoBehaviour
{
    private float timer = 0f;
    private string[] levels = new string[7];

    // Start is called before the first frame update
    void Start()
    {

        levels[0] = "Main Menu Scene";
        levels[1] = "Level0";
        levels[2] = "Level1";
        levels[3] = "Level2";
        levels[4] = "level3";
        levels[5] = "Level4";
        levels[6] = "Final Cutscene";
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 7; i++)
        {
            if (Input.GetKeyDown("f" + (i + 1)))
            {
                if (timer > 0)
                    SceneManager.LoadScene(levels[i]);
                else
                    timer = 3;
            }
        }
        timer -= Time.deltaTime;
    }
}
