using System;
using System.Collections.Generic;
using UnityEngine;

public static partial class Core {
    public static float DirectionToAngle(Vector3 vector, bool inDegrees = false) {
        if(inDegrees)
            return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        return Mathf.Atan2(vector.y, vector.x);
    }
}

// Priority Queue Code
public class PriorityQueue<T> {
    private List<Tuple<T , int>> elements = new List<Tuple<T , int>>();
    public int Count => elements.Count;

    public void Enqueue(T item , int priority) {
        elements.Add(Tuple.Create(item , priority));
    }

    public T Dequeue() {
        int bestIndex = 0;
        for (int i = 0 ; i < elements.Count ; ++i) {
            if (elements[i].Item2 < elements[bestIndex].Item2)
                bestIndex = i;
        }
        T bestItem = elements[bestIndex].Item1;
        elements.RemoveAt(bestIndex);
        return bestItem;
    }
}