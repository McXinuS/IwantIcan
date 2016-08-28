using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWantICan.Core.Helpers;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IWantICan.Core.ViewModels
{
	public abstract class BaseEditOfferViewModel : BaseViewModel
	{
		protected abstract ICanService CanService { get; set; }
		protected abstract IWantService WantService { get; set; }
		protected abstract IDialogService DialogService { get; set; }
		protected abstract ICategoryService CategoryService { get; set; }

		private OfferModel _offer;
		private int _category;
		private OfferType _type;
		private List<CategoryModel> _categories;

		private ObservableDictionary<string, string> _errors = new ObservableDictionary<string, string>();

		public OfferModel Offer
		{
			get { return _offer; }
			set { _offer = value; RaisePropertyChanged(() => Offer); }
		}

		public int Category
		{
			get { return _category; }
			set
			{
				_category = value;
				Offer.subCategoryModelId = _category;
				RaisePropertyChanged(() => Category);
			}
		}

		public OfferType Type
		{
			get { return _type; }
			set
			{
				_type = value;
				Offer.type = _type;
				RaisePropertyChanged(() => Type);
			}
		}

		public List<CategoryModel> Categories
		{
			get { return _categories; }
			set { _categories = value; RaisePropertyChanged(() => Categories); }
		}
		
		public ObservableDictionary<string, string> Errors
		{
			get { return _errors; }
			set { _errors = value; RaisePropertyChanged(() => Errors); }
		}

		public IMvxCommand SaveCommand
		{
			get { return new MvxCommand(Save); }
		}

		protected abstract void Save();
		
		protected bool Validate()
		{
			var toCheck = new List<Tuple<string, string, ValidationType>>
			{
				new Tuple<string, string, ValidationType> (Offer.name, "Name", ValidationType.Common),
				new Tuple<string, string, ValidationType> (Offer.description, "Description", ValidationType.Common)
			};
			return ValidatorHelper.Validate(toCheck, ref _errors);
		}

		public IMvxCommand ShowFilterCommand
		{
			get { return new MvxCommand(ShowFilter); }
		}

		protected void ShowFilter()
		{
			var categories = Categories.Select(c => c.name).ToArray();
			DialogService.Filter(
				categories,
				CategoryService.IndexOf(Category) + 1,
				s => Category = Categories[s - 1].id);
		}
	}
}
