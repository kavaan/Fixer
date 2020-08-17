using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fixer.Types
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}
