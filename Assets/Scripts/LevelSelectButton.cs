using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    public string levelToLoad;


    public void MenuLoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
        //		if (SceneManager.GetSceneByName (levelToLoad).IsValid ())
        //		{
        //			SceneManager.LoadScene (levelToLoad);
        //		}
        //		else
        //		{
        //			Debug.LogError ("Attempted to load invalid scene: " + levelToLoad);
        //			return;
        //		}		
    }
}
