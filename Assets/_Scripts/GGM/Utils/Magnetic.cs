using System.Collections;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public float dist = .2f;
    public float coinSpeed = 1f;
    public float waitTime = 0.5f;

    void Start()
    {
        StartCoroutine(MoveToPlayer());
    }

    IEnumerator MoveToPlayer()
    {
        // espera antes de começar
        yield return new WaitForSeconds(waitTime);

        // loop contínuo até a moeda chegar perto do player
        while (Vector3.Distance(transform.position, Player.Instance.transform.position) > dist)
        {
            coinSpeed++;
            transform.position = Vector3.MoveTowards(
                transform.position,
                Player.Instance.transform.position,
                Time.deltaTime * coinSpeed
            );

            yield return null; // continua no próximo frame
        }
    }
}
