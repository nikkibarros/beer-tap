namespace BeerTapExercise2.Model.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BeerTapExercise2.Model.BeerTapContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BeerTapExercise2.Model.BeerTapContext context)
        {
            context.Offices.AddOrUpdate(x => x.Id,
                new Office() {Id = 1, Name = "Vancouver"},
                new Office() {Id = 2, Name = "Regina"},
                new Office() {Id = 3, Name = "Winnipeg"},
                new Office() {Id = 4, Name = "Davidson (NC)"},
                new Office() {Id = 5, Name = "Manila"},
                new Office() {Id = 6, Name = "Sydney"}
            );

            context.BeerTaps.AddOrUpdate(x => x.Id,
                new BeerTap()
                {
                    Id = 1,
                    Volume = 50,
                    BeerTapState = BeerTapState.NotSoNew,
                    OfficeId = 1
                },
                new BeerTap()
                {
                    Id = 2,
                    Volume = 100,
                    BeerTapState = BeerTapState.New,
                    OfficeId = 1
                },
                new BeerTap()
                {
                    Id = 3,
                    Volume = 40,
                    BeerTapState = BeerTapState.AlmostGone,
                    OfficeId = 2
                },
                new BeerTap()
                {
                    Id = 4,
                    Volume = 75,
                    BeerTapState = BeerTapState.NotSoNew,
                    OfficeId = 3
                }
            );
        }
    }
}
