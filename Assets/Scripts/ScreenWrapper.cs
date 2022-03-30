using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckPositionAndWrapAround());
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
           
            yield return new WaitForSeconds(0.1f);
        }
    }
}
