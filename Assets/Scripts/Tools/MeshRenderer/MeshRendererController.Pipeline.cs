/// <summary>
/// Pipeline.
/// </summary>
public partial class MeshRendererController
{
  private void LateUpdate()
  {
    if (_vertices.Count == 0)
    {
      return;
    }

    _mesh.Clear();
    _mesh.SetVertices(_vertices);
    _mesh.SetColors(_colors);
    _mesh.SetTriangles(_triangles, 0);

    _meshFilter.sharedMesh = _mesh;

    ClearMeshData();
  }

  private void ClearMeshData()
  {
    _colors.Clear();
    _triangles.Clear();
    _vertices.Clear();
  }
}
