using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace GraphQL {
  public class GraphQLClient {
    private string url;

    public GraphQLClient(string url) {
      this.url = url;
    }

    [System.Serializable]
    private class GraphQLQuery {
      public string query;
      public object variables { get; set; }
    }

    public UnityWebRequest Query(string query, object variables) {
      var fullQuery = new GraphQLQuery () {
        query = query
        variables = variables
      };

      string json = JsonUtility.ToJson (fullQuery);
      //string sToken = "";

      UnityWebRequest request = UnityWebRequest.Post(url, UnityWebRequest.kHttpVerbPOST);

      byte[] payload = Encoding.UTF8.GetBytes (json);
      UploadHandler data = new UploadHandlerRaw (payload);

      request.uploadHandler = data;
      request.SetRequestHeader ("Content-Type", "application/json");
      //request.SetRequestHeader ("Authorization", "Bearer " + sToken);

      return request;
    }
  }
}
