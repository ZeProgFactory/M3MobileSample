using System.Collections.ObjectModel;
using static M3SDKMaui.M3Scanner;


namespace M3SDKMaui;
public partial class TestPage : ContentPage
{
   private ObservableCollection<string> _scanned { get; set; }

   public TestPage()
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

      MessagingCenter.Subscribe<App, M3Barcode>(this, "barcode", (sender, arg) =>
      {
         _scanned.Insert(0, "Data: " + arg.ToString());
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

   private void chkKeyDisable_CheckedChanged_KeyDisable(object sender, CheckedChangedEventArgs e)
   {
      if (scan == null)
      {
         scan = new M3Scanner();
      };

      scan.SetKeyDisable(chkKeyDisable.IsChecked);
   }

   private void chkVibrate_CheckedChanged_KeyDisable(object sender, CheckedChangedEventArgs e)
   {
      if (scan == null)
      {
         scan = new M3Scanner();
      };

      scan.VibrationEnable(chkVibrate.IsChecked);
   }

   private void chkBeep_CheckedChanged_KeyDisable(object sender, CheckedChangedEventArgs e)
   {
      if (scan == null)
      {
         scan = new M3Scanner();
      };

      scan.SetSound(chkBeep.IsChecked ? SoundMode.Beep : SoundMode.None);
   }
}
