
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.VBox vbox2;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	
	private global::Gtk.TextView textviewChat;
	
	private global::Gtk.HBox hbox1;
	
	private global::Gtk.Entry entryChat;
	
	private global::Gtk.Button buttonSend;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		this.AllowShrink = true;
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox2 = new global::Gtk.VBox ();
		this.vbox2.Name = "vbox2";
		this.vbox2.Spacing = 6;
		// Container child vbox2.Gtk.Box+BoxChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.textviewChat = new global::Gtk.TextView ();
		this.textviewChat.CanFocus = true;
		this.textviewChat.Name = "textviewChat";
		this.textviewChat.Editable = false;
		this.GtkScrolledWindow.Add (this.textviewChat);
		this.vbox2.Add (this.GtkScrolledWindow);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.GtkScrolledWindow]));
		w2.Position = 0;
		// Container child vbox2.Gtk.Box+BoxChild
		this.hbox1 = new global::Gtk.HBox ();
		this.hbox1.Name = "hbox1";
		this.hbox1.Spacing = 6;
		// Container child hbox1.Gtk.Box+BoxChild
		this.entryChat = new global::Gtk.Entry ();
		this.entryChat.CanFocus = true;
		this.entryChat.Name = "entryChat";
		this.entryChat.IsEditable = true;
		this.entryChat.InvisibleChar = '•';
		this.hbox1.Add (this.entryChat);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.entryChat]));
		w3.Position = 0;
		// Container child hbox1.Gtk.Box+BoxChild
		this.buttonSend = new global::Gtk.Button ();
		this.buttonSend.CanDefault = true;
		this.buttonSend.CanFocus = true;
		this.buttonSend.Name = "buttonSend";
		this.buttonSend.UseUnderline = true;
		this.buttonSend.Label = global::Mono.Unix.Catalog.GetString ("Send");
		this.hbox1.Add (this.buttonSend);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonSend]));
		w4.Position = 1;
		w4.Expand = false;
		w4.Fill = false;
		this.vbox2.Add (this.hbox1);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox1]));
		w5.Position = 1;
		w5.Expand = false;
		w5.Fill = false;
		this.Add (this.vbox2);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 693;
		this.DefaultHeight = 598;
		this.buttonSend.HasDefault = true;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
	}
}
