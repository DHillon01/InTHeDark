using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReEnter : MonoBehaviour
{ private float screenWidth;
        private float screenHeight;
        void Start()
        {
            screenWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
            screenHeight = Camera.main.orthographicSize * 2;
        }

        void Update()
        {
            // Update player position based on movement

            if (transform.position.x < -screenWidth / 2)
            {
                // Player exited left side
                transform.position = new Vector3(screenWidth / 2, transform.position.y, 0);
            }
            else if (transform.position.x > screenWidth / 2)
            {
                // Player exited right side
                transform.position = new Vector3(-screenWidth / 2, transform.position.y, 0);
            }
        if (transform.position.y < -screenHeight / 2)
        {
            // Player exited left side
            transform.position = new Vector3(screenHeight / 2, transform.position.x, 0);
        }
        else if (transform.position.y > screenHeight / 2)
        {
            // Player exited right side
            transform.position = new Vector3(-screenHeight / 2, transform.position.x, 0);
        }
        // Similar checks for top and bottom exits

        // ... rest of your game logic
    }
    }
