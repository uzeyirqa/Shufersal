using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelpersProject.Helpers;
using TechTalk.SpecFlow;

namespace Core.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeScenario]
        public void BeforeScenario()
        {
            //TODO: implement logic that has to run before executing each scenario
        }

        [AfterScenario]
        public void AfterScenario()
        {
            new WebDriverHelper().CloseAllWebDrivers();
        }
    }
}