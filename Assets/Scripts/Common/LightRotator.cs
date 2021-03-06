﻿using System;
using UnityEngine;

public class LightRotator : MonoBehaviour
{
    [SerializeField]
    private bool isMenu = true;
    void Update()
    {
        DateTime dateTime;
        if (isMenu)
            dateTime = DateTime.Now;
        else
            dateTime = GameTime.Instance.DateTime;

        transform.rotation = Quaternion.Euler(
            0,
            (float)Math.Tan((dateTime.Hour + (double)dateTime.Minute / 60 - 14) * Math.PI / 90) * 160,
            0);
    }
}
