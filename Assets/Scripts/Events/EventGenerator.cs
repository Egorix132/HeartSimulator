using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventGenerator : MonoBehaviour
{
    private List<GameEventData> eventList;

    System.Random random;

    private void Start()
    {
        random = new System.Random();
        eventList = Resources.LoadAll<GameEventData>("Events").ToList();
        StartCoroutine(GenerateEvent());
    }

    private IEnumerator GenerateEvent()
    {
        yield return new WaitForSeconds(GameManager.Instance.handicapTime);
        while (true)
        {
            DateTime time = GameTime.Instance.DateTime;

            GameEventData gameEvent
                = eventList?.FirstOrDefault(h =>
                {
                    return h.chance > random.Next(0, 100);
                });

            if (gameEvent != null)
            {
                EventSpawner.Instance.SpawnEvent(gameEvent);
            }

            yield return new WaitForSeconds(1);
        }
    }
}
