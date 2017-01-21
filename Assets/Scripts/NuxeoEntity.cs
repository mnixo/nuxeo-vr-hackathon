using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuxeoEntity {

    public string entityType;
    public string uid;
    public string path;
    public string type;
    public string parentRef;
    public string title;

    public string entityUrl;
    public string childrenUrl;

    public NuxeoEntity(JSONObject obj, string baseUrl) {
        entityType = obj.GetField("entity-type").str;
        uid = obj.GetField("uid").str;
        path = obj.GetField("path").str;
        type = obj.GetField("type").str;
        parentRef = obj.GetField("parentRef").str;
        title = obj.GetField("title").str;
        entityUrl = baseUrl + "api/v1/id/" + uid;
        childrenUrl = entityUrl + "/@children";
    }

    public NuxeoEntity() {

    }

}
