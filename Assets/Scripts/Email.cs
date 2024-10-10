using UnityEngine;
using UnityEngine.EventSystems;

public class Email : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private bool isDragging = false;
    private bool isEnteringRadiusTrash, isEnteringRadiusForward = false;

    RectTransform trash, forward;
    public float returnSpeed = 5f;
    public float triggerDistanceTrash = 50f;
    public float triggerDistanceForward = 50f;
    public GameObject textMessage, textForward, textTrash;

    private Canvas canvas; // Reference to the Canvas for proper mouse position conversion

    private void Start()
    {
        // Get the RectTransform component of the UI element
        rectTransform = GetComponent<RectTransform>();

        // Store the original position
        originalPosition = rectTransform.anchoredPosition;

        // Find the Canvas component (Assuming the script is on a child UI element of the Canvas)
        canvas = GetComponentInParent<Canvas>();

        trash = GameObject.Find("Trash").GetComponent<RectTransform>();
        forward = GameObject.Find("Forward").GetComponent<RectTransform>();
    }

    private void Update()
    {
        // If dragging, set position to mouse position
        if (isDragging)
        {
            ImageFollowsCursor();
            DetectArea();
        }

        // If not dragging, lerp back to the original position
        else if (rectTransform.anchoredPosition != originalPosition)
        {
            if (isEnteringRadiusForward)
            {
                print("SEND FORWARD");
                GetComponent<RectTransformMover>().StartMoveAndDestroy();
            }
            else if (isEnteringRadiusTrash)
            {
                print("SEND TRASH");
            }

            BackToOriginalPosition();
        }
    }

    // Called when the pointer is pressed down on the UI element
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    // Called when the pointer is released
    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    void ImageFollowsCursor()
    {
        Vector2 mousePos;
        // Convert the mouse position to UI RectTransform space
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            Input.mousePosition,
            null, //canvas.worldCamera, // Pass the canvas camera (especially for Screen Space - Camera)
            out mousePos);

        rectTransform.anchoredPosition = mousePos;        
    }

    void BackToOriginalPosition()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(
                rectTransform.anchoredPosition,
                originalPosition,
                Time.deltaTime * returnSpeed
            );
    }

    void DetectArea()
    {
        // TRASH AREA
        // Check if the two RectTransforms are overlapping
        float distanceTrash = CalculateDistanceBetweenRects(rectTransform, trash);

        // Check if the distance is within the trigger distance
        if (distanceTrash < triggerDistanceTrash)
        {
            if (!isEnteringRadiusTrash)
            {
                isEnteringRadiusTrash = true;
                OnRadiusEnterTrash();
            }
        }
        else
        {
            if (isEnteringRadiusTrash)
            {
                isEnteringRadiusTrash = false;
                OnRadiusExitTrash();
            }
        }

        // FORWARD AREA
        // Check if the two RectTransforms are overlapping
        float distanceForward = CalculateDistanceBetweenRects(rectTransform, forward);

        // Check if the distance is within the trigger distance
        if (distanceForward < triggerDistanceForward)
        {
            if (!isEnteringRadiusForward)
            {
                isEnteringRadiusForward = true;
                OnRadiusEnterForward();
            }
        }
        else
        {
            if (isEnteringRadiusForward)
            {
                isEnteringRadiusForward = false;
                OnRadiusExitForward();
            }
        }
    }

    float CalculateDistanceBetweenRects(RectTransform rect1, RectTransform rect2)
    {
        Vector3 rect1WorldPos = rect1.position; // Get world position of rect1
        Vector3 rect2WorldPos = rect2.position; // Get world position of rect2

        // Calculate the distance between the centers of the two RectTransforms
        float distance = Vector3.Distance(rect1WorldPos, rect2WorldPos);

        return distance;
    }

    // Trigger Enter event
    void OnRadiusEnterTrash()
    {
        Debug.Log("Enter TRASH");
        // Add your enter trigger logic here (e.g., enable something)
        textTrash.SetActive(true);
        textMessage.SetActive(false);
    }

    // Trigger Exit event
    void OnRadiusExitTrash()
    {
        Debug.Log("Exit TRASH");
        // Add your exit trigger logic here (e.g., disable something)
        textTrash.SetActive(false);
        textMessage.SetActive(true);
    }

    // Trigger Enter event
    void OnRadiusEnterForward()
    {
        Debug.Log("Enter FORWARD");
        // Add your enter trigger logic here (e.g., enable something)
        textForward.SetActive(true);
        textMessage.SetActive(false);
    }

    // Trigger Exit event
    void OnRadiusExitForward()
    {
        Debug.Log("Exit FORWARD");
        // Add your exit trigger logic here (e.g., disable something)
        textForward.SetActive(false);
        textMessage.SetActive(true);
    }

}
