using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform background;
    public RectTransform handle;
    private Vector2 inputVector = Vector2.zero;

    public void OnPointerDown(PointerEventData eventData) 
    { 
        OnDrag(eventData); 
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        
        // Телефон экранындағы саусақ орнын немесе ПК-дағы мышканы дұрыс анықтау
        Camera cam = null;
        if (background.GetComponentInParent<Canvas>().renderMode == RenderMode.ScreenSpaceCamera)
        {
            cam = background.GetComponentInParent<Canvas>().worldCamera;
        }

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, cam, out pos))
        {
            float bgWidth = background.rect.width;
            
            // Нормализация (мәнді -1 мен 1 аралығына келтіру)
            pos.x = pos.x / (bgWidth / 2);
            pos.x = Mathf.Clamp(pos.x, -1f, 1f);

            inputVector = new Vector2(pos.x, 0);

            // Көк дөңгелекті орнына жылжыту
            handle.anchoredPosition = new Vector2(inputVector.x * (bgWidth / 2 - handle.rect.width / 2), 0);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Саусақты көтергенде ортаға қайту
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public float Horizontal() 
    { 
        return inputVector.x; 
    }
}