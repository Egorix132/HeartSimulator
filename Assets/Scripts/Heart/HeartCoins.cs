using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class HeartCoins : MonoBehaviour
{
    [SerializeField] private Text heartCounter;
    [SerializeField] private float goldRange;
    [SerializeField] private float updateRate;

    private int heartCoins;
    private int myltiplyier;

    private void Start()
    {
        StartCoroutine(EvenessChecker());
    }

    private IEnumerator EvenessChecker()
    {
        yield return new WaitForSeconds(GameManager.Instance.handicapTime);
        while (true)
        {
            if (Math.Abs(BPM.Instance.Bpm - BPM.Instance.OldBpm) <= goldRange && BPM.Instance.Bpm > 0)
            {
                myltiplyier++;
                heartCoins += (int)Math.Sqrt(myltiplyier);               
            }
            else
                myltiplyier = 0;

            heartCounter.text = heartCoins.ToString();
            yield return new WaitForSeconds(updateRate);
        }
    }
}
