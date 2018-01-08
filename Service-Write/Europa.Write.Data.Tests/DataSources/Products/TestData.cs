using System;

namespace Europa.Write.Data.Tests.DataSources.Podcasts
{
    public class TestData
    {
        public static string FileName = "DataSources/Podcasts/TestData.xml";

        public static Guid Category1 = Guid.Parse("00000000-0000-0000-0000-000000000001");
        public static Guid Podcast1Category1 = Guid.Parse("00000000-0000-0000-0000-000000000011");
        public static Guid Podcast2Category1 = Guid.Parse("00000000-0000-0000-0000-000000000012");
        public static Guid Category2 = Guid.Parse("00000000-0000-0000-0000-000000000002");
        public static Guid Podcast1Category2 = Guid.Parse("00000000-0000-0000-0000-000000000021");
        public static Guid Podcast2Category2 = Guid.Parse("00000000-0000-0000-0000-000000000022");
        public static Guid Category3 = Guid.Parse("00000000-0000-0000-0000-000000000003");

        public static Guid CategoryDoesNotExist = Guid.Parse("00000000-0000-0000-0000-000000000099");
    }
}
