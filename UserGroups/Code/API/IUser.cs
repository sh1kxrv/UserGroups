namespace UserGroups.Code.API
{
    /// <summary>
    /// Абстракция пользователя
    /// </summary>
    public interface IUser
    {
        LevelAccess Access { get; }
        string Name { get; }
    }
}
