using System;
using System.Collections.Generic;
using System.Text;

namespace Fixer.Types
{
    public interface IStartupInitializer : IInitializer
    {
        void AddInitializer(IInitializer initializer);
    }
}
