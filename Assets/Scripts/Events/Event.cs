using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "ScriptableObjects/Event", order = 1)]
public class Event : ScriptableObject
{
    public int id;
    public float duration;
    public float idHormone;
    public float chance;
    public int acceptBpm;
    public TimeInterval timeInterval;
}
