using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }
    [SerializeField] private Text TimerText;
    [SerializeField] private Text AgeText;

    public DateTime DateTime { get; private set; } = DateTime.Now;

    private int year = 0;
    private int month = 0;

    public DateTime GetDateTime()
    {
        return DateTime;
    }

    void Awake()
    {
        if (Instance == null)
        { // Экземпляр менеджера был найден
            Instance = this; // Задаем ссылку на экземпляр объекта
        }
        else if (Instance == this)
        { // Экземпляр объекта уже существует на сцене
            Destroy(gameObject); // Удаляем объект
        }
        StartCoroutine(RunTime());
    }

    private void Update()
    {
        DateTime = DateTime.AddHours(Time.deltaTime); 
    }

    private IEnumerator RunTime()
    {
        bool dayChange = true;
        while (true)
        {
            if(DateTime.Hour == 0 && dayChange)
            {
                DateTime = DateTime.AddYears(1);
                DateTime = DateTime.AddMonths(1);             
                month += 1;
                year += 1;
            }
            if (month >= 12)
            {
                year += 1;
                month = 0;
            }
            dayChange = DateTime.Hour != 0;
            TimerText.text = DateTime.ToString("dd.MM.yyyy HH:mm");
            AgeText.text = $"{year} year & {month} months";
            yield return new WaitForSeconds(0.5f);           
        }
    }
}
