using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MainCharacter : MonoBehaviour
{
    // Private singleton for MainCharacter
    static private MainCharacter _Instance;
    static public MainCharacter Instance
    {
        get
        {
            return _Instance;
        }
        private set
        {
            if (_Instance != null)
            {
                Debug.LogWarning("Second attempt to set PlayerShip singleton _S.");
            }
            _Instance = value;
        }
    }

    [SerializeField]
    private float speed;

    void Awake()
    {
        Instance = this;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed;
        }
    }

    // This is called whenever the Player exits the bounds of OnScreenBounds
    private void OnTriggerExit(Collider other)
    {
        // NOTE: OnTriggerExit is still called when this.enabled==false
        if (!enabled)
        {
            return;
        }

        Debug.Log("Player has left the screen, so Game Over");
    }
}