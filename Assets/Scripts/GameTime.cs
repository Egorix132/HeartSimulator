using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    public static GameTime Instance { get; private set; }
    [SerializeField] private Text TimerText;
    [SerializeField] private Text AgeText;

    public DateTime DateTime { get; private set; } = DateTime.Now;

    public Age AgeTime { get; private set; } = new Age(0, 0);

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
            if (DateTime.Hour == 0 && dayChange)
            {
                DateTime = DateTime.AddYears(1);
                DateTime = DateTime.AddMonths(1);
                AgeTime = AgeTime.AddYear(1);
                AgeTime = AgeTime.AddMonth(1);
            }
            dayChange = DateTime.Hour != 0;
            TimerText.text = DateTime.ToString("dd.MM.yyyy HH:mm");
            AgeText.text = $"{AgeTime.Year()} year & {AgeTime.Month()} months";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
public struct Age
{
    private int countOfMonths;
    public int Year()
    {
        return countOfMonths / 12;
    }
    public int Month()
    {
        return countOfMonths % 12;
    }

    public Age(int year, int month)
    {
        countOfMonths = year * 12 + month;
    }

    public Age AddYear(int years)
    {
        countOfMonths += years * 12;
        return this;
    }

    public Age AddMonth(int month)
    {
        countOfMonths += month;
        return this;
    }

    public static bool operator >(Age age1, Age age2)
    {
        return age1.countOfMonths > age2.countOfMonths;
    }

    public static bool operator <(Age age1, Age age2)
    {
        return age2 > age1;
    }
}