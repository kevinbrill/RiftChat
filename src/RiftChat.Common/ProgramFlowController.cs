using System;
using rift.net;
using System.Linq;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.MicroKernel.Registration;

namespace RiftChat.Common
{
	public class ProgramFlowController
	{
		private LoginController loginController;
		private MainController mainController;
		private WindsorContainer container;
		private ILoginView loginView;

		public ProgramFlowController ()
		{
			container = new WindsorContainer ();

			container.Install (FromAssembly.InDirectory (new AssemblyFilter (".")));
		}

		public void Authenticate()
		{
			loginView = container.Resolve<ILoginView> ();

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