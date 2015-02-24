using System;
using rift.net.Models;
using System.Collections.Generic;
using rift.net;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.MicroKernel.Registration;

namespace RiftChat.Common
{
	public class MainController
	{
		private RiftChatClient client;
		private Session _session;
		private Character _character;
		private ChatController _chatController;
		private ContactController _friendsController;
		private ContactController _guildiesController;
		private WindsorContainer container;
		private IMainView _view;

		public MainController (Session session, Character character)
		{
			container = new WindsorContainer ();

			container.Install (FromAssembly.InDirectory (new AssemblyFilter (".")));

			_session = session;
			_character = character;
		}

		public List<Contact> Friends {
			get;
			set;
		}

		public List<Contact> Guildies {
			get;
			set;
		}

		public void Start()
		{
			if (client != null) {
				client.Stop ();
			}				

			client = new RiftChatClient (_session, _character);

			_friendsController = new ContactController (client);
			_friendsController.View = container.Resolve<IContactView>();
			_friendsController.Model = Friends;

			_guildiesController = new ContactController (client);
			_guildiesController.View = container.Resolve<IContactView>();
			_guildiesController.Model = Guildies;

			_chatController = new ChatController (client, ChatChannel.Guild);
			_chatController.View = container.Resolve<IChatView>();

			_view = container.Resolve<IMainView> ();
			_view.ChatView = _chatController.View;
			_view.FriendsView = _friendsController.View;
			_view.GuildiesView = _guildiesController.View;

			client.Connect ();

			client.Listen ();

			_view.ShowView ();
		}
	}
}