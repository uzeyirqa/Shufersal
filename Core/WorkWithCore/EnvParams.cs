using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Core.Context;
using HelperProject.Logging;
using HelpersProject.Helpers;
using NUnit.Framework;

namespace Core.Core
{
    public class EnvParams
    {
        // Names of parameters.
        private const string EnvNameNode = "name";
        private const string BrandNameNode = "brandname";
        private const string DomainNameNode = "domainname";
        private const string SalNode = "sal";
        private const string AppNode = "app";
        private const string BrandNode = "brand";


        // Test configurations.
        private static readonly string _env = TestContext.Parameters.Get("env");
        private static readonly string _brand = TestContext.Parameters.Get("brand");
        private static string _browser = TestContext.Parameters.Get("browser");

        // environment block from environment xml.
        private static XmlNode CurrentEnv { get; }

        static EnvParams()
        {
            var environmentXml = new XmlDocument();

            // Load environment xml.
            try
            {
                 var path = new FileHelper().GetPathToSolutionRoot() 
                            + GlobalConstants.EnvironmentConfig;

                environmentXml.Load(path);
            }
            catch (Exception e)
            {
                throw new FileLoadException($"Problem with loading environment xml - {e.Message}");
            }

            // Check if environment has been set.
            if (_env != "default")
            {
                if (_env == null)
                {
                    Logger.Warning("Environment hasn't been set; 'localhost' will be used");
                    _env = "localhost";
                }

                // Gets XmlElement by name of environment.
                foreach (XmlElement de in environmentXml.DocumentElement)
                {
                    if (!de.HasAttribute(EnvNameNode)) continue;

                    if (de.GetAttribute(EnvNameNode).Equals(_env))
                        CurrentEnv = de;
                }

                if (CurrentEnv == null)
                    throw new ArgumentException($"No environment - {_env}");
            }
        }

        /**************** Methods to provide tests with appropriate environment settings **************************/

        /*--- Test configurations ---*/

        public static string EnvName()
        {
            return _env;
        }

        public static string BrandName()
        {
            return TestContext.Parameters.Exists("brandName") ?
                TestContext.Parameters.Get("brandName") :
                GetBrandAsXmlNode().SelectSingleNode(BrandNameNode)?.InnerText;
        }

        /// <summary>
        /// Gets all brand names of current environment.
        /// </summary>
        /// <returns>List of brand objects (nodes).</returns>
        public static string NameOfBrand()
        {
            return GetBrandAsXmlNode().Attributes["name"].Value;
        }
        
        

        public static string BrowserName()
        {
            if (_browser == null || _browser.Equals("default"))
                _browser = "chrome";

            return _browser;
        }

        /*--- Domains ---*/

        public static string Domain()
        {
            return  GetBrandAsXmlNode().SelectSingleNode(SalNode).SelectSingleNode(AppNode).InnerText;
        }

        public static string DomainName()
        {
            return GetBrandAsXmlNode().SelectSingleNode(DomainNameNode)?.InnerText;
        }
        
        /*--- Other ---*/

        /// <summary>
        /// Select brand by specified name.
        /// </summary>
        /// <param name="brandName">Name of brand.</param>
        /// <returns>_brand parameters object ('node').</returns>
        public static XmlNode GetBrandAsXmlNode(string brandName = null)
        {
            brandName ??= _brand;

            foreach (XmlNode bn in CurrentEnv.SelectNodes(BrandNode))
                if (bn.Attributes["name"].Value.Equals(brandName))
                    return bn;

            throw new Exception($"No brand - {brandName}");
        }

        /// <summary>
        /// Gets all brand objects (nodes) of current environment.
        /// </summary>
        /// <returns>List of brand objects (nodes).</returns>
        public static XmlNodeList AllBrands()
        {
            return CurrentEnv.SelectNodes(BrandNode);
        }

        /// <summary>
        /// Gets all brand names of current environment.
        /// </summary>
        /// <returns>List of brand objects (nodes).</returns>
        public static List<string> AllBrandNames()
        {
            return (from XmlNode bn in AllBrands() select bn.Attributes["name"].Value).ToList();
        }

        public static List<string> AllEnvironmentNames()
        {
            var allEnvironmentNames = new List<string>();

            foreach (XmlNode bn in CurrentEnv.SelectNodes(BrandNode))
                allEnvironmentNames.Add(bn.SelectSingleNode(BrandNameNode)?.InnerText);

            return allEnvironmentNames;
        }
    }
}
