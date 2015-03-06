using System;
using Gtk;
using rift.net;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Diagnostics;
using rift.net.Models;
using RiftChat.Common;
using Castle.Core;

public partial class MainWindow: Gtk.Window, IMainView
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	#region IMainView implementation

	public void ShowView ()
	{
		Show ();
	}

	[DoNotWire]
	public IContactView FriendsView {
		get { return null; }
		set {
			var widget = (Widget)value;

			widget.Visible = true;
			widget.Events = ((global::Gdk.EventMask)(256));
			widget.Name = "contactwidgetFriends";
			this.vpaned1.Pack1 (widget, true, true);
		}
	}

	[DoNotWire]
	public IContactView GuildiesView {
		get { return null; }
		set {
			var widget = (Widget)value;

			widget.Visible = true;
			widget.Events = ((global::Gdk.EventMask)(256));
			widget.Name = "contactwidgetGuildies";
			this.vpaned1.Pack2 (widget, true, true);
		}
	}

	[DoNotWire]
	public IChatView ChatView {
		get { return null; }
		set {
			var widget = (Widget)value;

			widget.Visible = true;
			widget.Events = ((global::Gdk.EventMask)(256));
			widget.Name = "chatWidgetGuild";
			this.hpaned1.Pack2(widget, true, true);
		}
	}

	#endregion
}