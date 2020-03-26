﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu (fileName = "New Choice Category", menuName = "ScriptableObject/ChoiceCategory")]
public class ChoiceCategory : ScriptableObject
{
    public string CategoryName;
    public GameObject[] Options;


    [Header("Runtime Only")]
    public int runTimeLoopCount = 1;
    [Header("Pre-phase")]
    public GameObject character;
    public GameObject theme;
    [Header("First Loop")]
    public Opponent firstOpponent;
    public GameObject firstItem;
    [Header("Second Loop")]
    public Opponent secondOpponent;
    public GameObject secondItem;
    [Header("Third Loop")]
    public Opponent thirdOpponent;
    public GameObject thirdItem;
    [Header("Boss Fight")]
    public Opponent fourthOpponent;

}

[Serializable]
public class Opponent
{
    public GameObject minion;
    public GameObject modifier;
}


