using System;
using AspNetCore.WebApi.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomResponseAttribute : ProducesResponseTypeAttribute
    {
        public CustomResponseAttribute(int statusCode) : base(typeof(CustomResult), statusCode) { }
    }
}