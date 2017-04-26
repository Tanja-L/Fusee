using Fusee.Engine.Common;
using System.Collections.Generic;
using Fusee.Math.Core;
using System.Threading;

#if PLATFORM_DESKTOP
namespace Fusee.Engine.Imp.Graphics.Desktop
#elif PLATFORM_ANDROID
namespace Fusee.Engine.Imp.Graphics.Android
#endif
{
    /// <summary>
    /// In combination with <see cref="RenderContext.RenderAsInstance"/>, one can use this class to
    /// additionally define attributes which are available per mesh instance in the vertex shader.
    /// </summary>

    public class InstanceAttributesImp : IInstanceAttributesImp
    {
        internal int OffsetBufferObject;
    }
}



