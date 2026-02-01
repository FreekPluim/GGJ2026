using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Fader : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;

    [Space]
    [SerializeField] private float fadeDelay;
    [SerializeField] private float fadeTime;

    [Space]
    [SerializeField] private UnityEvent OnFadeEnd;

    private bool fadeIn;

    public void StartFade()
    {
        fadeIn = group.alpha < 0.5f;
        StartCoroutine(FadeCo());
    }

    private IEnumerator FadeCo()
    {
        yield return new WaitForSeconds(fadeDelay);

        float counter = 0f;
        while (counter < fadeTime)
        {
            counter += Time.deltaTime;
            group.alpha = fadeIn ? counter / fadeTime : 1f - (counter / fadeTime);
            yield return null;
        }

        group.alpha = fadeIn ? 1f : 0f;
        OnFadeEnd?.Invoke();
    }
}
