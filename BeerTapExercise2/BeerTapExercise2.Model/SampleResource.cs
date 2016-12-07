using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapExercise2.Model
{
    /// <summary>
    /// A Sample Resource, used as a placeholder. To be removed after real-world API resources have been added.
    /// </summary>
    public class SampleResource : IStatelessResource, IIdentifiable<string>
    {
        /// <summary>
        /// Unique Identifier for the SampleResource
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name for the SampleResource
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description for the SampleResource
        /// </summary>
        public string Description { get; set; }
    }
}
