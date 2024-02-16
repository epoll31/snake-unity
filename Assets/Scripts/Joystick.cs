using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour//, IBeginDragHandler, IDragHandler, IEndDragHandler
{
  public GameObject background;
  public GameObject handle;

  // [HideInInspector]
  // public float angle;
  //
  // public void OnBeginDrag(PointerEventData eventData)
  // {
  //   print("OnBeginDrag");
  //  
  //   
  // }
  //
  // public void OnEndDrag(PointerEventData eventData)
  // {
  //   print("OnEndDrag");
  //   handle.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
  // }
  //
  // public void OnDrag(PointerEventData eventData)
  // {
  //   print("OnDrag");
  //   Vector2 pos;
  //   RectTransformUtility.ScreenPointToLocalPointInRectangle(
  //     background.GetComponent<RectTransform>(),
  //     eventData.position,
  //     eventData.pressEventCamera,
  //     out pos
  //   );
  //
  //
  //   float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
  //   print(angle);
  //   float maxRadius = background.GetComponent<RectTransform>().rect.width / 2;
  //   float radius = Mathf.Min(maxRadius, pos.magnitude);
  //
  //   pos = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;
  //
  //   handle.GetComponent<RectTransform>().anchoredPosition = pos;
  // }

}
