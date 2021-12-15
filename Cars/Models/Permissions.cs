using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.Models
{
    public class Permissions
    {
        public static List<string> GeneratePermissionsList(string module)
        {
            //   if module do somthing else
            //if (module == "Map")
            //{
            //    return new List<string>()
            //{
            //    $"Permissions.{module}.View",
            //    $"Permissions.{module}.MapTracking",
            //    $"Permissions.{module}.Active_DeActive"

            //};
            //}
            if (module == "Users")
            {
                return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit"

            };

            }
            else if (module == "Roles")
            {
                return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.ManagePermissions"
            };

            }

            else
            {
                return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                 $"Permissions.{module}.Active_DeActive"
            };
            }
        }

        public static List<string> GenerateAllPermissions()
        {
            var allPermissions = new List<string>();

            var modules = Enum.GetValues(typeof(Modules));

            foreach (var module in modules)
                allPermissions.AddRange(GeneratePermissionsList(module.ToString()));

            return allPermissions;
        }

        //public static class Work_Permit
        //{
        //    public const string View = "Permissions.Work_Permit.View";
        //    public const string Create = "Permissions.Work_Permit.Create";
        //    public const string Edit = "Permissions.Work_Permit.Edit";
        //    public const string Active_DeActive = "Permissions.Work_Permit.Active_DeActive";
        //}
        //public static class Department
        //{
        //    public const string View = "Permissions.Department.View";
        //    public const string Create = "Permissions.Department.Create";
        //    public const string Edit = "Permissions.Department.Edit";
        //    public const string Active_DeActive = "Permissions.Department.Active_DeActive";
        //}
        //public static class Activity_Type
        //{
        //    public const string View = "Permissions.Activity_Type.View";
        //    public const string Create = "Permissions.Activity_Type.Create";
        //    public const string Edit = "Permissions.Activity_Type.Edit";
        //    public const string Active_DeActive = "Permissions.Activity_Type.Active_DeActive";
        //}
        //public static class Location
        //{
        //    public const string View = "Permissions.Location.View";
        //    public const string Create = "Permissions.Location.Create";
        //    public const string Edit = "Permissions.Location.Edit";
        //    public const string Active_DeActive = "Permissions.Location.Active_DeActive";
        //}
        //public static class Main_Area
        //{
        //    public const string View = "Permissions.Main_Area.View";
        //    public const string Create = "Permissions.Main_Area.Create";
        //    public const string Edit = "Permissions.Main_Area.Edit";
        //    public const string Active_DeActive = "Permissions.Main_Area.Active_DeActive";
        //}

        //public static class Production_Line
        //{
        //    public const string View = "Permissions.Production_Line.View";
        //    public const string Create = "Permissions.Production_Line.Create";
        //    public const string Edit = "Permissions.Production_Line.Edit";
        //    public const string Active_DeActive = "Permissions.Production_Line.Active_DeActive";
        //}
        //public static class Risk
        //{
        //    public const string View = "Permissions.Risk.View";
        //    public const string Create = "Permissions.Risk.Create";
        //    public const string Edit = "Permissions.Risk.Edit";
        //    public const string Active_DeActive = "Permissions.Risk.Active_DeActive";
        //}
        //public static class Map
        //{
        //    public const string View = "Permissions.Map.View";
        //    public const string MapTracking = "Permissions.Map.MapTracking";
        //    public const string Active_DeActive = "Permissions.Map.Active_DeActive";
        //}
        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";

        }
        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string ManagePermissions = "Permissions.Roles.ManagePermissions";


        }
    }
    public enum Modules
    {
      
        Users,
        Roles
    }
}
