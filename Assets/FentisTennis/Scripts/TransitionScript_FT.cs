using UnityEngine;
using UnityEngine.UI;

public class TransitionScript_FT : MonoBehaviour
{
    public RectTransform badge;
    public Canvas score;
    public ParticleSystem particles;
    public float duration = 2f;
    float elapsedTime = 0f;
    Vector3 initialScale;

    private void Start()
    {
        initialScale = badge.localScale;
    }

    private void Update()
    {
        if (elapsedTime < duration)
        {
            score.enabled = false;
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            badge.localEulerAngles = new Vector3(badge.localEulerAngles.x, Mathf.SmoothStep(-180, 0, t), badge.localEulerAngles.z);
            badge.localScale = initialScale * Mathf.Lerp(0.25f, 1f, t);
        }
        else
        {
            badge.localEulerAngles = Vector3.zero;
            badge.localScale = initialScale;
            elapsedTime = 0f;
            score.enabled = true;
            if (particles != null) particles.Play();
            gameObject.SetActive(false);
        }
    }
}
