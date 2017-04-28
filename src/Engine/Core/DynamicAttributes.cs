using Fusee.Engine.Common;
using System.Collections.Generic;
using Fusee.Math.Core;

namespace Fusee.Engine.Core
{
    /// <summary>
    /// With this class you are able to add more vertices at runtime (dynamically), that is,
    /// we do not need to create new buffer objects everytime changes happen and then delete the old ones.
    /// </summary>

    public class DynamicAttributes
    {
        internal IAttribImp _attribImp;

        private List<float3> _attributes = new List<float3>();
        private int _ptrToLastChanges = 0; // where the new offset gonna be stored

        private int _maxElements;

        public DynamicAttributes()
        {
            _maxElements = -1;
        }

        public DynamicAttributes(int maxElements)
        {
            _maxElements = maxElements;
        }

        /// <summary>
        /// Adds another float3 attribute.
        /// </summary>
        /// <returns>False, when attributes overstep the limit.</returns>
        public bool AddAttribute(float3 attribute)
        {
            if (_attributes.Count + 1 > _maxElements && _maxElements != -1)
                return false;

            _attributes.Add(attribute);
            return true;
        }

        /// <summary>
        /// Adds float3 attributes.
        /// </summary>
        /// <returns>False, when attributes overstep the limit.</returns>
        public bool AddAttributes(List<float3> attributes)
        {
            if (_attributes.Count + attributes.Count > _maxElements && _maxElements != -1)
                return false;

            _attributes.AddRange(attributes);
            return true;
        }

        public List<float3> GetOffsets()
        {
            return _attributes;
        }

        public int GetCount()
        {
            return _attributes.Count;
        }

        public int GetMaximumNumberOfElements()
        {
            return _maxElements;
        }

        public bool HasChanged()
        {
            return _ptrToLastChanges != _attributes.Count;
        }

        public int GetIndexOfLastChanges()
        {
            return _ptrToLastChanges;
        }

        public void Update()
        {
            _ptrToLastChanges = _attributes.Count;
        }
    }
}



