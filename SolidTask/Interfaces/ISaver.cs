using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTask.Interfaces
{
    public interface ISaver<in TSource>
    {
        public void Save(TSource source);
    }
}
