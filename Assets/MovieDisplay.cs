using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MovieDisplay : MonoBehaviour
{
    [SerializeField] private MovieElement _movieElementPrefab;
    [SerializeField] private Transform _movieLayout;

    public string apiURL = "https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=popularity.desc";
    public string apiKey = "00243336e2f949edba05fc655da4510e";
    public string authorizationToken = "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIwMDI0MzMzNmUyZjk0OWVkYmEwNWZjNjU1ZGE0NTEwZSIsInN1YiI6IjVhYzFjM2IxMGUwYTI2NGE1NzA1NmEwMSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.uy3Lj5gCGGhxulu3ocPzJVh10f7KE_x1IDSE16CGzKw";

    private void Start()
    {
        StartCoroutine(LoadData());
    }

    private IEnumerator LoadData()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiURL);
        request.SetRequestHeader("Authorization", authorizationToken);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error while fetching movie data: " + request.error);
            yield break;
        }

        string jsonResult = request.downloadHandler.text;
        var movieList = JsonUtility.FromJson<RootObject>(jsonResult);

        foreach (var movie in movieList.results)
        {
            Instantiate(_movieElementPrefab, _movieLayout).SetMovieData(movie);
        }
    }
}
