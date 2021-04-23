using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    Rigidbody rocketBody;
    AudioSource rocketAudio;

    public bool isTransitioning = false;

    // Start is called before the first frame 
    void Start()
    {
        rocketBody = GetComponent<Rigidbody>();
        rocketAudio = GetComponent<AudioSource>();
    }

    //Update is called once per frame
    void Update()
    {
            ProcessRotation();
            ProcessThrust();
            GetComponent<CollisionHandler>().enabled = false;
    }

    private void ProcessRotation() {
        if (Input.GetKey(KeyCode.A)) {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D)) {
            ApplyRotation(-rotationThrust);        
        }
    }

    private void ApplyRotation(float rotationThisFrame) {
        rocketBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rocketBody.freezeRotation = false;
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rocketBody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
            if (!rocketAudio.isPlaying) {
                rocketAudio.PlayOneShot(mainEngine);
            }
        }
        else {
            rocketAudio.Stop();
        }
    }
}
