using System;
using RiftChat.Common;
using Gtk;
using rift.net.Models;
using System.Collections.Generic;
using System.Linq;
using rift.net;

namespace RiftChat
{
	public class ContactView : TreeView, IContactView
	{
		ListStore model = new ListStore(typeof(Contact));
		Dictionary<string, TreeIter> iterators = new Dictionary<string, TreeIter>();
		TreeModelFilter filter = null;

		public ContactView ()
		{
			this.AppendColumn ("Name", new CellRendererText(), new TreeCellDataFunc(RenderContactName));

			IsOfflineVisible = false;
			IsWebVisible = false;

			filter = new TreeModelFilter (model, null);
			filter.VisibleFunc = new TreeModelFilterVisibleFunc (FilterView);
			this.Model = filter;
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