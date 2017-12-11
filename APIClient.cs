using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using GraphQL;

public class APIClient : MonoBehaviour {

  //not a real api url
  string apiUrl = "https://graphqlapi.com/queries";

  string query = @"query{
    organizations {
      id
    }
  }";

  string mutation = @"mutation AddMember($input: RegistrationInput!) {
    registration( input: $input ) {
      contact {
        name
        email
      }
    }
  }";

  [System.Serializable]
  private class Member {
    public string name;
    public string email;
  }


  public IEnumerator QueryCall (System.Action<bool> callback) {
    GraphQLClient client = new GraphQLClient (apiUrl);

    using( UnityWebRequest www = client.Query(query, "", "")) {
      yield return www.Send();

      if (www.isError) {
        Debug.Log (www.error);

        callback (false);
      } else {
        string responseString = www.downloadHandler.text;
        JSONObject response = new JSONObject (responseString);
        JSONObject data = response.GetField ("data");
        JSONObject organizations = data.GetField ("organizations");
        accesData( organizatios )

        callback (true);
      }
    }
  }

  public IEnumarator MutationCall () {
    GraphQLClient client = new GraphQLClient (apiUrl);

    Member memberInput = new Member () {
      name = "Bob Jones",
      email = "bob@jones.com"
    };

    string variables = @"{""input"": "+ JsonUtility.ToJson(memberInput) +" }";

    using( UnityWebRequest www = client.Query(mutation, variables, "AddMember")) {
      yield return www.Send();

      if (www.isError) {
        Debug.Log (www.error);
        // handle error
      } else {
        string responseString = www.downloadHandler.text;
        JSONObject reponse = new JSONObject (responseString);

        // handle json result with JSONObject
      }
    }
  }

  void accessData(JSONObject obj){
    switch(obj.type){
    case JSONObject.Type.OBJECT:
      for(int i = 0; i < obj.list.Count; i++){
        string key = (string)obj.keys[i];
        JSONObject j = (JSONObject)obj.list[i];
        Debug.Log(key);
        accessData(j);
      }
      break;
    case JSONObject.Type.ARRAY:
      foreach(JSONObject j in obj.list){
        accessData(j);
      }
      break;
    case JSONObject.Type.STRING:
      Debug.Log(obj.str);
      break;
    case JSONObject.Type.NUMBER:
      Debug.Log(obj.n);
      break;
    case JSONObject.Type.BOOL:
      Debug.Log(obj.b);
      break;
    case JSONObject.Type.NULL:
      Debug.Log("NULL");
      break;

    }
  }

}
