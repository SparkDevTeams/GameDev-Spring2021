using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{

    public float waitToLoad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waitToLoad > 0)
        {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0)
            {
                //load into the next scene   SceneManager.LoadScene();

                //Game manager load data
                //Quest manager load data
            }
        }
    }
}
