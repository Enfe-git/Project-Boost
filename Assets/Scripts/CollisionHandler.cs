using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentLevel;
    int maxLevel;
    [SerializeField] float invokeDelay = 1.5f;

    private void Start() 
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        maxLevel = SceneManager.sceneCountInBuildSettings - 1;
    }
    

    void OnCollisionEnter(Collision collision) 
    {
        switch(collision.gameObject.tag) {
            case "Friendly":
                Debug.Log("Object is friendly");
                break;
            case "Finish":
                if (currentLevel < maxLevel) {
                    Invoke("LoadNextLevel", invokeDelay);
                } else {
                    Debug.Log("gg");
                }
                break;
            default:
                StartCrashSequence();
                break;
        }
        
    }

    void StartCrashSequence() {
        GetComponent<Movement>().enabled = false;
        Invoke("LoadFirstLevel", invokeDelay);
    }

    private void LoadFirstLevel() {
        SceneManager.LoadScene(0);
    }

    void LoadNextLevel() {
        SceneManager.LoadScene(currentLevel + 1);

    }
}
