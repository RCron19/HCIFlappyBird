using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private float timeSincePipe = 0f;
    public float pipeDelay = 2f;
    public float yMax = 1.6f;
    public float yMin = -1.6f;

    [HideInInspector] public int highScore = 0;
    [HideInInspector] public int thisScore;

    public GameObject pipePrefab;

    public GameObject ScoreText;
    public GameObject GameOverBG;


    void Awake()
    {
        //check if there is another gamemanager and if so destroy it
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        //this should allow the instance to live between scenes, therefore letting us view the highscore from wherever
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            ScoreText.SetActive(true);
            GameOverBG.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        //only spawn pipes if we are on the game scene
        if (SceneManager.GetActiveScene().name != "GameScene")
        {
            return;
        }
        timeSincePipe += Time.deltaTime;
        if (timeSincePipe >= pipeDelay)
        {
            SpawnPipe();
        }
    }

    void SpawnPipe()
    {
        float randomY = Random.Range(yMin, yMax);
        Vector3 spawnPos = new Vector3(11f, randomY, 0f);

        Instantiate(pipePrefab, spawnPos, Quaternion.identity);
        timeSincePipe = 0f;
    }

    public void saveScore(int score)
    {
        thisScore = score;

        if (thisScore > highScore)
        {
            highScore = thisScore;
        }
    }
}
