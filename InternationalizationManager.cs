using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace recommenders
{
    public class InternationalizationManager
    {
        private string currentLanguage;
        private string defaultLanguage;
        private Dictionary<string, string> currentLanguageTranslationDictionary;
        private Dictionary<string, string> defaultLanguageTranslationDictionary;

        public InternationalizationManager()
        {
            currentLanguageTranslationDictionary = new Dictionary<string, string>();
            currentLanguage = "en-US";
            defaultLanguage = "en-US";
            LoadTranslationsInDictionaryForGivenLanguage(currentLanguage);
            defaultLanguageTranslationDictionary = currentLanguageTranslationDictionary.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value);
        }

        public void SetLanguage(string language)
        {
            currentLanguageTranslationDictionary = new Dictionary<string, string>();
            currentLanguage = language;
            LoadTranslationsInDictionaryForGivenLanguage(currentLanguage);
        }

        private void LoadTranslationsInDictionaryForGivenLanguage(string language)
        {
            string projectDirectoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string fileName = $"Resources.{language}.resx";
            string resxFilePath = Path.Combine(projectDirectoryPath, "Resources", fileName);

            if (File.Exists(resxFilePath))
            {
                XmlDocument xmlDocumentToLoadResxFile = new XmlDocument();
                xmlDocumentToLoadResxFile.Load(resxFilePath);

                XmlNodeList xmlDataAsNodeList = xmlDocumentToLoadResxFile.DocumentElement.SelectNodes("//data");
                if (xmlDataAsNodeList != null)
                {
                    foreach (XmlNode node in xmlDataAsNodeList)
                    {
                        XmlAttribute nameAttributeInXmlFile = node.Attributes["name"];
                        if (nameAttributeInXmlFile != null)
                        {
                            string attributeFromXmlFileAsDictionaryKey = nameAttributeInXmlFile.Value;
                            XmlNode valueNodeInXmlFile = node.SelectSingleNode("value");

                            if (valueNodeInXmlFile != null)
                            {
                                string attributeFromXmlFileAsDictionaryValue = valueNodeInXmlFile.InnerText;
                                currentLanguageTranslationDictionary[attributeFromXmlFileAsDictionaryKey] = attributeFromXmlFileAsDictionaryValue;
                            }
                            else
                            {
                                throw new Exception($"[ERROR]:Value attribute is null in resx file for language '{language}' at '{resxFilePath}'.");
                            }
                        }
                        else
                        {
                            throw new Exception($"[ERROR]:Name attribute is null in resx file for language '{language}' at '{resxFilePath}'.");
                        }
                    }
                }
                else
                {
                    throw new Exception($"[ERROR]:Resx file is null for language '{language}' at '{resxFilePath}'.");
                }
            }
            else
            {
                throw new Exception($"[ERROR]:Resx file not found for language '{language}' at '{resxFilePath}'.");
            }
        }

        public string GetTranslationForIdentifier(string identifier)
        {
            if (currentLanguageTranslationDictionary.ContainsKey(identifier))
            {
                return currentLanguageTranslationDictionary[identifier];
            }
            else
            {
                if (defaultLanguageTranslationDictionary.ContainsKey(identifier))
                    return defaultLanguageTranslationDictionary[identifier];
                else
                    throw new Exception($"[ERROR]:Translation not found for identifier '{identifier}' in language '{currentLanguage}' or '{defaultLanguage}'");
            }
        }
    }
}