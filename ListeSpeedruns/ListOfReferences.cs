using System;
using System.Collections.Generic;
using System.Text;

namespace ListeSpeedruns
{

	/// <summary>
	/// Liste de "références".Permet d'accéder de façon triée
	/// aux éléments d'une liste non triée.
	/// Utile pour les affichages à multiples colonnes permettant
	/// le tri par colonne.
	/// </summary>
	/// <typeparam name="T">Type des éléments de la liste à
	/// laquelle on fait réference.</typeparam>
	/// <remarks>Si T est IComparable, la liste peut faire un
	/// tri croissant par défaut, sinon il faut définir la
	/// fonction de comparaison pour trier.</remarks>

	public class ListOfReferences<T>
	{

		public delegate int ItemCompare(T elem_1, T elem_2);

		private IList<T> _referenced_list;
		private ItemCompare _comparer = null;
		private bool _comparer_required = true;
		private LinkedList<int> _liste_indexs=new LinkedList<int>();

		#region Constructeurs

		/// <summary>
		/// Constructeur par défaut
		/// </summary>
		public ListOfReferences()
		{
			_referenced_list = null;
		}

		/// <summary>
		/// Constructeur associant immédiatement la liste de
		/// valeurs à laquelle est rattachée la liste de références.
		/// </summary>
		/// <param name="list_to_reference_to">Liste contenant les
		/// éléments qui seront classés de façon triée par la
		/// liste de références.</param>
		public ListOfReferences(ref IList<T> list_to_reference_to)
		{
			_referenced_list = list_to_reference_to;
		}

		/// <summary>
		/// Constructeur associant immédiatement la liste de
		/// valeurs à laquelle est rattachée la liste de références,
		/// et permettant de préciser si un compareur est obligatoire
		/// (oui par défaut).
		/// </summary>
		/// <param name="list_to_reference_to">Liste contenant les
		/// éléments qui seront classés de façon triée par la
		/// liste de références.</param>
		/// <param name="bComparerRequired">Indique si le compareur est
		/// obligatoire ou non.</param>
		public ListOfReferences(ref IList<T> list_to_reference_to, bool bComparerRequired)
		{
			_referenced_list = list_to_reference_to;
			_comparer_required = bComparerRequired;
		}

		#endregion Constructeurs

		/// <summary>
		/// Fonction à appeler pour remplir la liste de
		/// références après avoir défini la liste de
		/// valeurs et éventuellement le comparateur.
		/// </summary>
		/// <returns>
		/// true si la fonction a réussi.
		/// false si la fonction a échoué sans lever
		/// d'exception.
		/// </returns>
		/// <exception cref="System.NotImplementedException">
		/// Thrown when comparison functions are needed and not
		/// defined.</exception>
		public bool Initialize()
		{

			bool success = false;
			_liste_indexs.Clear();

			if(_referenced_list!=null && _referenced_list.Count>0) {

				int nIndex=0;

				_liste_indexs.AddFirst(0);
				nIndex++;

				if (_comparer == null)
				{

					#region if (_comparer == null)

					if (_comparer_required)
					{
						_liste_indexs.Clear();
						throw new NotImplementedException("A comparison function was not defined.");
					}
					else
					{
						if (!(_referenced_list[nIndex] is IComparable))
						{
							_liste_indexs.Clear();
							throw new NotImplementedException("A comparison function was not defined and items are not IComparable.");
						}
						else
						{
							while (nIndex < _referenced_list.Count)
							{

								LinkedListNode<int> current_node = _liste_indexs.First;

								while (current_node != null && (((IComparable)(_referenced_list[nIndex])).CompareTo(_referenced_list[current_node.Value]) >= 0))
									current_node = current_node.Next;

								if (current_node == null)
									_liste_indexs.AddLast(nIndex);
								else
									_liste_indexs.AddBefore(current_node, nIndex);

								nIndex++;

							}
							success = true;
						}
					}

					#endregion if (_comparer == null)

				}
				else
				{
					while (nIndex < _referenced_list.Count)
					{

						LinkedListNode<int> current_node = _liste_indexs.First;

						while (current_node != null && _comparer(_referenced_list[nIndex], _referenced_list[current_node.Value]) >= 0)
							current_node = current_node.Next;

						if (current_node == null)
							_liste_indexs.AddLast(nIndex);
						else
							_liste_indexs.AddBefore(current_node, nIndex);

						nIndex++;

					}

					success = true;

				}

			}

			return success;

		}

		/// <summary>
		/// Définit la fonction permettant de comparer 2 valeurs.
		/// </summary>
		/// <param name="compare_function">Fonction de comparaison.</param>
		/// <remarks>La fonction doit comparer 2 objets dy type contenu dans
		/// la liste de valeurs et retourner,un nombre négatif si la 1ère
		/// valeur doit être placée avant la 2ême,un nombre positif si la
		/// 1ère valeur doit être avant après la 2ême valeur, et 0 si le tri
		/// ne les différencie pas.</remarks>
		public void SetComparer(ItemCompare compare_function)
		{
			_comparer = compare_function;
		}

		/// <summary>
		/// Définit la liste de valeurs.
		/// Efface le contenu courant de la liste de références.
		/// </summary>
		/// <param name="referenced_list">Liste de valeurs.</param>
		/// <exception cref="System.NotImplementedException">
		/// Thrown when comparison functions are needed and not
		/// defined.</exception>
		public void SetReferencedList(IList<T> referenced_list)
		{
			_liste_indexs.Clear();
			_referenced_list = referenced_list;
		}

		#region Méthodes de liste

		/// <summary>
		/// Ajoute l'élément correspondant à l'index spécifié 
		/// dans la liste de valeurs.
		/// </summary>
		/// <param name="nItemOriginalIndex">Index de l'élément dans
		/// la liste de valeurs.</param>
		/// <exception cref="System.NotImplementedException">
		/// Thrown when comparison functions are needed and not
		/// defined.</exception>
		public void Add(int nItemOriginalIndex)
		{

			if (_referenced_list != null)
			{

				if (_comparer == null)
				{

					#region if (_comparer == null)

					if (_comparer_required)
					{
						throw new NotImplementedException("A comparison function was not defined.");
					}
					else
					{
						if (!(_referenced_list[0] is IComparable))
						{
							throw new NotImplementedException("A comparison function was not defined and items are not IComparable.");
						}
						else
						{

							LinkedListNode<int> current_node = _liste_indexs.First;

							while (current_node != null && (((IComparable)(_referenced_list[nItemOriginalIndex])).CompareTo(_referenced_list[current_node.Value]) >= 0))
							{

								//update index of nodes that were moved at insertion
								if (current_node.Value >= nItemOriginalIndex)
									current_node.Value = current_node.Value + 1;

								current_node = current_node.Next;

							}

							if (current_node == null)
								_liste_indexs.AddLast(nItemOriginalIndex);
							else
							{

								//update index of nodes that were moved at insertion
								if (current_node.Value >= nItemOriginalIndex)
									current_node.Value = current_node.Value + 1;

								_liste_indexs.AddBefore(current_node, nItemOriginalIndex);

								current_node = current_node.Next;

								while (current_node != null)
								{

									//update index of nodes that were moved at insertion
									if (current_node.Value >= nItemOriginalIndex)
										current_node.Value = current_node.Value + 1;

									current_node = current_node.Next;

								}

							}

						}
					}

					#endregion if (_comparer == null)

				}
				else
				{

					LinkedListNode<int> current_node = _liste_indexs.First;

					while (current_node != null && _comparer(_referenced_list[nItemOriginalIndex], _referenced_list[current_node.Value]) >= 0)
					{

						//update index of nodes that were moved at insertion
						if (current_node.Value >= nItemOriginalIndex)
							current_node.Value = current_node.Value + 1;

						current_node = current_node.Next;

					}

					if (current_node == null)
						_liste_indexs.AddLast(nItemOriginalIndex);
					else
					{

						//update index of nodes that were moved at insertion
						if (current_node.Value >= nItemOriginalIndex)
							current_node.Value = current_node.Value + 1;

						_liste_indexs.AddBefore(current_node, nItemOriginalIndex);

						current_node = current_node.Next;

						while (current_node != null)
						{

							//update index of nodes that were moved at insertion
							if (current_node.Value >= nItemOriginalIndex)
								current_node.Value = current_node.Value + 1;

							current_node = current_node.Next;

						}

					}

				}

			}

		}

		/// <summary>
		/// Retire l'élément de la liste référencée correspondant
		/// à l'index spécifié dans la liste de valeurs.
		/// </summary>
		/// <param name="nItemOriginalIndex">Index de l'élément dans
		/// la liste de valeurs.</param>
		public void Remove(int nItemOriginalIndex)
		{

			if (_referenced_list != null)
			{

				LinkedListNode<int> current_node;

				_liste_indexs.Remove(nItemOriginalIndex);

				current_node = _liste_indexs.First;

				//decrease index of items that were after the removed one
				while (current_node != null)
				{

					if (current_node.Value >= nItemOriginalIndex)
						current_node.Value = current_node.Value - 1;

					current_node = current_node.Next;

				}

			}

		}

		/// <summary>
		/// Vide la liste d'indexs.
		/// </summary>
		public void Clear()
		{
			_liste_indexs.Clear();
		}

		/// <summary>
		/// Vide la liste d'indexs et supprime
		/// les liens à la liste référencée et
		/// au compareur s'ils existent.
		/// </summary>
		public void ClearAll()
		{

			if (_referenced_list != null)
				_referenced_list = null;

			_comparer = null;
			_comparer_required = true;
			_liste_indexs.Clear();

		}

		/// <summary>
		/// Détermine si une valeur existe dans la liste de
		/// valeurs.
		/// </summary>
		/// <param name="item">Objet à chercher.</param>
		/// <returns>true si item existe dans la liste de valeurs ; sinon, false</returns>
		/// <remarks>La fonction appelle directement Contains de la liste de valeurs,et
		/// retourne false si celle-ci n'est pas définie.</remarks>
		public bool Contains(T item)
		{

			bool bResult = false;

			if (_referenced_list != null)
				bResult = _referenced_list.Contains(item);

			return bResult;

		}

		/// <summary>
		/// Copie les éléments référencés et triés dans Array, en commençant à un index particulier de Array.
		/// </summary>
		/// <param name="array">Array unidimensionnel qui constitue la destination des éléments copiés à partir de ICollection. 
		/// Array doit avoir une indexation de base zéro.</param>
		/// <param name="index">Index de base zéro dans array au niveau duquel commencer la copie.</param>
		/// <exception cref="ArgumentNullException">array est nullNothingnullptrréférence Null</exception>
		/// <exception cref="ArgumentOutOfRangeException">index est inférieur à la limite inférieure de array.</exception>
		/// <exception cref="ArgumentException">array est multidimensionnel.
		/// - ou - 
		/// index est supérieur ou égal à la longueur de array et le Array source a une longueur supérieure à 0.
		/// - ou - 
		/// Le nombre d'éléments dans la liste est supérieur à la quantité d'espace disponible entre index et la fin du array de destination. 
		/// </exception>
		/// <exception cref="ArrayTypeMismatchException">Le cast automatique du type du Array source en type du array de destination est impossible.</exception>
		/// <exception cref="InvalidCastException">Au moins un élément du Array source ne peut pas être casté en type de array de destination.</exception>
		public void CopyTo(Array array, int index)
		{
			ToArray().CopyTo(array, index);
		}

		#endregion Méthodes de liste

		/// <summary>
		/// Créé et retourne un tableau contenant les valeurs de la 
		/// liste de valeurs triées selon les critères de la liste
		/// de références.
		/// </summary>
		/// <returns>Tableau contenant les valeurs triées.</returns>
		public T[] ToArray()
		{

			T[] return_array;

			if (_referenced_list == null || _liste_indexs.Count == 0)
				return_array = new T[0];
			else
			{

				LinkedListNode<int> noeud = _liste_indexs.First;

				return_array = new T[_referenced_list.Count];

				for (int i = 0; i < _referenced_list.Count; i++)
				{
					return_array[i] = _referenced_list[noeud.Value];
					noeud = noeud.Next;
				}

			}

			return return_array;

		}

		/// <summary>
		/// Produit une chaine représentant la liste de valeurs
		/// triée selon les critères de la liste de références
		/// sous la forme: [ elem1, elem2, ..., elemFinal, ]
		/// </summary>
		/// <returns>chaine représentant la liste.</returns>
		/// <remarks>Une liste vide ou absence de listes produit
		/// la chaine [  ]</remarks>
		public override string ToString()
		{

			StringBuilder builder = new StringBuilder();
			builder.Append("[ ");

			if (_referenced_list != null && _liste_indexs.Count == _referenced_list.Count)
			{
				foreach (int valeur_noeud in _liste_indexs)
				{
					builder.Append(_referenced_list[valeur_noeud].ToString());
					builder.Append(", ");
				}
				if(builder.Length>2)
					builder.Length = builder.Length - 2;
			}

			builder.Append(" ]");

			return builder.ToString();

		}




	}
}
