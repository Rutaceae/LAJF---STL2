﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GodController[] godcontrollers;

    public GameObject tutorialInstructions;
    public GameObject[] tutorialPanels, buttons;
    public int panelIndex, choice, amountOfChoices;
    public ButtonSounds buttonSounds;

    public InputManager inputManager;

    [Header("Player1 controls")]
    public KeyCode p1Select;
    public KeyCode p1Left, p1Right;

    public KeyCode p1SelectAlt;
    bool pressedDownHorizontalAxis;
    [Tooltip("Must match an Input-Axis")]
    public string p1SelectAltAxisName;
    float horizontalAxisValue;

    [SerializeField]
    private bool[] heardNarrator;
    private bool allowInput;

    private void Start()
    {
        inputManager.ToggleInputAllow(false);
        godcontrollers[0].ToggleCombatMode(false);
        godcontrollers[1].ToggleCombatMode(false);
        godcontrollers[2].ToggleCombatMode(false);
        godcontrollers[0].AssignRandomGod();
        godcontrollers[1].AssignRandomGod();
        godcontrollers[2].AssignRandomGod();
        panelIndex = -1;
        SwitchPanel(1);
    }


    public void SwitchPanel(int addition)
    {
        panelIndex += addition;

        if (panelIndex == tutorialPanels.Length)
            panelIndex = 0;
        else if (panelIndex < 0)
            panelIndex = tutorialPanels.Length - 1;

        for (int i = 0; i < tutorialPanels.Length; i++)
        {
            if (i == panelIndex)
            {
                tutorialPanels[i].gameObject.SetActive(true);
            }
            else
            {
                tutorialPanels[i].gameObject.SetActive(false);
            }
        }

        if (heardNarrator[panelIndex])
            return;

        if (addition == 1)
            StartCoroutine(StopInputWhileNarratorIsSpeaking());
    }

    IEnumerator StopInputWhileNarratorIsSpeaking()
    {
        AudioList audioList = FindObjectOfType<AudioList>();
        audioList.tutorialNarrator[panelIndex].Play();
        heardNarrator[panelIndex] = true;
        while (audioList.tutorialNarrator[panelIndex].isPlaying)
        {
            yield return new WaitForSeconds(0.1f);
        }
        AllowPanelInteraction();
    }

    void AllowPanelInteraction()
    {
        
        buttons[panelIndex].SetActive(true);
    }

    public void ToggleInputAllow(bool allowInput)
    {
        this.allowInput = allowInput;
    }

    public void SkipInstructions()
    {
        tutorialInstructions.SetActive(false);
        inputManager.ToggleInputAllow(true);
        godcontrollers[0].ToggleCombatMode(true);
        godcontrollers[1].ToggleCombatMode(true);
        godcontrollers[2].ToggleCombatMode(true);
        FindObjectOfType<MusicManager>().PlayMusic("Battle", 0.8f);
        FindObjectOfType<Spawner>().SpawnRandomEnemy();
    }

    public void EndCombat()
    {
        Debug.Log("Ending Combat");
        tutorialInstructions.SetActive(true);
        panelIndex = 2;
        SwitchPanel(1);
        ToggleInputAllow(false);
    }

    public void PlayAgain()
    {
        Destroy(FindObjectOfType<MusicManager>().gameObject);
        SceneManager.LoadScene("Tutorial");
    }

    public void BackToMenu()
    {
        Destroy(FindObjectOfType<MusicManager>().gameObject);
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (allowInput)
        {
            #region Alternate/ControllerInput
            //Debug.Log(Input.GetAxis(p1SelectAltAxisName) + " P1 Horizontal axis input");
            if (Input.GetAxis(p1SelectAltAxisName) != 0 && !pressedDownHorizontalAxis)
            {
                pressedDownHorizontalAxis = true;
                horizontalAxisValue = Input.GetAxis(p1SelectAltAxisName);
            }
            if (Input.GetAxis(p1SelectAltAxisName) == 0)
            {
                pressedDownHorizontalAxis = false;
            }
            #endregion Alternate/ControllerInput


            if (Input.GetKeyDown(p1Left) || horizontalAxisValue < 0)
            {
                int selection = (choice + (amountOfChoices - 1)) % amountOfChoices; //Move leftwards in choices.
                ChangeAndDisplaySelection(selection);
                horizontalAxisValue = 0;
            }
            if (Input.GetKeyDown(p1Right) || horizontalAxisValue > 0)
            {
                int selection = (choice + (amountOfChoices + 1)) % amountOfChoices; //Move right in choices.
                ChangeAndDisplaySelection(selection);
                horizontalAxisValue = 0;
            }
            if (Input.GetKeyDown(p1Select) || Input.GetKeyDown(p1SelectAlt))
            {
                SelectAButton();
            }

        }

    }

    void ChangeAndDisplaySelection(int selection)
    {

    }

    public void SelectAButton()
    {
        buttonSounds.OnChoiceMade();
    }
}
