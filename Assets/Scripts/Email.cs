using UnityEngine;
using UnityEngine.EventSystems;

public class Email : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private bool isDragging = false;

    RectTransform trash, forward;
    public float returnSpeed = 5f;
    public string textForward = "Forward";
    public string textTrash = "Trash";

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
            ImageFollowsCamera();
        }

        // If not dragging, lerp back to the original position
        else if (rectTransform.anchoredPosition != originalPosition)
        {
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

    bool IsOverlapping(RectTransform rect1, RectTransform rect2)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rect1, rect2.position, null) ||
            RectTransformUtility.RectangleContainsScreenPoint(rect2, rect1.position, null);
    }

    void ImageFollowsCamera()
    {
        // Convert the mouse position to UI RectTransform space
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            Input.mousePosition,
            null, //canvas.worldCamera, // Pass the canvas camera (especially for Screen Space - Camera)
            out mousePos);

        rectTransform.anchoredPosition = mousePos;

        // Check if the dragged image is inside the target area
        if (IsOverlapping(trash, rectTransform))
        {
            Debug.Log("Dragged image is inside TRASH!");
        }

        if (IsOverlapping(forward, rectTransform))
        {
            Debug.Log("Dragged image is inside FORWARD!");
        }
    }

    void BackToOriginalPosition()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(
                rectTransform.anchoredPosition,
                originalPosition,
                Time.deltaTime * returnSpeed
            );
    }

}