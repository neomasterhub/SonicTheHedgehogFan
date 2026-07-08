/// <summary>
/// Pipeline.
/// </summary>
public partial class MeshRendererController
{
  private void LateUpdate()
  {
    if (_vertices.Count == 0)
    {
      if (_mesh.vertexCount > 0)
      {
        _mesh.Clear();
        ClearMeshData();
      }

      return;
    }

    _mesh.Clear();
    _mesh.SetVertices(_vertices);
    _mesh.SetColors(_colors);
    _mesh.SetTriangles(_triangles, 0);

    ClearMeshData();
  }

  private void ClearMeshData()
  {
    _colors.Clear();
    _triangles.Clear();
    _vertices.Clear();
  }
}
