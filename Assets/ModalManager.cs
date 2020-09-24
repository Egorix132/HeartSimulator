using UnityEngine;
using UnityEngine.UI;

public class ModalManager : MonoBehaviour
{
    [SerializeField] private Text modalText;
    [SerializeField] private GameObject modal;

    public void SetModal(Vector3 pos, string text)
    {
        modal.transform.position = pos;
        modalText.text = text;
    }

    private void OnMouseUp()
    {
        PauseManager.Instance.CloseModal();
    }
}
