using System;
using rift.net.Models;
using System.Collections.Generic;
using rift.net;

namespace RiftChat.Common
{
	public interface IContactView
	{
		string ContactTypeName { get; set; }

		bool IsOfflineVisible { get; set; }
		bool IsWebVisible { get; set; }

		void AddContact (Contact contact);

		void Login (Contact contact, Location location);
		void Logout (Contact contact, Location location);
	}
}