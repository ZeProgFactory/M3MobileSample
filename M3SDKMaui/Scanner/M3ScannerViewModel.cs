using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ScannerM3;

public partial class M3ScannerViewModel : INotifyPropertyChanged, IM3Scanner
{
   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

   static M3ScannerViewModel _Current = null;

   public static M3ScannerViewModel Current
   {
      get
      {
         if (_Current == null)
         {
            _Current = new M3ScannerViewModel();
         };

         return _Current;
      }

      set
      {
         _Current = value;
      }
   }

   public bool IsScannerAvailable { get; internal set; }

   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

   #region INotifyPropertyChanged

   // - - - INotifyPropertyChanged Members - - - 

   public event PropertyChangedEventHandler PropertyChanged;

   public void ClearPropertyChanged()
   {
      foreach (Delegate d in PropertyChanged.GetInvocationList())
      {
         PropertyChanged -= (PropertyChangedEventHandler)d;
      }
   }

   protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
   {
      var handler = PropertyChanged;
      if (handler != null)
      {
         try
         {
            handler(this, new PropertyChangedEventArgs(propertyName));
         }
         catch (Exception ex)
         {
            Debug.WriteLine(string.Format("BaseViewModel.OnPropertyChanged({0})", propertyName) + Environment.NewLine + ex.Message);

            if (Debugger.IsAttached)
            {
               Debugger.Break();
            };
         };
      }
   }
   #endregion

   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

   #region - - - M3 - - - 

   /// <summary>
   /// Should be static for Droid ...
   /// </summary>
   static M3Scanner _M3Scanner = null;

   public void DecodeStart()
   {
      if (_M3Scanner != null)
      {
         _M3Scanner.DecodeStart();
      };
   }

   public void DecodeStop()
   {
      if (_M3Scanner != null)
      {
         try
         {
            _M3Scanner.DecodeStop();
         }
         catch
         {
            Debugger.Break();
         };
      };
   }

   public void RegisterReceiver()
   {
      if (_M3Scanner != null)
      {
         _M3Scanner.RegisterReceiver();
      };
   }

   public void UnregisterReceiver()
   {
      if (_M3Scanner != null)
      {
         _M3Scanner.UnregisterReceiver();
      };
   }

   /// <summary>
   /// Action Scanner Enable or Disable
   /// </summary>
   /// <param name="isEnabled"></param>
   public void SetEnable(bool isEnabled)
   {
      if (_M3Scanner != null)
      {
         _M3Scanner.SetEnable(isEnabled);
      };
   }

   public void GetScanParam(int param)
   {
      if (_M3Scanner != null)
      {
         _M3Scanner.GetScanParam(param);
      };
   }

   public void SetScanParam(int param, int value)
   {
      if (_M3Scanner != null)
      {
         _M3Scanner.SetScanParam(param, value);
      };
   }

   /// <summary>
   /// Trigger button disable
   /// </summary>
   /// <param name="isEnabled"></param>
   public void SetKeyDisable(bool isEnabled)
   {
      if (_M3Scanner != null)
      {
         _M3Scanner.SetKeyDisable(isEnabled);
      };
   }

   public void SetSound(SoundMode soundMode)
   {
      if (_M3Scanner != null)
      {
         _M3Scanner.SetSound(soundMode);
      };
   }

   public void VibrationEnable(bool vibration)
   {
      if (_M3Scanner != null)
      {
         _M3Scanner.VibrationEnable(vibration);
      };
   }

   public void SetReadMode(ReadMode readMode)
   {
      if (_M3Scanner != null)
      {
         _M3Scanner.SetReadMode(readMode);
      };
   }

   public void SetEndCharacter(EndCharacter endCharacter)
   {
      if (_M3Scanner != null)
      {
         _M3Scanner.SetEndCharacter(endCharacter);
      };
   }

   public void SetOutputMode(OutputMode outputMode)
   {
      if (_M3Scanner != null)
      {
         _M3Scanner.SetOutputMode(outputMode);
      };
   }

   #endregion

   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

   public OnBarcodeEventHandler OnBarcode { get; set; }

   private static readonly IEnumerable<string> Manufacturers = new[] { "emdoor", "M3Mobile", "M3", "Zebra Technologies" };

   public M3ScannerViewModel()
   {
      IsScannerAvailable = Manufacturers.Contains(DeviceInfo.Manufacturer);

      if (IsScannerAvailable)
      {
         _M3Scanner = new M3Scanner();

         _M3Scanner.OnBarcode += (sender, arg) =>
         {
            if (OnBarcode != null)
            {
               OnBarcode(this, arg );
            };
         };

         //ToDo: values 
         //MessagingCenter.Subscribe<App, int>(this, "value", (sender, arg) =>
         //{
         //   edValue.Text = "" + arg;
         //});
      };
   }

   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -
}
