using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 targetPosition;
  [SerializeField]  private bool isChasing = false;
    public GameObject player;
    Animator animator; [SerializeField] GameObject MonsterHuntEffect;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (isChasing)
        {
            animator.StopPlayback(); 
            // Move towards the target position (player's position)
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            Vector2 direction = (targetPosition - transform.position).normalized;

            // Calculate the angle to rotate the enemy
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the enemy to face the target

            transform.rotation = Quaternion.AngleAxis(angle+75, Vector3.forward);


            // If the enemy has reached the target position, stop chasing
            if (transform.position == targetPosition)
            {
                isChasing = false;
                Capture(); 

                transform.position = Vector2.MoveTowards(transform.position, targetPosition , moveSpeed * Time.deltaTime);

            }
        }
    }

    public void Capture()
    {
        if (!isChasing)
        {
            Debug.Log("Survivor taken");
            Instantiate(MonsterHuntEffect, transform.position, transform.rotation);
            player.GetComponent<SnakeController>().ShrinkSnake(this.gameObject);

        }
    }
    public void Chase(GameObject target)
    {
        player = target;
        targetPosition = target.transform.position;
        isChasing = true;
    }


}
