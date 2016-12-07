namespace BeerTapExercise2.Model
{
    /// <summary>
    /// iQmetrix link relation names
    /// </summary>
    public static class LinkRelations
    {
        /// <summary>
        /// link relation to describe the Identity resource.
        /// </summary>
        public const string SampleResource = "iq:SampleResource";

        public const string Offices = "iq:Offices";

        public static class BeerTaps
        {
            public const string BeerTapsUrl = "iq:BeerTaps";
            public const string PourGlass = "iq:PourGlass";
            public const string ReplaceKeg = "iq:ReplaceKeg";
            public const string AddTap = "iq:AddTap";
            public const string RemoveTap = "iq:RemoveTap";
        }

    }
}
