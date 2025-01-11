using System.Collections.ObjectModel;


namespace M3SDKMaui;
public partial class M3SDKPage : ContentPage
{
   private ObservableCollection<string> _scanned { get; set; }
   public M3Scanner Scan { get => scan; set => scan = value; }

   public M3SDKPage()
   {
      InitializeComponent();

      _scanned = new ObservableCollection<string>();
      listView_scanned.ItemsSource = _scanned;
   }

   M3Scanner scan = null;

   protected override void OnAppearing()
   {
      base.OnAppearing();

      if (Scan == null)
      {
         Scan = new M3Scanner();
      };

      Scan.RegisterReceiver();

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
      if (Scan == null)
      {
         Scan = new M3Scanner();
      };

      MessagingCenter.Unsubscribe<App, string>(this, "barcode");
      Scan.UnregisterReceiver();
      base.OnDisappearing();
   }

   private void Button_Clicked_Start(object sender, EventArgs e)
   {
      if (Scan == null)
      {
         Scan = new M3Scanner();
      };

      Scan.DecodeStart();
   }

   private void Button_Clicked_Stop(object sender, EventArgs e)
   {
      if (Scan == null)
      {
         Scan = new M3Scanner();
      };

      Scan.DecodeStop();
   }

   private void Button_Clicked_Enable(object sender, EventArgs e)
   {
      if (Scan == null)
      {
         Scan = new M3Scanner();
      };

      Scan.SetEnable(true);
   }

   private void Button_Clicked_Disable(object sender, EventArgs e)
   {
      if (Scan == null)
      {
         Scan = new M3Scanner();
      };

      Scan.SetEnable(false);
   }

   private void Button_Clicked_GetParam(object sender, EventArgs e)
   {
      int nParam = Int32.Parse(edParam.Text);

      if (Scan == null)
      {
         Scan = new M3Scanner();
      };

      Scan.GetScanParam(nParam);
      // return by MessagingCenter
   }

   private void Button_Clicked_SetParam(object sender, EventArgs e)
   {
      int nParam = Int32.Parse(edParam.Text);
      int nValue = Int32.Parse(edValue.Text);

      if (Scan == null)
      {
         Scan = new M3Scanner();
      };

      Scan.SetScanParam(nParam, nValue);
   }

   private void chkKeyDisable_CheckedChanged_KeyDisable(object sender, CheckedChangedEventArgs e)
   {
      if (Scan == null)
      {
         Scan = new M3Scanner();
      };

      Scan.SetKeyDisable(chkKeyDisable.IsChecked);
   }
}
