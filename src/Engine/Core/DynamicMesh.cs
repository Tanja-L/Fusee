using Fusee.Engine.Common;
using Fusee.Math.Core;
using System.Collections.Generic;

namespace Fusee.Engine.Core
{
    /// <summary>
    /// Almost the same as <see cref="Mesh"/>, but with the ability to allocate dynamically memory on the GPU.
    /// This way, no computation is needed, e.g. by removing memory manually via <see cref="RenderContext.Remove(Mesh)"/>.
    /// Still, only up to 65.535 vertices can be stored due to triangles type (ushort).
    /// </summary>
    public class DynamicMesh
    {
        internal IMeshImp _meshImp;

        // those lists refer to the values that are given in
        private List<float3> _vertices = new List<float3>();
        private List<float3> _normals = new List<float3>();
        private List<ushort> _triangles = new List<ushort>();
        private List<float2> _uvs = new List<float2>();
        private List<uint> _colors = new List<uint>();

        // hold pointers, from which positions on the newly added informations are stored
        private int _addedVerticesIndex = 0;
        private int _addedNormalsIndex = 0;
        private int _addedTrianglesIndex = 0;
        private int _addedUVsIndex = 0;
        private int _addedColorsIndex = 0;

        // Indicates whether meshes have been added since the last time Update() was called.
        private bool _hasChanged;

        /// <summary>
        /// Adds another mesh to this mesh.
        /// </summary>
        /// <param name="mesh">The mesh to add.</param>
        /// <returns>True, when adding was successfull, or false, when this mesh is full.</returns>
        public bool AddMesh(Mesh mesh)
        {
            // triangles ushort list only stores indices of vertices, so we only need to consider the vertices list for not being larger than 65000 items,
            // since ushort can only take values up to 65000.
            if (_vertices.Count + mesh.Vertices.Length > 65000)
            {
                return false;
            }

            ushort currentIndex = (ushort) _vertices.Count; // index of next value, not the last one (that would be -1)

            _vertices.AddRange(mesh.Vertices);

            if (mesh.Normals != null)
                _normals.AddRange(mesh.Normals);

            if (mesh.UVs != null)
                _uvs.AddRange(mesh.UVs);

            if (mesh.Colors != null)
                _colors.AddRange(mesh.Colors);

            // now comes the tricky part: the triangles

            if (mesh.Triangles != null)
            {
                foreach (ushort vertexIndex in mesh.Triangles)
                {
                    _triangles.Add((ushort)(vertexIndex + currentIndex));
                }
            }

            _hasChanged = true;

            return true;
        }

        /// <summary>
        /// Indicates whether meshes have been added since the last time Update() was called.
        /// </summary>
        public bool HasChanged()
        {
            bool hasChanged = _hasChanged;
            _hasChanged = false;

            return hasChanged;
        }

        /// <summary>
        /// Updates the pointers.
        /// </summary>
        public void Update()
        {
            _addedVerticesIndex = _vertices.Count;
            _addedNormalsIndex = _normals.Count;
            _addedTrianglesIndex = _triangles.Count;
            _addedUVsIndex = _uvs.Count;
            _addedColorsIndex = _colors.Count;

            _hasChanged = false;
        }

        #region Getter Methods Lists

        public List<float3> GetVertices()
        {
            return _vertices;
        }

        public List<uint> GetColors()
        {
            return _colors;
        }

        public List<float3> GetNormals()
        {
            return _normals;
        }

        public List<float2> GetUVs()
        {
            return _uvs;
        }

        public List<ushort> GetTriangles()
        {
            return _triangles;
        }

        #endregion

        #region Getter Methods Offsets

        public int GetVerticesOffset()
        {
            return _addedVerticesIndex;
        }

        public int GetNormalsOffset()
        {
            return _addedNormalsIndex;
        }

        public int GetUVsOffset()
        {
            return _addedUVsIndex;
        }

        public int GetColorsOffset()
        {
            return _addedColorsIndex;
        }

        public int GetTrianglesOffset()
        {
            return _addedTrianglesIndex;
        }

        #endregion
    }
}



