using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        var mr = GetComponentInChildren<MeshRenderer>();
        mat = mr.material;
        StartCoroutine(FlashingRoutine());
    }


    IEnumerator FlashingRoutine()
    {
        while (true)
        {
            mat.color = Color.black;
            yield return new WaitForEndOfFrame();
            mat.color = Color.red;
            yield return new WaitForEndOfFrame();
        }
    }
}
