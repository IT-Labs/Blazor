using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Enums;
using Core.Framework.Extensions;

namespace Core.Framework
{
    public static class PermissionExtension
    {


        public static List<Permissions> ToPermissions(this Permissions permission)
        {
            return permission.ToString().Split(',').Select(flag => (Permissions)Enum.Parse(typeof(Permissions), flag)).ToList();
        }

        public static List<string> ToListPermission(this Permissions permission)
        {
            return permission.GetDescription().ToString().Split(',').ToList();
        }

        public static PermissionRequirement ToRequirement(this Permissions value)
        {
            return new PermissionRequirement(value);
        }
    }
}
