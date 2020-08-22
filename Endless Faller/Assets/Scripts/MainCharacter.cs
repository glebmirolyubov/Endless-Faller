﻿using System.Collections;
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

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetCharacterStartingPosition();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
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

    public void SetCharacterStartingPosition()
    {
        transform.position = GameManager.Instance.gameSettingsSO.playerStartPosition;
    }

    // This is called whenever the Player exits the bounds of OnScreenBounds
    private void OnTriggerExit(Collider other)
    {
        // NOTE: OnTriggerExit is still called when this.enabled==false
        if (!enabled)
        {
            return;
        }

        if (other.gameObject.CompareTag("Screen Bounds"))
        {
            LevelManager.Instance.GameOver();
        }

        if (other.gameObject.CompareTag("Platform"))
        {
            LevelManager.Instance.IncrementScore();
        }
    }
}