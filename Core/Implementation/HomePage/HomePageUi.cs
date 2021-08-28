using Core.Core;
using Core.Interfaces;
using HelperProject;
using HelperProject.Logging;

namespace Core.Implimentation.HomePage
{
    public class HomePageUi : IHomePage
    {
        public void OpenShufersalHomePage()
        {
            SeleniumWrapper.GetDriver().Navigate().GoToUrl(EnvParams.Domain());
            Logger.Info("HomePage is open");
        }
    }
}
