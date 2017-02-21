namespace Extensions.ApplicationSettings
{
    /// <summary>
    /// Creates a settings interface specific to a user
    /// </summary>
    /// <typeparam name="TSettings">Settings type</typeparam>
    /// <typeparam name="TUser">User type</typeparam>
    public interface IUserSettings<TSettings,TUser> : ISettings<TSettings>
    {
        TUser user { get; }
    }
}
