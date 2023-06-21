using UnityEngine;

public class CameraSetups : MonoBehaviour
{
    public GameObject boundaries;
    public Transform top;
    public Transform bottom;
    public Transform left;
    public Transform right;
    public GameObject topRight;
    public GameObject bottomLeft;
    public RectTransform joystick;
    public float scaleSize;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));

        joystick.transform.position = bottomLeft;
        joystick.sizeDelta = new Vector2(Screen.width, Screen.height);
        
        Vector3 length = topRight - bottomLeft;
        SetupBoundaries(topRight, bottomLeft, length);
    }

    void SetupBoundaries(Vector3 point, Vector3 point2, Vector3 length)
    {
        boundaries.SetActive(true);

        Vector3 horizontalScale = new Vector3(length.x, scaleSize, 1f);
        Vector3 verticalScale = new Vector3(scaleSize, length.y, 1f);

        top.transform.position = new Vector3(-(length.x / 2), 0f, 0f);
        bottom.transform.position = new Vector3((length.x / 2), 0f, 0f);

        left.transform.position = new Vector3(0f, (length.y / 2), 0f);
        right.transform.position = new Vector3(0f, -(length.y / 2), 0f);

        top.transform.localScale = bottom.transform.localScale = horizontalScale;
        left.transform.localScale = right.transform.localScale = verticalScale;

        point.z = 0;
        point2.z = 0;
        
        topRight.transform.position = point;
        bottomLeft.transform.position = point2;
    }
}
