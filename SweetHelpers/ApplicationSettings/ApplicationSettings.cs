using System;
using System.Linq;
using Newtonsoft.Json;
using Serilog;
using Extensions.Data;
using System.Reflection;
using SweetHelpers.Exceptions;
using SweetHelpers.Extensions;

namespace Extensions.Settings
{
    public abstract class ApplicationSettings
    {
        #region Properties
        public abstract ILogger logger { get; set; }
        //public abstract Context ctx { get; set; }


        #endregion

        public ApplicationSettings()
        {
            //appSettings = new AppSettings();
            //amazonCollectorSettings = new AmazonCollectorSettings();
        }

        #region Inner Classes

        //public class AppSettings
        //{
        //    public string userTheam { get; set; }

        //}
        //public class AmazonCollectorSettings
        //{
        //    public bool? showOnlyMyEntries { get; set; }
        //    public bool? saveFilter { get; set; }
        //    public string filter { get; set; }
        //    public int monthsTillExpiration { get; set; }
        //}

        #endregion

        #region Methods
        Context getDbContext()
        {
            return new Context();
        }

        public void initializeSettings()
        {
            logger.Debug("Initializing settings");

            UserApplicationSettings userAppSettings;
            var applicationAssembly = Assembly.GetEntryAssembly().GetName();

            using (var db = getDbContext())
            {
                //get application settings from db
                logger.Debug("Getting application settings from db");
                userAppSettings = db.userApplicationSettings.FirstOrDefault(u => u.userName == Environment.UserName 
                //& u.applicationGuid.ToString() == applicationAssembly.Name
                );

                if (userAppSettings != null)
                {
                    logger.Debug("User settings found {Json}", userAppSettings.applicationSettings);
                    populateSettings(userAppSettings.applicationSettings);
                }
                else
                {
                    userAppSettings = new UserApplicationSettings()
                    {
                        userName = Environment.UserName,
            //add application id
                    };
                    //add user to db
                    db.userApplicationSettings.Add(userAppSettings);

                    logger.Debug("Saving new user");

                    try
                    {
                        var i = db.SaveChanges();
                        logger.Information("New user saved");
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, ex.GetBottomException().Message);
                    }

                }
            }
        }

        public void saveAppSettings()
        {

            using (var db = getDbContext())
            {
                var settings = db.userApplicationSettings.Single(u => u.userName == Environment.UserName);

                var settingsStr = serializeSettings();
                if (!settingsStr.IsNullOrEmpty())
                {
                    logger.Verbose("Saving user settings {json}", settingsStr);
                    settings.applicationSettings = settingsStr;
                    settings.dateUpdated = DateTime.Now;
                    db.SaveChanges();
                    logger.Debug("Settings Saved.");
                }
            }
        }

        string serializeSettings()
        {
            logger.Debug("Serializing user settings");
            string settingsStr = string.Empty;
            try
            {
                settingsStr = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Serializing user settings");
            }

            return settingsStr;
        }

        void populateSettings(string jsonSettings)
        {
            logger.Verbose("Deserializing josn {Json}", jsonSettings);

            //ApplicationSettings settings = new ApplicationSettings();

            try
            {
                JsonConvert.PopulateObject(jsonSettings, this);
                //_ApplicationSettings = JsonConvert.DeserializeObject<ApplicationSettings>(jsonSettings);
            }
            catch (Exception ex)
            {
                logger.Warning(ex, "Error parsing the settings");
            }

            //return settings;
        }

        #endregion
    }
}
