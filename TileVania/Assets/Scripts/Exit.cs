using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine("LoadNexrScene");
    }
    IEnumerator LoadNexrScene()
    {
    int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        yield return new WaitForSeconds(1f);
        if(nextScene==SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        FindObjectOfType<ScenePersist>().ResetGamepersist();

        SceneManager.LoadScene(nextScene);
    }
}
