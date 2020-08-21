using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: If Camera.main is going to move or rotate at all, then it will need to
//  have a Rigidbody attached so that the physics engine properly updates the 
//  position and rotation of this BoxCollider.

/// <summary>
/// This class should be attached to a child of Camera.main. It triggers various
///  behaviors to happen when a GameObject exits the screen.<para/>
/// NOTE: This GameObject must have a BoxCollider attached.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class ScreenBounds : MonoBehaviour
{
    static private ScreenBounds S; // Private but unprotected Singleton.

    public float zScale = 10;

    // Keep perspective camera's field of view constant so that I can correctly calculate the screen bounds.
    const int fieldOfView = 60;

    Camera cam;
    BoxCollider boxColl;
    float cachedOrthographicSize, cachedAspect;
    Vector3 cachedCamScale;


    void Awake()
    {
        S = this;

        cam = Camera.main;
        cam.fieldOfView = fieldOfView;

        // No need to check whether boxColl is null because of RequireComponent above.
        boxColl = GetComponent<BoxCollider>();
        // Setting boxColl.size to 1 ensures that other calculations will be correct.
        boxColl.size = Vector3.one;

        transform.position = Vector3.zero;
        ScaleSelf();
    }

    private void Update()
    {
        ScaleSelf();
    }

    // Scale this Transform to match what cam can see.
    private void ScaleSelf()
    {
        // Check here to see whether anything has changed about cam.orthographicSize
        //  or cam.aspect. If those values are the same as cached, then there is no
        //  need to change the localScale of this.transform.
        if (cam.orthographicSize != cachedOrthographicSize || cam.aspect != cachedAspect
            || cam.transform.localScale != cachedCamScale)
        {
            transform.localScale = CalculateScale();
        }
    }

    private Vector3 CalculateScale()
    {
        cachedOrthographicSize = cam.orthographicSize;
        cachedAspect = cam.aspect;
        cachedCamScale = cam.transform.localScale;

        Vector3 scaleDesired, scaleColl;

        scaleDesired.z = zScale;
        scaleDesired.y = cam.orthographicSize * 2;
        scaleDesired.x = scaleDesired.y * cam.aspect;

        // This line makes use of the Vector3 extension method defined in Vector3Extensions
        scaleColl = scaleDesired.ComponentDivide(cachedCamScale);

        return scaleColl;
    }
}