namespace RDIFramework.Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class ResourceManager
    {
        public SortedList<string, Resources> languageResources = new SortedList<string, Resources>();
        private static object object_0 = new object();
        private static volatile ResourceManager resourceManager_0 = null;
        private string string_0 = string.Empty;

        public string Get(string language, string key)
        {
            if (string.IsNullOrEmpty(language))
            {
                language = "zh-CHS";
            }
            if (!this.languageResources.ContainsKey(language))
            {
                return string.Empty;
            }
            return this.languageResources[language].Get(key);
        }

        public Hashtable GetLanguages()
        {
            Hashtable hashtable = new Hashtable();
            IEnumerator<KeyValuePair<string, Resources>> enumerator = this.languageResources.GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<string, Resources> current = enumerator.Current;
                current = enumerator.Current;
                hashtable.Add(current.Key, current.Value.displayName);
            }
            return hashtable;
        }

        public Hashtable GetLanguages(string path)
        {
            Hashtable hashtable = new Hashtable();
            DirectoryInfo info = new DirectoryInfo(path);
            if (info.Exists)
            {
                FileInfo[] files = info.GetFiles();
                for (int i = 0; i < files.Length; i++)
                {
                    Resources resources = Class1.smethod_0(files[i].FullName);
                    hashtable.Add(resources.language, resources.displayName);
                }
            }
            return hashtable;
        }

        public Resources GetResources(string filePath)
        {
            Resources resources = new Resources();
            if (File.Exists(filePath))
            {
                resources = Class1.smethod_0(filePath);
                resources.createIndex();
            }
            return resources;
        }

        public void Init(string filePath)
        {
            this.string_0 = filePath;
            DirectoryInfo info = new DirectoryInfo(filePath);
            this.languageResources.Clear();
            if (info.Exists)
            {
                FileInfo[] files = info.GetFiles();
                for (int i = 0; i < files.Length; i++)
                {
                    Resources resources = Class1.smethod_0(files[i].FullName);
                    resources.createIndex();
                    this.languageResources.Add(resources.language, resources);
                }
            }
        }

        public void Serialize(Resources resources, string filePath)
        {
            Class1.smethod_1(filePath, resources);
        }

        public static ResourceManager Instance
        {
            get
            {
                if (resourceManager_0 == null)
                {
                    lock (object_0)
                    {
                        if (resourceManager_0 == null)
                        {
                            resourceManager_0 = new ResourceManager();
                        }
                    }
                }
                return resourceManager_0;
            }
        }
    }
}

