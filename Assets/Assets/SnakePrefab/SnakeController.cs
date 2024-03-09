using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
public class SnakeController : MonoBehaviour
{

    // Settings
    public float MoveSpeed ;
  [SerializeField]  private float LastSurvivorSpeed;
    public float SteerSpeed = 180;
    public float BodySpeed = 3.5f;
    public int Gap = 10;
    public Transform tail_object;
    // References
    private Transform startPos;
    public GameObject DeathEffect;
    Food survivor;
    // Lists
   [SerializeField] private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {


        Move();
        HandleInput();
        GameOn();
    }
    private void GameOn()
    {
        if (ScoreManager.Instance.isPLaying)
        {
            MoveSpeed = 3.5f;
        }
        if (ScoreManager.Instance.isDead)
        {
            MoveSpeed = 0f;
            
            BodyParts.Clear();

        }
        if (ScoreManager.Instance.isWin)
        {
            MoveSpeed = 0f;
            foreach (var part in BodyParts)
            {
                var survivor = part.GetComponent<Food>();
                survivor.WIN();
            }
        }
    }
    private void Move()
    {
        // Move forward
        transform.position += transform.right * MoveSpeed * Time.deltaTime;


        // Store position history
        PositionsHistory.Insert(0, tail_object.position);
        MoveBodyParts();
    }
    private void HandleInput()
    {

        // Steer
        float steerDirection = Input.GetAxis("Horizontal"); // Returns value -1, 0, or 1
        transform.Rotate(Vector3.forward * -steerDirection * SteerSpeed * Time.deltaTime);
    }
    private void MoveBodyParts()
    {
        // Move body parts
        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];

            // Move body towards the point along the snake's path
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection.normalized * BodySpeed * Time.deltaTime;

            // Rotate body towards the point along the snake's path
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            body.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            index++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
     
            if (collision.gameObject.CompareTag("survivor"))
        {
            survivor = collision.gameObject.GetComponent<Food>();

            if (!survivor.isAdded)
            {

                survivor.LightChange();
                survivor.gameObject.layer = LayerMask.NameToLayer("Survivor");
                GrowSnake(collision.gameObject);

                survivor.isAdded = true;
            }
        }
        else
        {
            return;
        }

        
    }
    public void GrowSnake(GameObject survivor)
    {
        if (!BodyParts.Contains(survivor))
        {
            // Add the survivor to the list
            BodyParts.Add(survivor); 
            ScoreManager.Instance.IncreaseSurvivorScore(1);

        }
    }
    public void ShrinkSnake(GameObject Enemy)
    {
        if (BodyParts.Count > 0)
        {
            GameObject survivorInLast = BodyParts[BodyParts.Count - 1];
            BodyParts.RemoveAt(BodyParts.Count - 1);
            survivorInLast.transform.position = Vector2.MoveTowards(survivorInLast.transform.position, Enemy.transform.position, 5f );
            ScoreManager.Instance.DecreaseSurvivorScore(1);
            Destroy(survivorInLast ); 

            Destroy(Enemy);
        }
        else
        {
            Instantiate(DeathEffect, transform.position, transform.rotation);
          Destroy(Enemy );
            ScoreManager.Instance.GameOver();
            
        }
    }
}
