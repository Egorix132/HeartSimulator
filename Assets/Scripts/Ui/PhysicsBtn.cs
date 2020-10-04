using UnityEngine;
using UnityEngine.Events;

public class PhysicsBtn : MonoBehaviour
{
    public UnityEvent action;
    private void OnMouseUp()
    {
        action?.Invoke();
    }
}
