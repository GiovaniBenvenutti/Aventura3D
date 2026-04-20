using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsBehavior : MonoBehaviour
{
    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseSize()
    {
        transform.localScale *= 1.1f;
        if (transform.localScale.magnitude > originalScale.magnitude * 1.5f)
        {
            transform.localScale = originalScale;
        }
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        Vector3 originalPosition = transform.position;
        float duration = 0.5f;
        float magnitude = 0.1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.position = originalPosition + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void Shrink()
    {
        transform.localScale *= 0.9f;
    }
}
