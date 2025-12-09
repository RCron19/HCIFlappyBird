using UnityEngine;

using TMPro;


public class ScoreTracker : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    int score;


    const string scorePrefix = "Score: ";
    const string highScorePrefix = "High Score: ";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (gameObject.name == "ScoreText")
        {
            RectTransform scoreTextRectTransform = scoreText.GetComponent<RectTransform>();
            scoreTextRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 10, scoreTextRectTransform.rect.width);
            scoreTextRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 10, scoreTextRectTransform.rect.height);
            score = 0;

            ShowInfo();
        }
        // else if(gameObject.name == "HighScore")
        // {
        //     ShowInfo();
        // }
        

    }

    void OnEnable()
    {
        score = GameManager.Instance.thisScore;
        if(gameObject.name == "FinalScoreText" || gameObject.name == "HighScore")
        {
            ShowInfo();
        }
        
        
    }



    void ShowInfo()
    {
        if(gameObject.name == "ScoreText" || gameObject.name == "FinalScoreText")
        {
           scoreText.text = scorePrefix + score; 
        }
        else if (gameObject.name == "HighScore")
        {
            scoreText.text = highScorePrefix + GameManager.Instance.highScore;
        }
        
    }

    public void IncScore(int value)
    {
        score += value;
        GameManager.Instance.saveScore(score);
        ShowInfo();
    }

}
