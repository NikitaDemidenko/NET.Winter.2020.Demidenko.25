using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTask.Interfaces
{
    public interface ILoader<out TResult>
    {
        public TResult Load();
    }
}
