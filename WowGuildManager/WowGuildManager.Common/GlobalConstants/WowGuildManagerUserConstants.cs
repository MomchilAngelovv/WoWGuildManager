﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WowGuildManager.Common.GlobalConstants
{
    public static class WowGuildManagerUserConstants
    {
        public static string Admin = "Admin";
        public static string RaidLeader = "RaidLeader";
        public static string DefaultUser = "DefaultUser";

        public static string AdminDescription = "This role is initialy created with database and has full access to everything.";
        public static string RaidLeaderDescription = "This role gives access to create raid events and give guild points.";
        public static string DefaultUserDescription = "This role is set by default when creating new character and only provide only basic access.";

        public static string AdminEmail = "Admin@admin.com";
        public static string AdminUserName = "Admin";
        public static string AdminPassword = "123";
    }
}