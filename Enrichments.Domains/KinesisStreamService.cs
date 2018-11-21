using Enrichments.Contracts;
using System;
using System.Threading.Tasks;

namespace Enrichments.Domains
{
    public class KinesisStreamService : IStreamService
    {
        public Task Stream(object a)
        {
            return Task.CompletedTask;
        }
    }
}
