using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TextFadeInOut : MonoBehaviour
{
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;
    public float delayBeforeShowingText = 1f;
    public Text uiText;
    public DisappearanceKey disappearanceKey = DisappearanceKey.E;
    public List<KeyCode> customDisappearanceKeys = new List<KeyCode>();

    private bool canFadeOut = false;

    public enum DisappearanceKey
    {
        E,
        ArrowUp,
        ArrowDown,
        ArrowLeft,
        ArrowRight,
        A,
        B,
        C,
        D,
    }

    private void Start()
    {
        uiText.enabled = false;
        StartCoroutine(FadeInText());
    }

    public void HideText()
    {
        uiText.enabled = false;
    }

    private void Update()
    {
        if (canFadeOut)
        {
            bool keyPressed = false;

            if (Input.GetKeyDown((KeyCode)disappearanceKey))
            {
                keyPressed = true;
            }
            else
            {
                foreach (KeyCode customKey in customDisappearanceKeys)
                {
                    if (Input.GetKeyDown(customKey))
                    {
                        keyPressed = true;
                        break;
                    }
                }
            }

            if (keyPressed)
            {
                StartCoroutine(FadeOutText());
            }
        }
    }

    private IEnumerator FadeInText()
    {
        yield return new WaitForSeconds(delayBeforeShowingText);

        uiText.enabled = true;

        float startAlpha = 0f;
        float currentTime = 0f;

        while (currentTime < fadeInDuration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 1f, currentTime / fadeInDuration);

            Color textColor = uiText.color;
            textColor.a = alpha;
            uiText.color = textColor;

            yield return null;
        }

        canFadeOut = true;
    }

    private IEnumerator FadeOutText()
    {
        float startAlpha = 1f;
        float currentTime = 0f;

        while (currentTime < fadeOutDuration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, currentTime / fadeOutDuration);

            Color textColor = uiText.color;
            textColor.a = alpha;
            uiText.color = textColor;

            yield return null;
        }

        uiText.enabled = false;
    }
}
