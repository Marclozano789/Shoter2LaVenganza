using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZaWardo : MonoBehaviour
{
    [Header("Duración de la pausa en segundos")]
    public float stopDuration = 3f;

    [Header("Filtro en Canvas (Image que cubre toda la pantalla)")]
    public Image colorFilter; // Asignar en el Inspector

    [Header("Opciones de fade")]
    [Range(0f, 1f)] public float maxAlpha = 0.5f; // opacidad máxima del filtro
    public float fadeSpeed = 3f; // velocidad del fade-in/fade-out

    private bool isStopped = false;

    void Start()
    {
        if (colorFilter != null)
        {
            colorFilter.enabled = true;
            Color c = colorFilter.color;
            c.a = 0f; // empieza transparente
            colorFilter.color = c;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isStopped)
        {
            StartCoroutine(StopTimeCoroutine());
        }
    }

    private IEnumerator StopTimeCoroutine()
    {
        isStopped = true;

        // Activar filtro y hacer fade-in
        if (colorFilter != null)
            yield return StartCoroutine(FadeFilter(0f, maxAlpha));

        // Detener tiempo del juego
        Time.timeScale = 0f;

        // Temporizador usando tiempo real (unscaledDeltaTime)
        float elapsed = 0f;
        while (elapsed < stopDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        // Restaurar tiempo
        Time.timeScale = 1f;

        // Fade-out del filtro
        if (colorFilter != null)
            yield return StartCoroutine(FadeFilter(maxAlpha, 0f));

        isStopped = false;
    }

    // Corrutina para fade-in / fade-out
    private IEnumerator FadeFilter(float fromAlpha, float toAlpha)
    {
        float t = 0f;
        Color c = colorFilter.color;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime * fadeSpeed;
            c.a = Mathf.Lerp(fromAlpha, toAlpha, t);
            colorFilter.color = c;
            yield return null;
        }
    }
}
