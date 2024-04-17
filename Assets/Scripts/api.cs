using UnityEngine;

public class api : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Test();
    }

    async void Test()
    {
        API api = new API("http://localhost:5000");
        try
        {
            await api.Register("testfinfinfin", "test");
            Debug.Log("Registered");
            var cookies = await api.Login("testfinfinfin", "test");
            Debug.Log("Logged in");
            await api.Authenticate(cookies);
            Debug.Log("Authenticated");
        }
        catch (APIException e)
        {
            Debug.LogError(e.Message);
        }


    }
}
