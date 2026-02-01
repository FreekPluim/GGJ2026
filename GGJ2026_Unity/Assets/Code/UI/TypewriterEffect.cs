using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text label;

    [Header("Settings")]
    [SerializeField] private bool beginOnStart = true;
    [SerializeField] private float startDelay;
    [SerializeField] private float typeDelay;

    [SerializeField] private string specialDelayChars;
    [SerializeField] private float specialDelay;

    [Space]
    [SerializeField] private UnityEvent onEffectEnded;

    private string initialString;
    private bool skipReq;

    private void Start()
    {
        initialString = label.text;
        label.text = "";
        if (beginOnStart) { StartEffect(); }
    }

    public void SkipCurrentEffect()
    {
        skipReq = true;
    }

    public void StartEffect()
    {
        skipReq = false;
        StartCoroutine(TypeCo());
    }

    private IEnumerator TypeCo()
    {
        yield return new WaitForSeconds(startDelay);

        string typedText = "";
        for (int i = 0; i < initialString.Length; i++)
        {
            if (skipReq) { break; }

            typedText += initialString[i];
            label.text = typedText;

            float delay = typeDelay;
            if (specialDelayChars.Contains(initialString[i]))
            {
                delay = specialDelay;
            }

            yield return new WaitForSeconds(delay);
        }

        label.text = initialString;
        onEffectEnded?.Invoke();
    }
}
