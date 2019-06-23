using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public string nextSceneName;
    private Scene nextScene;
    // Start is called before the first frame update
    void Start()
    {
        nextScene = SceneManager.GetSceneByName(nextSceneName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision");
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadScene(nextSceneName);
            SceneManager.SetActiveScene(nextScene);
        }
        
    }
    
}
