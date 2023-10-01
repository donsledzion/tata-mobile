using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Kidbook.Models;
using UnityEngine.Networking;
using System.Collections;
using System;
using System.IO;
using Unity.VisualScripting;

public class FeedsPost : MonoBehaviour
{
    [Header("Main components Rect Transforms")]
    [SerializeField] private RectTransform headerRect;
    [SerializeField] private RectTransform quoteRect;
    [SerializeField] private RectTransform pictureRect;
    [SerializeField] private RectTransform footerRect;

    [Header("Header Section")]
    [SerializeField] private Image thumb;
    [SerializeField] private TextMeshProUGUI dimNameTMP;
    [SerializeField] private Image iconFollow;
    [SerializeField] private Image iconLike;


    [Header("Quote")]
    [SerializeField] private TextMeshProUGUI quoteTMP;

    [Header("Picture")]
    [SerializeField] private Image picture;
    [SerializeField] private GameObject loadingCircle;

    [Header("Footer Section")]
    [SerializeField] TextMeshProUGUI nameTMP;
    [SerializeField] TextMeshProUGUI dateTMP;

    internal void FillPost(Post postModel)
    {
        dimNameTMP.text = postModel.kid.dim_name;
        quoteTMP.text = postModel.sentence;
        nameTMP.text = postModel.kid.first_name;
        dateTMP.text = postModel.said_at;
        quoteRect.sizeDelta = new Vector2(quoteRect.sizeDelta.x, quoteTMP.preferredHeight+50f);
        StartCoroutine(GetPicture(thumb,postModel.kid.GetThumb()));
        StartCoroutine(GetPicture(picture, postModel.GetPicture(), loadingCircle));
    }

    IEnumerator GetPicture(Image targetImage, string imageURL, GameObject loadingImage = null)
    {
        Texture2D texture = null;
        if (File.Exists(Path.Combine(Application.persistentDataPath, imageURL)))
        {
            Debug.Log("<color=green>File " + imageURL + " exists in storage</color>");
            texture = GetPictureFromStorage(imageURL);
            yield return new WaitForEndOfFrame();
        }
        else
        {

            Debug.Log("<color=red>File " + imageURL + " not found in storage</color>");
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(Consts.BaseURL + imageURL);
            yield return www.SendWebRequest();
            loadingImage?.SetActive(true);

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                yield break;
            }
            else
            {
                texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                StorePictureToStorage(texture, imageURL);
                loadingImage?.SetActive(false);
            }
        }
        float pictureRatio = (float)texture.width / (float)texture.height;
        float fittedHeight = texture.width / pictureRatio;
        Debug.Log("Texture width: " + texture.width + " height: " + texture.height + " | ratio: " + pictureRatio + " | fitted height: " + fittedHeight);
        pictureRect.sizeDelta = new Vector2(pictureRect.sizeDelta.x, fittedHeight * (pictureRect.sizeDelta.x / texture.width));
        targetImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
        
    }

    private void StorePictureToStorage(Texture2D texture, string imageURL)
    {
        string[] dirs = imageURL.Split('/');
        string path = Application.persistentDataPath;
        for (int i = 0; i < dirs.Length - 1; i++)
        {
            path = Path.Combine(path, dirs[i]);
            Directory.CreateDirectory(path);
        }
        string imageFile = dirs[dirs.Length - 1];
        byte[] textureBytes;

        switch (imageFile.Split('.')[1])
        {
            case "png":
                textureBytes = texture.EncodeToPNG();
                break;
            case "jpg":
                textureBytes = texture.EncodeToJPG();
                break;
            default:
                textureBytes = texture.EncodeToJPG();
                break;
        }
        File.WriteAllBytes(Path.Combine(path, imageFile), textureBytes);


    }

    private Texture2D GetPictureFromStorage(string imageURL)
    {
        byte[] textureBytes = File.ReadAllBytes(Path.Combine(Application.persistentDataPath, imageURL));
        Texture2D loadedTexture = new Texture2D(0, 0);
        loadedTexture.LoadImage(textureBytes);
        return loadedTexture;
    }
}
