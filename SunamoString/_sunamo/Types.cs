namespace SunamoString._sunamo;

internal class Types
{
    internal static Type ObjectType => typeof(object);
    internal static Type StringBuilderType => typeof(StringBuilder);
    internal static Type IEnumerableType => typeof(IEnumerable);
    internal static Type StringType => typeof(string);
    internal static Type FloatType => typeof(float);
    internal static Type DoubleType => typeof(double);
    internal static Type IntType => typeof(int);
    internal static Type LongType => typeof(long);
    internal static Type ShortType => typeof(short);
    internal static Type DecimalType => typeof(decimal);
    internal static Type SbyteType => typeof(sbyte);
    internal static Type ByteType => typeof(byte);
    internal static Type UshortType => typeof(ushort);
    internal static Type UintType => typeof(uint);
    internal static Type UlongType => typeof(ulong);
    internal static Type DateTimeType => typeof(DateTime);
    internal static Type BinaryType => typeof(byte[]);
    internal static Type CharType => typeof(char);
    internal static readonly List<Type> allBasicTypes = new()
{
ObjectType, StringType, StringBuilderType, IntType, DateTimeType,
DoubleType, FloatType, CharType, BinaryType, ByteType, ShortType, BinaryType, LongType, DecimalType, SbyteType, UshortType, UintType, UlongType
};
    internal static Type ListType => typeof(IList);
    #region Same seria as in DefaultValueForTypeT
    internal static Type BoolType => typeof(bool);
    #region Signed numbers
    #endregion
    #region Unsigned numbers
    #endregion
    internal static Type GuidType => typeof(Guid);
    #endregion
}