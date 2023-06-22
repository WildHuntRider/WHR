using System;
using System.Collections.Generic;

namespace WHR.XML.Utility
{
    internal class UUIDGeneratorV4
    {
        public static List<Guid> GenerateUUIDV4(int numUuids)
        {
            List<Guid> uuids = new List<Guid>();

            for (int i = 0; i < numUuids; i++)
            {
                Guid uuid = Guid.NewGuid();
                uuids.Add(uuid);
            }

            return uuids;
        }
    }
}
