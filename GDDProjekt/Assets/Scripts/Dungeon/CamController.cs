using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform player;
    public float lerpSpeed = 2.5f;
    CameraShake camShake;


    private void Start()
    {
        camShake = GetComponent<CameraShake>();
    }
  
    void FixedUpdate () 
    {
        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, player.position.y, lerpSpeed*Time.deltaTime);
        position.x = Mathf.Lerp(this.transform.position.x, player.position.x, lerpSpeed*Time.deltaTime);
        this.transform.position = position;
    }

    public void shake(){
        StartCoroutine(camShake.Shake(0.15f,0.4f));
    }

}
