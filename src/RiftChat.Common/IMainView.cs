using System;

namespace RiftChat.Common
{
	public interface IMainView
	{
		IContactView FriendsView {
			get;
			set;
		}

		IContactView GuildiesView {
			get;
			set;
		}

		IChatView ChatView { 
			get;
			set;
		}

		void ShowView();
	}
}