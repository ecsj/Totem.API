﻿using System.ComponentModel;
using System.Reflection;

namespace Domain.Base;

public static class Extensions
{

    public static DateTime BrazilDateTime(this DateTime dataHora)
    {
        TimeZoneInfo timeZone = GetTimeZone();
        return TimeZoneInfo.ConvertTime(dataHora, timeZone);
    }
    private static TimeZoneInfo GetTimeZone()
    {
        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
        }
        catch (Exception)
        {
            return TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        }
    }
    public static string GetDescription(this Enum GenericEnum)
    {
        Type genericEnumType = GenericEnum.GetType();
        MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
        if ((memberInfo != null && memberInfo.Length > 0))
        {
            var _Attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (_Attribs?.Any() ?? false)
            {
                return (_Attribs.FirstOrDefault() as DescriptionAttribute)?.Description;
            }
        }
        return GenericEnum.ToString();
    }
}
