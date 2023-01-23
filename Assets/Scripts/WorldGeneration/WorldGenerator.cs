using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class WorldGenerator : MonoBehaviour
{
  [Header("Size")]
	public int Width, Height;

  private Vector3[] _vertices;

  private Mesh _mesh;

  private void Awake () {
		Generate();
	}

  private Mesh Square ((int, int, int) position, (int, int, int) rotation) {
		const int SQUARE_WIDTH = 1;
		const int SQUARE_HEIGHT = 1;
		const int SQUARE_LENGTH = 1;

		var (posX, posY, posZ) = position;
		var (rotX, rotY, rotZ) = rotation;

		Mesh _mesh = new Mesh();

		_vertices = new Vector3[(SQUARE_WIDTH * rotX + 1) * (SQUARE_HEIGHT * rotY + 1) * (SQUARE_LENGTH * rotZ + 1)];

		for (int i = 0, x = 0; x <= SQUARE_WIDTH * rotX; x++) {
			for (int y = 0; y <= SQUARE_HEIGHT * rotY; y++) {
				for (int z = 0; z <= SQUARE_LENGTH * rotZ; z++, i++) {
					_vertices[i] = new Vector3(x + posX, y + posY, z + posZ);
				}
			}
		}
		_mesh.vertices = _vertices;
    
		int[] triangles = new int[6];

		triangles[0] = (0 + Width * 0);
		triangles[1] = ((0 + 1) + Width * (0 + 1));
		triangles[2] = ((0 + 1) + Width * 0);
		triangles[3] = ((0 + 1) + Width * 0);
		triangles[4] =((0 + 1) + Width * (0 + 1));
		triangles[5] = ((0 + 2) + Width * (0 + 1));
		
		_mesh.triangles = triangles;

		return _mesh;
	}

	private void Generate () {
		GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
		_mesh.name = "Procedural Grid";

    Mesh[] meshes = new Mesh[1] {
			Square((0, 0, 0), (1, 1, 1)),
		};

		CombineInstance[] combine = new CombineInstance[meshes.Length];

		for (int i = 0; i < combine.Length; i++) {
			combine[i].mesh = meshes[i];
			combine[i].transform = transform.localToWorldMatrix;
		}

		_mesh.CombineMeshes(combine);
		gameObject.SetActive(true);
	}
}