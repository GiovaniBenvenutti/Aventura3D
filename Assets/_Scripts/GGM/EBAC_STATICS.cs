using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
//using DO.Tweening;

public static class EBAC_STATICS
{


    public static T GetRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static T GetNewRandom<T>(this List<T> list, T lastValue = default)
    {
        // Se lastValue não foi passado, usa o primeiro elemento da lista
        if (EqualityComparer<T>.Default.Equals(lastValue, default(T)))
        {
            lastValue = list[0];
        }

        // Se só existe um elemento, não há como escolher outro diferente
        if (list.Count <= 1) return lastValue;

        // Cria uma lista só com os elementos diferentes de lastValue
        var candidates = list.FindAll(item => !EqualityComparer<T>.Default.Equals(item, lastValue));

        // Sorteia entre os candidatos
        return candidates[Random.Range(0, candidates.Count)];
    }



   

    public static void RandomizeColor(this GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Color newColor = new Color(Random.value, Random.value, Random.value);
            renderer.material.color = newColor;
            Debug.Log($"Nova cor aplicada ao objeto {obj.name}: {newColor}");
        }
        else
        {
            Debug.LogWarning($"O objeto {obj.name} não possui Renderer para aplicar cor.");
        }
    }
    



}


