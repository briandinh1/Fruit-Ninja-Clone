using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeHandler : MonoBehaviour {

    private bool cutting;
    private Rigidbody2D rigidBody;
    private Camera mainCamera;
    private GameObject currentTrail; // used to track the current trail
    private CircleCollider2D circleCollider;
    private Vector2 previousPosition; // used to calculate speed of blade
    public float minVelocity; // blade has to move this fast to cut things

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) StartCut();
        else if (Input.GetMouseButtonUp(0)) StopCut();
        
        if (cutting) UpdateBlade();
	}

    private void UpdateBlade() {
        // update blade position to where the mouse is in the world
        Vector2 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        rigidBody.position = newPosition;
        // calculate velocity of blade
        float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
        // allow blade to cut things if its reached minimum required velocity
        circleCollider.enabled = (velocity > minVelocity) ? true : false;
        previousPosition = newPosition;
    }

    private void StartCut() {
        cutting = true;
        previousPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void StopCut() {
        cutting = false;
        circleCollider.enabled = false;
    }
}
