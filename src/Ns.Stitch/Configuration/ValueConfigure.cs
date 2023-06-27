using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;

namespace Ns.StitchFramework.Configuration
{
    public static class ValueConfigure
    {
        [ExcludeFromCodeCoverage]
        public static CultureInfo DefaultCulture { get; set; } = CultureInfo.CurrentCulture;

        public static void ApplyDefaults()
        {
            DefaultByte();
            DefaultDecimal();
            DefaultDouble();
            DefaultFloat();
            DefaultInt();
            DefaultLong();
            DefaultShort();    
            DefaultString();
            DefaultSByte();
            DefaultUInt();
            DefaultULong();
            DefaultUShort();
            DefaultDate();
        }

        private static void DefaultDate()
        {
            Value<DateTime, DateTime>(v => v);
            Value<DateTime, DateTimeOffset>(v => v);
            Value<DateTimeOffset, DateTime>(v => v.UtcDateTime);
            Value<DateTimeOffset, DateTimeOffset>(v => v);
        }

        private static void Value<From, To>(Expression<Func<From, To>> expression)
        {
            Ns.Value<From, To>.Configure(expression);
        }

        private static void DefaultByte()
        {
            Value<byte, byte>(v => v);
            Value<byte, sbyte>(v => (sbyte)v);
            Value<byte, decimal>(v => v);
            Value<byte, double>(v => v);
            Value<byte, float>(v => v);
            Value<byte, int>(v => v);
            Value<byte, uint>(v => v);
            Value<byte, long>(v => v);
            Value<byte, ulong>(v => v);
            Value<byte, short>(v => v);
            Value<byte, ushort>(v => v);
            Value<byte, string>(v => v.ToString());
        }
        
        private static void DefaultSByte()
        {
            Value<sbyte, byte>(v => (byte)v);
            Value<sbyte, sbyte>(v => v);
            Value<sbyte, decimal>(v => v);
            Value<sbyte, double>(v => v);
            Value<sbyte, float>(v => v);
            Value<sbyte, int>(v => v);
            Value<sbyte, uint>(v => (uint)v);
            Value<sbyte, long>(v => v);
            Value<sbyte, ulong>(v => (ulong)v);
            Value<sbyte, short>(v => v);
            Value<sbyte, ushort>(v => (ushort)v);
            Value<sbyte, string>(v => v.ToString());
        }

        private static void DefaultDecimal()
        {
            Value<decimal, byte>(v => (byte)v);
            Value<decimal, sbyte>(v => (sbyte)v);
            Value<decimal, decimal>(v => v);
            Value<decimal, double>(v => (double)v);
            Value<decimal, float>(v => (float)v);
            Value<decimal, int>(v => (int)v);
            Value<decimal, uint>(v => (uint)v);
            Value<decimal, long>(v => (long)v);
            Value<decimal, ulong>(v => (ulong)v);
            Value<decimal, short>(v => (short)v);
            Value<decimal, ushort>(v => (ushort)v);
            Value<decimal, string>(v => v.ToString(DefaultCulture));
        }
        
        private static void DefaultDouble()
        {
            Value<double, byte>(v => (byte)v);
            Value<double, sbyte>(v => (sbyte)v);
            Value<double, decimal>(v => (decimal)v);
            Value<double, double>(v => v);
            Value<double, float>(v => (float)v);
            Value<double, int>(v => (int)v);
            Value<double, uint>(v => (uint)v);
            Value<double, long>(v => (long)v);
            Value<double, ulong>(v => (ulong)v);
            Value<double, short>(v => (short)v);
            Value<double, ushort>(v => (ushort)v);
            Value<double, string>(v => v.ToString(DefaultCulture));
        }
        
        private static void DefaultFloat()
        {
            Value<float, byte>(v => (byte)v);
            Value<float, sbyte>(v => (sbyte)v);
            Value<float, decimal>(v => (decimal)v);
            Value<float, double>(v => v);
            Value<float, float>(v => v);
            Value<float, int>(v => (int)v);
            Value<float, uint>(v => (uint)v);
            Value<float, long>(v => (long)v);
            Value<float, ulong>(v => (ulong)v);
            Value<float, short>(v => (short)v);
            Value<float, ushort>(v => (ushort)v);
            Value<float, string>(v => v.ToString(DefaultCulture));
        }
        
        private static void DefaultInt()
        {
            Value<int, byte>(v => (byte)v);
            Value<int, sbyte>(v => (sbyte)v);
            Value<int, decimal>(v => v);
            Value<int, double>(v => v);
            Value<int, float>(v => v);
            Value<int, int>(v => v);
            Value<int, uint>(v => (uint)v);
            Value<int, long>(v => v);
            Value<int, ulong>(v => (ulong)v);
            Value<int, short>(v => (short)v);
            Value<int, ushort>(v => (ushort)v);
            Value<int, string>(v => v.ToString());
        }
        
        private static void DefaultUInt()
        {
            Value<uint, byte>(v => (byte)v);
            Value<uint, sbyte>(v => (sbyte)v);
            Value<uint, decimal>(v => v);
            Value<uint, double>(v => v);
            Value<uint, float>(v => v);
            Value<uint, int>(v => (int)v);
            Value<uint, uint>(v => v);
            Value<uint, long>(v => v);
            Value<uint, ulong>(v => v);
            Value<uint, short>(v => (short)v);
            Value<uint, ushort>(v => (ushort)v);
            Value<uint, string>(v => v.ToString());
        }

        private static void DefaultLong()
        {
            Value<long, byte>(v => (byte)v);
            Value<long, sbyte>(v => (sbyte)v);
            Value<long, decimal>(v => v);
            Value<long, double>(v => v);
            Value<long, float>(v => v);
            Value<long, int>(v => (int)v);
            Value<long, uint>(v => (uint)v);
            Value<long, long>(v => v);
            Value<long, ulong>(v => (ulong)v);
            Value<long, short>(v => (short)v);
            Value<long, ushort>(v => (ushort)v);
            Value<long, string>(v => v.ToString());
        }    
        
        private static void DefaultULong()
        {
            Value<ulong, byte>(v => (byte)v);
            Value<ulong, sbyte>(v => (sbyte)v);
            Value<ulong, decimal>(v => v);
            Value<ulong, double>(v => v);
            Value<ulong, float>(v => v);
            Value<ulong, int>(v => (int)v);
            Value<ulong, uint>(v => (uint)v);
            Value<ulong, long>(v => (long)v);
            Value<ulong, ulong>(v => v);
            Value<ulong, short>(v => (short)v);
            Value<ulong, ushort>(v => (ushort)v);
            Value<ulong, string>(v => v.ToString());
        }

        private static void DefaultShort()
        {
            Value<short, byte>(v => (byte)v);
            Value<short, sbyte>(v => (sbyte)v);
            Value<short, decimal>(v => v);
            Value<short, double>(v => v);
            Value<short, float>(v => v);
            Value<short, int>(v => v);
            Value<short, uint>(v => (uint)v);
            Value<short, long>(v => v);
            Value<short, ulong>(v => (ulong)v);
            Value<short, short>(v => v);
            Value<short, ushort>(v => (ushort)v);
            Value<short, string>(v => v.ToString());
        }

        private static void DefaultUShort()
        {
            Value<ushort, byte>(v => (byte)v);
            Value<ushort, sbyte>(v => (sbyte)v);
            Value<ushort, decimal>(v => v);
            Value<ushort, double>(v => v);
            Value<ushort, float>(v => v);
            Value<ushort, int>(v => v);
            Value<ushort, uint>(v => v);
            Value<ushort, long>(v => v);
            Value<ushort, ulong>(v => v);
            Value<ushort, short>(v => (short)v);
            Value<ushort, ushort>(v => v);
            Value<ushort, string>(v => v.ToString());
        }
        
        private static void DefaultString()
        {
            Value<string, byte>(v => byte.Parse(v));
            Value<string, sbyte>(v => sbyte.Parse(v));
            Value<string, decimal>(v => decimal.Parse(v));
            Value<string, double>(v => double.Parse(v));
            Value<string, float>(v => float.Parse(v));
            Value<string, int>(v => int.Parse(v));
            Value<string, uint>(v => uint.Parse(v));
            Value<string, long>(v => long.Parse(v));
            Value<string, ulong>(v => ulong.Parse(v));
            Value<string, short>(v => short.Parse(v));
            Value<string, ushort>(v => ushort.Parse(v));
            Value<string, string>(v => v.ToString());
        }
    }
}
