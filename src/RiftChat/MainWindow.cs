using System;
using Gtk;
using rift.net;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Diagnostics;
using rift.net.Models;
using RiftChat.Common;

public partial class MainWindow: Gtk.Window
{
	private const string LOGIN_LOGOUT = "LoginLogout";
	private const string GUILD_CHAT = "GuildChat";

	private RiftChatClient chatClient;
	ContactController friendsController;
	ContactController guildiesController;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();

		this.textviewChat.ModifyBase(StateType.Normal, new Gdk.Color(0,0,0));

		foreach (var tag in GetNamedTags()) {
			this.textviewChat.Buffer.TagTable.Add (tag);
		}

		var sessionFactory = new SessionFactory ();

		var session = sessionFactory.Login ("", "");

		var securedClient = new RiftClientSecured (session);

		var bruun = securedClient.ListCharacters ().FirstOrDefault (x => x.FullName == "Bruun@Wolfsbane");

		var friends = securedClient.ListFriends (bruun.Id).OrderBy (x => x.Name).ToList();
		var guildies = securedClient.ListGuildmates (bruun.Guild.Id).OrderBy (x => x.Name).ToList();

		chatClient = new RiftChatClient (session, bruun);

		friendsController = new ContactController (chatClient) { View = (IContactView)treeview2, Model = friends };
		guildiesController = new ContactController (chatClient) { View = (IContactView)treeview3, Model = guildies };

		chatClient.Connect ();

		chatClient.Listen ();

		chatClient.Connected += (sender, e) => {
			Application.Invoke( delegate {
				var iter = this.textviewChat.Buffer.GetIterAtLine(this.textviewChat.Buffer.LineCount);

				this.textviewChat.Buffer.Insert(ref iter, "Connected!\n");
			});
		};

		chatClient.Login += (sender, e) => {
			Application.Invoke( delegate {
				WriteMessage( string.Format("{0}{1} has come online.", e.Character.Name, e.Location == Location.Web ? " (m)" : ""), LOGIN_LOGOUT );
			});
		};

		chatClient.Logout += (sender, e) => {
			Application.Invoke( delegate {
				WriteMessage( string.Format("{0}{1} has gone offline.", e.Character.Name, e.Location == Location.Web ? " (m)" : ""), LOGIN_LOGOUT );
			});
		};

		chatClient.GuildChatReceived += (sender, e) => {
			Application.Invoke( delegate {
				WriteMessage (string.Format ("{0}: {1}", e.Sender.Name, e.Text), GUILD_CHAT);
			});
		};

		buttonSend.Clicked += (sender, e) => {
			if( string.IsNullOrWhiteSpace( entryChat.Text ) ) {
				return;
			}

			chatClient.SendGuildMessage( entryChat.Text.Trim() );
			entryChat.Text = string.Empty;
			if( entryChat.CanFocus ){
				entryChat.GrabFocus();
			}
		};
	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		chatClient.Stop ();

		Application.Quit ();
		a.RetVal = true;
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