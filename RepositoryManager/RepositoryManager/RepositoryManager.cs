using System.Text.Json;
using System.Xml.Linq;

namespace RepositoryManager
{
    public class Repository
    {
        private readonly string _rootPath;
        private static readonly object _locker = new object();
        /*
         itemType 1 = JSON
         itemType 2 = XML
         */
        public Repository(string rootPath)
        {
            _rootPath = rootPath;
        }

        void Initialize() { }

        // Add and new file for string content
        public void Register(string itemName, string itemContent, int itemType)
        {
            lock (_locker)
            {
                switch (itemType)
                {
                    case 1:
                        RegisterJSON(itemName, itemContent);
                        break;
                    case 2:
                        RegisterXML(itemName, itemContent);
                        break;
                }
            }
        }

        // Add and new file for integer content
        public void Register(string itemName, int itemContent, int itemType)
        {
            string itemContentString = itemContent.ToString();
            lock (_locker)
            {
                switch (itemType)
                {
                    case 1:
                        RegisterJSON(itemName, itemContentString);
                        break;
                    case 2:
                        RegisterXML(itemName, itemContentString);
                        break;
                }
            }
        }

        // Add and new file for double content
        public void Register(string itemName, double itemContent, int itemType)
        {
            string itemContentString = itemContent.ToString();
            lock (_locker)
            {
                switch (itemType)
                {
                    case 1:
                        RegisterJSON(itemName, itemContentString);
                        break;
                    case 2:
                        RegisterXML(itemName, itemContentString);
                        break;
                }
            }
        }

        // Register JSON File
        private void RegisterJSON(string itemName, string itemContent)
        {
            string savePath = _rootPath + itemName + ".json";

            if (File.Exists(savePath))
            {
                Console.WriteLine("File with that name already exist");
                return;
            }

            string jsonString = JsonSerializer.Serialize(itemContent);
            File.WriteAllText(savePath, jsonString);
        }

        // Register XML File
        private void RegisterXML(string itemName, string itemContent)
        {
            string savePath = _rootPath + itemName + ".XML";
            if (File.Exists(savePath))
            {
                Console.WriteLine("File with that name already exist");
                return;
            }

            XDocument xmlDoc = new XDocument(new XElement(itemContent));
            xmlDoc.Save(savePath);
        }

        // get saved file type
        public int GetType(string itemName)
        {
            lock (_locker)
            {
                string path = _rootPath + itemName;
                int type;

                if (File.Exists(path + ".json"))
                {
                    type = 1;
                }
                else if (File.Exists(path + ".xml"))
                {
                    type = 2;
                }
                else
                {
                    type = -1;
                }

                return type;
            }
        }

        // delete file
        public bool Deregister(string itemName)
        {
            string path = _rootPath + itemName;

            lock (_locker)
            {
                if (File.Exists(path + ".json"))
                {
                    File.Delete(path + ".json");
                    return true;
                }
                else if (File.Exists(path + ".xml"))
                {
                    File.Delete(path + ".xml");
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}
