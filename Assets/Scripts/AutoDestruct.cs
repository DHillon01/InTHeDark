using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruct : MonoBehaviour
{  [SerializeField] private float selfDestructTime = 5f; // Time in seconds before self-destruction
    [SerializeField] private float screenEdgeOffset = 1f; // Distance from screen edge for out-of-screen check

    private Camera mainCamera;
    private float screenLeftLimit;
    private float screenRightLimit;
    private float screenTopLimit;
    private float screenBottomLimit;

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found! AutoDestruct script may not work as intended.");
            return;
        }

        // Calculate screen limits based on main camera
        screenLeftLimit = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height / 2f, mainCamera.nearClipPlane)).x + screenEdgeOffset;
        screenRightLimit = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2f, mainCamera.nearClipPlane)).x - screenEdgeOffset;
        screenTopLimit = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height, mainCamera.nearClipPlane)).y - screenEdgeOffset;
        screenBottomLimit = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, 0, mainCamera.nearClipPlane)).y + screenEdgeOffset;

        // Start a coroutine to handle timed self-destruction
        StartCoroutine(SelfDestructRoutine());
    }

    private void Update()
    {
        // Check if object is out of screen and destroy it
        if (transform.position.x < screenLeftLimit || transform.position.x > screenRightLimit ||
            transform.position.y > screenTopLimit || transform.position.y < screenBottomLimit)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator SelfDestructRoutine()
    {
        yield return new WaitForSeconds(selfDestructTime);
        Destroy(gameObject);
    }
}
