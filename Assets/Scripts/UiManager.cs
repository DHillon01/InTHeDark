using UnityEngine;
using TMPro; // Assuming you're using TextMeshPro UI
using System.Collections;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI enemyScoreText; // Reference to enemy score UI text
    public TextMeshProUGUI survivorScoreText; // Reference to survivor score UI text
    public TextMeshProUGUI FinalScore;
    public TextMeshProUGUI FinaleSurvivorSaved;

    private void Start()
    {
        // Subscribe to ScoreManager events in Start or Awake
        ScoreManager.Instance.OnEnemyScoreChange += UpdateEnemyScoreUI;
        ScoreManager.Instance.OnSurvivorScoreChange += UpdateSurvivorScoreUI;

        // Optionally, update UI with initial scores (if needed)
        UpdateEnemyScoreUI(ScoreManager.Instance.GetEnemyScore());
        UpdateSurvivorScoreUI(ScoreManager.Instance.GetSurvivorScore());
    }

    private void UpdateEnemyScoreUI(int newScore)
    {
        enemyScoreText.text = "" + newScore;
        StartCoroutine(Pulse_E());
        FinalScore.text = "" + newScore;
    }

    private void UpdateSurvivorScoreUI(int newScore)
    {
        survivorScoreText.text = "" + newScore;
        StartCoroutine(Pulse_S());
        FinaleSurvivorSaved.text = "" + newScore;
    }

    private void OnDestroy() // Unsubscribe from events in OnDestroy (optional)
    {
        ScoreManager.Instance.OnEnemyScoreChange -= UpdateEnemyScoreUI;
        ScoreManager.Instance.OnSurvivorScoreChange -= UpdateSurvivorScoreUI;
    }

    private IEnumerator Pulse_S()
    {
        for (float i = 1f;i<= 1.2f; i += 0.05f)
        {
            survivorScoreText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        survivorScoreText.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        for (float i = 1.2f; i >= 1f;i -= 0.05f)
        {
            survivorScoreText.rectTransform.localScale= new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        survivorScoreText.rectTransform.localScale = new Vector3(1f,1f, 1f);
    }


    private IEnumerator Pulse_E()
    {
        for (float i = 1f; i <= 1.2f; i += 0.05f)
        {
            enemyScoreText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        enemyScoreText.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        for (float i = 1.2f; i >= 1f; i -= 0.05f)
        {
            enemyScoreText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        enemyScoreText.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }
}
