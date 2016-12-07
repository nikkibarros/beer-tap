using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerTapExercise2.Model
{
    interface IStatefulBeerTap
    {
        BeerTapState BeerTapState { get; }
    }
}
