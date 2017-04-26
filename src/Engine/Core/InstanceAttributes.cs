using Fusee.Engine.Common;
using System.Collections.Generic;
using Fusee.Math.Core;
using System.Threading;

namespace Fusee.Engine.Core
{
    /// <summary>
    /// In combination with <see cref="RenderContext.RenderAsInstance"/>, one can use this class to
    /// additionally define attributes which are available per mesh instance in the vertex shader.
    /// Is not thread-safe, AddOffset(s) must be called before RenderAsInstance().
    /// </summary>

    public class InstanceAttributes
    {
        internal IInstanceAttributesImp _iaImp;

        private List<float3> _offsets = new List<float3>();
        private int _ptrToLastChanges = 0; // where the new offset gonna be stored
        
        public void AddOffset(float3 offset)
        {
            _offsets.Add(offset);
        }

        public void AddOffsets(List<float3> offsets)
        {
            _offsets.AddRange(offsets);
        }

        public List<float3> GetOffsets()
        {
            return _offsets;
        }

        public int GetCount()
        {
            return _offsets.Count;
        }

        public bool HasChanged()
        {
            return _ptrToLastChanges != _offsets.Count;
        }

        public int GetIndexOfLastChanges()
        {
            return _ptrToLastChanges;
        }

        public void Update()
        {
            _ptrToLastChanges = _offsets.Count;
        }
    }
}



