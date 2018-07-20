using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHandler : MonoBehaviour {

    private GameManager gameManager;
    private Rigidbody2D rigidBody;
    public float launchForce; // how strong to launch the bomb at spawn time

    // Use this for initialization
    void Start () {
        gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameManager.gameOver)
            gameObject.SetActive(false);
	}

    private void OnEnable() {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(transform.up * launchForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Blade") {
            gameObject.SetActive(false);
            gameManager.ActivateBomb();
            
        }
        else if (collision.gameObject.name == "KillBox") {
            gameObject.SetActive(false);
        }
    }
}
