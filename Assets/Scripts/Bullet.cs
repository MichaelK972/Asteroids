using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 60f;
        StartCoroutine(CheckPosition());
    }

    private IEnumerator CheckPosition()
    {
        Vector3 positionWC, positionVP;
        while (1 == 1)
        {
            positionWC = gameObject.transform.position;
            positionVP = Camera.main.WorldToViewportPoint(positionWC);
            
            if ((positionVP.x < -0.1) || (positionVP.y < -0.1) || (positionVP.x > 1.1) || (positionVP.y > 1.1))
                Destroy(gameObject);
            yield return new WaitForSeconds(0.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
