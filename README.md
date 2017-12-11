# GraphQL Client for Unity using UnityWebRequest

Here is a sample client I made for a Unity app that utilizes a GraphQL api.
I based my code on these sources:

- [bkniffler/graphql-net-client](https://github.com/bkniffler/graphql-net-client)
- [JSONObject](http://wiki.unity3d.com/index.php?title=JSONObject)

### Sample call

``` c#

public class SomeGameObject : MonoBehaviour {

  public APIClient api;

  void Start () {
    StartCoroutine (api.QueryCall( (bool success) => {
        if (success)
          Debug.Log( "success!");
        else
          Debug.Log( "fail!");
    }))
  }
}

```

*For any clarification or comment, feel free to get in touch with me! :)*
