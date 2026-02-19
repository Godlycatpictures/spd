using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HideSecretScript : MonoBehaviour
{

    private Tilemap tilemap;
    private Color color;

    [SerializeField]private float transitionSpeed = 1f;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        color = tilemap.color;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {

            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(ToggleSecret(0f, transitionSpeed));

        }
    }
   
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(ToggleSecret(1f, transitionSpeed));
        }
    }
   

    private IEnumerator ToggleSecret(float transparency, float transitionSpeed)
    {
        color = tilemap.color;
        float startAlpha = color.a;
        float time = 0f;

        while (time < transitionSpeed)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, transparency, time / transitionSpeed);
            tilemap.color = color;
            yield return null;
        }

        color.a = transparency;
        tilemap.color = color;
        fadeCoroutine = null;
    }
}
