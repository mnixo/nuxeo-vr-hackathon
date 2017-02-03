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
    public string description;
    public List<string> facets;
    public string fileDataUrl;

    public string entityUrl;
    public string childrenUrl;

    public Texture2D image; // for Picture documents
    public Mesh mesh; // for 3D documents

    public NuxeoEntity(JSONObject obj, string baseUrl) {
        
        entityType = obj.GetField("entity-type").str;
        uid = obj.GetField("uid").str;
        path = obj.GetField("path").str;
        type = obj.GetField("type").str;
        parentRef = obj.GetField("parentRef").str;
        title = obj.GetField("title").str;
        facets = new List<string>();
        foreach (JSONObject facet in obj.GetField("facets").list) {
            facets.Add(facet.str);
        }
        if (obj.HasField("properties")) {
            JSONObject properties = obj.GetField("properties");
            if (properties.HasField("file:content")) {
                JSONObject fileContent = properties.GetField("file:content");
                fileDataUrl = fileContent.GetField("data").str;
            }
            if (properties.HasField("dc:description")) {
                description = properties.GetField("dc:description").str;
            }
        }
        if (is3d()) {
            type = "3D";
        }
        entityUrl = baseUrl + "api/v1/id/" + uid;
        childrenUrl = entityUrl + "/@children";

    }

    public NuxeoEntity() {

    }

    public bool isFolderish() {
        return facets.Contains("Folderish");
    }

    public bool isPicture() {
        return type.Equals("Picture");
    }

    public bool is3d() {
        if (fileDataUrl == null) {
            return false;
        }
        return fileDataUrl.EndsWith(".obj", System.StringComparison.Ordinal);
    }

}
