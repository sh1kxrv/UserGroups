namespace UserGroups.Code
{
    public enum LevelAccess
    {
        Admin,
        HRD,
        Storage,
        Paymaster,
        Accountant,
        Application,
        Unknown
    }
    public static class LevelParser
    {
        public static LevelAccess GetAccess(int number)
        {
            switch (number)
            {
                case 1:
                    return LevelAccess.Accountant;
                case 2:
                    return LevelAccess.Admin;
                case 3:
                    return LevelAccess.HRD;
                case 4:
                    return LevelAccess.Paymaster;
                case 5:
                    return LevelAccess.Storage;
                default:
                    return LevelAccess.Unknown;
            }
        }
    }
}
