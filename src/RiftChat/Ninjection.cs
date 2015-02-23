using System;
using Ninject.Modules;
using RiftChat.Common;

namespace RiftChat
{
	public class Ninjection : NinjectModule
	{
		public override void Load ()
		{
			Bind<IChatView>().To<ChatWidget> ();
			Bind<IContactView> ().To<ContactWidget> ();
		}
	}
}

