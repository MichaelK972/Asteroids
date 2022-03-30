using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceFighter : MonoBehaviour
{
    public ParticleSystem flameParticleSystem;

    private IEnumerator coroutine;

    private bool isFiring;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckPositionAndWrapAround());

        coroutine = FireBullet();
        isFiring = false;
        //StartCoroutine(coroutine);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 50f);
            if (!flameParticleSystem.isPlaying)
                flameParticleSystem.Play ();
        } 
        else if (flameParticleSystem.isPlaying)
            flameParticleSystem.Stop (); 

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<Rigidbody>().AddTorque(transform.up * -2f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Rigidbody>().AddTorque(transform.up * 2f);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (isFiring == false)
            {
                isFiring = true;
                StartCoroutine(coroutine);
            }
        }
        else
        {
            isFiring = false;
            StopCoroutine(coroutine);
        }
        //Check if Q is pressed(Q is used to teleport the spaceship)
         if (Input.GetKeyDown(KeyCode.Q))
        {
            //If Q was pressed, call the teleport function
            Teleport();
        }
    }

    public void Teleport()
        //Function to teleport the player spaceship
    {
        //Check if the player has teleports available
        if (GameManager.instance.teleports > 0) { 
            //Set the maximum number of attempts to find a suitable loaction to 20
            int tries = 20;
            //Trie to find a valid location
            while (tries >= 0)
            {
                bool valid = false;
                //Create a new random vector in the view port
                Vector3 locationVP = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 30f);
                //Convert vector from viewport to word coords
                Vector3 locationWC = Camera.main.ViewportToWorldPoint(locationVP);
                //Check the distance from this new vector from the postion of all asteroids currently in the game
                foreach (GameObject asteroid in GameManager.instance.asteroids)
                {
                    float distance = Vector3.Distance(asteroid.transform.position, locationWC);
                    //If this distance is less than 20, it is too close and therefore invalid
                    if (distance < 20)
                    {
                        valid = true;
                        valid = false;
                        //Exit the loop as the location is already deamed invalid
                        break;
                    }
                    else
                    {
                        //Else if the distance is greater than 20, it is valid
                        valid = true;
                    }
                }
                //Check the distance from this new vector from the postion of all asteroids currently in the game
                foreach (GameObject sAsteroid in GameManager.instance.specialAsteroids)
                {
                    float distance = Vector3.Distance(sAsteroid.transform.position, locationWC);

                    //If this distance is less than 20, it is too close and therefore invalid
                    if (distance < 20)
                    {
                        valid = true;
                        valid = false;
                        //Exit the loop as the location is already deamed invalid
                        break;
                    }
                    else
                    {
                        //Else if the distance is greater than 20, it is valid
                        valid = true;
                    }
                }
                //If the new vector is valid, teleprort the spaceship to the new location
                if (valid)
                {
                    transform.position = locationWC;
                    //Decrement the number of teleports
                    GameManager.instance.teleports--;
                    //Update the UI with new value for the number teleports
                    GameManager.instance.UpdateTeleports();
                    //Since a valid location/vector was found, we can exit the loop
                    break;
                }
                //If a valid location was not found, try again with a new random vactor
                else
                {
                    //Decrement number of attempts remaining
                    tries--;
                }
            }
        }
    }

    private IEnumerator FireBullet()
    {
        while (1 == 1)
        {
            if (isFiring == true)
            {
                GameObject GO = (GameObject)Instantiate(Resources.Load("bullet"));
                GO.transform.position = transform.position + transform.forward * 3f;
                GO.transform.rotation = transform.rotation;
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator CheckPositionAndWrapAround()
    {
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
}
