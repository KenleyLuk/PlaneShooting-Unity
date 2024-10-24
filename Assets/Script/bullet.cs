using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 10f;
    // public float rotationSpeed = 180f;
    // public GameObject playerBullet;
    // public delegate void BlockEventHandler();
    // public static event BlockEventHandler BlockDestroyed; // Event to notify when a block is destroyed

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        // if (transform.position.x > 0)
        // {
            // float step = rotationSpeed * Time.deltaTime;
            // Vector3 targetDirection = Vector3.up;
            // Vector3 newDirection = Vector3.RotateTowards(transform.right, targetDirection, step, 0.0f);
            // transform.rotation = Quaternion.LookRotation(newDirection, Vector3.forward);
        // }

        if(transform.position.y > 9f)
        {
            Debug.Log("Hello, this is a message logged in Unity!" + transform.position.y);
            Destroy(gameObject);
            // BulletDestroyed?.Invoke(); // Invoke the BlockDestroyed event
        }
    }
}
