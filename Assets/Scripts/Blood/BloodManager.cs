using UnityEngine;

class BloodManager : MonoBehaviour
{
    [SerializeField] private Material bloodMaterial;
    [SerializeField] private Color defaultColor;

    private SpriteRenderer heartBlood;

    private void Start()
    {
        heartBlood = HeartBlood.Instance.transform.GetChild(0).GetComponent<SpriteRenderer>();      
        HormoneSpawner.OnAppear += (h, _) => ChangeBloodColor(h.data.color);
        HormoneSpawner.OnRelease += () => ChangeBloodColor(defaultColor);

        ChangeBloodColor(defaultColor);
    }

    private void ChangeBloodColor(Color color)
    {
        heartBlood.color = color;
        bloodMaterial.SetColor("_Color", color);
    }
}
