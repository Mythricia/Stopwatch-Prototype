using UnityEngine;
using UnityEngine.UI;

public class LevelListAutoPopulate : MonoBehaviour
{
    Transform listTransform;

    public string[] levelsToPopulate;
    public GameObject levelLoadButtonPrefab;


    void Start()
    {
        if (levelsToPopulate == null || levelLoadButtonPrefab == null)
            return;

        listTransform = this.transform;

        foreach (string s in levelsToPopulate)
        {
            GameObject newButton = Instantiate(levelLoadButtonPrefab);
            newButton.transform.SetParent(listTransform);
            newButton.GetComponent<LevelSelectButton>().levelToLoad = s;
            Text btnText = newButton.GetComponentInChildren<Text>();
            btnText.text = s;
        }
    }
}
