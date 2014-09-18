namespace RDIFramework.Utilities
{
    using System;

    public enum SerializedType : ushort
    {
        Bool = 4,
        Byte = 6,
        ByteArray = 0,
        CompressedByteArray = 0xff,
        CompressedObject = 0x100,
        CompressedString = 0x101,
        Datetime = 3,
        Double = 14,
        Float = 13,
        Int = 9,
        Long = 11,
        Object = 1,
        Short = 7,
        String = 2,
        UInt = 10,
        ULong = 12,
        UShort = 8
    }
}

