﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioList : MonoBehaviour
{
    public ChoiceCategory runtimeChoices;
    public int voiceLineIndex;

    // SFX 
    public AudioSource
         attack1,
         attack2,
         choiceHasBeenMade,
         deathEnemy,
         deathHero,
         explosion,
         forceFieldBegin,
         forceFieldEnd,
         forceFieldHit,
         forceFieldWhileUp,
         hurt,
         jump,
         land,
         lightningStrike,
         lightningZap,
         loseFx,
         narratorHit,
         narratorVoiceLines,
         resurrection,
         select,
         selectionPicked,
         textToSpeechSource,
         winFx;

    public AudioSource[] narratorVoiceFillers, narratorEnter, godSources;

    public void PlayWithVariablePitch(AudioSource audioSource)
    {
        audioSource.pitch = Random.Range(0.92f, 1.08f);
        audioSource.Play();
    }

    public void OnHeroOpenedBox()
    {
        winFx.Play();
    }

    IEnumerator PlayOnDelay(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.Play();
    }

    public void OnChoiceMade()
    {
        choiceHasBeenMade.Play();
    }

    public void OnNarratorHit()
    {
        narratorHit.Play();
        if (textToSpeechSource.isPlaying)
        {
            textToSpeechSource.Stop();
        }
    }

    public void OnForcefieldToggle(bool active)
    {
        switch (active)
        {
            case true:
                forceFieldBegin.Play();
                forceFieldWhileUp.Play();
                break;

            case false:
                PlayWithVariablePitch(forceFieldEnd);
                if (forceFieldBegin.isPlaying)
                {
                    forceFieldBegin.Stop();
                }
                if (forceFieldWhileUp.isPlaying)
                {
                    forceFieldWhileUp.Stop();
                }
                break;
        }
    }



    #region NarratorVoiceLines

    public void OnHeroLose(int heroHP)
    {
        if (heroHP <= 0)
        {
            StartCoroutine(PlayOnDelay(loseFx, 1.5f));
        }
    }

    public void OnHeroPicked()
    {
        selectionPicked.clip = runtimeChoices.chosenHero.picked;
        selectionPicked.Play();
    }

    public void OnGodPicked(int PlayerNumber)
    {
        godSources[PlayerNumber-2].clip = runtimeChoices.chosenGods[PlayerNumber - 2].representationClip;
        godSources[PlayerNumber - 2].Play();
    }

    #endregion NarratorVoiceLines



}


