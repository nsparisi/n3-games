using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Sprite : MonoBehaviour
{
	public Color color = new Color (1f, 1f, 1f, 1f);
	public Color Color {
		get { 
			return color; 
		}
		set {
			color = value;
			colors[0] = color;
			colors[1] = color;
			colors[2] = color;
			colors[3] = color;
			mesh.colors = colors;
		}
	}
	
	public Vector2 size = new Vector2(1f, 1f);
	public Vector2 Size {
		get { 
			return size;
		} 
		set {
			size = value;
			vertices [0] = new Vector3 (0, 0f, 0f);
			vertices [1] = new Vector3 (value.x, 0f, 0f);
			vertices [2] = new Vector3 (0f, value.y, 0f);
			vertices [3] = new Vector3 (value.x, value.y, 0f);
			mesh.vertices = vertices;
		}
	}
	
	/// <summary>
	/// Use this. Order is left, down, right, up (like two sets of vector 2s).
	/// </summary>
	public Vector4 uv = new Vector4(0f,0f,1f,1f);
	public Vector4 UV {
		get {
			return uv;
		}
		set {
			uv = value;
			uvs[0] = new Vector2(uv.x, uv.y);
			uvs[1] = new Vector2(uv.z, uv.y);
			uvs[2] = new Vector2(uv.x, uv.w);
			uvs[3] = new Vector2(uv.z, uv.w);
			mesh.uv = uvs;
		}
	}
	
	private MeshFilter filter;
	private Mesh mesh;
	private Vector3[] vertices = new Vector3[4];
	/// <summary>
	/// Don't ever use this. Lower left, lower right, upper left, upper right
	/// </summary>
	private Vector2[] uvs = {new Vector2 (0f, 0f), new Vector2 (1f, 0f), new Vector2 (0f, 1f), new Vector2 (1f, 1f)};
	private Color[] colors = { Color.white, Color.white, Color.white, Color.white };
	private int[] triangles = {2,1,0,2,3,1};
	
	public void Awake()
	{
		mesh = new Mesh ();
		filter = this.GetComponent<MeshFilter> ();
		filter.mesh = mesh;
		
		Size = size;
		UV = uv;
		Color = color;
		mesh.triangles = triangles;
	}
}
