using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetX : MonoBehaviour
{
    private Rigidbody rb;
    private GameManagerX gameManagerX;
    public int pointValue;
    public GameObject explosionFx;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();
        Destroy(gameObject, gameManagerX.objectLifespan);
    }

    // When target is clicked, destroy it, update score, and generate explosion if object is bad - GameOver
    private void OnMouseDown()
    {
        if (gameManagerX.isGameActive)
        {
            Destroy(gameObject);
            gameManagerX.UpdateScore(pointValue);
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);
    }
}
