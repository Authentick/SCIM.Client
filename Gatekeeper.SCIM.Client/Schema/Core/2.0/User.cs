using System.Collections.Generic;

namespace Gatekeeper.SCIM.Client.Schema.Core20
{
    class User : ISchema
    {
        public string SchemaIdentifier => "urn:ietf:params:scim:schemas:core:2.0:User";

        string UserName { get; set; }
        INameComponent Name { get; set; }
        string DisplayName { get; set; }
        string NickName { get; set; }
        string ProfileUrl { get; set; }
        string Title { get; set; }
        string UserType { get; set; }
        // TODO: Use enum
        string PreferredLanguage { get; set; }
        // TODO: Use enum
        string Locale { get; set; }
        // TODO: USe enum
        string Timezone { get; set; }
        bool Active { get; set; }
        string Password { get; set; }
        IEnumerable<string> Groups { get; set; }

        interface INameComponent
        {
            string value { get; set; }
        }

        class FormattedName : INameComponent
        {
            public string value { get; set; }
        }

        class FamilyName : INameComponent
        {
            public string value { get; set; }
        }

        class GivenName : INameComponent
        {
            public string value { get; set; }
        }

        class HonoricPrefix : INameComponent
        {
            public string value { get; set; }
        }

        class HonoricSuffix : INameComponent
        {
            public string value { get; set; }
        }
    }
}
