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
	private RiftChatClient chatClient;
	ContactController friendsController;
	ContactController guildiesController;
	ChatController guildChatController;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();

		var sessionFactory = new SessionFactory ();

		var session = sessionFactory.Login ("","");

		var securedClient = new RiftClientSecured (session);

		var bruun = securedClient.ListCharacters ().FirstOrDefault (x => x.FullName == "Bruun@Wolfsbane");

		var friends = securedClient.ListFriends (bruun.Id).OrderBy (x => x.Name).ToList();
		var guildies = securedClient.ListGuildmates (bruun.Guild.Id).OrderBy (x => x.Name).ToList();

		chatClient = new RiftChatClient (session, bruun);

		// Set the contact on the chat view
		chatwidgetGuild.Player = bruun;

		friendsController = new ContactController (chatClient) { View = (IContactView)treeview2, Model = friends };
		guildiesController = new ContactController (chatClient) { View = (IContactView)treeview3, Model = guildies };

		guildChatController = new ChatController (chatClient, ChatChannel.Guild) { View = chatwidgetGuild };

		chatClient.Connect ();

		chatClient.Listen ();
	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		chatClient.Stop ();

		Application.Quit ();
		a.RetVal = true;
	}
}