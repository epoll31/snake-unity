using UnityEngine;

public class SnakeHandler : MonoBehaviour
{
    public GameObject headPrefab;
    public GameObject bodyPrefab;
    public FoodHandler foodHandler;
    [Range(0.1f, float.MaxValue)]
    public float lengthPerPart;

    GameObject head;
    GameObject lastPart;
    int count = 0;
    public float speed = 2;
    public float coastSpeed = 1;

    private float speedToUse;

    public float joystickAngle;
    public float joystickAmplifier = 1f;

    private Vector2 newPosition;

    public float angle;
    [Range(0, 1)]
    public float steerLerpConstant = 1;
    // public float steerLerpDeadzone = 1;

    void Start()
    {
        angle = UnityEngine.Random.Range(0f, 360f);

        head = lastPart = Instantiate(headPrefab, transform);

        SnakeFoodCollider snakeFoodCollider = head.GetComponent<SnakeFoodCollider>();
        snakeFoodCollider.snakeHandler = this;
        snakeFoodCollider.foodHandler = foodHandler;

        // head.GetComponent<SpriteRenderer>().sortingOrder = -++count;


        AddParts(4);
    }

    public void AddParts(int count)
    {
        for (int i = 0; i < count; i++)
        {
            AddPart();
        }
    }

    void AddPart()
    {
        GameObject go = Instantiate(bodyPrefab, transform);
        go.transform.position = lastPart.transform.position;

        go.GetComponent<SpriteRenderer>().sortingOrder = -++count;

        SnakePartHandler newPart = go.GetComponent<SnakePartHandler>();

        newPart.Snake = this;
        newPart.PreviousPart = lastPart;

        lastPart = newPart.gameObject;
        SingleState.Instance.gameData.Length++;
    }

    private void GetAngleByDrag()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 mousePos = touch.position;
            mousePos.z = Camera.main.nearClipPlane;
            Vector2 target = Camera.main.ScreenToWorldPoint(mousePos);

            Vector2 toTarget = target - (Vector2)head.transform.position;

            // if (toTarget.magnitude > lengthPerPart)
            // {
            //   speedToUse = speed;
            //   angle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
            // }
            speedToUse = speed;

            float usableLerpConstant = steerLerpConstant;

            if (toTarget.magnitude < lengthPerPart * 2)
            {
                usableLerpConstant = 0;
            }

            angle = Mathf.LerpAngle(
                angle,
                Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg,
                usableLerpConstant
            );
        }
        else
        {
            speedToUse = coastSpeed;
        }

    }

    private void CheckBounds()
    {
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * Screen.width / Screen.height;

        if (
            head.transform.position.y < Camera.main.transform.position.y - halfHeight
            || head.transform.position.y > Camera.main.transform.position.y + halfHeight
        )
        {
            SingleState.Instance.PlayClip(Sounds.Thud);
            angle = -angle;
            newPosition.y = Mathf.Clamp(
                newPosition.y,
                Camera.main.transform.position.y - halfHeight,
                Camera.main.transform.position.y + halfHeight
            );
        }
        if (
            head.transform.position.x < Camera.main.transform.position.x - halfWidth
            || head.transform.position.x > Camera.main.transform.position.x + halfWidth
        )
        {
            angle = 180 - angle;
            SingleState.Instance.PlayClip(Sounds.Thud);
            newPosition.x = Mathf.Clamp(
                newPosition.x,
                Camera.main.transform.position.x - halfWidth,
                Camera.main.transform.position.x + halfWidth
            );
        }
    }

    void Update()
    {
        newPosition = head.transform.position;
        if (SingleState.Instance.settings.ControlType == ControlType.Drag)
        {
            GetAngleByDrag();
        }
        else
        {
            if (Input.touchCount > 0)
            {
                speedToUse = speed * joystickAmplifier;
            }
            else
            {
                speedToUse = coastSpeed;
            }

            angle = Mathf.LerpAngle(angle, joystickAngle, steerLerpConstant);
        }

        CheckBounds();

        Vector2 direction = new(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        if (SingleState.Instance.State == States.Playing)
        {
            head.transform.SetPositionAndRotation(
                newPosition + speedToUse * Time.deltaTime * direction.normalized,
                Quaternion.Euler(0, 0, angle + 90)
            );
        }
    }
}
