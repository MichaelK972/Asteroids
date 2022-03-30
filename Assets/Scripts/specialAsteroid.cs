using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialAsteroid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        //Randomly rotate the special asteroid
        transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
        //start the coroutine to wrap the special asteroid around the viewport
        StartCoroutine(CheckPositionAndWrapAround());
        //Add force to the special asteroid
        GetComponent<Rigidbody>().AddForce(transform.forward * 500f);
    }


    private void OnTriggerEnter(Collider other)
    {
        //Check if the object that collided was the spaceship
        if (other.gameObject.name == "spacefighter01")
        {
            //Create an explosion effect
            GameObject expl = (GameObject)Instantiate(Resources.Load("SmallExplosion"));
            expl.transform.position = gameObject.transform.position;
            //Remove the special asteroids from the list of special asteroids
            GameManager.instance.specialAsteroids.Remove(gameObject);
            //Destroy the special asteroid
            Destroy(gameObject);
            //Since the special asteroid was destoryed, albeit bye the spaceship, increment the number teleports
            GameManager.instance.teleports++;
            //Update the UI to reflect the new value for teleports
            GameManager.instance.UpdateTeleports();
            //Decrement the the number of lives
            GameManager.instance.lives--;
            //Update the UI to refelct the new value for lives
            GameManager.instance.UpdateLives();
            //Destroy the space ship
            Destroy(other.gameObject.transform.parent.gameObject);
            //Increase the score by 15
            GameManager.instance.score += 15;
            //Update UI to show new score
            GameManager.instance.UpdateScore();
            //If the player still has lives, create a new player space ship
            if (GameManager.instance.lives > 0)
            {
                GameManager.instance.CreatePlayerSpaceship();
            }
            //Else, the game is over
            else
            {   
                //Destrtoy all asteroids currently in the game
                foreach (GameObject GO in GameManager.instance.asteroids)
                {
                    Destroy(GO);
                }
                //Set the game state to the menu
                GameManager.instance.ChangeState("menu");
            }
        }
        //check if the object colliding was a bullet
        if (other.gameObject.name == "bullet(Clone)")
        {
            //Create an explosion effect
            GameObject expl = (GameObject)Instantiate(Resources.Load("SmallExplosion"));
            expl.transform.position = gameObject.transform.position;
            //Destroy the bullet
            Destroy(other.gameObject);
            //Remove the special asteroid from the list of special asteroids
            GameManager.instance.specialAsteroids.Remove(gameObject);
            //Destroy the special asteroid
            Destroy(gameObject);
            //Since the special asteroid was destroyed, increment the number of teleports
            GameManager.instance.teleports++;
            //Update the UI to reflect the new value for teleports
            GameManager.instance.UpdateTeleports();
            //Increase the score by 15
            GameManager.instance.score += 15;
            //Update UI to show new score
            GameManager.instance.UpdateScore();
        }
    }


    private IEnumerator CheckPositionAndWrapAround()
    {
        //Enumeretor to wrap the object arounf the viewport
        Vector3 positionWC, positionVP;
        while (1 == 1)
        {
            positionWC = gameObject.transform.position;
            positionVP = Camera.main.WorldToViewportPoint(positionWC);
            if ((positionVP.x < -0.05) || (positionVP.x > 1.05))
            {
                positionWC.x = -positionWC.x;
            }

            if ((positionVP.y < -0.05) || (positionVP.y > 1.05))
            {
                positionWC.z = -positionWC.z;
            }

            gameObject.transform.position = positionWC;

            yield return new WaitForSeconds(0.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
