using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLinker : MonoBehaviour
{
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void ShowExplain(GameObject NextExplain)
    {
        NextExplain.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
}
