#define PROGRESSBAR_ADDON //should be done, may need bW break when trying to exit, debug currently load txt file by default

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using MediaInfoLib;
using System.IO;

///
/// TODO: Create GetListFromFile(string filename) method
/// 

namespace ListeSpeedruns
{

	public partial class Form1 : Form
	{

		bool bModified = false;
		string opened_file = null;
		string MI_version = "MediaInfo inconnu";

		public List<ListViewItem> list_speedruns;

		bool bTriParNom = false;

		ListOfReferences<ListViewItem> list_speedruns_by_time = new ListOfReferences<ListViewItem>();
		bool bTriDureeInverse = true;
		ListOfReferences<ListViewItem> list_speedruns_by_name = new ListOfReferences<ListViewItem>();
		bool bTriNomInverse = true;

		bool bResizedTimeColumn = false;

#if PROGRESSBAR_ADDON
		bool bIsUpdating = false; 
#endif

		/* Contenu d'un ListViewItem:
		 * 0 : titre
		 * 1 : durée
		 * 2 : numéro
		*/

		#region Constructeurs

		////////////////
		////////////////
		////////////////

		public Form1(string[] args)
		{

			InitializeComponent();

			MediaInfo MI_tmp = new MediaInfo();
			MI_version = MI_tmp.Option("Info_Version");
			MI_tmp.Close();

			if (args != null && args.Length == 1 && Path.GetExtension(args[0]) == ".txt")
			{

#if PROGRESSBAR_ADDON
				Disable_GUI_For_BackgroundWorker();
				bW_constructeur.RunWorkerAsync(args);
#else
				int nIndex = 1;
				StreamReader lecteur = new StreamReader(args[0]);
				string ligne_lue = null;
				List<ListViewItem> temp_list = new List<ListViewItem>();

				opened_file = args[0];

				list_speedruns = new List<ListViewItem>();

				while ((ligne_lue = lecteur.ReadLine()) != null)
				{

					ListViewItem item;

					string[] chaine_parsee;
					chaine_parsee = ligne_lue.Split(new char[] { '\t' }, 2);

					#region create ListViewItem

					item = new ListViewItem(chaine_parsee[1]);	//titre
					item.ToolTipText = item.SubItems[0].Text;

					item.SubItems.Add(chaine_parsee[0]);		//durée
					item.SubItems.Add(nIndex.ToString());		//numéro

					#endregion add to list_view

					nIndex++;

					temp_list.Add(item);

				}

				list_speedruns_by_name.SetComparer(this.CompareTitreItem);
				list_speedruns_by_name.SetReferencedList(list_speedruns);
				list_speedruns_by_time.SetComparer(this.CompareDureeItemInverse);
				list_speedruns_by_time.SetReferencedList(list_speedruns);

				AddEntries(temp_list.ToArray());
				temp_list.Clear();
				lecteur.Close();

				bModified = false; 
#endif

			}
			else
			{

				//création des listes de références pour tri
				list_speedruns = new List<ListViewItem>();

				list_speedruns_by_name.SetComparer(this.CompareTitreItem);
				list_speedruns_by_name.SetReferencedList(list_speedruns);
				list_speedruns_by_time.SetComparer(this.CompareDureeItemInverse);
				list_speedruns_by_time.SetReferencedList(list_speedruns);

				list_speedruns_by_name.Initialize();
				list_speedruns_by_time.Initialize();

				bModified = false;


			}

		}

		#endregion Constructeurs

		////////////////
		////////////////
		////////////////

		#region Drag&Drop

		private void Form1_DragEnter(object sender, DragEventArgs e)
		{

#if PROGRESSBAR_ADDON
			if (!bIsUpdating) 
#endif
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
					e.Effect = DragDropEffects.Copy;

		}

		private void Form1_DragDrop(object sender, DragEventArgs e)
		{

			string[] liste_éléments_dragndrop = null;
			liste_éléments_dragndrop = (string[])e.Data.GetData(DataFormats.FileDrop);

			if (liste_éléments_dragndrop != null)
			{
				Disable_GUI_For_BackgroundWorker();
				bW_dragNdrop.RunWorkerAsync(liste_éléments_dragndrop);
			}

		}

		#endregion Drag&Drop

		////////////////
		////////////////
		////////////////

		#region Menus

		private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{

#if PROGRESSBAR_ADDON
			if (bIsUpdating)
			{
				MessageBox.Show("Impossible de quitter pendant un traitement de données", "Quitter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				e.Cancel = true;
			}
			else
#endif
			if (!bModified)
				e.Cancel = false;
			else
				if (
					MessageBox.Show("Les modifications seront perdues.Quitter?", "Quitter", MessageBoxButtons.YesNo) == DialogResult.Yes
				)
					e.Cancel = false;
				else
					e.Cancel = true;

		}

		private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
		{

			OpenFileDialog ofn = new OpenFileDialog();
			ofn.Filter = "(*.txt)|*.txt|(*.*)|*.*";
			ofn.FilterIndex = 0;

			if (ofn.ShowDialog() == DialogResult.OK)
			{

				int nIndex = 1;
				StreamReader lecteur = new StreamReader(ofn.FileName);
				string ligne_lue = null;
				List<ListViewItem> temp_list = new List<ListViewItem>();

				opened_file = ofn.FileName;

				bTriParNom = false;
				bTriNomInverse = false;
				bTriDureeInverse = true;
				list_speedruns.Clear();
				list_speedruns_by_name.Clear();
				list_speedruns_by_time.Clear();

				while ((ligne_lue = lecteur.ReadLine()) != null)
				{

					ListViewItem item;

					string[] chaine_parsee;
					chaine_parsee = ligne_lue.Split(new char[] { '\t' }, 2);

					#region create ListViewItem

					item = new ListViewItem(chaine_parsee[1]);	//titre
					item.ToolTipText = item.SubItems[0].Text;

					item.SubItems.Add(chaine_parsee[0]);		//durée
					item.SubItems.Add(nIndex.ToString());		//numéro

					#endregion add to list_view

					nIndex++;

					temp_list.Add(item);

				}

				list_speedruns_by_name.SetComparer(this.CompareTitreItem);
				list_speedruns_by_name.SetReferencedList(list_speedruns);
				list_speedruns_by_time.SetComparer(this.CompareDureeItemInverse);
				list_speedruns_by_time.SetReferencedList(list_speedruns);

				AddEntries(temp_list.ToArray());
				temp_list.Clear();
				lecteur.Close();

				bModified = false;

			}

		}

		private void enregistrerToolStripMenuItem_Click(object sender, EventArgs e)
		{

			SaveFileDialog sfn=new SaveFileDialog();

			sfn.AddExtension = true;
			sfn.DefaultExt = "txt";

			if (opened_file != null)
				sfn.FileName = opened_file;

			sfn.Filter = "(*.txt)|*.txt|(*.*)|*.*";
			sfn.FilterIndex = 0;
			sfn.OverwritePrompt = true;

			if (sfn.ShowDialog() == DialogResult.OK)
			{

				//create backup of old file
				if (File.Exists(sfn.FileName))
				{

					if(File.Exists(sfn.FileName + ".bak")) {

						int i=2;

						while (File.Exists(sfn.FileName + string.Format(".{0:000}", i) + ".bak") && i<1000)
							i++;

						if (i < 1000)
							File.Copy(sfn.FileName, sfn.FileName + string.Format(".{0:000}", i) + ".bak");
						else
						{
							File.Copy(sfn.FileName, sfn.FileName + ".bak", true);
							MessageBox.Show("Un fichier de sauvegarde a été écrasé.Il y en avait vraiment 1000?!!");
						}

					}
					else
						File.Copy(sfn.FileName, sfn.FileName + ".bak");

				}

				StreamWriter writer = new StreamWriter(sfn.FileName);

				ListViewItem[] items_list=list_speedruns_by_time.ToArray();

				if (bTriDureeInverse) //le tri inverse est le tri par défaut du fichier de sortie
					foreach (ListViewItem item in items_list)
					{
						writer.WriteLine(item.SubItems[1].Text + "\t" + item.SubItems[0].Text);
					}
				else
					for (int i = list_speedruns.Count - 1; i >= 0; i--)
					{
						writer.WriteLine(items_list[i].SubItems[1].Text + "\t" + items_list[i].SubItems[0].Text);
					}

				writer.Close();
				bModified = false;

			}

		}

		private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
		{

			if (GUI_listView.FocusedItem != null)
			{

				//Pour être sûr que l'élément supprimé et l'élément annoncé dans la messagebox
				//sont bien le même
				ListViewItem selected_item = GUI_listView.FocusedItem;

				DialogResult resultat =
					MessageBox.Show(
						string.Format("Supprimer la ligne de {0}?", selected_item.Text),
						"Supprimer une ligne",
						MessageBoxButtons.YesNo
					);

				if (resultat == DialogResult.Yes)
				{
					RemoveEntry(selected_item);
					bModified = true;
				}

			}

		}

		private void aProposToolStripMenuItem1_Click(object sender, EventArgs e)
		{

			MessageBox.Show(
				"Créé par Damien Barral\n\n" +
				"Utilise " + MI_version +"\n" +
				"http://mediainfo.sourceforge.net",
				"A propos"
			);

		}

		#endregion Menus

		////////////////
		////////////////
		////////////////

		#region Clic sur une colonne (changement du tri des éléments)

		/// <summary>
		/// Action lors d'un clic sur un titre de colonne.
		/// Réorganise la liste sur l'élément cliqué.
		/// N'affecte pas le fichier généré en sortie.
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Paramètre contenant notamment l'index de base 0
		/// de la colonne cliquée.</param>

		private void GUI_listView_ColumnClick(object sender, ColumnClickEventArgs e)
		{

#if PROGRESSBAR_ADDON
			if (!bIsUpdating)
			{
#endif

				if (e.Column == 0) //colonne Titre
				{

					GUI_listView.BeginUpdate();

					if (bTriNomInverse)
						list_speedruns_by_name.SetComparer(CompareTitreItem);
					else
						list_speedruns_by_name.SetComparer(CompareTitreItemInverse);

					list_speedruns_by_name.Initialize();

					GUI_listView.Items.Clear();
					GUI_listView.Items.AddRange(list_speedruns_by_name.ToArray());

					bTriNomInverse = !bTriNomInverse;
					bTriParNom = true;

					GUI_listView.EndUpdate();

				}
				else
				if (e.Column == 1) //colonne Durée
				{

					GUI_listView.BeginUpdate();

					if (bTriDureeInverse)
						list_speedruns_by_time.SetComparer(CompareDureeItem);
					else
						list_speedruns_by_time.SetComparer(CompareDureeItemInverse);

					list_speedruns_by_time.Initialize();

					GUI_listView.Items.Clear();
					GUI_listView.Items.AddRange(list_speedruns_by_time.ToArray());

					bTriDureeInverse = !bTriDureeInverse;
					bTriParNom = false;

					GUI_listView.EndUpdate();

				}
				else
					MessageBox.Show("Pas de tri sur cet élément.");

				if (GUI_listView.SelectedIndices.Count > 0)
					GUI_listView.EnsureVisible(GUI_listView.SelectedIndices[0]);

#if PROGRESSBAR_ADDON
			}
#endif

		}

		#endregion Clic sur une colonne (changement du tri des éléments)

		#region Renommage d'un élément depuis la liste visuelle

		/// <summary>
		/// Modification du nom d'un élément de la liste
		/// </summary>
		/// <param name="sender">Paramètre par défaut.</param>
		/// <param name="e">Contient le nouveau texte à assigner.</param>
		/// <remarks>Annule l'édition ordinaire de texte, et regénère
		/// complètement les listes y compris la visuelle.</remarks>

		private void GUI_listView_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{

#if PROGRESSBAR_ADDON
			if (bIsUpdating)
				e.CancelEdit = true;
			else
#endif

			if (e.Label != null)
			{

				int nIndexListeGlobale = list_speedruns.IndexOf(GUI_listView.Items[e.Item]);
				int nIndexItemModifie;
				ListViewItem item_modifie;

			#region Création de l'item modifié

				item_modifie = new ListViewItem(e.Label);								//titre
				item_modifie.ToolTipText = item_modifie.SubItems[0].Text;
				item_modifie.SubItems.Add(GUI_listView.Items[e.Item].SubItems[1].Text);	//durée
				item_modifie.SubItems.Add(GUI_listView.Items[e.Item].SubItems[2].Text);	//numéro

				#endregion Création de l'item modifié

				RemoveEntry(GUI_listView.Items[e.Item]);
				AddEntry(item_modifie);
				nIndexItemModifie = GUI_listView.Items.IndexOf(item_modifie);

				GUI_listView.Items[nIndexItemModifie].Selected = true;
				GUI_listView.Items[nIndexItemModifie].Focused = true;
				GUI_listView.EnsureVisible(nIndexItemModifie);

				bModified = true;
				e.CancelEdit = true;	//la méthode remplace le processus normal d'édition

			}

		}

		#endregion Renommage d'un élément depuis la liste visuelle

		#region fonction spéciale CompareDurees(string,string)

		/// <summary>
		/// Compare 2 durées telles qu'utilisées par le programme.
		/// </summary>
		/// <param name="duree_1">durée telles qu'utilisées par le programme</param>
		/// <param name="duree_2">durée telles qu'utilisées par le programme</param>
		/// <returns>
		///		-1 : t1 est inférieur à t2
		///		0 : t1 est égal à t2
		///		1 : t1 est supérieur à t2
		///</returns>

		private int CompareDurees(string duree_1, string duree_2)
		{

			string[] duree_1_splittee = duree_1.Split(new string[] { " h ", " mn ", " s" }, 4, StringSplitOptions.None);
			string[] duree_2_splittee = duree_2.Split(new string[] { " h ", " mn ", " s" }, 4, StringSplitOptions.None);

			TimeSpan duree_1_timespan = new TimeSpan(
				int.Parse(duree_1_splittee[0]),
				int.Parse(duree_1_splittee[1]),
				int.Parse(duree_1_splittee[2])
			);

			TimeSpan duree_2_timespan = new TimeSpan(
				int.Parse(duree_2_splittee[0]),
				int.Parse(duree_2_splittee[1]),
				int.Parse(duree_2_splittee[2])
			);

			return TimeSpan.Compare(duree_1_timespan, duree_2_timespan);

		}
		#endregion fonction spéciale CompareDurees(string,string)

		#region fonctions spéciales AddEntry(ListViewItem),AddEntries(ListViewItem[]) et RemoveEntry(ListViewItem)

		//<summary>
		 //Ajoute une entrée aux listes
		 //</summary>
		 //<param name="item_to_add">entrée à ajouter</param>

		void AddEntry(ListViewItem item_to_add)
		{

			list_speedruns.Add(item_to_add);

			list_speedruns_by_name.Add(list_speedruns.Count - 1);
			list_speedruns_by_time.Add(list_speedruns.Count - 1);

			GUI_listView.BeginUpdate();

			GUI_listView.Items.Clear();

			if (bTriParNom)
				GUI_listView.Items.AddRange(list_speedruns_by_name.ToArray());
			else 
				GUI_listView.Items.AddRange(list_speedruns_by_time.ToArray());

			GUI_listView.EndUpdate();

			if (!bResizedTimeColumn)
			{
				GUI_listView.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
				bResizedTimeColumn = true;
			}

		}

		//<summary>
		//Ajoute plusieurs entrées aux listes
		//</summary>
		//<param name="items_to_add">entrées à ajouter</param>

		void AddEntries(ListViewItem[] items_to_add)
		{

			foreach (ListViewItem item_to_add in items_to_add)
			{

				list_speedruns.Add(item_to_add);

				list_speedruns_by_name.Add(list_speedruns.Count - 1);
				list_speedruns_by_time.Add(list_speedruns.Count - 1);

			}

			GUI_listView.BeginUpdate();

			GUI_listView.Items.Clear();

			if (bTriParNom)
				GUI_listView.Items.AddRange(list_speedruns_by_name.ToArray());
			else
				GUI_listView.Items.AddRange(list_speedruns_by_time.ToArray());

			GUI_listView.EndUpdate();

			if (!bResizedTimeColumn)
			{
				GUI_listView.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
				bResizedTimeColumn = true;
			}

		}

		//<summary>
		//Retire une entrée aux listes
		//</summary>
		//<param name="item_to_remove">entrée à retirer</param>

		void RemoveEntry(ListViewItem item_to_remove)
		{

			int nRemovedItemIndex = list_speedruns.IndexOf(item_to_remove);

			list_speedruns.Remove(item_to_remove);
			list_speedruns_by_name.Remove(nRemovedItemIndex);
			list_speedruns_by_time.Remove(nRemovedItemIndex);

			GUI_listView.BeginUpdate();

			GUI_listView.Items.Clear();

			if (bTriParNom)
				GUI_listView.Items.AddRange(list_speedruns_by_name.ToArray());
			else
				GUI_listView.Items.AddRange(list_speedruns_by_time.ToArray());

			GUI_listView.EndUpdate();

		}

		#endregion fonctions spéciales AddEntry(ListViewItem),AddEntries(ListViewItem[]) et RemoveEntry(ListViewItem)

		////////////////
		////////////////
		////////////////

		#region Compareurs pour tri

		int CompareTitreItem(ListViewItem item1, ListViewItem item2)
		{
			return item1.SubItems[0].Text.CompareTo(item2.SubItems[0].Text);
		}

		int CompareTitreItemInverse(ListViewItem item1, ListViewItem item2)
		{
			return -CompareTitreItem(item1, item2);
		}

		int CompareDureeItem(ListViewItem item1, ListViewItem item2)
		{
			return CompareDurees(item1.SubItems[1].Text, item2.SubItems[1].Text);
		}

		int CompareDureeItemInverse(ListViewItem item1, ListViewItem item2)
		{
			return -CompareDureeItem(item1, item2);
		}

		#endregion Compareurs pour tri

#if PROGRESSBAR_ADDON

		////////////////
		////////////////
		////////////////

		#region Enable_Disable_GUI_during_background_work

		void Disable_GUI_For_BackgroundWorker()
		{
			GUI_progressBar.Value = 0;
			GUI_panel_for_progressbar.Visible = true;
			bIsUpdating = true;
			this.Cursor = Cursors.No;
			this.menuStrip1.Enabled = false;
			this.GUI_listView.Enabled = false;
		}

		void Enable_GUI_After_BackgroundWorker()
		{
			GUI_panel_for_progressbar.Visible = false;
			bIsUpdating = false;
			this.Cursor = Cursors.Default;
			this.menuStrip1.Enabled = true;
			this.GUI_listView.Enabled = true;
		}

		#endregion

		#region BackgroundWorker constructeur

		private void bW_constructeur_DoWork(object sender, DoWorkEventArgs e)
		{

			int nIndex = 1;
			Int64 nombre_octets_lus = 0;
			string ligne_lue = null;
			string[] args = e.Argument as string[];
			List<ListViewItem> temp_list = new List<ListViewItem>();
			StreamReader lecteur = null;

			try
			{

				FileInfo infos_fichiers = new FileInfo(args[0]);
				lecteur = new StreamReader(args[0]);

				opened_file = args[0];

				list_speedruns = new List<ListViewItem>();

				Application.DoEvents();

				while ((ligne_lue = lecteur.ReadLine()) != null)
				{

					ListViewItem item;

					string[] chaine_parsee;
					chaine_parsee = ligne_lue.Split(new char[] { '\t' }, 2);

					foreach (string sous_chaine in chaine_parsee)
						nombre_octets_lus += sous_chaine.Length;
					bW_constructeur.ReportProgress(Math.Min((int)(100 * nombre_octets_lus / infos_fichiers.Length), 98));

					#region create ListViewItem

					item = new ListViewItem(chaine_parsee[1]);	//titre
					item.ToolTipText = item.SubItems[0].Text;

					item.SubItems.Add(chaine_parsee[0]);		//durée
					item.SubItems.Add(nIndex.ToString());		//numéro

					#endregion add to list_view

					nIndex++;

					temp_list.Add(item);

				}

				list_speedruns_by_name.SetComparer(this.CompareTitreItem);
				list_speedruns_by_name.SetReferencedList(list_speedruns);
				list_speedruns_by_time.SetComparer(this.CompareDureeItemInverse);
				list_speedruns_by_time.SetReferencedList(list_speedruns);

				bW_constructeur.ReportProgress(99);

				e.Result = temp_list.ToArray();
				temp_list.Clear();
				lecteur.Close();

				bModified = false;

			}
			catch (Exception)
			{

				MessageBox.Show("Le chargement de "+args[0]+" a échoué","Echec du chargement de l'argument",MessageBoxButtons.OK,MessageBoxIcon.Warning);

				list_speedruns = new List<ListViewItem>();

				list_speedruns_by_name.SetComparer(this.CompareTitreItem);
				list_speedruns_by_name.SetReferencedList(list_speedruns);
				list_speedruns_by_time.SetComparer(this.CompareDureeItemInverse);
				list_speedruns_by_time.SetReferencedList(list_speedruns);

				e.Result = temp_list.ToArray();
				temp_list.Clear();

				if(lecteur != null)
					lecteur.Close();

				bModified = false;

			}

			bW_constructeur.ReportProgress(100);
			

		}

		private void bW_constructeur_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			GUI_progressBar.Value = e.ProgressPercentage;
		}

		private void bW_constructeur_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			AddEntries(e.Result as ListViewItem[]);
			Enable_GUI_After_BackgroundWorker();
		}

		#endregion BackgroundWorker constructeur

		#region BackgroundWorker Drag&Drop

		private void bW_dragNdrop_DoWork(object sender, DoWorkEventArgs e)
		{

			string[] liste_éléments_dragndrop = e.Argument as string[];

			if (liste_éléments_dragndrop.Length == 1)
			{
				#region if (Path.GetExtension(liste_éléments_dragndrop[0]) == ".txt")

				if (Path.GetExtension(liste_éléments_dragndrop[0]) == ".txt")
				{

					DialogResult choix = MessageBox.Show(
						string.Format("Si une liste était ouverte,ses modifications seront perdues\n" +
										"Charger la liste du fichier {0}?", liste_éléments_dragndrop[0]),
						"Liste de speedruns",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.None
					);

					if (choix == DialogResult.Yes)
					{

						int nIndex = 1;
						Int64 nombre_octets_lus = 0;
						string ligne_lue = null;
						FileInfo infos_fichiers = new FileInfo(liste_éléments_dragndrop[0]);
						StreamReader lecteur = new StreamReader(liste_éléments_dragndrop[0]);
						List<ListViewItem> temp_list = new List<ListViewItem>();

						opened_file = liste_éléments_dragndrop[0];

						bTriParNom = false;
						bTriNomInverse = false;
						bTriDureeInverse = true;
						list_speedruns.Clear();
						list_speedruns_by_name.Clear();
						list_speedruns_by_time.Clear();

						while ((ligne_lue = lecteur.ReadLine()) != null)
						{

							ListViewItem item;
							string[] chaine_parsee;

							chaine_parsee = ligne_lue.Split(new char[] { '\t' }, 2);
							foreach (string chaine in chaine_parsee)
								nombre_octets_lus += chaine.Length;
							bW_dragNdrop.ReportProgress(Math.Min((int)(100 * nombre_octets_lus / infos_fichiers.Length), 98));

							#region create ListViewItem

							item = new ListViewItem(chaine_parsee[1]);	//titre
							item.ToolTipText = item.SubItems[0].Text;

							item.SubItems.Add(chaine_parsee[0]);		//durée
							item.SubItems.Add(nIndex.ToString());		//numéro

							#endregion add to list_view

							nIndex++;

							temp_list.Add(item);

						}

						list_speedruns_by_name.SetComparer(this.CompareTitreItem);
						list_speedruns_by_name.SetReferencedList(list_speedruns);
						list_speedruns_by_time.SetComparer(this.CompareDureeItemInverse);
						list_speedruns_by_time.SetReferencedList(list_speedruns);

						bW_dragNdrop.ReportProgress(99);

						e.Result = temp_list.ToArray();
						temp_list.Clear();
						lecteur.Close();

						bModified = false;

						bW_dragNdrop.ReportProgress(100);

					}
					else
						e.Result = null;

				}

				#endregion if (Path.GetExtension(liste_éléments_dragndrop[0]) == ".txt")
				#region if (Path.GetExtension(liste_éléments_dragndrop[0]) != ".txt")

				else //1 seul fichier passé qui n'est pas une liste
				{

					TimeSpan duree_reelle;
					string duree_reelle_txt;
					ListViewItem item;

					//video number start at 1, so last existing is list_speedruns.Count
					int i = list_speedruns.Count + 1;

					string duree_recupere = null;

					MediaInfo MI;

					bW_dragNdrop.ReportProgress(25);

					MI = new MediaInfo();
					MI.Open(liste_éléments_dragndrop[0]);
					duree_recupere = MI.Get(StreamKind.General, 0, "Duration");
					MI.Close();

					bW_dragNdrop.ReportProgress(50);

					duree_reelle = TimeSpan.FromMilliseconds(double.Parse(duree_recupere));
					duree_reelle_txt = string.Format("{0:00} h {1:00} mn {2:00} s", duree_reelle.Hours, duree_reelle.Minutes, duree_reelle.Seconds);

					item = new ListViewItem(Path.GetFileNameWithoutExtension(liste_éléments_dragndrop[0]));
					item.ToolTipText = item.SubItems[0].Text;
					item.SubItems.Add(duree_reelle_txt);
					item.SubItems.Add(i.ToString());

					bW_dragNdrop.ReportProgress(75);

					e.Result = item;

					bModified = true;

					bW_dragNdrop.ReportProgress(100);

				}

				#endregion if (Path.GetExtension(liste_éléments_dragndrop[0]) != ".txt")

			}
			else
			{
				#region if (liste_éléments_dragndrop.Length != 1)

				ListViewItem[] conteneur = new ListViewItem[liste_éléments_dragndrop.Length];

				// start at 1, so last existing is list_speedruns.Count
				int num_video = list_speedruns.Count + 1;
				int index_video_traitee = 0;

				foreach (string path_to_video in liste_éléments_dragndrop)
				{

					index_video_traitee++;
					bW_dragNdrop.ReportProgress(Math.Min(100 * index_video_traitee / liste_éléments_dragndrop.Length,99));

					string duree_recupere = null;

					MediaInfo MI = new MediaInfo();
					MI.Open(path_to_video);
					duree_recupere = MI.Get(StreamKind.General, 0, "Duration");
					MI.Close();

					if (duree_recupere != "")
					{

						TimeSpan duree_reelle;
						string duree_reelle_txt;
						ListViewItem item;

						duree_reelle = TimeSpan.FromMilliseconds(double.Parse(duree_recupere));
						duree_reelle_txt = string.Format("{0:00} h {1:00} mn {2:00} s", duree_reelle.Hours, duree_reelle.Minutes, duree_reelle.Seconds);

						item = new ListViewItem(Path.GetFileNameWithoutExtension(path_to_video));
						item.ToolTipText = item.SubItems[0].Text;
						item.SubItems.Add(duree_reelle_txt);
						item.SubItems.Add(num_video.ToString());

						//num_video (min:Count+1) - Count - 1 = 0-->liste_éléments_dragndrop.Length
						conteneur[num_video - list_speedruns.Count - 1] = item;

						num_video++;

					}

				}

				e.Result = conteneur;//AddEntries(conteneur);

				bModified = true;

				bW_dragNdrop.ReportProgress(100);

				#endregion if (liste_éléments_dragndrop.Length != 1)
			}

		}

		private void bW_dragNdrop_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			GUI_progressBar.Value = e.ProgressPercentage;
		}

		private void bW_dragNdrop_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Result != null)
			{
				if(e.Result is ListViewItem[])
					AddEntries(e.Result as ListViewItem[]);
				else
				if (e.Result is ListViewItem)
					AddEntry(e.Result as ListViewItem);
			}
			Enable_GUI_After_BackgroundWorker();
		}

		#endregion BackgroundWorker Drag&Drop

	}

#endif

}
