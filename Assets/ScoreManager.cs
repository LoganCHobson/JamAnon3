using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public List<AudioClip> clipList;
    private AudioSource audioSource;
    public TMP_Text scoreText;
    public TMP_Text moneyText;
    public int score;

    public Queue<GameObject> scoreEffectQueue = new Queue<GameObject>();
    public Queue<GameObject> moneyEffectQueue = new Queue<GameObject>();

    public GameObject scoreEffectPrefab;
    public GameObject moneyEffectPrefab;
    public Canvas canvas;

    public static ScoreManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString("N0");
    }

    public void AddScore(int points)
    {
        int rand = Random.Range(0, clipList.Count);
        audioSource.clip = clipList[rand];
        audioSource.Play();
        score += points;
        EnqueueScoreEffect("+" + points, scoreText.transform.position);
    }

    public void AddMoney(int money)
    {
        EnqueueMoneyEffect("+" + money, moneyText.transform.position);
    }

    private void EnqueueScoreEffect(string scoreValue, Vector3 targetPosition)
    {
        GameObject effect = Instantiate(scoreEffectPrefab, canvas.transform);
        TMP_Text effectText = effect.GetComponentInChildren<TMP_Text>();
        effectText.text = scoreValue;
        PositionEffectAbove(effect.GetComponent<RectTransform>(), targetPosition);
        scoreEffectQueue.Enqueue(effect);
        ScoreEffect scoreEffectAnimation = effect.GetComponent<ScoreEffect>();
        scoreEffectAnimation.onEffectComplete.AddListener(DequeueEffect);
    }

    private void EnqueueMoneyEffect(string moneyValue, Vector3 targetPosition)
    {
        GameObject effect = Instantiate(moneyEffectPrefab, canvas.transform);
        TMP_Text effectText = effect.GetComponentInChildren<TMP_Text>();
        effectText.text = moneyValue;
        PositionEffectAbove(effect.GetComponent<RectTransform>(), targetPosition);
        moneyEffectQueue.Enqueue(effect);
        ScoreEffect moneyEffectAnimation = effect.GetComponent<ScoreEffect>();
        moneyEffectAnimation.onEffectComplete.AddListener(DequeueMoneyEffect);
    }

    private void PositionEffectAbove(RectTransform effectTransform, Vector3 targetPosition)
    {
        Vector3 abovePosition = new Vector3(targetPosition.x + 100f, targetPosition.y + 50f, targetPosition.z);
        effectTransform.position = abovePosition;
    }

    private void DequeueEffect()
    {
        if (scoreEffectQueue.Count > 0)
        {
            GameObject effect = scoreEffectQueue.Dequeue();
            Destroy(effect);
        }
    }

    private void DequeueMoneyEffect()
    {
        if (moneyEffectQueue.Count > 0)
        {
            GameObject effect = moneyEffectQueue.Dequeue();
            Destroy(effect);
        }
    }
}
