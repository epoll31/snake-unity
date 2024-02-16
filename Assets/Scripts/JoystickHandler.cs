using UnityEngine;

public class JoystickHandler : MonoBehaviour
{
    public Joystick joystickPrefab;
    private Joystick joystick;

    public SnakeHandler snake;



    void Update()
    {
        if (SingleState.Instance.settings.ControlType == ControlType.Joystick)
        {
            if (Input.touchCount > 0 && joystick == null)
            {
                joystick = Instantiate(joystickPrefab, transform);
                joystick.transform.position = Input.GetTouch(0).position;
            }
            else if (Input.touchCount == 0 && joystick != null)
            {
                Destroy(joystick.gameObject);
                joystick = null;
            }

            if (joystick != null)
            {
                Vector2 pos = Input.GetTouch(0).position - (Vector2)joystick.transform.position;
                float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

                float maxRadius = joystick.background.GetComponent<RectTransform>().rect.width / 2;
                float radius = Mathf.Min(maxRadius, pos.magnitude);

                pos = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;

                joystick.handle.GetComponent<RectTransform>().anchoredPosition = pos;

                snake.joystickAngle = angle;
                snake.joystickAmplifier = (radius / maxRadius) / 2 + 0.5f;
            }
        }
    }
}
