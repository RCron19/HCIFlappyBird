using UnityEngine;

public class Bird : MonoBehaviour
{

    public AudioClip flapSound;
    public AudioClip dieSound;

    //These are just here so we can easily disable and enable things 
    //within this script due to the bird handling collision
    public GameObject ScoreText;
    public GameObject GameOverBG;

    public float jumpForce = 5f;

    private AudioSource actionSound;
    private float volume = 1.5f;

    private Rigidbody2D rb;

    private bool gameOver = false;

    public float minLoudnessToFlap = 1.0f;  // threshold to trigger a flap
    public float maxLoudness = 2.0f;        // cap loudness for normalization
    public float flapStrength = 5f;         // how strong the upward kick is
    public bool continuousHeightControl = true; // switch modes
    private bool started = false;

    private ScoreTracker scoreTrackerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOver = false;
        Debug.Log("TTS: " + Time.timeScale);
        actionSound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        scoreTrackerScript = GameObject.Find("ScoreText").GetComponent<ScoreTracker>();
        Time.timeScale = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MicInput.Instance == null) return;
        float loudness = MicInput.Instance.Loudness;
        if (!started)
        {
            if(loudness > minLoudnessToFlap)
            {
                Time.timeScale = 1;
                started = true;
            } else
            {
                return;
            }
        }
        if(gameOver)
        {
            Time.timeScale = 0;
            actionSound.PlayOneShot(dieSound, volume);
            volume = 0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse Down at " + Input.mousePosition);
            actionSound.PlayOneShot(flapSound, volume);
            rb.linearVelocity = Vector2.up * jumpForce;
            //scoreTrackerScript.IncScore(1);

        }

        if (!continuousHeightControl)
        {
            // MODE 1: Classic �clap/ shout = flap�
            if (loudness > minLoudnessToFlap)
            {
                // set vertical velocity so each loud event is a flap
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, flapStrength);
            }
        }
        else
        {
            // MODE 2: proportional height control
            float normalized = Mathf.Clamp01(loudness / maxLoudness);
            // Map 0�1 loudness into a vertical velocity range
            float targetUp = normalized * flapStrength;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, targetUp);
        }
    }

    void onGUI()
    {
        //Event m_Event = Event.current;
        //Debug.Log(m_Event);

        //if (m_Event.type == EventType.MouseDown)
        //{
        //    Debug.Log("Mouse Down at " + Input.mousePosition);
        //    actionSound.PlayOneShot(flapSound, 1.5f);

        //}
    }

    void OnCollisionEnter2D()
    {
        //This should all probably be done in the GameManager for ease of use. 
        //Joe can change this up later.
        ScoreText.SetActive(false);
        GameOverBG.SetActive(true);
        gameOver = true;
        
    }
}
