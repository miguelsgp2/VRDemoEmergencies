using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreloadManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("loadNextScene", 1.01f);
    }

    private void loadNextScene()
    {
        SceneFader.instance.LoadSceneFading("MainMenuScene");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
