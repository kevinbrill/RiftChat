using System;
using RiftChat.Common;
using Gtk;
using System.Collections.Generic;
using rift.net.Models;
using rift.net;
using System.ComponentModel;
using Ninject.Modules;

namespace RiftChat
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class ContactWidget : Gtk.Bin, IContactView
	{
		ListStore model = new ListStore(typeof(Contact));
		Dictionary<string, TreeIter> iterators = new Dictionary<string, TreeIter>();
		TreeModelFilter filter = null;

		public ContactWidget ()
		{
			this.Build ();

			this.treeviewContacts.AppendColumn ("Name", new CellRendererText(), new TreeCellDataFunc(RenderContactName));

			IsOfflineVisible = false;
			IsWebVisible = false;

			this.togglebuttonMobile.Active = IsWebVisible;
			this.togglebuttonOffline.Active = IsOfflineVisible;

			filter = new TreeModelFilter (model, null);
			filter.VisibleFunc = new TreeModelFilterVisibleFunc (FilterView);
			this.treeviewContacts.Model = filter;

			this.togglebuttonMobile.Toggled += (sender, e) => {
				IsWebVisible = this.togglebuttonMobile.Active;
				filter.Refilter();
			};

			this.togglebuttonOffline.Toggled += (sender, e) => {
				IsOfflineVisible = this.togglebuttonOffline.Active;
				filter.Refilter();
			};
		}

		#region IContactView implementation

		public void AddContact (Contact contact)
		{
			var iter = model.AppendValues (contact);

			iterators [contact.Id] = iter;

			filter.Refilter ();
		}

		public void Login (rift.net.Models.Contact contact, Location location)
		{
			// Look up the iterator for the contact
			if (!iterators.ContainsKey (contact.Id)) {
				return;
			}

			var iter = iterators [contact.Id];

			Application.Invoke (delegate {
				model.EmitRowChanged (model.GetPath (iter), iter);
			});
		}

		public void Logout (rift.net.Models.Contact contact, Location location)
		{
			// Look up the iterator for the contact
			if (!iterators.ContainsKey (contact.Id)) {
				return;
			}

			var iter = iterators [contact.Id];

			Application.Invoke (delegate {
				model.EmitRowChanged (model.GetPath (iter), iter);
			});
		}

		public bool IsOfflineVisible { get; set; }

		public bool IsWebVisible { get; set; }

		public string ContactTypeName
		{
			get { return this.labelName.Text; }
			set { this.labelName.Text = value; }
		}

		#endregion

		private void RenderContactName( TreeViewColumn column, CellRenderer cell, TreeModel model, TreeIter iter )
		{
			var contact = model.GetValue (iter, 0) as Contact;
			var textCell = (cell as CellRendererText);

			if (contact.Presence.IsOnlineInGame) {
				textCell.Foreground = "black";
				textCell.Style = Pango.Style.Normal;
			} else if (contact.Presence.IsOnlineOnWeb) {
				textCell.Foreground = "darkgray";
				textCell.Style = Pango.Style.Italic;
			} else {
				textCell.Foreground = "lightgray";
				textCell.Style = Pango.Style.Normal;
			}

			textCell.Text = contact.Name;
		}

		private bool FilterView( TreeModel model, TreeIter iter )
		{
			var contact = model.GetValue (iter, 0) as Contact;

			if (contact == null) {
				return false;
			}

			if (IsOfflineVisible && !contact.Presence.IsOnlineOnWeb && !contact.Presence.IsOnlineInGame) {
				return true;
			} else if (IsWebVisible && (contact.Presence.IsOnlineInGame || contact.Presence.IsOnlineOnWeb)) {
				return true;
			} else {
				return contact.Presence.IsOnlineInGame;
			}
		}
	}
}

