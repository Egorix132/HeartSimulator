using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hormone", menuName = "ScriptableObjects/Hormone", order = 1)]
public class HormoneData : ScriptableObject
{
    public int id;
    public float duration;
    public float minBorder;
    public float maxBorder;
    public int priority;
    public float chance;
    public TimeInterval timeInterval;
    public float necessaryTouchTime;

    public float moveTime;
    public float disappearTime;
    public Color color;
}

[Serializable]
public struct TimeInterval
{
    public HoursAndMinutes minTime;
    public HoursAndMinutes maxTime; 

    public bool Contains(DateTime time)
    {
        DateTime minDateTime = minTime;
        DateTime maxDateTime = maxTime;

        if (maxTime.hour > minTime.hour)
            return time.TimeOfDay > minDateTime.TimeOfDay && time.TimeOfDay < maxDateTime.TimeOfDay;
        else
            return time.TimeOfDay > minDateTime.TimeOfDay || time.TimeOfDay < maxDateTime.TimeOfDay;
    
    }
}

[Serializable]
public struct HoursAndMinutes
{
    public int hour;
    public int minute;

    public static implicit operator DateTime(HoursAndMinutes HaM)
    {
        return new DateTime(2020,06,11, HaM.hour, HaM.minute, 0);
    }
    public static implicit operator HoursAndMinutes(DateTime dt)
    {
        HoursAndMinutes HaM = new HoursAndMinutes
        {
            hour = dt.Hour,
            minute = dt.Minute
        };
        return HaM;
    }
}
