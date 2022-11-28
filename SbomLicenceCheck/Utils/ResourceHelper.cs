using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SbomLicenceCheck.Utils
{
    /// <summary>
    /// Helper class to read embedded resources in assembly.
    /// </summary>
    public static class ResourceHelper
    {
        /// <summary>
        /// Read embedded resource as Stream.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="folder"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Stream ReadResource(Assembly assembly, string folder, string fileName)
        {
            string resourcePath;
            var assemblyName = assembly.GetName().Name;
            if (folder != null)
            {
                resourcePath = $"{assemblyName}.{folder}.{fileName}";
            }
            else
            {
                resourcePath = $"{assemblyName}.{fileName}";
            }

            return assembly.GetManifestResourceStream(resourcePath) ?? throw new FileNotFoundException($"{assembly.GetName()},{folder ?? string.Empty},{fileName}");
        }

        /// <summary>
        /// Read embedded resource as String.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="folder"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ReadResourceAsString(Assembly assembly, string folder, string fileName)
        {
            using var stream = ReadResource(assembly, folder, fileName);
            using var streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }
    }
}
