using Enrichments.Contracts;
using Orleans;
using System;
using System.Threading.Tasks;

namespace Enrichments.Grains
{
    public class VisitorGrain : Grain, IVisitor
    {
        private readonly IStreamService _streamer;

        public VisitorGrain(IStreamService streamer)
        {
            _streamer = streamer ?? throw new ArgumentNullException(nameof(streamer));
        }
        public async Task Echo()
        {
            await _streamer.Stream("test");
        }
    }
}
