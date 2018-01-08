using System;

namespace Europa.Write.Data.Tests.DataSources.Tags
{
    public class TestData
    {
        /*
         * tag: fast
         * tag: cheap
         * tag: good
         */

        public static string FileName = "DataSources/Tags/TestData.xml";
        public static Guid TagId1 = Guid.Parse("00000000-0000-0000-0000-000000000001");
        public static Guid TagId2 = Guid.Parse("00000000-0000-0000-0000-000000000002");
        public static Guid TagId3 = Guid.Parse("00000000-0000-0000-0000-000000000003");
        public static string TagName1 = "fast";
        public static string TagName2 = "cheap";
        public static string TagName3 = "good";
    }
}
