using System;
using rift.net.Models;
using System.Collections.Generic;
using rift.net;
using Ninject;

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
		private IKernel kernel = new StandardKernel();
		private IMainView _view;

		public MainController (Session session, Character character)
		{
			kernel.Load(AppDomain.CurrentDomain.GetAssemblies());

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
			_friendsController.Model = Friends;
			_friendsController.View = kernel.Get<IContactView>();

			_guildiesController = new ContactController (client);
			_guildiesController.Model = Guildies;
			_guildiesController.View = kernel.Get<IContactView>();

			_chatController = new ChatController (client, ChatChannel.Guild);
			_chatController.View = kernel.Get<IChatView>();

			_view = kernel.Get<IMainView> ();
			_view.ChatView = _chatController.View;
			_view.FriendsView = _friendsController.View;
			_view.GuildiesView = _guildiesController.View;

			client.Connect ();

			client.Listen ();

			_view.ShowView ();
		}
	}
}