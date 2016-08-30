using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWantICan.Core.Helpers;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using IWantICan.Core.Services.Messenger;
using MvvmCross.Core.ViewModels;

namespace IWantICan.Core.ViewModels
{
	public abstract class BaseEditOfferViewModel : BaseViewModel
	{
		protected readonly ICanService CanService;
		protected readonly IWantService WantService;
		protected readonly IDialogService DialogService;
		protected readonly ICategoryService CategoryService;
		protected readonly IMessengerService Messenger;

		private OfferModel _offer;
		private int _category;
		private OfferType _type;
		private List<CategoryModel> _categories;

		private ObservableDictionary<string, string> _errors = new ObservableDictionary<string, string>();

		protected BaseEditOfferViewModel(ICanService canService,
			IWantService wantService,
			ICategoryService categoryService,
			IDialogService dialogService,
			IMessengerService messenger)
		{
			CanService = canService;
			WantService = wantService;
			CategoryService = categoryService;
			DialogService = dialogService;
			Messenger = messenger;

			Categories = categoryService.GetCategoryList();
		}

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

		protected virtual void SendOfferActionMessage(MessengerOfferActionType type)
		{
			Messenger.SendOfferActionMessage(this, type);
		}

		public IMvxCommand SaveCommand
		{
			get { return new MvxCommand(Save); }
		}

		protected virtual async void Save()
		{
			if (!Validate())
				return;

			var ok = await OnSave(Offer);

			if (ok)
			{
				DialogService.Alert(
					Constants.DialogSaveSuccess,
					Constants.DialogTitleSuccess,
					"ОК",
					() => Close(this));
			}
			else
			{
				DialogService.Alert(
					Constants.DialogSaveFailed,
					Constants.DialogTitleError,
					"ОК");
			}
		}
		
		protected virtual Task<bool> OnSave(OfferModel offer)
		{
			return Task.FromResult(false);
		}

		public IMvxCommand DeleteCommand
		{
			get { return new MvxCommand(DoDeleteCommand); }
		}

		private void DoDeleteCommand()
		{
			DialogService.Alert(
				Constants.DialogDeleteConfirm,
				Constants.DialogTitleConfirm,
				Constants.DialogButtonOk,
				Constants.DialogButtonCancel,
				Delete);
		}

		private async void Delete()
		{
			var ok = await OnDelete(Offer);

			if (ok)
			{
				DialogService.Alert(Constants.DialogDeleteSuccess,
					Constants.DialogTitleSuccess,
					"ОК",
					() => Close(this));
			}
			else
			{
				DialogService.Alert(Constants.DialogDeleteFailed,
					Constants.DialogTitleError,
					"ОК");
			}
		}

		protected virtual Task<bool> OnDelete(OfferModel offer)
		{
			return Task.FromResult(false);
		}

		protected bool Validate()
		{
			var toCheck = new List<Tuple<string, string, ValidationType>>
			{
				new Tuple<string, string, ValidationType> (Offer.name, "Name", ValidationType.Common),
				new Tuple<string, string, ValidationType> (Offer.description, "Description", ValidationType.Common),
				new Tuple<string, string, ValidationType> (Offer.subCategoryModelId.ToString(), "Category", ValidationType.Category)
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
