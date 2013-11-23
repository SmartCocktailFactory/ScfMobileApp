using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common.Model {
  public class ScfDrinkService : IDrinkService {

    #region Members
    private RequestNS.RequestFactory _Factory = null;
		List<DTO.Drink> _Drinks = new List<DTO.Drink>();
    #endregion

    #region Properties
		public IList<DTO.Drink> Drinks {
      get {
        if (this._Drinks.Count == 0) {
          this._RequestDrinks();
        }
        return new List<DTO.Drink>(this._Drinks.Select(x => ((DTO.Drink)x.Clone())));
      }
    }
    #endregion

    #region Constructor
    public ScfDrinkService(RequestNS.RequestFactory requestFactory) {
      this._Factory = requestFactory;
    }
    #endregion

    #region Model.IDrinkService
    public event EventHandler<DrinkNamesChangedEventArgs> OnDrinkNamesChanged;
    public event EventHandler<DrinksChangedEventArgs> OnDrinksChanged;


    public IList<string> DrinkNames {
      get { return this.Drinks.Select(x => x.Name).ToList(); }
    }
    #endregion

    #region Public methods
    public void ResetService() {
			this._Drinks = new List<DTO.Drink>();
    }
    #endregion

    #region Private methods

    private void _RequestDrinks() {
      Task.Factory.StartNew(() => {
        RequestNS.ARequest request = this._Factory.CreateGetDrinkRequest();
        request.OnRequestCompleted += getDrinkRequest_OnRequestCompleted;
        request.Execute();
      });
    }


    private void _NotifyDrinkListChanged() {
      if (this.OnDrinkNamesChanged != null) {
        Task.Factory.StartNew(() => {
          this.OnDrinkNamesChanged(this, new DrinkNamesChangedEventArgs(this.DrinkNames));
        });
      }
    }


    #endregion

    #region Event handlers

    void getDrinkRequest_OnRequestCompleted(object sender, RequestNS.RequestCompletedEventArgs e) {
      if (e.Request.State != RequestNS.RequestStates.Successful) {
        this._RequestDrinks();
        return;
      }
        RequestNS.RequestDrinkList drinkList = e.Request as RequestNS.RequestDrinkList;

        this._Drinks = drinkList.GetDrinks();

        this._NotifyDrinkListChanged();
     }

    #endregion
  }
}
