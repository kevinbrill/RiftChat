using System;
using Ninject.Modules;
using RiftChat.Common;
using Castle.MicroKernel.Registration;

namespace RiftChat
{
	public class Ninjection : IWindsorInstaller
	{
		public void Install (Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
		{
			container.Register (Component.For<ILoginView> ().ImplementedBy<LoginWindow> ());
			container.Register (Component.For<IChatView> ().ImplementedBy<ChatWidget> ());
			container.Register (Component.For<IContactView> ().ImplementedBy<ContactWidget> ());
			container.Register (Component.For<IMainView> ().ImplementedBy<MainWindow> ());
		}
	}
}