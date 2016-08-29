using System.Threading.Tasks;
using IWantICan.Core.Helpers;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using IWantICan.Core.Services.Messenger;
using MvvmCross.Core.ViewModels;

namespace IWantICan.Core.ViewModels
{
    public class EditOfferViewModel : BaseEditOfferViewModel
	{
		public EditOfferViewModel(ICanService canService,
            IWantService wantService,
            ICategoryService categoryService,
            IDialogService dialogService,
			IMessengerService messenger) 
			: base(canService, wantService, categoryService, dialogService, messenger)
		{ }

	    public void Init(string offer)
        {
			Offer = offer.Deserialize<OfferModel>();
			Type = Offer.type;
		    Category = Offer.subCategoryModelId;
        }

	    protected override async Task<bool> OnSave(OfferModel offer)
	    {
		    bool ok;

		    if (Offer.type.Equals(OfferType.Can))
		    {
			    ok = await CanService.UpdateCan(Offer);
		    }
		    else
		    {
			    ok = await WantService.UpdateWant(Offer);
		    }

		    if (ok)
		    {
			    SendOfferActionMessage(MessengerOfferActionType.Update);
		    }

			return ok;
		}

	    protected override async Task<bool> OnDelete(OfferModel offer)
	    {
		    bool ok;

		    if (Offer.type == OfferType.Can)
		    {
			    ok = await CanService.DeleteCan(Offer.id);
		    }
		    else
		    {
			    ok = await WantService.DeleteWant(Offer.id);
			}

			if (ok)
			{
				SendOfferActionMessage(MessengerOfferActionType.Delete);
			}

			return ok;
	    }
	}
}
