using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rocketBody;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;

    // Start is called before the first frame 
    void Start()
    {
        rocketBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessRotation();
        ProcessThrust();
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
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rocketBody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        }
    }
}
