namespace Extensions.ApplicationSettings
{
    /// <summary>
    /// Creates a settings interface specific to an application
    /// </summary>
    /// <typeparam name="TSettings">Settings type</typeparam>
    /// <typeparam name="TApplication">The application type </typeparam>
    public interface IAppSettings<TSettings,TApplication> : ISettings<TSettings>
    {
        TApplication ApplicationId { get; }
    }
}
