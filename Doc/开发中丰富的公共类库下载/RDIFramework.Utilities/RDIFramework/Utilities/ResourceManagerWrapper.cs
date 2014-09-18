namespace RDIFramework.Utilities
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public class ResourceManagerWrapper
    {
        private static object object_0 = new object();
        private ResourceManager resourceManager_0;
        private static volatile ResourceManagerWrapper resourceManagerWrapper_0 = null;

        public string Get(string key)
        {
            if (this.resourceManager_0 == null)
            {
                return string.Empty;
            }
            return this.resourceManager_0.Get(SystemInfo.CurrentLanguage, key);
        }

        public string Get(UserInfo userInfo, string key)
        {
            if ((userInfo == null) || string.IsNullOrEmpty(userInfo.CurrentLanguage))
            {
                return this.resourceManager_0.Get(SystemInfo.CurrentLanguage, key);
            }
            return this.resourceManager_0.Get(userInfo.CurrentLanguage, key);
        }

        public string Get(string language, string key)
        {
            return this.resourceManager_0.Get(language, key);
        }

        public Hashtable GetLanguages()
        {
            if (this.resourceManager_0 == null)
            {
                return null;
            }
            return this.resourceManager_0.GetLanguages();
        }

        public Hashtable GetLanguages(string path)
        {
            return this.resourceManager_0.GetLanguages(path);
        }

        public Resources GetResources(string language)
        {
            return this.resourceManager_0.languageResources[language];
        }

        public Resources GetResources(string path, string language)
        {
            string filePath = path + @"\" + language + ".xml";
            return this.resourceManager_0.GetResources(filePath);
        }

        public void LoadResources(string path)
        {
            this.resourceManager_0 = ResourceManager.Instance;
            this.resourceManager_0.Init(path);
        }

        public void Serialize(string path, string language, string key, string value)
        {
            Resources resources = this.GetResources(path, language);
            resources.Set(key, value);
            string filePath = path + @"\" + language + ".xml";
            this.resourceManager_0.Serialize(resources, filePath);
        }

        public static ResourceManagerWrapper Instance
        {
            get
            {
                if (resourceManagerWrapper_0 == null)
                {
                    lock (object_0)
                    {
                        if (resourceManagerWrapper_0 == null)
                        {
                            resourceManagerWrapper_0 = new ResourceManagerWrapper();
                        }
                    }
                }
                return resourceManagerWrapper_0;
            }
        }
    }
}

