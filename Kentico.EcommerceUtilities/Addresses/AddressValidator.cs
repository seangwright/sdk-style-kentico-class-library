using System;
using System.Collections.Generic;
using System.Linq;
using CMS.Ecommerce;
using CMS.Globalization;

namespace Kentico.EcommerceUtilities.Addresses
{
    public class AddressValidator
    {
        private readonly IEnumerable<string> forbiddenPostalCodes;

        public AddressValidator(IEnumerable<string> forbiddenPostalCodes) =>
            this.forbiddenPostalCodes = forbiddenPostalCodes;

        public bool IsAddressValid(AddressInfo address)
        {
            if (string.IsNullOrWhiteSpace(address.AddressLine1))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(address.AddressZip))
            {
                return false;
            }

            bool isPostalCodeInvalid = forbiddenPostalCodes
                .Any(p => string.Equals(address.AddressZip, p, StringComparison.OrdinalIgnoreCase));

            if (isPostalCodeInvalid)
            {
                return false;
            }

            var state = StateInfoProvider.GetStateInfo(address.AddressStateID);

            if (state is null)
            {
                return false;
            }

            var country = CountryInfoProvider.GetCountryInfo(address.AddressCountryID);

            if (country is null)
            {
                return false;
            }

            return true;
        }
    }
}
