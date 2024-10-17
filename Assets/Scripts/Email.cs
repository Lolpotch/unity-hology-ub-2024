using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Email : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    Image image;
    private Vector2 originalPosition;
    private bool isDragging = false;
    bool isDragable = true;
    private bool isEnteringRadiusTrash, isEnteringRadiusForward = false;
    private bool decreaseOnce;
    Vector2 trashDefaultSize, forwardDefaultSize;

    RectTransform trash, forward;
    public float returnSpeed = 5f;
    public float triggerDistanceTrash = 50f;
    public float triggerDistanceForward = 50f;
    public float increaseSizePercentage = 1.5f;
    public GameObject textMessage, textForward, textTrash;
    public Color trashColorEmail, forwardColorEmail;
    public int happyEffectForward, moneyEffectForward, securityEffectForward;
    public int happyEffectTrash, moneyEffectTrash, securityEffectTrash;

    private Canvas canvas; // Reference to the Canvas for proper mouse position conversion

    private void Start()
    {
        // Get the RectTransform component of the UI element
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        // Store the original position
        originalPosition = rectTransform.anchoredPosition;

        // Find the Canvas component (Assuming the script is on a child UI element of the Canvas)
        canvas = GetComponentInParent<Canvas>();

        trash = GameObject.Find("Trash").GetComponent<RectTransform>();
        forward = GameObject.Find("Forward").GetComponent<RectTransform>();

        trashDefaultSize =  trash.sizeDelta;
        forwardDefaultSize =  forward.sizeDelta;

        isDragable = true;
        decreaseOnce = false;
    }

    private void Update()
    {
            // If dragging, set position to mouse position
            if (isDragging && isDragable)
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
                    GetComponent<RectTransformMover>().StartMoveAndDestroy(Vector2.right);
                    GetComponent<ImageColorChanger>().StartChangeColor(Color.green);

                    Image fImage = forward.GetComponent<Image>();
                    Color currentColor = fImage.color;
                    currentColor.a = .6f;
                    fImage.color = currentColor;

                    //Size back to normal
                    forward.sizeDelta = forwardDefaultSize;

                    isDragable = false;

                    if (!decreaseOnce)
                    {
                        decreaseOnce = true;
                        FindObjectOfType<EmailRemaining>().DecreaseRemaining(1);

                        if (happyEffectForward != 0)
                        {FindObjectOfType<StatsEffect>().EffectHappy(happyEffectForward);}
                        if (moneyEffectForward != 0)
                        {FindObjectOfType<StatsEffect>().EffectMoney(moneyEffectForward);}
                        if (securityEffectForward != 0)
                        {FindObjectOfType<StatsEffect>().EffectSecurity(securityEffectForward);}

                        FindAnyObjectByType<SoundEffectPlayer>().PlaySwipe();
                    }

                    
                }
                else if (isEnteringRadiusTrash)
                {
                    print("SEND TRASH");
                    GetComponent<RectTransformMover>().StartMoveAndDestroy(Vector2.left);
                    GetComponent<ImageColorChanger>().StartChangeColor(Color.red);

                    Image tImage = trash.GetComponent<Image>();
                    Color currentColor = tImage.color;
                    currentColor.a = .6f;
                    tImage.color = currentColor;

                    //Size back to normal
                    trash.sizeDelta = trashDefaultSize;

                    isDragable = false;

                    if (!decreaseOnce)
                    {
                        decreaseOnce = true;
                        FindObjectOfType<EmailRemaining>().DecreaseRemaining(1);
                        
                        if (happyEffectTrash != 0)
                        {FindObjectOfType<StatsEffect>().EffectHappy(happyEffectTrash);}
                        if (moneyEffectTrash != 0)
                        {FindObjectOfType<StatsEffect>().EffectMoney(moneyEffectTrash);}
                        if (securityEffectTrash != 0)
                        {FindObjectOfType<StatsEffect>().EffectSecurity(securityEffectTrash);}

                        FindAnyObjectByType<SoundEffectPlayer>().PlaySwipe();
                    }

                    
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

        image.color = trashColorEmail;

        Image tImage = trash.GetComponent<Image>();
        Color currentColor = tImage.color;
        currentColor.a = 1f;
        tImage.color = currentColor;

        // Make the Image size bigger by modifying the RectTransform size
        trash.sizeDelta = new Vector2(trashDefaultSize.x * increaseSizePercentage, trashDefaultSize.y * increaseSizePercentage);
    }

    // Trigger Exit event
    void OnRadiusExitTrash()
    {
        Debug.Log("Exit TRASH");
        // Add your exit trigger logic here (e.g., disable something)
        textTrash.SetActive(false);
        textMessage.SetActive(true);

        image.color = Color.white;

        Image tImage = trash.GetComponent<Image>();
        Color currentColor = tImage.color;
        currentColor.a = .6f;
        tImage.color = currentColor;

        //Size back to normal
        trash.sizeDelta = trashDefaultSize;
    }

    // Trigger Enter event
    void OnRadiusEnterForward()
    {
        Debug.Log("Enter FORWARD");
        // Add your enter trigger logic here (e.g., enable something)
        textForward.SetActive(true);
        textMessage.SetActive(false);

        image.color = forwardColorEmail;

        Image fImage = forward.GetComponent<Image>();
        Color currentColor = fImage.color;
        currentColor.a = 1f;
        fImage.color = currentColor;

        // Make the Image size bigger by modifying the RectTransform size
        forward.sizeDelta = new Vector2(forwardDefaultSize.x * increaseSizePercentage, forwardDefaultSize.y * increaseSizePercentage);
    }

    // Trigger Exit event
    void OnRadiusExitForward()
    {
        Debug.Log("Exit FORWARD");
        // Add your exit trigger logic here (e.g., disable something)
        textForward.SetActive(false);
        textMessage.SetActive(true);

        image.color = Color.white;

        Image fImage = forward.GetComponent<Image>();
        Color currentColor = fImage.color;
        currentColor.a = .6f;
        fImage.color = currentColor;

        //Size back to normal
        forward.sizeDelta = forwardDefaultSize;
    }

}
