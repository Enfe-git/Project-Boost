using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) 
    {
        switch(collision.gameObject.tag) {
            case "Friendly":
                Debug.Log("Object is friendly");
                break;
            case "Finish":
                Debug.Log("Finished");
                break;
            default:
                Debug.Log("Bumped into an obstacle");
                break;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
