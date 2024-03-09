using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject Particleeffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Enemy")))
        {
            Instantiate(Particleeffect, other.gameObject.transform.position, other.gameObject.transform.rotation);

            Destroy(other.gameObject);
            ScoreManager.Instance.IncreaseEnemyScore(1);       
            
        }

        if (other.gameObject.CompareTag("survivor"))
        {
            

                Instantiate(Particleeffect, other.gameObject.transform.position, other.gameObject.transform.rotation);

                Destroy(other.gameObject);

            
        }
    }

    
}
