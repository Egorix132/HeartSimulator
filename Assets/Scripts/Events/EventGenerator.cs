using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventGenerator : MonoBehaviour
{
    private List<GameEventData> eventList;

    System.Random random;
    private float callDawn = 0;

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
            callDawn--;
            DateTime time = GameTime.Instance.DateTime;

            GameEventData gameEvent
                = eventList?.FirstOrDefault(h =>
                {
                    return h.chance > random.Next(0, 100);
                });

            if (gameEvent != null && callDawn <= 0)
            {
                callDawn = gameEvent.duration;
                EventSpawner.Instance.SpawnEvent(gameEvent);
            }

            yield return new WaitForSeconds(1);
        }
    }
}
