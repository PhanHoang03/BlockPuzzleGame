using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCtrl : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI scoreText;
    [SerializeField] protected TextMeshProUGUI highScoreText;
    [SerializeField] protected int score;
    public int Score => score;
    [SerializeField] protected int highScore;
    [SerializeField] protected bool isHighScore = false;
    public bool IsHighScore => isHighScore;

    void Awake()
    {
        this.ResetValue();
    }

    public void ResetValue()
    {
        this.score = 0;
        this.highScore = PlayerPrefs.GetInt("HighScore");
        scoreText.text = "Score: " + this.score.ToString();
        highScoreText.text = "High Score: " + this.highScore.ToString();
        Debug.Log(this.highScore);
    }

    void Update()
    {
        this.ChangeText();
    }

    public void AddScore (int amount)
    {
        this.score += amount;
    }

    protected virtual void ChangeText()
    {
        scoreText.text = "Score: " + this.score.ToString();
        this.ChangeHighScore();
        highScoreText.text = "High Score: " + this.highScore.ToString();
    }

    protected virtual void ChangeHighScore()
    {
        if (this.score > this.highScore) 
        {
            this.highScore = this.score;
            this.isHighScore = true;
        }
        PlayerPrefs.SetInt("HighScore", this.highScore);
    }
}
