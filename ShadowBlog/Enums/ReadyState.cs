using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Enums
{
    public enum ReadyState
    {
        //This enum when in use means the post isn't ready.
        Incomplete,
        //This enum: when in use, it suggests it's ready to be viewed by the public.
        ProductionReady,
        //This enum: when in use, it suggests it's in preview mode.
        InPreview

    }
}
