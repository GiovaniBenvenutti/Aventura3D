using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentHelper : MonoBehaviour
{
    public List<Transform> positions;
    public float duration = 1f;

    private int _index;

    private void Start() 
    {
        transform.position = positions[0].position;
        _index = Random.Range(0, positions.Count);
        NextIndex();
        StartCoroutine(StartMoviment(transform, 1f));
    }

    private void NextIndex()
    {
        _index++;
        if(_index >= positions.Count) _index = 0;
    }
    
    IEnumerator StartMoviment (Transform target, float speed) 
    {
        float time = 0f;

        while(true) 
        {
            var currentPosition = transform.position;

            while(time < duration) 
            {
                time += Time.deltaTime;
                var t = time / duration;
                target.position = Vector3.Lerp(currentPosition, positions[_index].position, t);
                yield return null;
            }

            time = 0f;
            NextIndex();

            yield return null;
        }
    }
}
