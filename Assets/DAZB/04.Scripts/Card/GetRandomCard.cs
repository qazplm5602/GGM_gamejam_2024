using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NumbeWeight {
    public int number;
    public float weight;
}

[System.Serializable]
public class ShapeWeight {
    public CardShape cardShape;
    public float shapeWeight;
}

public class GetRandomCard : MonoBehaviour {
    public static GetRandomCard instance;
    private void Awake() {
        instance = this;
    }
    public NumbeWeight[] numbeWeights;
    public ShapeWeight[] shapeWeights;

    private void Start() {
        numbeWeights = new NumbeWeight[14];
        shapeWeights = new ShapeWeight[4];
        for (int i = 1; i <= 14; ++i) {
            numbeWeights[i - 1] = new NumbeWeight();
            numbeWeights[i - 1].number = i;
            numbeWeights[i - 1].weight = 25;
        }
        for (int i = 1; i <= 4; ++i) {
            shapeWeights[i - 1] = new ShapeWeight();
            shapeWeights[i - 1].cardShape = (CardShape)i - 1;
            shapeWeights[i - 1].shapeWeight = 25;
        }
    }

    public Card GetRandom() {
        return new Card(GetShape(), GetNumber());
    }

    private int GetNumber() {
        int totalWeight = 0;
        foreach (NumbeWeight item in numbeWeights) {
            totalWeight += (int)item.weight;
        }
        float randomNumber = Random.Range(0, totalWeight);
        foreach (NumbeWeight item in numbeWeights) {
            if (randomNumber < item.weight) {
                return item.number;
            }
            randomNumber -= item.weight;
        }
        return 0;
    }

    private CardShape GetShape() {
        float totalWeight = 0;
        foreach (ShapeWeight item in shapeWeights) {
            totalWeight += item.shapeWeight;
        }
        float randomNumber = Random.Range(0, totalWeight);
        foreach (ShapeWeight item in shapeWeights) {
            if (randomNumber < item.shapeWeight) {
                return item.cardShape;
            }
            randomNumber -= item.shapeWeight;
        }
        return CardShape.SPADE;
    }
}