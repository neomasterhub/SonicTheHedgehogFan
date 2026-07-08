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
    _mesh.SetTriangles(_triangles, 0, false);

    _meshFilter.sharedMesh = _mesh;

    ClearMeshData();
  }

  private void ClearMeshData()
  {
    _vertices.Clear();
    _colors.Clear();
    _triangles.Clear();
  }
}
