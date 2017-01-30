/* ******************************

obj runtime importer by SavaB
----------------------------------
This script is free to use, as long as credits for the script are reserved to Sam Van Battel (SavaB).

How To Use?
--------------
 - Add the script to the main camera. Adjust _textFieldString and _textureLink to direct to the
   right model and texture.
   
Notes
-------
 - This script is still under development. Check the website or the communityforum topic regularly topic
   get the latest version

Contact:
----------
SavaB
programmer - technical artist
www.savab-multimedia.com

****************************** */

using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

public class objReaderCSharpV4 : MonoBehaviour {

	public string _textFieldString = "http://people.sc.fsu.edu/~burkardt/data/obj/cessna.obj";
	public string _textureLink = "";
	public string _meshName = "unknown";
	
	Mesh _myMesh;
	Material _myMaterial = new Material(Shader.Find("Diffuse"));
	
	Vector3[] _vertexArray;
	ArrayList _vertexArrayList = new ArrayList();
	Vector3[] _normalArray;
	ArrayList _normalArrayList = new ArrayList();
	Vector2[] _uvArray;
	ArrayList _uvArrayList = new ArrayList();
	
	int[] _triangleArray;
	
	ArrayList _facesVertNormUV = new ArrayList();









	
    internal class PlacesByIndex {
		public PlacesByIndex(int index) {
			_index = index;
		}
		public int _index;
		public ArrayList _places = new ArrayList();
    }
	
	// Use this for initialization
	public IEnumerator Init (string gameObjectName) {
		yield return StartCoroutine(SomeFunction(gameObjectName));
	}
	
	public void OnGUI() {
		_textFieldString = GUI.TextField(new Rect(10,10,300,20), _textFieldString);
		_textureLink = GUI.TextField(new Rect(10,40,300,20), _textureLink);
		
		if (GUI.Button(new Rect(10,70,100,20), "click")) {
			StartCoroutine(Init("GameObject"));
		}
	}
	
	void initArrayLists() {
		_uvArrayList = new ArrayList();
		_normalArrayList = new ArrayList();
		_vertexArrayList = new ArrayList();
		_facesVertNormUV = new ArrayList();
	}

    public Mesh getMesh() {
        if (_myMesh != null)
            _myMesh.Clear();
        _myMesh = new Mesh();
        _myMesh.vertices = _vertexArray;
        _myMesh.triangles = _triangleArray;
        return _myMesh;
    }
	
	public IEnumerator SomeFunction(string gameObjectName) {
		GameObject obj_gameobject = GameObject.Find(gameObjectName);
		//Debug.Log("started parsing the obj...");
		initArrayLists();
		// re-initialize the mesh and name it
		if (_myMesh != null)
			_myMesh.Clear();
		_myMesh = new Mesh();
		_myMesh.name = _meshName;
		// retrieve data from OBJ file
		WWW www3d = new WWW(_textFieldString);
		yield return www3d;
		string s = www3d.data;
		//replace double spaces and dot-notations
		s = s.Replace("  ", " ");
		s = s.Replace("  ", " ");
		//~ s = s.Replace(".", ",");
		// call loadFile() and pass through the data from the OBJ file --> here we load the OBJ
		LoadFile(s);
		//set the vertices and triangles to the unity mesh
		_myMesh.vertices = _vertexArray;
		_myMesh.triangles = _triangleArray;
		if (_uvArrayList.Count > 0)
			_myMesh.uv = _uvArray;
		if (_normalArrayList.Count > 0)
			_myMesh.normals = _normalArray;
		else
			_myMesh.RecalculateNormals();
		//calculate the bounds
		_myMesh.RecalculateBounds();
		// check if there is allready a MeshFilter present, if not add one
		if ((MeshFilter)obj_gameobject.GetComponent("MeshFilter") == null)
			obj_gameobject.AddComponent<MeshFilter>();
		//assign the mesh to the meshfilter
		MeshFilter temp;
		temp = (MeshFilter)obj_gameobject.GetComponent("MeshFilter");
		temp.mesh = _myMesh;
		// check if there is allready a MeshRenderer present, if not add one
		if ((MeshRenderer)obj_gameobject.GetComponent("MeshRenderer") == null)
			obj_gameobject.AddComponent<MeshRenderer>();
		// retrieve the texture
		if (_uvArrayList.Count > 0 && _textureLink != "") {
			WWW wwwtx = new WWW(_textureLink);
			yield return wwwtx;
			_myMaterial.mainTexture = wwwtx.texture;
		}
		// assign the texture to the meshrenderer
		MeshRenderer temp2;
		temp2 = (MeshRenderer)obj_gameobject.GetComponent("MeshRenderer");
		if (_uvArrayList.Count > 0 && _textureLink != "") {
			temp2.material = _myMaterial;
			_myMaterial.shader = Shader.Find("Diffuse");
		}
		
		yield return new WaitForFixedUpdate();
	}
	
	public void LoadFile(string s) {
		// split the file into lines by detecting the breaklines
		string[] lines = s.Split("\n"[0]);
		
		foreach (string item in lines) {
			ReadLine(item);
		}



		//disassemble the obj to a non-indexed format and assemble them back to an indexed format, but only by vertex
		ArrayList tempArrayList = new ArrayList();
		for (int i = 0; i < _facesVertNormUV.Count; ++i) {
			if (_facesVertNormUV[i] != null) {
				PlacesByIndex indextemp = new PlacesByIndex(i);
				indextemp._places.Add(i);
				for (int j = 0; j < _facesVertNormUV.Count; ++j) {
					if (_facesVertNormUV[j] != null) {
						if (i != j) {
							Vector3 iTemp = (Vector3)_facesVertNormUV[i];
							Vector3 jTemp = (Vector3)_facesVertNormUV[j];
							if (iTemp.x == jTemp.x && iTemp.y == jTemp.y) {
								indextemp._places.Add(j);
								_facesVertNormUV[j] = null;
							}
						}
					}
				}
				tempArrayList.Add(indextemp);
			}
		}
		//init the arrays
		_vertexArray = new Vector3[tempArrayList.Count];
		_uvArray = new Vector2[tempArrayList.Count];
		_normalArray = new Vector3[tempArrayList.Count];
		_triangleArray = new int[_facesVertNormUV.Count];

		//fill arrays
		int teller = 0;
		foreach (PlacesByIndex item in tempArrayList) {
			foreach (int item2 in item._places) {
				_triangleArray[item2] = teller;
			}
			Vector3 vTemp = (Vector3)_facesVertNormUV[item._index];
			_vertexArray[teller] = (Vector3)_vertexArrayList[(int)vTemp.x - 1];
			if (_uvArrayList.Count > 0) {
				Vector3 tVec = (Vector3)_uvArrayList[(int)vTemp.y - 1];
				_uvArray[teller] = new Vector2(tVec.x, tVec.y);
			}
			if (_normalArrayList.Count > 0) {
				_normalArray[teller] = (Vector3)_normalArrayList[(int)vTemp.z - 1];
			}
			teller++;
		}
	}
	
	public void ReadLine(string s) {
		//remove any trailing white-space chararcters to ensure that there will be no empty splits
		char[] charsToTrim = {' ', '\n', '\t', '\r'};
		s= s.TrimEnd(charsToTrim);
		//split the incoming string in words
		string[] words = s.Split(" "[0]);
		//trim each word to avoid white-space chararcters
		foreach (string item in words)
			item.Trim();
		//assemble all vertices, normals and uv-coordinates
		if (words[0] == "v")
			_vertexArrayList.Add(new Vector3(System.Convert.ToSingle(words[1], CultureInfo.InvariantCulture), System.Convert.ToSingle(words[2], CultureInfo.InvariantCulture), System.Convert.ToSingle(words[3], CultureInfo.InvariantCulture)));

		if (words[0] == "vn")
			_normalArrayList.Add(new Vector3(System.Convert.ToSingle(words[1], CultureInfo.InvariantCulture), System.Convert.ToSingle(words[2], CultureInfo.InvariantCulture), System.Convert.ToSingle(words[3], CultureInfo.InvariantCulture)));
		if (words[0] == "vt") 
			_uvArrayList.Add(new Vector3(System.Convert.ToSingle(words[1], CultureInfo.InvariantCulture), System.Convert.ToSingle(words[2], CultureInfo.InvariantCulture)));
		//assemble the faces by index, and disassemble them back to each point
		if (words[0] == "f") {
			ArrayList temp = new ArrayList();
			ArrayList triangleList = new ArrayList();
			for (int j = 1; j < words.Length; ++j)
			{
				Vector3 indexVector = new Vector3(0,0);
				string[] indices = words[j].Split("/"[0]);
				indexVector.x = System.Convert.ToInt32(indices[0], CultureInfo.InvariantCulture);
				if (indices.Length > 1) {
					if (indices[1] != "")
						indexVector.y = System.Convert.ToInt32(indices[1], CultureInfo.InvariantCulture);
				}
				if (indices.Length > 2) {
					if (indices[2] != "")
						indexVector.z = System.Convert.ToInt32(indices[2], CultureInfo.InvariantCulture);
				}
				temp.Add(indexVector);
			}
			for (int i = 1; i < temp.Count - 1; ++i) {
				triangleList.Add(temp[0]);
				triangleList.Add(temp[i]);
				triangleList.Add(temp[i+1]);
			}

			foreach (Vector3 item in triangleList) {
				_facesVertNormUV.Add(item);
			}
		}
	}
}
