using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class HeartCoinsIncreaser : MonoBehaviour
{
    [SerializeField] private float goldRange;
    [SerializeField] private float updateRate;
    [SerializeField] private Color goldColor;

    [SerializeField] private int goldGivenCount;


    private VisualEffect screenEffects;

    private int myltiplyier;

    private void Start()
    {
        screenEffects = MainCamera.Instance.GetComponent<VisualEffect>();

        goldRange += PlayerPrefs.GetInt("Gold Range", 0) * 0.4f;
        goldGivenCount += PlayerPrefs.GetInt("Gold Multiplier", 0);
        StartCoroutine(EvenessChecker());
    }

    private IEnumerator EvenessChecker()
    {
        yield return new WaitForSeconds(GameManager.Instance.handicapTime);
        while (true)
        {
            if (BPM.Instance.Bpm > 0 
                && (Math.Abs(BPM.Instance.Bpm - BPMBorders.Instance.MaxBorder) <= goldRange
                || Math.Abs(BPM.Instance.Bpm - BPMBorders.Instance.MinBorder) <= goldRange))
            {
                myltiplyier++;
                CoinManager.Instance.AddCoins((int)Math.Sqrt(myltiplyier));
                screenEffects.SetEffect(goldColor, 1);
            }
            else
                myltiplyier = 0;

            yield return new WaitForSeconds(updateRate);
        }
    }
}
