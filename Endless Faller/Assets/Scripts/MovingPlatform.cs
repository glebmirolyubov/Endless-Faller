using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    const float TOTAL_PLATFORM_WIDTH = 12f;

    public Transform leftPlatformAnchor, rightPlatformAnchor;

    private float speed;

    private void OnEnable()
    {
        speed = GameManager.Instance.IncreasePlatformsSpeed();

        RandomlyGeneratePlatformShape(GameManager.Instance.gameSettingsSO.initialPlatformGap);
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.up * speed;
    }

    private void RandomlyGeneratePlatformShape(float platformGap)
    {
        float platformLengthAllowance = TOTAL_PLATFORM_WIDTH - platformGap;

        float leftPlatformLength = Random.Range(0, platformLengthAllowance);
        float rightPlatformLength = platformLengthAllowance - leftPlatformLength;

        leftPlatformAnchor.localScale = new Vector3(leftPlatformLength, 1f, 1f);
        rightPlatformAnchor.localScale = new Vector3(rightPlatformLength, 1f, 1f);
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
            gameObject.SetActive(false);
        }
    }
}