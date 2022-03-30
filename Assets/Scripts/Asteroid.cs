using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(0, Random.Range(0,360), 0));
        GetComponent<Rigidbody>().AddForce(transform.forward * 500f);
        //GetComponent<Rigidbody>().AddTorque(Random.rotation.eulerAngles);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckAndDestroy()
    {
        if (gameObject.name == "asteroid1(Clone)")
        {
            for(int i = 0; i < 4; i++)
            {
                GameObject GO = (GameObject)Instantiate(Resources.Load("asteroid1small"));
                GO.transform.position = gameObject.transform.position;
                GameManager.instance.asteroids.Add(GO);
            }
        }
        if (gameObject.name == "asteroid1small(Clone)")
        {
            GameManager.instance.score += 10;
            GameManager.instance.UpdateScore();
        }
        else
        {
            GameManager.instance.score += 5;
            GameManager.instance.UpdateScore();
        }
        GameManager.instance.asteroids.Remove(gameObject);
        Destroy(gameObject);

        if (GameManager.instance.asteroids.Count == 0)
        {
            GameManager.instance.currentGameLevel++;
            GameManager.instance.StartNextLevel();
        }

        
        GameObject expl = (GameObject)Instantiate(Resources.Load("SmallExplosion"));
        expl.transform.position = gameObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "spacefighter01")
        {
            GameManager.instance.lives--;
            GameManager.instance.UpdateLives();
            Destroy(other.gameObject.transform.parent.gameObject);

            if (GameManager.instance.lives > 0)
            {
                GameManager.instance.CreatePlayerSpaceship();
            }
            else
            {
                foreach (GameObject GO in GameManager.instance.asteroids)
                {
                    Destroy(GO);
                }
                //Destroy all special asteroids currently in the game
                foreach (GameObject GO in GameManager.instance.specialAsteroids)
                {
                    Destroy(GO);
                }
                GameManager.instance.ChangeState("menu");
            }
            CheckAndDestroy();
        }
        if (other.gameObject.name == "bullet(Clone)")
        {
            Destroy(other.gameObject);
            CheckAndDestroy();
        }

        //Destroy(gameObject);
        //Destroy(other.gameObject);
    }


}
