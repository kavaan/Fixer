using System;
using System.Collections.Generic;
using System.Text;

namespace Fixer.Types
{
    public interface IIdentifiable<out T>
    {
        T Id { get; }
    }
}
