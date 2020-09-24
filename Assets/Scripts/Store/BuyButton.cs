using UnityEngine;

public class BuyButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        UpgradeManager.Instance.Buy();
    }
}
