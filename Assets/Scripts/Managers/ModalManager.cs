using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class ModalManager : MonoBehaviour
{
    [SerializeField] private Text modalText;
    [SerializeField] private GameObject modal;

    private bool closable = true;

    public void SetModal(Vector3 pos, string text, bool closeable = true)
    {
        GetComponent<BoxCollider2D>().enabled = true;
        modal.transform.position = pos;
        modalText.text = text;
        this.closable = closeable;
    }

    private void OnMouseUp()
    {
        if (!closable)
        {
            return;
        }
        PauseManager.Instance.CloseModal();
    }
}
