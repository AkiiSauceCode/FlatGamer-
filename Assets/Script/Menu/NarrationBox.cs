using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class NarrationBox : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public Image imageA;
    public Image imageB;

    public string[] lines;
    public Sprite[] cutsceneSprites;

    public float textSpeed = 0.03f;
    public float transitionDuration = 0.5f;
    public GameObject narrationBox;
    public GameObject TutorialBox;
    public GameObject SkipButton;
    private int index = 0;
    private bool isTyping;
    private bool isTransitioning;

    private Image currentImage;
    private Image nextImage;
    private RectTransform currentRect;
    private RectTransform nextRect;

    public float swipeDistance = 1034.46f;
    private Vector2 fixedPosition = Vector2.zero;

    void Start()
    {
        TutorialBox.SetActive(false);
        currentImage = imageA;
        nextImage = imageB;

        currentRect = currentImage.rectTransform;
        nextRect = nextImage.rectTransform;

        currentImage.sprite = cutsceneSprites[0];
        currentRect.anchoredPosition = fixedPosition;
        nextImage.gameObject.SetActive(false);

        StartCoroutine(TransitionEnter());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTransitioning)
        {
            if (isTyping)
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }


    IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = "";

        foreach (char c in lines[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    IEnumerator TransitionEnter()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (index >= lines.Length - 1)
        {
            SkipButton.SetActive(false);
            narrationBox.SetActive(false);
            TutorialBox.SetActive(true);
            return;
        }

        index++;
        StartCoroutine(SwipeTransition());
    }

    IEnumerator SwipeTransition()
    {
        isTransitioning = true;

        nextImage.sprite = cutsceneSprites[index];
        nextImage.gameObject.SetActive(true);

        currentRect.anchoredPosition = fixedPosition;
        nextRect.anchoredPosition = new Vector2(swipeDistance, fixedPosition.y);

        float elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            float t = elapsed / transitionDuration;

            currentRect.anchoredPosition = Vector2.Lerp(fixedPosition, new Vector2(-swipeDistance, fixedPosition.y), t);
            nextRect.anchoredPosition = Vector2.Lerp(new Vector2(swipeDistance, fixedPosition.y), fixedPosition, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        currentRect.anchoredPosition = new Vector2(-swipeDistance, fixedPosition.y);
        nextRect.anchoredPosition = fixedPosition;

        // Swap images
        Image tempImg = currentImage;
        currentImage = nextImage;
        nextImage = tempImg;

        RectTransform tempRect = currentRect;
        currentRect = nextRect;
        nextRect = tempRect;

        nextImage.gameObject.SetActive(false);

        textComponent.text = "";
        StartCoroutine(TypeLine());

        isTransitioning = false;
    }

    
}
