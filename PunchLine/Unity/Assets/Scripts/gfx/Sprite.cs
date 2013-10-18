using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Sprite : MonoBehaviour {
	
	public Color color;
	
	public Vector2 size;
	public Vector2 Size {
		get { 
			return size;
		} 
		set {
			size = value;
			vertices = new Vector3[4];
			vertices[0] = new Vector3(0, 0f, 0f);
			vertices[1] = new Vector3(value.x, 0f, 0f);
			vertices[2] = new Vector3(0f, value.y, 0f);
			vertices[3] = new Vector3(value.x, value.y, 0f);
			mesh.vertices = vertices;
		}
	}
	
	private MeshFilter filter;
//	private MeshRenderer renderer;
	private Mesh mesh;
	
	private Vector3[] vertices;
	private Vector2[] uvs = {new Vector2(0f,0f), new Vector2(1f,0f), new Vector2(0f,1f), new Vector2(1f,1f)};
	private int[] triangles = {2,1,0,2,3,1};
	
	void Awake()
	{
		mesh = new Mesh();
		filter = this.GetComponent<MeshFilter>();
		filter.mesh = mesh;
//		renderer = this.GetComponent<MeshRenderer>();
		
		Size = size;
		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.triangles = triangles;
	}
	
	void Start()
	{
		
	}
	
	void Update()
	{
		
	}
}
