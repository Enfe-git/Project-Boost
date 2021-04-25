using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float invokeDelay = 1.5f;
    [SerializeField] AudioClip playerCrash;
    [SerializeField] AudioClip playerSuccess;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource rocketAudio;
    int currentLevel;
    int maxLevel;
    bool isTransitioning = false;

    private void Start() 
    {
        rocketAudio = GetComponent<AudioSource>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        maxLevel = SceneManager.sceneCountInBuildSettings - 1;
    }
    

    void OnCollisionEnter(Collision collision) 
    {
        if (!isTransitioning) 
        {
            switch (collision.gameObject.tag) 
            {
                case "Friendly":
                    Debug.Log("Object is friendly");
                    break;
                case "Finish":
                    isTransitioning = true;
                    StartSuccessSequence();
                    break;
                default:
                    isTransitioning = true;
                    StartCrashSequence();
                    break;
            }
        }
    }


    private void StartSuccessSequence() {
        rocketAudio.Stop();
        successParticles.Play();
        AudioSource.PlayClipAtPoint(playerSuccess, new Vector3(0, 0, 0));
        GetComponent<Movement>().enabled = false;
        if (currentLevel < maxLevel) {
            Invoke("LoadNextLevel", invokeDelay);
        }
        else {
            Debug.Log("gg");
        }
    }

    void StartCrashSequence() {
        rocketAudio.Stop();
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        rocketAudio.PlayOneShot(playerCrash, 0.3f);
        Invoke("LoadFirstLevel", invokeDelay);
    }

    private void LoadFirstLevel() {
        SceneManager.LoadScene(0);
    }

    void LoadNextLevel() {
        SceneManager.LoadScene(currentLevel + 1);

    }
}
