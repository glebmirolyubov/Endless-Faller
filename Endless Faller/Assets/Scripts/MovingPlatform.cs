using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    const float TOTAL_PLATFORM_WIDTH = 12f;

    public Transform leftPlatformAnchor, rightPlatformAnchor;

    [SerializeField]
    private float speed;
    private float platformGap;

    private void OnEnable()
    {
        platformGap = GameManager.Instance.gameSettingsSO.initialPlatformGap;

        RandomlyGeneratePlatformShape();
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.up * speed;
    }

    private void RandomlyGeneratePlatformShape()
    {
        float platformLengthAllowance = TOTAL_PLATFORM_WIDTH - platformGap;

        float leftPlatformLength = Random.Range(0, platformLengthAllowance);
        float rightPlatformLength = platformLengthAllowance - leftPlatformLength;

        leftPlatformAnchor.localScale = new Vector3(leftPlatformLength, 1f, 1f);
        rightPlatformAnchor.localScale = new Vector3(rightPlatformLength, 1f, 1f);
    }

    // Wait for some time before disabling the platform after it escapes play area bounds
    // So that it would look nice on the screen to the player.
    IEnumerator DisableGameObject()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
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