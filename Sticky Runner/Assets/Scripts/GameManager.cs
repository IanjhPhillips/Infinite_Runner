using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Main manager class
//Keeps track of scores, UI
//Handles scene changes

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;

    //UI attributes
    private static Canvas canvas;
    private Text scoreText, highScoreText;

    //Score attributes
    private int score;
    private int highScore;
    
    //Track current scene
    private int currentSceneIndex;
    
    //stops score from changing in between scenes
    private static bool paused;
    

    private void Awake()
    {

        //Only one GameManager can exist at a time, static attribute gameManager keeps track
        if (gameManager == null)
        {
            gameManager = this;
            GameObject.DontDestroyOnLoad(gameManager.gameObject);
            gameManager.highScore = 0;
        }
        else if (gameManager != this)
        {
            Destroy(this.gameObject);
        }
    }

    //Initialization
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
    void FixedUpdate () {

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

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

    //Initialize method that takes not input, uses current scene
    //intializes canvas, currentScene
    //resets score
    //unpauses game
    private void Initialize()
    {
        this.currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (currentSceneIndex) {

            case 0:
                canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
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

    //Initialize method that takes a scene index
    //intializes canvas, currentScene
    //resets score
    //unpauses game
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

    //Loads scene based off index passed and boolean passed
    //If true, adds index to current scene index to find index of scene to be loaded
    //If false, uses only passed value to find index of scene to be loaded
    public static void LoadScene(int index, bool offSetIndexByCurrentLevel) {

        paused = true;
        if (offSetIndexByCurrentLevel && gameManager.currentSceneIndex >= 0)
            SceneManager.LoadScene(gameManager.currentSceneIndex + index);
        else
            SceneManager.LoadScene(index);

    }

    private void SetScoreText() {

        scoreText.text = "Score: " + gameManager.score.ToString();
        highScoreText.text = "High Score: " + gameManager.highScore.ToString();

    }

    //mutators
    public int getScore() { return score; }
    public void setScore(int score) { this.score = score; }

    public int getHighScore() { return highScore; }
    public void setHighScore(int highScore) { gameManager.highScore = highScore; }

    //un-append sceneloaded listener
    void onDisable() {

        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

}
