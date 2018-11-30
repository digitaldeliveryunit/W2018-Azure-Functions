using System;
using AutoMapper;

namespace com.petronas.myevents.api.Extensions
{
    public static class AutoMappingExtension
    {
        public static T Map<T>(this object source)
        {
            return Mapper.Map<T>(source);
        }
    }
}
