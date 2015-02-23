using System;
using Ninject;
using rift.net;
using System.Linq;

namespace RiftChat.Common
{
	public class ProgramFlowController
	{
		private LoginController loginController;
		private MainController mainController;
		private IKernel kernel;

		public ProgramFlowController ()
		{
			kernel = new StandardKernel ();
			kernel.Load(AppDomain.CurrentDomain.GetAssemblies());
		}

		public void Authenticate()
		{
			var loginView = kernel.Get<ILoginView> ();

			loginController = new LoginController (loginView);
			loginController.LoginSuccess += HandleLoginSuccess;
			loginController.CharacterSelected += HandleCharacterSelected;
		}

		void HandleCharacterSelected (object sender, rift.net.Models.Character e)
		{
			var client = new RiftClientSecured (loginController.Session);

			mainController = new MainController (loginController.Session, e);

			mainController.Friends = client.ListFriends( e.Id ).OrderBy (x => x.Name).ToList();
			mainController.Guildies = client.ListGuildmates (e.Guild.Id).OrderBy (x => x.Name).ToList ();

			mainController.Start ();
		}

		void HandleLoginSuccess (object sender, EventArgs e)
		{

		}
	}
}