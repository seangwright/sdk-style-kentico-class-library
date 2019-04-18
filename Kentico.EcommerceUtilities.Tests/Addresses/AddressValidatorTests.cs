using CMS.Ecommerce;
using CMS.Globalization;
using CMS.Tests;
using FluentAssertions;
using Kentico.EcommerceUtilities.Addresses;
using NUnit.Framework;

namespace Kentico.EcommerceUtilities.Tests.Addresses
{
    [TestFixture]
    public class AddressValidatorTests : UnitTests
    {
        [Test]
        public void IsAddressValid_Will_Return_True_For_An_Address_With_All_Required_And_Valid_Values()
        {
            Fake<AddressInfo>();
            Fake<StateInfo, StateInfoProvider>().WithData(
                new StateInfo
                {
                    StateID = 10,
                    StateName = "TestState"
                });
            Fake<CountryInfo, CountryInfoProvider>().WithData(
                new CountryInfo
                {
                    CountryID = 2,
                    CountryName = "TestCountry"
                });

            var address = new AddressInfo
            {
                AddressID = 1,
                AddressLine1 = "1234 Test",
                AddressStateID = 10,
                AddressCountryID = 2,
                AddressZip = "11111"
            };

            var sut = new AddressValidator(new string[] { "12345" });

            sut.IsAddressValid(address).Should().BeTrue();
        }

        [Test]
        public void IsAddressValid_Will_Return_False_For_An_Address_Without_Line1()
        {
            Fake<AddressInfo>();

            var address = new AddressInfo
            {
                AddressID = 1,
                AddressLine1 = "",
                AddressStateID = 10,
                AddressCountryID = 2,
                AddressZip = "11111"
            };

            var sut = new AddressValidator(new string[] { "12345" });

            sut.IsAddressValid(address).Should().BeFalse();
        }
    }
}