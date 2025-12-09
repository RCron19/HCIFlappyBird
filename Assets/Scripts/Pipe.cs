using UnityEngine;

public class Pipe : MonoBehaviour
{

    private bool cleared = false;
    public GameObject bird;
    private float speed = 3f;
    private ScoreTracker scoreTrackerScript;

    public AudioClip scoreSound;
    private AudioSource actionSound;
    private float volume = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bird = GameObject.Find("Bird");
        scoreTrackerScript = GameObject.Find("ScoreText").GetComponent<ScoreTracker>();
        Debug.Log(bird);
        actionSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("PipeX: " + transform.position.x + " BirdX: " + bird.transform.position.x);

        if (bird.transform.position.x > gameObject.transform.position.x && cleared == false)
        {
            cleared = true;
            scoreTrackerScript.IncScore(1);
            Debug.Log("Score");
            actionSound.PlayOneShot(scoreSound, volume);
        }

        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < -11f)
        {
            Destroy(gameObject);
        }

    }

}
