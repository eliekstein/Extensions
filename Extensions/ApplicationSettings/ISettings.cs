namespace Extensions.ApplicationSettings
{
    /// <summary>
    /// Creates a settings interface
    /// </summary>
    /// <typeparam name="TSettings">Settings type</typeparam>
    public interface ISettings<TSettings>
    {
        TSettings Get();
        void Save();
    }
}
