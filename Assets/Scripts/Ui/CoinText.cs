using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    private Text heartCounter;

    // Start is called before the first frame update
    void Start()
    {
        heartCounter = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        heartCounter.text = CoinManager.Instance.Coins.ToString();
    }
}
