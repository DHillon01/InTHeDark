using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInContact : MonoBehaviour
{
  
    public GameObject player;
    public Enemy enemy;
    
    public GameObject particleEffect;


    private void Start()
    {
        enemy = gameObject.transform.parent.parent.GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemy.player = player;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            // Start the particle effect
            enemy.Chase(other.gameObject);
            // Destroy the circle
            ScoreManager.Instance.EnemyApproch();
this.gameObject.SetActive(true);
            // Start the enemy chasing the player
        }
    }

}
