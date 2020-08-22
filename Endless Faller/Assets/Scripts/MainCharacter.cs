using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MainCharacter : MonoBehaviour
{
    // Private singleton for MainCharacter
    static private MainCharacter _Instance;

    [SerializeField]
    private float speed;
    private Rigidbody rb;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        SetCharacterStartState();
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

    public void SetCharacterStartState()
    {
        transform.position = GameManager.Instance.gameSettingsSO.playerStartPosition;
        rb.velocity = Vector3.zero;
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

    // ---------------- Static Section ---------------- //

    /// <summary>
    /// <para>This static public property provides some protection for the Singleton _Instance.</para>
    /// <para>get {} does return null, but throws an error first.</para>
    /// <para>set {} allows overwrite of _Instance by a 2nd instance, but throws an error first.</para>
    /// </summary>
    static public MainCharacter Instance
    {
        get
        {
            if (_Instance == null)
            {
                Debug.LogError("MainCharacter:Instance getter - Attempt to get value of Instance before it has been set.");
                return null;
            }
            return _Instance;
        }
        private set
        {
            if (_Instance != null)
            {
                Debug.LogError("MainCharacter:Instance setter - Attempt to set Instance when it has already been set.");
            }
            _Instance = value;
        }
    }
}