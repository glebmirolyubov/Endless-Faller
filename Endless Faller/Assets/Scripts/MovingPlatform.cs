using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            transform.position += Vector3.up * speed;
    }
}