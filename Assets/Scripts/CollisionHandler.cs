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
    bool collisionDisabled = false;

    private void Start() 
    {
        rocketAudio = GetComponent<AudioSource>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        maxLevel = SceneManager.sceneCountInBuildSettings - 1;
    }

    private void Update() {
        if (Debug.isDebugBuild) {
            RespondToDebugKeys();
        }
    }

    private void RespondToDebugKeys() {
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C)) {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (isTransitioning || collisionDisabled) { return; }
        {
            switch (collision.gameObject.tag) 
            {
                case "Friendly":
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
        rocketAudio.PlayOneShot(playerCrash, 0.15f);
        Invoke("LoadFirstLevel", invokeDelay);
    }

    private void LoadFirstLevel() {
        SceneManager.LoadScene(currentLevel);
    }

    void LoadNextLevel() {
        SceneManager.LoadScene(currentLevel + 1);

    }
}
