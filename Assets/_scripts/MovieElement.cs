using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MovieElement : MonoBehaviour
{
    [SerializeField] private RawImage _icon;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _overview;

    private Result _movieData;

    public void SetMovieData(Result data)
    {
        _movieData = data;
        UpdateData();
    }

    private void UpdateData()
    {
        StartCoroutine(DownloadImage(_movieData.backdrop_path));

        _title.text = _movieData.title;
        _overview.text = _movieData.overview;
    }

    IEnumerator DownloadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
            Debug.Log(request.error);
        else
        {
            _icon.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
}
