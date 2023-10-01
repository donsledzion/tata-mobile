using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringSplitter : MonoBehaviour
{
    [SerializeField] private string stringToSplit;
    void Start()
    {
        SplitString(stringToSplit);
    }

    [ContextMenu("Split String")]
    public void SplitString(string stringToSplit)
    {
        string[] splittedStrings = stringToSplit.Split('/');
        foreach (string str in splittedStrings)
        {
            Debug.Log(str);
        }
    }
}
