using Foundation;
using UIKit;

namespace EscolaBiblica.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIResponder, IUIApplicationDelegate
    {
        //private bool _autenticado = false;

        //public UIStoryboard MainStoryboard => UIStoryboard.FromName("Main", null);

        [Export("window")]
        public UIWindow Window { get; set; }

        [Export("application:didFinishLaunchingWithOptions:")]
        public bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            //Window = new UIWindow(UIScreen.MainScreen.Bounds);
            //Window.MakeKeyAndVisible();

            //if (!_autenticado)
            //{
            //    var loginController = UIStoryboard.FromName("LoginStoryboard", null).InstantiateViewController(nameof(LoginController)) as LoginController;
            //    loginController.LoginComSucesso += (sender, e) =>
            //    {
            //        var mainController = MainStoryboard.InstantiateViewController(nameof(TabController));
            //        Window.RootViewController = mainController;

            //        UIView.Transition(Window, 0.5, UIViewAnimationOptions.TransitionFlipFromTop, () => Window.RootViewController = mainController, null);
            //    };

            //    Window.RootViewController = loginController;
            //}
            //else
            //{
            //    Window.RootViewController = MainStoryboard.InstantiateViewController(nameof(TabController));
            //}

            return true;
        }

        #region UISceneSession Lifecycle

        [Export("application:configurationForConnectingSceneSession:options:")]
        public UISceneConfiguration GetConfiguration(UIApplication application, UISceneSession connectingSceneSession, UISceneConnectionOptions options)
        {
            // Called when a new scene session is being created.
            // Use this method to select a configuration to create the new scene with.
            return UISceneConfiguration.Create("Default Configuration", connectingSceneSession.Role);
        }

        [Export("application:didDiscardSceneSessions:")]
        public void DidDiscardSceneSessions(UIApplication application, NSSet<UISceneSession> sceneSessions)
        {
            // Called when the user discards a scene session.
            // If any sessions were discarded while the application was not running, this will be called shortly after `FinishedLaunching`.
            // Use this method to release any resources that were specific to the discarded scenes, as they will not return.
        }

        #endregion
    }
}

