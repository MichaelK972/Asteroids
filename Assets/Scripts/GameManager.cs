using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentGameLevel;
    public GameObject menuUI;
    public GameObject gameUI;
    private string state;
    public static GameManager instance;

    public List<GameObject> asteroids;

    //List of special asteroid gameobjects
    public List<GameObject> specialAsteroids;

    public Text scoreText;
    public Text bestText;
    public Text livsText;

    //Teleports UI text field
    public Text teleportText;

    public int score;
    private int best;
    public int lives;

    //Number of availible teleports
    public int teleports;

   
    // Start is called before the first frame update
    void Start()
    {
        best = 0;
        asteroids = new List<GameObject>();
        ChangeState("menu");
        instance = this;
        Camera.main.transform.position = new Vector3(0, 30, 0);
        Camera.main.transform.LookAt(Vector3.zero, new Vector3(0, 0, 1));

    }

    public void ChangeState(string newState) {
        state = newState;
        if (state == "menu")
        {
            menuUI.SetActive(true);
            gameUI.SetActive(false);
        }
        if (state == "game")
        {
            menuUI.SetActive(false);
            gameUI.SetActive(true);
        }
    }

    public void UpdateScore()
    {
        if (score > best)
        {
            best = score;
        }
        scoreText.text = "Score: " + score.ToString();
        bestText.text = "Best: " + best.ToString();
    }

    public void UpdateLives()
    {
        livsText.text = "Lives: " + lives.ToString();
    }

    public void UpdateTeleports()
    {
        teleportText.text = "Teleports: " + teleports.ToString();
    }

    public void StartNewGame()
    {
        ChangeState("game");
        currentGameLevel = 1;
        score = 0;
        lives = 3;

        //Set number of available teleports to one
        teleports = 1;
        //Update the UI to display current number of teleports
        UpdateTeleports();

        UpdateScore();
        UpdateLives();

        //Clear asteroids stored from possible prevoius games
        asteroids.Clear();
        //Clear special asteroids stored from possible previous games
        specialAsteroids.Clear();

        StartNextLevel();
        
        CreatePlayerSpaceship();
        
    }

    public void SpawnSpecialAsteroid()
        //Function to spawn a special asteroid
    {
       //Intantiate a new specialAsteroid
        GameObject SA = (GameObject)Instantiate(Resources.Load("specialAsteroid"));
        SA.transform.position = Vector3.zero;
        Vector3 leftEdgePointVP;
        //Create a new vector which is a random position on the left side of the viewport
        leftEdgePointVP = new Vector3(0, Random.Range(0f, 1f), 30f);
        //Convert to world coordinates
        Vector3 leftEdgePointWC = Camera.main.ViewportToWorldPoint(leftEdgePointVP);
        //Postion the special asteroid at the new vector
        SA.transform.position = leftEdgePointWC;
        //Add the special asteroid to the list of special asteroids
        specialAsteroids.Add(SA);
    }

    public void StartNextLevel()
    {
        for (int i = 0; i < currentGameLevel; i++)
        {
            //Check if there is a special asteroid in the game as only one per level.
            if (specialAsteroids.Count == 0)
            {
                //If there is not, spawn a new special asteroid
                SpawnSpecialAsteroid();
            }

            GameObject GO = (GameObject)Instantiate(Resources.Load("asteroid1"));
            GO.transform.position = Vector3.zero;
            Vector3 leftEdgePointVP;
            leftEdgePointVP = new Vector3(0, Random.Range(0f, 1f), 30f);
            Vector3 leftEdgePointWC = Camera.main.ViewportToWorldPoint(leftEdgePointVP);
            GO.transform.position = leftEdgePointWC;
            asteroids.Add(GO);
        }
    }

    public void CreatePlayerSpaceship()
    {
        GameObject GO = (GameObject)Instantiate(Resources.Load("spaceship"));
    }


    

    // Update is called once per frame
    void Update()
    {
     
    }
}
