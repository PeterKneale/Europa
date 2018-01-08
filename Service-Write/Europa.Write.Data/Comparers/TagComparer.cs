using System;
using System.Collections.Generic;

namespace Europa.Write.Data
{
    public class TagComparer : EqualityComparer<Tag>
    {
        public override bool Equals(Tag x, Tag y)
        {
            //Check whether the compared objects reference the same data.
            if (ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return x.Id == y.Id && x.Name == y.Name;
        }

        public override int GetHashCode(Tag tag)
        {
            //Check whether the object is null
            if (ReferenceEquals(tag, null)) return 0;

            //Get hash codes
            var hashPodcastName = tag.Name == null ? 0 : tag.Name.GetHashCode();
            var hashPodcastId = tag.Id == Guid.Empty ? 0 : tag.Id.GetHashCode();

            //Calculate the hash code.
            return hashPodcastName ^ hashPodcastId;
        }
    }
}
