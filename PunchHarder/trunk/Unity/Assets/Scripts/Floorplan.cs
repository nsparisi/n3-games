using UnityEngine;
using System.Collections;

public class Floorplan : MonoBehaviour {

    private Mesh mesh;
    private int gridWidth = 10;
    private int gridHeight = 10;


	// Use this for initialization
	void Start () 
    {
        GetComponent<MeshFilter>().mesh = CreateMesh(10, 10);
	}
	

    Mesh CreateMesh(int width, int height)
    {
        // parameters
        gridWidth = width;
        gridHeight = height;

        // globals
        Vector3 origin = new Vector3(0, 0, 0);
        Vector3 normal = new Vector3(0, 1, 0);
        float tileWidth = 1;
        float tileHeight = 1;

        // local vars
        int vetexCountX = gridWidth * 2;
        int vetexCountZ = gridHeight * 2;

        // assign verticies to mesh
        Vector3[] vertices = new Vector3[vetexCountX * vetexCountZ];
        Vector3[] normals = new Vector3[vetexCountX * vetexCountZ];
        Vector2[] uvs = new Vector2[vetexCountX * vetexCountZ];

        for (int i = 0; i < vetexCountZ; i+=2)
        {
            for (int j = 0; j < vetexCountX; j+=2)
            {
                float x = j * tileWidth / 2 + origin.x;
                float z = i * tileHeight / 2 + origin.z;
                float y = origin.y;
                int index = i * vetexCountX + j * 2;

                vertices[index] =      new Vector3(x,              y, z);
                vertices[index + 1] =  new Vector3(x + tileWidth,  y, z);
                vertices[index + 2] =  new Vector3(x,              y, z + tileHeight);
                vertices[index + 3] =  new Vector3(x + tileWidth,  y, z + tileHeight);

                normals[index] = normal;
                normals[index + 1] = normal;
                normals[index + 2] = normal;
                normals[index + 3] = normal;

                uvs[index] = new Vector2(0, 0);
                uvs[index + 1] = new Vector2(1, 0);
                uvs[index + 2] = new Vector2(0, 1);
                uvs[index + 3] = new Vector2(1, 1);
            }
        }

        // assign triangles - 2 per square, 3 verts per triangle
        int[] triangles = new int[gridHeight * gridWidth * 2 * 3];

        // go through every square on the grid
        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                // corners of square
                int bl = j * 4 + i * 4 * gridWidth;
                int br = bl + 1;
                int ul = bl + 2;
                int ur = bl + 3;

                // current triangle
                int index = (i * gridWidth + j) * 2 * 3;

                // triangle 1 clock-wise
                triangles[index] = bl;
                triangles[index + 1] = ur;
                triangles[index + 2] = br;

                // triangle 2 clock-wise
                triangles[index + 3] = bl;
                triangles[index + 4] = ul;
                triangles[index + 5] = ur;

            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.normals = normals;
        mesh.triangles = triangles;
        return mesh;
    }
}
