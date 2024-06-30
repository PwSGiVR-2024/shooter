using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BloodEffectManager : MonoBehaviour
{
    public static BloodEffectManager Instance { get; private set; }

    [SerializeField] private List<Image> _bloodSplatters;
    [SerializeField] private float _fadeOutDuration = 0.5f;
    [SerializeField] private int _minSplatters = 1;
    [SerializeField] private int _maxSplatters = 3;

    private void Awake()
    {
        if (transform.parent != null)
        {
            transform.SetParent(null); // Make it a root object
        }
        // Singleton pattern implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Ensure all splatters are initially invisible
        foreach (var splatter in _bloodSplatters)
        {
            splatter.enabled = false;
        }
    }

    public void ShowBloodEffect()
    {
        int splatterCount = Random.Range(_minSplatters, _maxSplatters + 1);
        List<Image> availableSplatters = new List<Image>(_bloodSplatters);

        for (int i = 0; i < splatterCount; i++)
        {
            if (availableSplatters.Count == 0)
            {
                break;
            }

            int index = Random.Range(0, availableSplatters.Count);
            Image splatter = availableSplatters[index];
            availableSplatters.RemoveAt(index);

            // Randomize position
            splatter.rectTransform.anchoredPosition = new Vector2(
                Random.Range(-Screen.width / 2f, Screen.width / 2f),
                Random.Range(-Screen.height / 2f, Screen.height / 2f)
            );

            // Randomize rotation
            splatter.rectTransform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

            // Randomize scale
            float scale = Random.Range(0.5f, 1.5f);
            splatter.rectTransform.localScale = new Vector3(scale, scale, 1);

            StartCoroutine(FadeOutSplatter(splatter));
        }
    }

    private System.Collections.IEnumerator FadeOutSplatter(Image splatter)
    {
        splatter.enabled = true;
        splatter.gameObject.SetActive(true);
        splatter.color = new Color(splatter.color.r, splatter.color.g, splatter.color.b, 1f);

        float elapsedTime = 0f;
        while (elapsedTime < _fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / _fadeOutDuration);
            splatter.color = new Color(splatter.color.r, splatter.color.g, splatter.color.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        splatter.gameObject.SetActive(false);
        splatter.enabled = false;
    }
}