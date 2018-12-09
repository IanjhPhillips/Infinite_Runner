using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    public static GameManager gameManager;

    private int score;
    private int highScore;
    private int currentSceneIndex;
    private static Canvas canvas;
    private Text scoreText, highScoreText;
    private static bool paused;
    

    private void Awake()
    {


        if (gameManager == null)
        {
            gameManager = this;
            GameObject.DontDestroyOnLoad(gameManager.gameObject);
            //GameObject.DontDestroyOnLoad(gameManager);
            gameManager.highScore = 0;
        }
        else if (gameManager != this)
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {

        SceneManager.sceneLoaded += OnSceneLoaded;
        paused = true;
        this.Initialize();

	}

    private void OnSceneLoaded (Scene scene, LoadSceneMode mode)
    {
        print("loaded");
        this.Initialize(scene.buildIndex);
    }

    // Update is called once per frame
    void Update () {

        if (!paused) {

            score++;
            if (gameManager.score > gameManager.highScore)
                gameManager.highScore = gameManager.score;

            if (currentSceneIndex == 0)
            {
                SetScoreText();
            }

        }


    }


    private void Initialize()
    {
        this.currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (currentSceneIndex) {

            case 0:
                canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                //DeleteExtraCanvas();
                gameManager.scoreText = canvas.transform.GetChild(0).gameObject.GetComponent<Text>();
                gameManager.highScoreText = canvas.transform.GetChild(1).gameObject.GetComponent<Text>();
                gameManager.SetScoreText();
                gameManager.score = 0;
                print("score = " + gameManager.score);
                print("scene: " + gameManager.currentSceneIndex);

                break;
            default:
                print("defaulting");
                break;
                
        }
        paused = false;
    }

    private void Initialize(int level)
    {
        this.currentSceneIndex = level;
        switch (level)
        {

            case 0:
                canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                gameManager.scoreText = canvas.transform.GetChild(0).gameObject.GetComponent<Text>();
                gameManager.highScoreText = canvas.transform.GetChild(1).gameObject.GetComponent<Text>();
                gameManager.SetScoreText();
                gameManager.score = 0;
                break;
            default:
                print("defaulting");
                break;

        }
        paused = false;
    }

    public static void LoadScene(int index, bool offSetIndexByCurrentLevel) {

        paused = true;
        print("You died with score " + gameManager.getScore());
        if (offSetIndexByCurrentLevel && gameManager.currentSceneIndex >= 0)
            SceneManager.LoadScene(gameManager.currentSceneIndex + index);
        else
            SceneManager.LoadScene(index);
        //gameManager.Reset();

    }

    private void SetScoreText() {

        scoreText.text = "Score: " + gameManager.score.ToString();
        highScoreText.text = "High Score: " + gameManager.highScore.ToString();

    }

    public int getScore() { return score; }
    public void setScore(int score) { this.score = score; }

    public int getHighScore() { return highScore; }
    public void setHighScore(int highScore) { gameManager.highScore = highScore; }

    void onDisable() {

        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

}
