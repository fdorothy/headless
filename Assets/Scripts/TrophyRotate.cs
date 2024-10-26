using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyRotate : MonoBehaviour
{
    float angleY = 0f;

    // Update is called once per frame
    void Update()
    {
        angleY += Time.deltaTime * 30f;
        transform.rotation = Quaternion.Euler(0f, angleY, 0f);
    }
}
