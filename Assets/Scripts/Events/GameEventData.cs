using System.Collections;
using UnityEngine;

public abstract class GameEventData : ScriptableObject
{
    public int id;
    public float duration;
    public float disappearTime;
    public float chance;
    public TimeInterval timeInterval;
    public float spawnRadius;

    public Sprite sprite;

    public abstract void RunEvent();
}
