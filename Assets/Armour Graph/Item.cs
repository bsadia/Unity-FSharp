﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Item : ScriptableObject
{
    private static readonly int MAX = 100;
    private static readonly int MIN = 0;

    public Item(string name)
    {
        Name = name;
        Intellect = Random.Range(MIN, MAX);
        Strength = Random.Range(MIN, MAX);
        Agility = Random.Range(MIN, MAX);
        
    }

    public string Name { get; set; }
    public int Intellect { get; set; } = 0;
    public int Strength { get; set; } = 0;
    public int Agility { get; set; } = 0;

    public override string ToString()
    {
        return $"{Name}: [{Intellect}, {Strength}, {Agility}]";
    }
}
