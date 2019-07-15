namespace WowGuildManager.Common.GlobalConstants
{
    public static class WowGuildManagerUserConstants
    {
        public static string DefaultUser = "DefaultUser";
        public static string RaidLeader = "RaidLeader";
        public static string GuildMaster = "GuildMaster";
        public static string Admin = "Admin";

        public static string DefaultUserDescription = "This role is set by default when creating new character and only provide only basic access.";
        public static string RaidLeaderDescription = "This role gives access to create raid events and give guild points.";
        public static string GuildMasterDescription = "This role is set by admin and has the authority to make changes inside the guild.";
        public static string AdminDescription = "This role is initialy created with database and has full access to everything.";

        public static string AdminEmail = "Admin@admin.com";
        public static string AdminUserName = "Admin";
        public static string AdminPassword = "123";

        public static string NullUserWarningMessage = "Warning! Null user!";
    }
}
