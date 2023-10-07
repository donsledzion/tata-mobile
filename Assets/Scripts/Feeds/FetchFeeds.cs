using Kidbook.Models;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FetchFeeds : MonoBehaviour
{
    [SerializeField] private Button fetchFeedsButton;
    [SerializeField] private Transform targetViewport;
    [SerializeField] private GameObject feedPostPrefab;
    [SerializeField] private ScreenSwitcher screenSwitcher;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject loadingCircleFetchingFeedsForTheFirstTime;

    private int lastLoadedPage = 1;
    private bool loading = false;

    [ContextMenu("Fetch My Feeds")]
    public void FetchMyFeeds()
    {
        //screenSwitcher.SwitchToFeedsPanel();
        StartCoroutine(FetchFeedsCor());
    }
    private IEnumerator FetchFeedsCor()
    {
        loading = true;
        string pageToLoad = lastLoadedPage == 1 ? string.Empty : "?page="+lastLoadedPage.ToString();
        string getReqeust = Consts.BaseAPI_URL + Consts.URI.FetchFeeds + pageToLoad;
        UnityWebRequest request = UnityWebRequest.Get(getReqeust);
        request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString(Consts.TokenKey));
        request.SetRequestHeader("Accept", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            Debug.Log(request.downloadHandler.text);
            fetchFeedsButton.interactable = true;
        }
        else
        {
            fetchFeedsButton.interactable = true;

            string plainResponse = request.downloadHandler.text;
            Debug.Log(plainResponse);
            var posts = Newtonsoft.Json.JsonConvert.DeserializeObject<Post[]>(plainResponse);
            int postCounter = 1 ;
            if (posts.Length < 1)
            {
                // Handle user popup that there is no feeds to display :|
                yield break;
            }
            if (loadingCircleFetchingFeedsForTheFirstTime.activeSelf == true)
                loadingCircleFetchingFeedsForTheFirstTime.SetActive(false);

            foreach (Post post in posts)
            {
                GameObject postInstance = Instantiate(feedPostPrefab, targetViewport);
                postInstance.name = "Post-" + lastLoadedPage +"-"+ postCounter;
                postCounter++;
                FeedsPost feedsPost = postInstance.GetComponent<FeedsPost>();
                feedsPost.FillPost(post);
            }
            lastLoadedPage++;
        }
        yield return new WaitForEndOfFrame();
        loading = false;
    }

    public void CheckScrollingPosition()
    {
        float position = scrollRect.verticalNormalizedPosition;
        if (position < 0.2f)
        {            
            Debug.Log("Time to load more feeds!");
            if (!loading)
                StartCoroutine(FetchFeedsCor());
            else
                Debug.Log("But there is loading in progres!");
        }
    }

    private void OnEnable()
    {
        FetchMyFeeds();
    }
}
