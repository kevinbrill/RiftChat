using System;
using rift.net;

namespace RiftChat.Common
{
	public enum ChatChannel {
		Guild,
		Officer
	}

	public class ChatController
	{
		private RiftChatClient _client;
		private IChatView _view;

		public ChatController (RiftChatClient client, ChatChannel channel)
		{
			_client = client;
			Channel = channel;

			switch (channel) {
			case ChatChannel.Guild:
				_client.GuildChatReceived += HandleChatReceived;
				break;
			case ChatChannel.Officer:
				_client.OfficerChatReceived += HandleChatReceived;
				break;
			}
		}

		public ChatChannel Channel {
			get;
			private set;
		}

		void HandleChatReceived (object sender, Message e)
		{
			View.MessageReceived (e);
		}

		public IChatView View 
		{ 
			get { return _view; }
			set {
				if (_view != null) {
					_view.SendMessage -= HandleSendMessage;
				}

				_view = value;
				_view.SendMessage += HandleSendMessage;
			}
		}

		void HandleSendMessage (object sender, string e)
		{
			_client.SendGuildMessage (e);
		}
	}
}