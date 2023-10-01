using System.Collections;
using Kidbook.Models;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FetchKids : MonoBehaviour
{
    [SerializeField] private Button fetchKidsButton;
    [SerializeField] private Transform targetViewport;
    [SerializeField] private GameObject kidButtonPrefab;
    public void FetchMyKids()
    { 
        StartCoroutine(FetchKidsCor());
    }

    private IEnumerator FetchKidsCor()
    {

        UnityWebRequest request = UnityWebRequest.Get(Consts.BaseAPI_URL + Consts.URI.FetchKids);
        request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString(Consts.TokenKey));
        request.SetRequestHeader("Accept", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            Debug.Log(request.downloadHandler.text);
            fetchKidsButton.interactable = true;
        }
        else
        {
            fetchKidsButton.interactable = true;

            string plainResponse = request.downloadHandler.text;
            Debug.Log(plainResponse);
            var kids = Newtonsoft.Json.JsonConvert.DeserializeObject<Kid[]>(plainResponse);
            Debug.Log("Kids count: "+kids.Length);
            foreach (Kid kid in kids)
            {
                GameObject kidButton = Instantiate(kidButtonPrefab, targetViewport);
                kidButton.GetComponentInChildren<TextMeshProUGUI>().text = kid.dim_name;
                Debug.Log("Kid: " + kid.dim_name);
            }
        }
    }
}
