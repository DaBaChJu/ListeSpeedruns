namespace ListeSpeedruns
{
	partial class Form1
	{
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.GUI_listView = new System.Windows.Forms.ListView();
			this.titre = new System.Windows.Forms.ColumnHeader();
			this.duree = new System.Windows.Forms.ColumnHeader();
			this.numero = new System.Windows.Forms.ColumnHeader();
			this.GUI_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.supprimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ouvrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enregistrerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.quiiterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aProposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aProposToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.GUI_toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.GUI_progressBar = new System.Windows.Forms.ProgressBar();
			this.GUI_panel_for_progressbar = new System.Windows.Forms.Panel();
			this.bW_constructeur = new System.ComponentModel.BackgroundWorker();
			this.bW_dragNdrop = new System.ComponentModel.BackgroundWorker();
			this.GUI_contextMenuStrip.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.GUI_panel_for_progressbar.SuspendLayout();
			this.SuspendLayout();
			// 
			// GUI_listView
			// 
			this.GUI_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.titre,
            this.duree,
            this.numero});
			this.GUI_listView.ContextMenuStrip = this.GUI_contextMenuStrip;
			this.GUI_listView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GUI_listView.LabelEdit = true;
			this.GUI_listView.LabelWrap = false;
			this.GUI_listView.Location = new System.Drawing.Point(0, 24);
			this.GUI_listView.Name = "GUI_listView";
			this.GUI_listView.ShowItemToolTips = true;
			this.GUI_listView.Size = new System.Drawing.Size(632, 429);
			this.GUI_listView.TabIndex = 0;
			this.GUI_listView.UseCompatibleStateImageBehavior = false;
			this.GUI_listView.View = System.Windows.Forms.View.Details;
			this.GUI_listView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.GUI_listView_AfterLabelEdit);
			this.GUI_listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.GUI_listView_ColumnClick);
			// 
			// titre
			// 
			this.titre.Text = "Titre";
			this.titre.Width = 314;
			// 
			// duree
			// 
			this.duree.Text = "Durée";
			// 
			// numero
			// 
			this.numero.Text = "N°";
			this.numero.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// GUI_contextMenuStrip
			// 
			this.GUI_contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.supprimerToolStripMenuItem});
			this.GUI_contextMenuStrip.Name = "GUI_contextMenuStrip";
			this.GUI_contextMenuStrip.Size = new System.Drawing.Size(123, 26);
			// 
			// supprimerToolStripMenuItem
			// 
			this.supprimerToolStripMenuItem.Name = "supprimerToolStripMenuItem";
			this.supprimerToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.supprimerToolStripMenuItem.Text = "Supprimer";
			this.supprimerToolStripMenuItem.Click += new System.EventHandler(this.supprimerToolStripMenuItem_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.aProposToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.menuStrip1.Size = new System.Drawing.Size(632, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fichierToolStripMenuItem
			// 
			this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirToolStripMenuItem,
            this.enregistrerToolStripMenuItem,
            this.toolStripSeparator1,
            this.quiiterToolStripMenuItem});
			this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
			this.fichierToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
			this.fichierToolStripMenuItem.Text = "Fichier";
			// 
			// ouvrirToolStripMenuItem
			// 
			this.ouvrirToolStripMenuItem.Name = "ouvrirToolStripMenuItem";
			this.ouvrirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.ouvrirToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.ouvrirToolStripMenuItem.Text = "Ouvrir...";
			this.ouvrirToolStripMenuItem.Click += new System.EventHandler(this.ouvrirToolStripMenuItem_Click);
			// 
			// enregistrerToolStripMenuItem
			// 
			this.enregistrerToolStripMenuItem.Name = "enregistrerToolStripMenuItem";
			this.enregistrerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.enregistrerToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.enregistrerToolStripMenuItem.Text = "Enregistrer sous...";
			this.enregistrerToolStripMenuItem.Click += new System.EventHandler(this.enregistrerToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
			// 
			// quiiterToolStripMenuItem
			// 
			this.quiiterToolStripMenuItem.Name = "quiiterToolStripMenuItem";
			this.quiiterToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.quiiterToolStripMenuItem.Text = "Quitter";
			this.quiiterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
			// 
			// aProposToolStripMenuItem
			// 
			this.aProposToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aProposToolStripMenuItem1});
			this.aProposToolStripMenuItem.Name = "aProposToolStripMenuItem";
			this.aProposToolStripMenuItem.Size = new System.Drawing.Size(24, 20);
			this.aProposToolStripMenuItem.Text = "?";
			// 
			// aProposToolStripMenuItem1
			// 
			this.aProposToolStripMenuItem1.Name = "aProposToolStripMenuItem1";
			this.aProposToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
			this.aProposToolStripMenuItem1.Text = "A propos";
			this.aProposToolStripMenuItem1.Click += new System.EventHandler(this.aProposToolStripMenuItem1_Click);
			// 
			// GUI_progressBar
			// 
			this.GUI_progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GUI_progressBar.Location = new System.Drawing.Point(0, 0);
			this.GUI_progressBar.Name = "GUI_progressBar";
			this.GUI_progressBar.Size = new System.Drawing.Size(632, 23);
			this.GUI_progressBar.TabIndex = 3;
			// 
			// GUI_panel_for_progressbar
			// 
			this.GUI_panel_for_progressbar.Controls.Add(this.GUI_progressBar);
			this.GUI_panel_for_progressbar.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.GUI_panel_for_progressbar.Location = new System.Drawing.Point(0, 430);
			this.GUI_panel_for_progressbar.Name = "GUI_panel_for_progressbar";
			this.GUI_panel_for_progressbar.Size = new System.Drawing.Size(632, 23);
			this.GUI_panel_for_progressbar.TabIndex = 4;
			this.GUI_panel_for_progressbar.Visible = false;
			// 
			// bW_constructeur
			// 
			this.bW_constructeur.WorkerReportsProgress = true;
			this.bW_constructeur.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bW_constructeur_DoWork);
			this.bW_constructeur.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bW_constructeur_RunWorkerCompleted);
			this.bW_constructeur.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bW_constructeur_ProgressChanged);
			// 
			// bW_dragNdrop
			// 
			this.bW_dragNdrop.WorkerReportsProgress = true;
			this.bW_dragNdrop.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bW_dragNdrop_DoWork);
			this.bW_dragNdrop.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bW_dragNdrop_RunWorkerCompleted);
			this.bW_dragNdrop.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bW_dragNdrop_ProgressChanged);
			// 
			// Form1
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(632, 453);
			this.Controls.Add(this.GUI_panel_for_progressbar);
			this.Controls.Add(this.GUI_listView);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "Liste de speedruns";
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.GUI_contextMenuStrip.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.GUI_panel_for_progressbar.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView GUI_listView;
		private System.Windows.Forms.ColumnHeader numero;
		private System.Windows.Forms.ColumnHeader titre;
		private System.Windows.Forms.ColumnHeader duree;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ouvrirToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem enregistrerToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem quiiterToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip GUI_contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem supprimerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aProposToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aProposToolStripMenuItem1;
		private System.Windows.Forms.ToolTip GUI_toolTip;
		private System.Windows.Forms.ProgressBar GUI_progressBar;
		private System.Windows.Forms.Panel GUI_panel_for_progressbar;
		private System.ComponentModel.BackgroundWorker bW_constructeur;
		private System.ComponentModel.BackgroundWorker bW_dragNdrop;

	}
}

