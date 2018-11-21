using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Enrichments.Contracts
{
    public interface IVisitor: Orleans.IGrainWithStringKey
    {
        Task Echo();
    }
}
