﻿namespace WowGuildManager.Common.GlobalConstants
{
    public static class ErrorConstants
    {
        public static string InvalidUserErrorMessage = "There is no such user!";
        public static string InvalidCharacterErrorMessage = "There is no such character!";
        public static string InvalidCharacterKickErrorMessage = "Can not kick that player!";
        public static string MaximumRegisteredPlayers = $"User cannot have more than {CharacterConstants.MaximumAllowedCharactersPerUser} characters in the guild!";
        public static string InvalidClassTypeErrorMessage = "Invalid class type!";
        public static string InvalidRoleTypeErrorMessage = "Invalid role type!";
        public static string InvalidRankTypeErrorMessage = "Invalid rank type!";
        public static string InvalidDestinationNameErrorMessage = "Invalid destination name!";
        public static string InvalidDungeonErrorMessage = "There is no such dungeon!";
        public static string InvalidRaidErrorMessage = "There is no such raid!";
        public static string AlreadyHasGuildMasterErrorMessage = "Cannot have more than one GuildMaster at a time.";
        public static string InvalidDataProvided = "Invalid data provided!";
        public static string InvalidRaidKickErrorMessage = "There is no player signed for this this raid!";
        public static string InvalidImageErrorMessage = "There is no such Image!";
        public static string InvalidCharacterNameErrorMessage = "There is already character with this name!";
        public static string AccessDeniedErrorMessage = "You don not have right to do this!";
    }
}
