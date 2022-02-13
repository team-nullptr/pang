using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
	GameController gameController;
	PlayerControls controls;

	void Awake() {
		// Set up controls
		controls = new PlayerControls();

		controls.Control.Pause.performed += ctx => PauseEvent();
		controls.Control.Restart.performed += ctx => RestartEvent();
	}

	void OnEnable()
	{
		controls.Control.Enable();
	}

    void Start()
    {
		// Get the GameController
        GameObject gameControllerObject = GameObject.Find("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent<GameController>();
    }

	void PauseEvent() {
		gameController.PauseMenu();
	}

	void RestartEvent() {
		gameController.Restart();
	}
}
