using System;
using RiftChat.Common;
using System.Diagnostics;
using System.Net;
using Gtk;
using System.Collections.Generic;

namespace RiftChat
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class ChatWidget : Gtk.Bin, IChatView
	{
		private const string LOGIN_LOGOUT = "LoginLogout";
		private const string GUILD_CHAT = "GuildChat";

		public ChatWidget ()
		{
			this.Build ();

			this.textviewChat.ModifyBase(StateType.Normal, new Gdk.Color(0,0,0));

			foreach (var tag in GetNamedTags()) {
				this.textviewChat.Buffer.TagTable.Add (tag);
			}

			buttonSend.Clicked += (sender, e) => {
				if( string.IsNullOrWhiteSpace( entryChat.Text ) ) {
					return;
				}

				if( this.SendMessage != null ) 
					this.SendMessage( this, entryChat.Text.Trim() );

				entryChat.Text = string.Empty;
				if( entryChat.CanFocus ){
					entryChat.GrabFocus();
				}
			};
		}

		public event EventHandler<string> SendMessage;

		public void MessageReceived (rift.net.Message message)
		{
			Application.Invoke (delegate {
				WriteMessage (string.Format ("{0}: {1}", message.Sender.Name, message.Text), GUILD_CHAT);
			});
		}

		private List<TextTag> GetNamedTags()
		{
			var tags = new List<TextTag> ();

			var tag = new TextTag(LOGIN_LOGOUT);
			tag.Weight = Pango.Weight.Light;
			tag.ForegroundGdk = new Gdk.Color( 190, 190, 190 );
			tags.Add (tag);

			tag = new TextTag (GUILD_CHAT);
			tag.Weight = Pango.Weight.Normal;
			tag.ForegroundGdk = new Gdk.Color (50, 205, 50);
			tags.Add (tag);

			return tags;
		}

		private void WriteMessage(string message, string tagName, DateTime? dateTime = null)
		{
			var iter = this.textviewChat.Buffer.EndIter;

			var formattedMessage = string.Format ("{0} {1}{2}", dateTime == null ? DateTime.Now.ToString ("HH:mm:ss") : dateTime.Value.ToString ("HH:mm:ss"), WebUtility.HtmlDecode (message), System.Environment.NewLine);

			try {
				this.textviewChat.Buffer.InsertWithTagsByName(ref iter, formattedMessage, tagName);	
				this.textviewChat.ScrollToIter( this.textviewChat.Buffer.EndIter, 0, false, 0, 0 );
			} catch (Exception ex) {
				Debug.WriteLine (ex.ToString ());
			}
		}
	}
}

