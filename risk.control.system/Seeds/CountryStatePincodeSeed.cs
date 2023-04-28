using risk.control.system.Data;
using risk.control.system.Models;

namespace risk.control.system.Seeds
{
    public static class CountryStatePincodeSeed
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            var india = new Country
            {
                Name = "INDIA",
                Code = "IND",
            };
            var indiaCountry = await context.Country.AddAsync(india);
            var australia = new Country
            {
                Name = "AUSTRALIA",
                Code = "AUS",
            };

            var australiaCountry = await context.Country.AddAsync(australia);
            var canada = new Country
            {
                Name = "CANADA",
                Code = "CAN",
            };

            var canadaCountry = await context.Country.AddAsync(canada);
            var up = new State
            {
                CountryId = indiaCountry.Entity.CountryId,
                Name = "UTTAR PRADESH",
                Code = "UP"
            };

            var upState = await context.State.AddAsync(up);
            var ontario = new State
            {
                CountryId = canadaCountry.Entity.CountryId,
                Name = "ONTARIO",
                Code = "ON"
            };

            var ontarioState = await context.State.AddAsync(ontario);

            var delhi = new State
            {
                CountryId = indiaCountry.Entity.CountryId,
                Name = "NEW DELHI",
                Code = "NDL"
            };

            var delhiState = await context.State.AddAsync(delhi);

            var victoria = new State
            {
                CountryId = australiaCountry.Entity.CountryId,
                Name = "VICTORIA",
                Code = "VIC"
            };

            var victoriaState = await context.State.AddAsync(victoria);

            var tasmania = new State
            {
                CountryId = australiaCountry.Entity.CountryId,
                Name = "TASMANIA",
                Code = "TAS"
            };

            var tasmaniaState = await context.State.AddAsync(tasmania);

            var newDelhi = new PinCode
            {
                Name = "NEW DELHI",
                Code = "110001",
                State = delhiState.Entity,
                Country = indiaCountry.Entity
            };

            var newDelhiPinCode = await context.PinCode.AddAsync(newDelhi);

            var northDelhi = new PinCode
            {
                Name = "NORTH DELHI",
                Code = "110002",
                State = delhiState.Entity,
                Country = indiaCountry.Entity
            };

            var northDelhiPinCode = await context.PinCode.AddAsync(northDelhi);

            var indirapuram = new PinCode
            {
                Name = "INDIRAPURAM",
                Code = "201014",
                State = upState.Entity,
                Country = indiaCountry.Entity
            };

            var indiraPuramPinCode = await context.PinCode.AddAsync(indirapuram);

            var bhelupur = new PinCode
            {
                Name = "BHELUPUR",
                Code = "221001",
                State = upState.Entity,
                Country = indiaCountry.Entity
            };

            var bhelupurPinCode = await context.PinCode.AddAsync(bhelupur);

            var forestHill = new PinCode
            {
                Name = "FOREST HILL",
                Code = "3131",
                State = victoriaState.Entity,
                Country = australiaCountry.Entity
            };

            var forestHillPinCode = await context.PinCode.AddAsync(forestHill);

            var vermont = new PinCode
            {
                Name = "VERMONT",
                Code = "3133",
                State = victoriaState.Entity,
                Country = australiaCountry.Entity
            };

            var vermontPinCode = await context.PinCode.AddAsync(vermont);

            var tasmaniaCity = new PinCode
            {
                Name = "TASMANIA CITY",
                Code = "7000",
                State = tasmaniaState.Entity,
                Country = australiaCountry.Entity
            };

            var tasmaniaCityCode = await context.PinCode.AddAsync(tasmaniaCity);

            var torontoCity = new PinCode
            {
                Name = "TORONTO",
                Code = "9101",
                State = ontarioState.Entity,
                Country = canadaCountry.Entity
            };

            var torontoCityCode = await context.PinCode.AddAsync(tasmaniaCity);

        }
    }
}
