using Core.Interfaces;
using Core.WorkWithCore;
using HelperProject;

namespace Core.Implimentation.HomePage
{
    public class HomePageUi : IHomePage
    {
        public void OpenShufersalHomePage() => SeleniumWrapper.GetDriver().Navigate().GoToUrl(EnvParams.ShufersalDomain());
    }
}
