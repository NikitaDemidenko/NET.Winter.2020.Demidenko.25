using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTask.Interfaces
{
    public interface IParser<in TSource, out TResult>
    {
        public TResult Parse(TSource source);
    }
}
