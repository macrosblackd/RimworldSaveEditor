using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using Verse;

namespace RimWorldDefDumperMod
{
    public static class EnumUtils
    {
        public static IEnumerable<TEnum> GetEnabledFlags<TEnum>(TEnum val) where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof (TEnum).IsEnum || !typeof(TEnum).HasAttribute<FlagsAttribute>())
            {
                Log.Error(string.Format("{0} is not an enum or is not a flag type", typeof (TEnum).Name));
                return null;
            }

            var names = Enum.GetNames(typeof (TEnum));
            var flags = names.Select(n => (TEnum)Enum.Parse(typeof(TEnum), n));

            return flags.Where(f => (((int) (object) f) & ((int) (object) val)) == ((int) (object) f));
        }

        public static string GetName<TEnum>(TEnum value)
        {
            return Enum.GetName(typeof (TEnum), value);
        }
    }
}