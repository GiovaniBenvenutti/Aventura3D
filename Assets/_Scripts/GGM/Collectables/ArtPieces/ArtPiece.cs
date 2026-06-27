using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtPiece : MonoBehaviour
{
    public GameObject currentArtPiece;

    public void ChangePiece(GameObject newPiece)
    {
        if(currentArtPiece != null) Destroy(currentArtPiece);

        currentArtPiece = Instantiate(newPiece, transform);
        currentArtPiece.transform.localPosition = Vector3.zero;
    }
}
