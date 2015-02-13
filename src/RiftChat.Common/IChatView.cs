using System;
using rift.net;

namespace RiftChat.Common
{
	public interface IChatView
	{
		event EventHandler<string> SendMessage;

		void MessageReceived( Message message );
	}
}

