using UnityEngine;
using System.Collections;

// Copy meshes from children into the parent's Mesh.
// CombineInstance stores the list of meshes.  These are combined
// and assigned to the attached Mesh.

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class ChildrenMeshesCombinator : MonoBehaviour
{
    private MeshFilter _meshFilter => GetComponent<MeshFilter>();
    private MeshCollider _meshCollider => GetComponent<MeshCollider>();

    public void Combine()
    {
        MeshFilter[] _childMeshFilters = GetComponentsInChildren<MeshFilter>();

        CombineInstance[] combination = new CombineInstance[_childMeshFilters.Length];

        int i = 0;
        while (i < _childMeshFilters.Length)
        {
            combination[i].mesh = _childMeshFilters[i].mesh;
            combination[i].transform = _childMeshFilters[i].transform.localToWorldMatrix;
            _childMeshFilters[i].gameObject.SetActive(false);

            i++;
        }

        _meshFilter.mesh = new Mesh();
        _meshFilter.mesh.CombineMeshes(combination);

        _meshCollider.sharedMesh = _meshFilter.mesh;

        transform.gameObject.SetActive(true);
    }
}