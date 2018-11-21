using System;
using System.Threading.Tasks;

namespace Enrichments.Contracts
{
    public interface IStreamService
    {
        Task Stream(object a);
    }
}
