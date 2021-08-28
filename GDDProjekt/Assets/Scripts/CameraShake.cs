using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    // function must be called in a coroutine:
    // StartCoroutine(CameraShake#Shake)

    // usage:
    // 1. script must be added to the camera in the scene.
    // 2. add the following line in the script which must shake the cam:
    //      public CameraShake camShake;
    // 3. in the UnityEditor, click on the game object which holds the property
    // 4. add the main camera object (which holds the CameraShake script)
    //      to the chosen game object
    // 5. call the shake function using `StartCoroutine(camShake.Shake(...))`



    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * magnitude;
            float y = originalPosition.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
