using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

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
    }



    private void ProcessRotation() {
        if (Input.GetKey(KeyCode.A)) {
            RotateLeft();

        }
        else if (Input.GetKey(KeyCode.D)) {
            RotateRight();
        }
        else {
            StopRotating();
        }
    }


    private void ApplyRotation(float rotationThisFrame) {
        rocketBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rocketBody.freezeRotation = false;
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            StartThrusting();
        }
        else {
            StopThrusting();
        }
    }

    private void StartThrusting() {
        rocketBody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        if (!rocketAudio.isPlaying) {
            rocketAudio.PlayOneShot(mainEngine);
        }
        if (!mainBoosterParticles.isPlaying) {
            mainBoosterParticles.Play();
        }
    }

    private void StopThrusting() {
        rocketAudio.Stop();
        mainBoosterParticles.Stop();
    }

    private void RotateRight() {
        ApplyRotation(-rotationThrust);
        if (!rightBoosterParticles.isPlaying) {
            rightBoosterParticles.Play();
        }
    }

    private void RotateLeft() {
        ApplyRotation(rotationThrust);
        if (!leftBoosterParticles.isPlaying) {
            leftBoosterParticles.Play();
        }
    }

    private void StopRotating() {
        rightBoosterParticles.Stop();
        leftBoosterParticles.Stop();
    }
}
