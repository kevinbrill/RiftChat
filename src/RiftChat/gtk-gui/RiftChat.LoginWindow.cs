
// This file has been generated by the GUI designer. Do not modify.
namespace RiftChat
{
	public partial class LoginWindow
	{
		private global::Gtk.VBox vbox1;
		
		private global::Gtk.Frame frame1;
		
		private global::Gtk.Alignment GtkAlignment;
		
		private global::Gtk.VBox vbox3;
		
		private global::Gtk.HBox hbox2;
		
		private global::Gtk.Label label2;
		
		private global::Gtk.Entry entryUsername;
		
		private global::Gtk.HBox hbox3;
		
		private global::Gtk.Label label3;
		
		private global::Gtk.Entry entryPassword;
		
		private global::Gtk.HButtonBox hbuttonbox1;
		
		private global::Gtk.Button buttonLogin;
		
		private global::Gtk.Button buttonCancel;
		
		private global::Gtk.Label GtkLabel2;
		
		private global::Gtk.Frame frame2;
		
		private global::Gtk.Alignment GtkAlignment1;
		
		private global::Gtk.VBox vbox2;
		
		private global::Gtk.HBox hbox1;
		
		private global::Gtk.ComboBox comboboxCharacters;
		
		private global::Gtk.Button buttonCharacterSelect;
		
		private global::Gtk.Label GtkLabel4;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget RiftChat.LoginWindow
			this.Name = "RiftChat.LoginWindow";
			this.Title = global::Mono.Unix.Catalog.GetString ("LoginWindow");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.BorderWidth = ((uint)(3));
			// Container child RiftChat.LoginWindow.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.frame1 = new global::Gtk.Frame ();
			this.frame1.Name = "frame1";
			this.frame1.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame1.Gtk.Container+ContainerChild
			this.GtkAlignment = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment.Name = "GtkAlignment";
			this.GtkAlignment.LeftPadding = ((uint)(12));
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Username");
			this.hbox2.Add (this.label2);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.label2]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.entryUsername = new global::Gtk.Entry ();
			this.entryUsername.CanFocus = true;
			this.entryUsername.Name = "entryUsername";
			this.entryUsername.IsEditable = true;
			this.entryUsername.InvisibleChar = '•';
			this.hbox2.Add (this.entryUsername);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.entryUsername]));
			w2.Position = 1;
			this.vbox3.Add (this.hbox2);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox2]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Password");
			this.hbox3.Add (this.label3);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.label3]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.entryPassword = new global::Gtk.Entry ();
			this.entryPassword.CanFocus = true;
			this.entryPassword.Name = "entryPassword";
			this.entryPassword.IsEditable = true;
			this.entryPassword.Visibility = false;
			this.entryPassword.InvisibleChar = '•';
			this.hbox3.Add (this.entryPassword);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.entryPassword]));
			w5.Position = 1;
			this.vbox3.Add (this.hbox3);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox3]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbuttonbox1 = new global::Gtk.HButtonBox ();
			this.hbuttonbox1.Name = "hbuttonbox1";
			// Container child hbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
			this.buttonLogin = new global::Gtk.Button ();
			this.buttonLogin.CanFocus = true;
			this.buttonLogin.Name = "buttonLogin";
			this.buttonLogin.UseUnderline = true;
			this.buttonLogin.Label = global::Mono.Unix.Catalog.GetString ("Login");
			this.hbuttonbox1.Add (this.buttonLogin);
			global::Gtk.ButtonBox.ButtonBoxChild w7 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox1 [this.buttonLogin]));
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString ("Cancel");
			this.hbuttonbox1.Add (this.buttonCancel);
			global::Gtk.ButtonBox.ButtonBoxChild w8 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox1 [this.buttonCancel]));
			w8.Position = 1;
			w8.Expand = false;
			w8.Fill = false;
			this.vbox3.Add (this.hbuttonbox1);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbuttonbox1]));
			w9.Position = 2;
			w9.Expand = false;
			w9.Fill = false;
			this.GtkAlignment.Add (this.vbox3);
			this.frame1.Add (this.GtkAlignment);
			this.GtkLabel2 = new global::Gtk.Label ();
			this.GtkLabel2.Name = "GtkLabel2";
			this.GtkLabel2.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Login</b>");
			this.GtkLabel2.UseMarkup = true;
			this.frame1.LabelWidget = this.GtkLabel2;
			this.vbox1.Add (this.frame1);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.frame1]));
			w12.Position = 0;
			w12.Expand = false;
			w12.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.frame2 = new global::Gtk.Frame ();
			this.frame2.Name = "frame2";
			this.frame2.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame2.Gtk.Container+ContainerChild
			this.GtkAlignment1 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment1.Name = "GtkAlignment1";
			this.GtkAlignment1.LeftPadding = ((uint)(12));
			// Container child GtkAlignment1.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.comboboxCharacters = global::Gtk.ComboBox.NewText ();
			this.comboboxCharacters.Name = "comboboxCharacters";
			this.hbox1.Add (this.comboboxCharacters);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.comboboxCharacters]));
			w13.Position = 0;
			w13.Expand = false;
			w13.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonCharacterSelect = new global::Gtk.Button ();
			this.buttonCharacterSelect.CanFocus = true;
			this.buttonCharacterSelect.Name = "buttonCharacterSelect";
			this.buttonCharacterSelect.UseUnderline = true;
			this.buttonCharacterSelect.Label = global::Mono.Unix.Catalog.GetString ("Select");
			this.hbox1.Add (this.buttonCharacterSelect);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonCharacterSelect]));
			w14.PackType = ((global::Gtk.PackType)(1));
			w14.Position = 1;
			w14.Expand = false;
			w14.Fill = false;
			this.vbox2.Add (this.hbox1);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox1]));
			w15.Position = 0;
			w15.Expand = false;
			w15.Fill = false;
			this.GtkAlignment1.Add (this.vbox2);
			this.frame2.Add (this.GtkAlignment1);
			this.GtkLabel4 = new global::Gtk.Label ();
			this.GtkLabel4.Name = "GtkLabel4";
			this.GtkLabel4.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Character Select</b>");
			this.GtkLabel4.UseMarkup = true;
			this.frame2.LabelWidget = this.GtkLabel4;
			this.vbox1.Add (this.frame2);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.frame2]));
			w18.Position = 1;
			w18.Expand = false;
			w18.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 186;
			this.Show ();
		}
	}
}
