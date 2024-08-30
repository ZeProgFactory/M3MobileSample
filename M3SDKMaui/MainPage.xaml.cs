using System.Collections.ObjectModel;


namespace M3SDKMaui;
public partial class MainPage : ContentPage
{
   private ObservableCollection<string> _scanned { get; set; }

   public MainPage()
   {
      InitializeComponent();

      _scanned = new ObservableCollection<string>();
      listView_scanned.ItemsSource = _scanned;
   }

   M3Scanner scan = null;

   protected override void OnAppearing()
   {
      base.OnAppearing();

      if (scan == null)
      {
         scan = new M3Scanner();
      };

      scan.RegisterReceiver();

      MessagingCenter.Subscribe<App, string>(this, "barcode", (sender, arg) =>
      {
         _scanned.Add("Data: " + arg);
      });

      MessagingCenter.Subscribe<App, int>(this, "value", (sender, arg) =>
      {
         edValue.Text = "" + arg;
      });
   }

   protected override void OnDisappearing()
   {
      if (scan == null)
      {
         scan = new M3Scanner();
      };

      MessagingCenter.Unsubscribe<App, string>(this, "barcode");
      scan.UnregisterReceiver();
      base.OnDisappearing();
   }

   private void Button_Clicked_Start(object sender, EventArgs e)
   {
      if (scan == null)
      {
         scan = new M3Scanner();
      };

      scan.DecodeStart();
   }

   private void Button_Clicked_Stop(object sender, EventArgs e)
   {
      if (scan == null)
      {
         scan = new M3Scanner();
      };

      scan.DecodeStop();
   }

   private void Button_Clicked_Enable(object sender, EventArgs e)
   {
      if (scan == null)
      {
         scan = new M3Scanner();
      };

      scan.SetEnable(true);
   }

   private void Button_Clicked_Disable(object sender, EventArgs e)
   {
      if (scan == null)
      {
         scan = new M3Scanner();
      };

      scan.SetEnable(false);
   }

   private void Button_Clicked_GetParam(object sender, EventArgs e)
   {
      int nParam = Int32.Parse(edParam.Text);

      if (scan == null)
      {
         scan = new M3Scanner();
      };

      scan.GetScanParam(nParam);
      // return by MessagingCenter
   }

   private void Button_Clicked_SetParam(object sender, EventArgs e)
   {
      int nParam = Int32.Parse(edParam.Text);
      int nValue = Int32.Parse(edValue.Text);

      if (scan == null)
      {
         scan = new M3Scanner();
      };

      scan.SetScanParam(nParam, nValue);
   }

   private void chkKeyDisable_CheckedChanged_KeyDisable(object sender, CheckedChangedEventArgs e)
   {
      if (scan == null)
      {
         scan = new M3Scanner();
      };

      scan.SetKeyDisable(chkKeyDisable.IsChecked);
   }
}
