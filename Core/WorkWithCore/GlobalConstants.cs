using System;
using NUnit.Framework;

namespace Core.Context
{
    public static class GlobalConstants
    {
        public static readonly string ImplementationType = TestContext.Parameters.Get("testRunType");
        // Target environment configuration file.
        public const string EnvironmentConfig = "environments.xml";
    }
}
