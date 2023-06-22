using System;
using System.Collections.Generic;

namespace WHR.XML.Utility
{
    internal class UUIDGeneratorV1
    {
        private static readonly DateTimeOffset GregorianCalendarStart = new DateTimeOffset(1582, 10, 15, 0, 0, 0, TimeSpan.Zero);

        public static List<Guid> GenerateUUIDV1(int numUuids)
        {
            List<Guid> uuids = new List<Guid>();

            for (int i = 0; i < numUuids; i++)
            {
                var uuidBytes = new byte[16];
                Array.Copy(
                    BitConverter.GetBytes(
                        (DateTimeOffset.UtcNow - GregorianCalendarStart).Ticks), uuidBytes, 8);

                var random = new Random((int)DateTime.Now.Ticks + i);
                var segment = new ArraySegment<byte>(uuidBytes, 8, 8);
                for (int j = 0; j < segment.Count; j++)
                {
                    segment.Array[segment.Offset + j] = (byte)random.Next(256);
                }

                // version V1
                uuidBytes[7] &= 0x0f;
                uuidBytes[7] |= 1 << 4;

                // layout
                uuidBytes[8] &= 0x3f;
                uuidBytes[8] |= 0x80;

                uuids.Add(new Guid(uuidBytes));
            }

            return uuids;
        }
    }
}
