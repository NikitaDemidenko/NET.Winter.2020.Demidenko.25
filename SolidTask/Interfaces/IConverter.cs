using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTask.Interfaces
{
    public interface IConverter<in TSource, out TResult>
    {
        public TResult Convert(TSource source);
    }
}
