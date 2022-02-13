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
		controls.Control.NextLevel.performed += ctx => NextLevelEvent();
	}

	void OnEnable()
	{
		controls.Control.Enable();
	}

	void OnDisable() {
		controls.Control.Disable();
	}

    void Start()
    {
		// Get the GameController
		gameController = GetComponent<GameController>();
    }

	void PauseEvent() {
		gameController.PauseMenu();
	}

	void RestartEvent() {
		gameController.Restart();
	}

	void NextLevelEvent() {
		gameController.NextLevel();
	}
}
