using System.Collections.Generic;
using BeerTapExercise2.Model;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Hypermedia;

namespace BeerTapExercise2.WebApi.Hypermedia
{
    public class BeerTapStateProvider : ResourceStateProviderBase<BeerTap, BeerTapState>
    {
        public override BeerTapState GetFor(BeerTap resource)
        {
            return resource.BeerTapState;
        }

        protected override IDictionary<BeerTapState, IEnumerable<BeerTapState>> GetTransitions()
        {
            return new Dictionary<BeerTapState, IEnumerable<BeerTapState>>
            {
                {
                    BeerTapState.New, new[]
                    {
                        BeerTapState.NotSoNew,
                    }
                },
                {
                    BeerTapState.NotSoNew, new[]
                    {
                        BeerTapState.AlmostGone,
                    }
                },
                {
                    BeerTapState.AlmostGone, new[]
                    {
                        BeerTapState.Dry,
                        BeerTapState.New,
                    }
                },
                {
                    BeerTapState.Dry, new[]
                    {
                        BeerTapState.New,
                    }
                },
            };
        }

        public override IEnumerable<BeerTapState> All
        {
            get { return EnumEx.GetValuesFor<BeerTapState>(); }
        }
    }
}