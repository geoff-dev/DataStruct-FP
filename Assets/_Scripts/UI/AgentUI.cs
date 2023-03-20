using UnityEngine;
using UnityEngine.EventSystems;

public class AgentUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private AgentType type;
    
    public void OnPointerDown(PointerEventData eventData) {
        EventsManager.OnPointerClick.InvokeAction(type);
    }
}