using UnityEngine;

class BloodManager : MonoBehaviour
{
    [SerializeField] private Material bloodMaterial;

    private void Update()
    {
        float minTime = BPM.Instance.CalcIdealTime(BPMBorders.Instance.MinBorder);
        minTime = minTime < 0 ? -minTime : 0;
        minTime = minTime < 4 ? minTime : 4;
        bloodMaterial.SetFloat("_DangerLevel", minTime / 4);
    }
}
