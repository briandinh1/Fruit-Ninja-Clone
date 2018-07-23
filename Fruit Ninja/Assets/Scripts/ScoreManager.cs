using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public int scoreAwarded;
    private int currentScore;
    public Text currentScoreUI;
    private int highScore;
    public Text highScoreUI;
    public bool powerupActive;
    public AudioSource powerupSound;

	// Use this for initialization
	void Start () {
        currentScore = 0;
        highScore = 0;
        currentScoreUI.text = "0";
        highScoreUI.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void ResetScore() {
        currentScoreUI.text = "0";
        currentScore = 0;
    }

    public void ActivatePowerup() {
        if (powerupSound.isPlaying) powerupSound.Stop();
        powerupSound.Play();
        powerupActive = true;
        StartCoroutine(DeactivatePowerup()); 
        AddScore(10);
    }

    IEnumerator DeactivatePowerup() {
        yield return new WaitForSeconds(5f);
        powerupActive = false;
    }

    // default parameter is used for picking up new powerups
    public void AddScore(int score = 0) {
        if (score > 0) currentScore += 10;
        else currentScore += scoreAwarded * (powerupActive ? 2 : 1);
        if (currentScore >= highScore)
            highScore = currentScore;
        currentScoreUI.text = "" + currentScore;
        highScoreUI.text = "" + highScore;
    }
}
