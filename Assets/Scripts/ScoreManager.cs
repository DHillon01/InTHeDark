using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public int enemyScore; // Stores the current enemy score
    public int survivorScore; // Stores the current survivor score
    public static ScoreManager Instance;
    // Events to update UI or other systems based on score changes
    public event System.Action<int> OnEnemyScoreChange;
    public event System.Action<int> OnSurvivorScoreChange;

    [SerializeField] AudioClip GunShot;
    [SerializeField] AudioClip EnemyDie; [SerializeField] AudioClip EnemyApproach;
    [SerializeField] AudioClip SurvivorDie;
    [SerializeField] AudioClip SurvivorSaved;

    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject GamePanel;
    [SerializeField] GameObject GameoverPanel;
    public bool isStart;
        public bool isPLaying;
        public bool isDead;
        public bool isWin;

    // Start is called before the first frame update
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
        else
        {
            Instance = this;
            isStart = true;
        }


    }
    void Start()
    {
        enemyScore = 0;
        survivorScore = 0;
        isStart = true;isPLaying = false;isDead = false;isWin = false;
    }

    public void PlayGunShot()
    {
        audioSource.volume = 0.5f;

        audioSource.PlayOneShot(GunShot);

    }

    // Increase enemy score

    public void IncreaseEnemyScore(int points)
    {
        enemyScore += points;
        OnEnemyScoreChange?.Invoke(enemyScore); // Fire the event if any listeners are registered
        audioSource.PlayOneShot(EnemyDie);
        audioSource.volume = 1f;
    }

    // Decrease enemy score (use with caution)

    // Increase survivor score
    public void IncreaseSurvivorScore(int points)
    {
        survivorScore += points;
        OnSurvivorScoreChange?.Invoke(survivorScore);
        audioSource.PlayOneShot(SurvivorSaved);
        audioSource.volume = 1f;
        GameWin();

    }

    // Decrease survivor score (use with caution)
    public void DecreaseSurvivorScore(int points)
    {
        survivorScore -= points;
        survivorScore = Mathf.Clamp(survivorScore, 0, int.MaxValue); // Ensure score doesn't go negative
        OnSurvivorScoreChange?.Invoke(survivorScore); audioSource.PlayOneShot(SurvivorDie); audioSource.volume = 1f;


    }
    public void GameStart()
    {
        isPLaying = true;
        isDead = false;
        isWin = false;
    }
    private void GameWin()
    {
        if (survivorScore >= 6)
        {
            isPLaying = false;
            isWin = true;
            GameoverPanel.SetActive(true);
        }
    }

    public void GameOver()
    {
        isPLaying = false;
        isDead = true;
        GameoverPanel.SetActive(true);

    }
    public void EnemyApproch()
    {
        audioSource.PlayOneShot(EnemyApproach);
    }

    // Accessors for reading the scores
    public int GetEnemyScore()
    {
        return enemyScore;
    }

    public int GetSurvivorScore()
    {
        return survivorScore;
    }

    public void PlayAgain(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
