using System;
using rift.net;
using rift.net.Models;
using System.Collections.Generic;
using System.Linq;

namespace RiftChat.Common
{
	public class ContactController
	{
		private RiftChatClient _client;
		private List<Contact> _model;

		public IContactView View { get; set; }

		public List<Contact> Model { 
			get { return _model; }
			set {
				_model = value;

				_model.ForEach (View.AddContact);
			}
		}

		public ContactController (RiftChatClient client)
		{
			_client = client;

			_client.Login += OnLogin;
			_client.Logout += OnLogout;
		}

		private void OnLogin( object sender, rift.net.Models.Action e) {

			var character = _model.FirstOrDefault (x => x.Id == e.Character.Id);

			if (character != null) {
				if (e.Location == Location.Game) {
					character.Presence.IsOnlineInGame = true;
				} else if (e.Location == Location.Web) {
					character.Presence.IsOnlineOnWeb = true;
				}
			}

			View.Login( e.Character, e.Location );
		}

		private void OnLogout( object sender, rift.net.Models.Action e) {

			var character = _model.FirstOrDefault (x => x.Id == e.Character.Id);

			if (character != null) {
				if (e.Location == Location.Game) {
					character.Presence.IsOnlineInGame = false;
				} else if (e.Location == Location.Web) {
					character.Presence.IsOnlineOnWeb = false;
				}
			}

			View.Logout( e.Character, e.Location );
		}
	}
}