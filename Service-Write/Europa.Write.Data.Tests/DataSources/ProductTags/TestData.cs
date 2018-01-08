using System;

namespace Europa.Write.Data.Tests.DataSources.PodcastTags
{
    public class TestData
    {
        public static string FileName = "DataSources/PodcastTags/TestData.xml";
        public static Guid Category = Guid.Parse("00000000-0000-0000-0000-000000000001");
        public static Guid Podcast1 = Guid.Parse("00000000-0000-0000-0000-000000000011");
        public static Guid Podcast2 = Guid.Parse("00000000-0000-0000-0000-000000000012");
        public static Guid Podcast3 = Guid.Parse("00000000-0000-0000-0000-000000000013");
        public static Guid Tag1Id = Guid.Parse("00000000-0000-0000-0000-000000000021");
        public static Guid Tag2Id = Guid.Parse("00000000-0000-0000-0000-000000000022");
        public static Guid Tag3Id = Guid.Parse("00000000-0000-0000-0000-000000000023");
        public static Guid TagDoesNotExistId = Guid.Parse("11111111-0000-0000-0000-000000000000");
        public static string TagDoesNotExist = "does-not-exit";
        public static string Tag1 = "fast";
        public static string Tag2 = "cheap";
        public static string Tag3 = "good";
    }
}
