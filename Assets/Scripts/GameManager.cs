using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public ObjectPooler[] fruitPool;
    public ObjectPooler bombPool;
    public ObjectPooler powerupPool;
    public Transform[] spawnPoints;
    private ScoreManager scoreManager;
    public AudioSource[] squishSound;
    public AudioSource bombSound;
    public AudioSource powerupSound;
    public float spawnDelay; // how long between each fruit spawn;
    public float delayVariance; // determine the random range of spawn delays
    public float delayMultiplier; // fruits will start spawning faster and faster
    public int delayThreshold; // how many fruits needed to be cut before decreasing spawn time
    public int bombChance;
    public int bombChanceLimit; // bombs spawn rate will cap out at this limit
    public int powerupChance;
    private int fruitsCut;
    public bool gameOver;

	// Use this for initialization
	void Start () {
        fruitsCut = 0;
        scoreManager = FindObjectOfType<ScoreManager>();
        StartCoroutine(SpawnObjects());
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void ActivateBomb() {
        if (bombSound.isPlaying) bombSound.Stop();
        bombSound.Play();
        scoreManager.ResetScore();
        gameOver = true; 
    }

    public void CutFruit() {
        scoreManager.AddScore();
        int sound = Random.Range(0, squishSound.Length);
        if (squishSound[sound].isPlaying) squishSound[sound].Stop(); // prevent overloading of sounds 
        squishSound[sound].Play();
        if (++fruitsCut == delayThreshold) {
            fruitsCut = 0; // prevent overflow, but players shouldnt really get to 2^31 fruits anyway :)
            spawnDelay *= delayMultiplier;
            if (bombChance < bombChanceLimit) bombChance += 2;
        }
    }

    private void Spawn() {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        int chance = Random.Range(0, 100 + powerupChance); // extra % used to determine powerup spawn
        GameObject spawnedObject;
        if (chance <= bombChance)
            spawnedObject = bombPool.GetPooledObject();
        else if (chance > bombChance && chance <= 100)
            spawnedObject = fruitPool[Random.Range(0, fruitPool.Length)].GetPooledObject();
        else
            spawnedObject = powerupPool.GetPooledObject();
        spawnedObject.transform.position = spawnPoint.position;
        spawnedObject.transform.rotation = spawnPoint.rotation;
        spawnedObject.SetActive(true);
    }

    IEnumerator SpawnObjects() {
        while (true) {
            if (!gameOver) {
                float delay = Random.Range(spawnDelay * delayVariance, spawnDelay / delayVariance);
                yield return new WaitForSeconds(delay);
                Spawn(); // fruit, bomb, or powerup;
            }
            else {
                yield return new WaitForSeconds(3f);
                gameOver = false;
            }
        }
    }
}
