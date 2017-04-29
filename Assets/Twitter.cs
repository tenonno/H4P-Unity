using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using twitter;
using UnityEngine.Networking;

public class Twitter : MonoBehaviour
{
    void Start()
    {
        twitter.Client.consumerKey = "oaakUm1WL80VMb6WWNAA8yAFB";
        twitter.Client.consumerSecret = "A5lLwAgSHxkTScQ17i1uVg0yr6LnDbmAAkpm5DVYCpwENZmeL7";
        twitter.Client.accessToken = "858049623700537344-Ag3otAiQbkYMiGTP3LXGsdFBJO5aIYi";
        twitter.Client.accessTokenSecret = "IBwOJEgBNTIvJ2YzhzfVHdHR2Un9nYxBphLBNwbZat1gH";


    }





    public void RunGistFeelesScript()
    {
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters["count"] = 1.ToString();   // 取得するツイート数
        StartCoroutine(twitter.Client.Get("statuses/home_timeline", parameters, new twitter.TwitterCallback(this.Callback)));
    }

    void Callback(bool success, string response)
    {
        if (success)
        {
            StatusesHomeTimelineResponse Response = JsonUtility.FromJson<StatusesHomeTimelineResponse>(response);

            foreach (var tweet in Response.items)
            {

                StartCoroutine("GetText", tweet.text);

            }

        }
        else
        {
            Debug.Log(response);
        }
    }


    IEnumerator GetText(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        // リクエスト送信
        yield return request.Send();

        // 通信エラーチェック
        if (request.isError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (request.responseCode == 200)
            {
                // UTF8文字列として取得する
                string text = request.downloadHandler.text;


                var a = MiniJSON.Json.Deserialize(text) as Dictionary<string, object>;
                var b = a["files"] as Dictionary<string, object>;

                

                foreach (var key in b.Keys)
                {

                    var c = b[key] as Dictionary<string, object>;

                    var aa = c["content"] as string;


                    var main_lua = @"
                        local _ENV = require('castl.runtime');
                    " + aa;

                    Debug.Log(aa);

                    LuaEngine.Instance.DoString(main_lua);



                }

            }
        }
    }



}